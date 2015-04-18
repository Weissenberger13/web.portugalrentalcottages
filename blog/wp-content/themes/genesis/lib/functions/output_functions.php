<?php
/**
 * undocumented 
 *
 */

// removes WP Generator for security reasons
remove_action('wp_head', 'wp_generator');

/**
 * This function looks for a favicon. If it finds
 * one, it will output the proper code in the <head>
 *
 * @since 0.2.2
 */
add_action('genesis_meta', 'genesis_load_favicon');
function genesis_load_favicon() {
	if(file_exists(CHILD_DIR.'/images/favicon.ico'))
		$favicon = CHILD_URL.'/images/favicon.ico';
	elseif(file_exists(CHILD_DIR.'/images/favicon.gif'))
		$favicon = CHILD_URL.'/images/favicon.gif';
	elseif(file_exists(CHILD_DIR.'/images/favicon.png'))
		$favicon = CHILD_URL.'/images/favicon.png';
	elseif(file_exists(CHILD_DIR.'/images/favicon.jpg'))
		$favicon = CHILD_URL.'/images/favicon.jpg';
	else
		$favicon = PARENT_URL.'/images/favicon.ico';

	$favicon = apply_filters('genesis_favicon_url', $favicon);

	if($favicon) :
	echo "\n".'<!--The Favicon-->'."\n";
	echo '<link rel="Shortcut Icon" href="'. esc_url( $favicon ). '" type="image/x-icon" />'."\n";
	endif;
}

/**
 * Show Parent and Child information in the document head if specified by the user.
 * This can be helpful for diagnosing problems with the theme, because you can
 * easily determine if anything is out of date, needs to be updated.
 *
 * @since 1.0
 */
add_action('genesis_meta', 'genesis_show_theme_info_in_head');
function genesis_show_theme_info_in_head() {
	if( !genesis_get_option( 'show_info' ) ) return;
	
	// Show Parent Info
	echo "\n".'<!-- Theme Information -->'."\n";
	echo '<meta name="wp_theme" content="'. esc_attr( PARENT_THEME_NAME ) .' '. esc_attr( PARENT_THEME_VERSION ) .'" />'."\n";
	
	// If there is no child theme, don't continue
	if ( CHILD_DIR == PARENT_DIR ) return;
	
	// Show Child Info
	$child_info = get_theme_data(CHILD_DIR.'/style.css');
	echo '<meta name="wp_template" content="'. esc_attr( $child_info['Name'] ) .' '. esc_attr( $child_info['Version'] ) .'" />'."\n";
}

/**
 * This function outputs the default header, including the #header div, the inner .wrap,
 * the #title-area div along with #title and #description, as well as the .widget-area.
 *
 * @since 1.0.2
 */
add_action('genesis_header', 'genesis_do_header');
function genesis_do_header() { ?>
	<div id="header">
	<div class="wrap">

		<div id="title-area">
			<?php genesis_site_title(); ?>
			<?php genesis_site_description(); ?>
		</div><!-- end #title-area -->

		<?php if(!genesis_get_option('header_full')) : ?>
		<div class="widget-area">
			<?php dynamic_sidebar('Header Right'); ?>
		</div><!-- end .widget-area -->
		<?php endif; ?>

	</div><!-- end .wrap -->
	</div><!--end #header-->
<?php
}

/**
 * This function strips out tags and shortcodes,
 * limits the output to $max_char characters,
 * and appends an ellipses and more link to the end.
 *
 * @since 0.1
 **/
function get_the_content_limit($max_char, $more_link_text = '(more...)', $stripteaser = 0) {
    $content = get_the_content('', $stripteaser);

    $content = apply_filters('get_the_content_limit', $content);

    // Strip tags and shortcodes
    $content = strip_tags(strip_shortcodes($content), apply_filters('get_the_content_limit_allowedtags', '<script>,<style>'));

    // Inline styles/scripts
    $content = trim(preg_replace('#<(s(cript|tyle)).*?</\1>#si', '', $content));

    // Truncate $content to $max_char
    if((strlen($content) > $max_char) && ($espacio = strpos($content, ' ', $max_char ))) {
        $content = substr($content, 0, $espacio);
    }

    if(empty($more_link_text)) {
        $link = '';
    }
    else {
        $link = sprintf('%s <a href="%s" rel="nofollow">%s</a>', '&hellip;', get_permalink(), $more_link_text);
    }

    return sprintf('<p>%s %s</p>', $content, $link);
}
function the_content_limit($max_char, $more_link_text = '(more...)', $stripteaser = 0) {
	$content = get_the_content_limit($max_char, $more_link_text, $stripteaser);
	echo apply_filters('the_content_limit', $content);
}

/**
 * Using a filter, we're replacing the default search form
 * with one of our own, specific to Genesis.
 *
 * @since 0.2
 */
add_filter('get_search_form', 'genesis_search_form');
function genesis_search_form() {
	
	$value = (get_search_query()) ? esc_attr(apply_filters('the_search_query', get_search_query())) : apply_filters('genesis_search_text', esc_attr__('Search this website...', 'genesis'));
	
	$form = '
		<form method="get" class="searchform" action="' . get_option('home') . '/" >
			<input type="text" value="'.$value.'" name="s" class="s" />
			<input type="submit" class="searchsubmit" value="'. esc_attr__('Search', 'genesis') .'" />
		</form>
	';
	
	return apply_filters('genesis_search_form', $form, $value);
}

/**
 * Display Breadcrumbs above the Loop
 * Concedes priority to popular breadcrumb plugins
 *
 * @since 0.1.6
 */
add_action('genesis_before_loop', 'genesis_do_breadcrumbs');
function genesis_do_breadcrumbs() {
	
	// Conditional Checks
	if( is_front_page() && !genesis_get_option( 'breadcrumb_home' ) ) return;
	if( is_single() && !genesis_get_option( 'breadcrumb_single' ) ) return;
	if( is_page() && !genesis_get_option( 'breadcrumb_page' ) ) return;
	if( ( is_archive() || is_search() ) && !genesis_get_option('breadcrumb_archive')) return;

	if ( function_exists( 'bcn_display' ) ) {
		echo '<div class="breadcrumb">'; bcn_display(); echo '</div>';
	}
	elseif ( function_exists( 'yoast_breadcrumb' ) ) {
		yoast_breadcrumb('<div class="breadcrumbs">','</div>');
	}
	elseif ( function_exists( 'breadcrumbs' ) ) {
		breadcrumbs();
	}
	elseif ( function_exists( 'crumbs' ) ) {
		crumbs();
	}
	else {
		include_once( GENESIS_TOOLS_DIR.'/breadcrumb.php' );
		genesis_breadcrumb( __( 'Home', 'genesis' ), '/', '<div class="breadcrumb">', '</div>' );
	}
}

/**
 * Add the post info (byline) under the title
 *
 * @since 0.2.3
 */
add_action('genesis_before_post_content', 'genesis_post_info');
function genesis_post_info() {
	if(is_page()) return; // don't do post-info on pages
	
	echo '<div class="post-info">'."\n\t\t\t\t";
	
		genesis_post_date();
		genesis_post_author_posts_link(__('By', 'genesis'));
		genesis_post_comments_link(__('Leave a Comment', 'genesis'), __('1 Comment', 'genesis'), __('% Comments', 'genesis'));
		
		edit_post_link(__('(Edit)', 'genesis'), '', ''); // if logged in
		
	echo "\n\t\t\t\t".'</div>'."\n\n";
}

/**
 * Add the post meta after the post content 
 *
 * @since 0.2.3
 */
add_action('genesis_after_post_content', 'genesis_post_meta');
function genesis_post_meta() {
	if(is_page()) return; // don't do post-meta on pages
	
	echo '<div class="post-meta">'."\n\t\t\t\t";
	
		genesis_post_categories_link(', ', __('Filed Under', 'genesis'));
		genesis_post_tags_link(', ', __('Tagged With', 'genesis'));
	
	echo "\n\t\t\t\t".'</div>'."\n\n";
}

/**
 * Add the author info box after a single post
 * 
 * @since 1.0
 */
add_action('genesis_after_post', 'genesis_do_author_box');
function genesis_do_author_box() { 
	if( is_single() && genesis_get_option( 'author_box' ) ) :
?>
	<div class="author-box">
		<p><?php echo get_avatar( get_the_author_meta('email'), '70' ); ?><b><?php _e('About', 'genesis'); ?> <?php the_author(); ?></b><br /><?php the_author_meta( 'description' ); ?></p>
	</div><!-- end .authorbox -->
<?php
	endif;
}

/**
 * The Genesis-specific post date
 *
 * @since 0.2.3
 */
function genesis_post_date($format = '', $label = '') {
	// pull the $format, or use the default
	$format = (!empty($format)) ? $format : get_option('date_format');
	
	$label = (!empty($label)) ? trim($label).' ' : '';

	echo sprintf('<span class="time published" title="%3$s">%1$s%2$s</span> ', $label, get_the_time($format), get_the_time('Y-m-d\TH:i:sO'));
}

/**
 * The Genesis-specific post author link
 *
 * @since 0.2.3
 */
function genesis_post_author_posts_link($label = '') {
	global $authordata;
	$id = $authordata->ID;
	$nicename = $authordata->user_nicename;
	$display_name = get_the_author();
	
	$url = get_author_posts_url($id, $nicename);
	$title = sprintf(__('Posts by %s', 'genesis'), $display_name);
	
	$link = sprintf( '<a href="%1$s" title="%2$s" class="url fn">%3$s</a>', esc_url( $url ), esc_attr( $title ), esc_html( $display_name ) );
	
	// nofollow the link, if necessary
	$link = (genesis_get_seo_option('nofollow_author_link')) ? genesis_rel_nofollow($link) : $link;
	
	$label = (!empty($label)) ? trim($label).' ' : '';
	
	echo sprintf('<span class="author vcard">%1$s%2$s</span> ', esc_html( $label ), $link);
}

/**
 * The Genesis-specific post comments link
 *
 * @since 0.2.3 
 */
function genesis_post_comments_link($zero = false, $one = false, $more = false) {
	global $id;

	$number = get_comments_number($id);

	if ( $number > 1 )
		$output = str_replace('%', number_format_i18n($number), ( false === $more ) ? __('% Comments', 'genesis') : $more);
	elseif ( $number == 0 )
		$output = ( false === $zero ) ? __('No Comments', 'genesis') : $zero;
	else // must be one
		$output = ( false === $one ) ? __('1 Comment', 'genesis') : $one;
	
	$link = sprintf('<a href="%s">%s</a>', get_permalink().'#respond', $output);
	
	// nofollow the link, if necessary
	$link = (genesis_get_seo_option('nofollow_comments_link')) ? genesis_rel_nofollow($link) : $link;
	
	echo sprintf('<span class="post-comments">%s</span> ', $link);
}

/**
 * The Genesis-specific post categories link
 *
 * @since 0.2.3
 */
function genesis_post_categories_link($sep = ', ', $label = '') {
	$links = get_the_category_list($sep);
	$links = (genesis_get_seo_option('nofollow_cat_link')) ? genesis_rel_nofollow($links) : $links;
	
	$label = (!empty($label)) ? trim($label).' ' : '';
	
	echo sprintf('<span class="categories">%1$s%2$s</span> ', $label, $links);
}

/**
 * The Genesis-specific post tags link
 *
 * @since 0.2.3
 */
function genesis_post_tags_link($sep = ', ', $label = '') {
	$label = (!empty($label)) ? trim($label).' ' : '';
	
	$links = get_the_tag_list($label, $sep);
	$links = (genesis_get_seo_option('nofollow_tag_link')) ? genesis_rel_nofollow($links) : $links;
	
	echo sprintf('<span class="tags">%s</span> ', $links);
}

/**
 * The default post navigation, hooked to genesis_after_endwhile
 *
 * @since 0.2.3
 */
add_action('genesis_after_endwhile', 'genesis_posts_nav');
function genesis_posts_nav() {
	$nav = genesis_get_option('posts_nav');
	
	if($nav == 'prev-next')
		genesis_prev_next_posts_nav();
	elseif($nav == 'numeric')
		genesis_numeric_posts_nav();
	else
		genesis_older_newer_posts_nav();
}

/**
 * Display older/newer posts navigation
 * 
 * @since 0.2.2
 */
function genesis_older_newer_posts_nav() {
	
	$older = get_next_posts_link('&laquo; ' . __('Older Posts', 'genesis'));
	$newer = get_previous_posts_link(__('Newer Posts', 'genesis') . ' &raquo;');
	
	$nav = '
	<div class="navigation">
		<div class="alignleft">'.$older.'</div>
		<div class="alignright">'.$newer.'</div>
	</div><!-- end .navigation -->
	';
	
	if(!empty($older) || !empty($newer))
		echo $nav;
}

/**
 * Display prev/next posts navigation
 * 
 * @since 0.2.2
 */
function genesis_prev_next_posts_nav() {
	
	$prev = get_previous_posts_link();
	$next = get_next_posts_link();
	
	$nav = '
	<div class="navigation">
		<div class="alignleft">'.$prev.'</div>
		<div class="alignright">'.$next.'</div>
	</div><!-- end .navigation -->
	';
	
	if(!empty($prev) || !empty($next))
		echo $nav;
}

/**
 * Display numeric posts navigation (similar to WP-PageNavi)
 *
 * @since 0.2.3
 */
function genesis_numeric_posts_nav() {
	if(is_singular()) return; // do nothing
	
	global $wp_query, $paged;
	
	$paged = intval($paged); if($paged == 0) $paged = 1;
	$max = intval($wp_query->max_num_pages);
	$newline = "\n";
	
	echo '<div class="navigation">'.$newline;
	echo '<ul>'.$newline;
		
	//	add current page to the array
	if($paged >= 1)
		$links[] = $paged;
	
	//	add the pages around the current page to the array
	if($paged >= 3) {
		$links[] = $paged - 1; $links[] = $paged - 2;
	}
	if(($paged + 2) <= $max) { 
		$links[] = $paged + 2; $links[] = $paged + 1;
	}
	
	//	Previous Post Link
	if(get_previous_posts_link())
		echo '<li>'.get_previous_posts_link(__('&laquo; Previous', 'genesis')).'</li>'.$newline;
	
	//	Link to first Page, plus ellipeses, if necessary
	if(!in_array(1, $links)) {
		if($paged == 1) $current = ' class="active"'; else $current = null;
		echo '<li'.$current.'><a href="'.get_pagenum_link(1).'">1</a></li>'.$newline;
		
		if(!in_array(2, $links))
		echo '<li>&hellip;</li>'.$newline;
	}
	
	//	Link to Current page, plus 2 pages in either direction (if necessary).
	sort($links);
	foreach($links as $link) {
		if($paged == $link) $current = ' class="active"'; else $current = null;
		echo '<li'.$current.'><a href="'.get_pagenum_link($link).'">'.$link.'</a></li>'.$newline;
	}
	
	//	Link to last Page, plus ellipses, if necessary
	if(!in_array($max, $links)) {
		if(!in_array($max - 1, $links))
		echo '<li>&hellip;</li>'.$newline;
		
		if($paged == $max) $current = ' class="active"'; else $current = null;
		echo '<li'.$current.'><a href="'.get_pagenum_link($max).'">'.$max.'</a></li>'.$newline;
	}
	
	//	Next Post Link
	if(get_next_posts_link())
		echo '<li>'.get_next_posts_link(__('Next &raquo;', 'genesis')).'</li>'.$newline;
	
	echo '</ul>'.$newline;
	echo '</div>'.$newline;
}

/**
 * Output the contents of the footer
 *
 * @since 1.0.1
 */
add_action('genesis_footer', 'genesis_do_footer');
function genesis_do_footer() { ?>
	<div class="gototop">
		<p><a rel="nofollow" href="#wrap" ><?php _e("Return to top of page", 'genesis'); ?></a></p>
	</div><!--end .gototop-->
	<div class="creds">
		<p>
		<?php 
			_e("Copyright", 'genesis'); ?> &copy; <?php echo date('Y');
			echo apply_filters('genesis_footer_credits', ' &middot; <a title="'.PARENT_THEME_NAME.' Theme Framework" href="http://www.studiopress.com/themes/genesis">'.PARENT_THEME_NAME.' Theme Framework</a> '.__("by", 'genesis').' <a title="StudioPress Premium WordPress Themes" href="http://www.studiopress.com">StudioPress</a> &middot; <a href="http://wordpress.org">WordPress</a>'); 
		?>
		&middot; <?php wp_loginout(); ?>
		</p>
	</div><!--end .creds-->
<?php
}

/**
 * Output header scripts in to <code>wp_head()</code>
 * Allow shortcodes
 *
 * @since 0.2.3
 */
add_filter('genesis_header_scripts', 'do_shortcode');
add_action('wp_head', 'genesis_header_scripts');
function genesis_header_scripts() {
	$text = apply_filters('genesis_header_scripts', genesis_get_option('header_scripts'));
	
	echo $text;
}
add_filter('genesis_footer_scripts', 'do_shortcode');
add_action('wp_footer', 'genesis_footer_scripts');
function genesis_footer_scripts() {
	$text = apply_filters('genesis_footer_scripts', genesis_option('footer_scripts'));
	
	echo $text;
}