<?php
/**
 * This file handles the granular control of SEO for
 * the framework.
 *
 * It handles <title> tag generation, meta keywords/descriptions,
 * and the <link rel="canonical"> tag.
 *
 * If either HeadSpace2, All-in-One SEO Pack, or SEO Title Tag
 * are active, they will be given priority over the framework SEO.
 *
 * @package Genesis
 * @author Nathan Rice
 **/

/**
 * 
 */
add_action('genesis_site_title', 'genesis_seo_site_title');
function genesis_seo_site_title() {
	// Set what goes inside the wrapping tags
	$inside = '<a href="'.get_bloginfo('siteurl').'">'.get_bloginfo('name').'</a>';
	
	// Determine which wrapping tags to use
	$wrap = ((genesis_get_seo_option('home_h1_on') == 'title') && (is_home())) ? 'h1' : 'p';

	// Build the Title
	$title = sprintf('<%s id="title">%s</%s>', $wrap, $inside, $wrap);
	
	// Return (filtered)
	echo apply_filters('genesis_seo_title', $title, $inside, $wrap);
	echo "\n";
}

/**
 * 
 */
add_action('genesis_site_description', 'genesis_seo_site_description');
function genesis_seo_site_description() {
	// Set what goes inside the wrapping tags
	$inside = esc_html ( get_bloginfo( 'description' ) );
	
	// Determine which wrapping tags to use
	$wrap = ((genesis_get_seo_option('home_h1_on') == 'description') && (is_home())) ? 'h1' : 'p';

	// Build the Description
	$description = sprintf('<%s id="description">%s</%s>', $wrap, $inside, $wrap);
	
	// Return (filtered)
	echo apply_filters('genesis_seo_description', $description, $inside, $wrap);
	echo "\n";
}


/**
 * This function does 3 things:
 * 1. Pulls the values for $sep and $seplocation, uses defaults if necessary
 * 2. Determines if the site title should be appended
 * 3. Allows the user to set a custom title on a per-page/post basis
 *
 * @since 0.1.3
 */
add_action('genesis_title', 'wp_title');
add_filter('wp_title', 'genesis_default_title', 10, 3);
function genesis_default_title($title, $sep, $seplocation) {
	$sep = (genesis_get_seo_option('doctitle_sep')) ? genesis_get_seo_option('doctitle_sep') : '&mdash;';
	$seplocation = (genesis_get_seo_option('doctitle_seplocation')) ? genesis_get_seo_option('doctitle_seplocation') : 'right';
	
	//	if viewing the homepage
	if(is_front_page()) {
		// make $title = site name
		$title = get_bloginfo('name');
		
		// append site description, if necessary
		$title = (genesis_get_seo_option('append_description_home')) ? $title." $sep ".get_bloginfo('description') : $title;
	}
	
	//	if viewing a post/page/attachment
	if(is_singular()) {
		//	The User Defined Title (Genesis)
		if(genesis_get_custom_field('_genesis_title')) {
			$title = genesis_get_custom_field('_genesis_title');
		}
		//	All-in-One SEO Pack Title (latest, vestigial)
		elseif(genesis_get_custom_field('_aioseop_title')) {
			$title = genesis_get_custom_field('_aioseop_title');
		}
		//	Headspace Title (vestigial)	
		elseif(genesis_get_custom_field('_headspace_page_title')) {
			$title = genesis_get_custom_field('_headspace_page_title');
		}
		//	SEO Title Tag (vestigial)
		elseif(genesis_get_custom_field('title_tag')) {
			$title = genesis_get_custom_field('title_tag');
		}
		//	All-in-One SEO Pack Title (old, vestigial)
		elseif(genesis_get_custom_field('title')) {
			$title = genesis_get_custom_field('title');
		}
	}
	
	//	if we don't want site name appended, or if we're on the homepage
	if(genesis_get_seo_option('append_site_title') == FALSE || is_home()) 
		return esc_html ( $title );
	
	// else
	$title = ($seplocation == 'right') ? $title." $sep ".get_bloginfo('name') : get_bloginfo('name')." $sep ".$title;
		return esc_html( $title );
}

/**
 * This function generates <code>meta</code> code for SEO.
 * It generates a description and keywords.
 *
 * @since 0.1.3
 */
add_action('genesis_meta','genesis_seo_meta');
function genesis_seo_meta() {
	global $post;
	
/*
	the description will be made from either (in order of priority)
	1. site description (tagline) if on home page
	2. All-in-One SEO or Headspace2 Description (vestigial)
	3. '_genesis_description' hidden custom field
	4. the manual post excerpt
	5. output nothing (no auto-generated description)
*/
	
	// if we're on the homepage
	if(is_home()) {
		$description = (genesis_get_seo_option('home_description')) ? genesis_get_seo_option('home_description') : get_bloginfo('description');
	}
	// else if description is set via custom field
	elseif(genesis_get_custom_field('_genesis_description')) {
		$description = genesis_get_custom_field('_genesis_description');
	}
	// else if the user used All-in-One SEO Pack (latest, vestigial)
	elseif(genesis_get_custom_field('_aioseop_description')) {
		$description = genesis_get_custom_field('_aioseop_description');
	}
	// else if the user used Headspace2 (vestigial)
	elseif(genesis_get_custom_field('_headspace_description')) {
		$description = genesis_get_custom_field('_headspace_description');
	}
	// else if the user used All-in-One SEO Pack (old, vestigial)
	elseif(genesis_get_custom_field('description')) {
		$description = genesis_get_custom_field('description');
	}
	// else if there is a manual post excerpt
	elseif($post->post_excerpt) {
		$description = $post->post_excerpt;
	}
	// if all else fails, leave description blank
	else {
		$description = null;
	}
	
	// Add the description, but only if one exists, and where applicable
	if(is_home() || is_single() || is_page()) {
		if(!empty($description))
		echo '<meta name="description" content="'.esc_attr( $description ).'" />'."\n";
	}
	
/*
	the keyword list will be made from either (in order of priority)
	1. Site keywords, set by user in theme options
	2. All-in-One SEO Keywords (vestigial)
	3. '_prodigy_keywords' hidden custom field
	4. the post tags, set by the user
*/
	
	// if we're on the homepage
	if(is_home()) {
		$keywords = genesis_get_seo_option('home_keywords');
	}
	// else if keywords are set via custom field
	elseif(genesis_get_custom_field('_genesis_keywords')) {
		$keywords = genesis_get_custom_field('_genesis_keywords');
	}
	// else if keywords are set via All-in-One SEO Pack (latest, vestigial)
	elseif(genesis_get_custom_field('_aioseop_keywords')) {
		$keywords = genesis_get_custom_field('_aioseop_keywords');
	}
	// else if keywords are set via All-in-One SEO Pack (old, vestigial)
	elseif(genesis_get_custom_field('keywords')) {
		$keywords = genesis_get_custom_field('keywords');
	}
	// if all else fails, use the post tags
	else {
		$post_tags = '';
		foreach((array)get_the_tags($post->ID) as $tag) { 
			if($tag) $post_tags .= ','. $tag->name;
		}
		$keywords = substr($post_tags,1); //removing the first "," from the list
	}
	
	// Add the keywords, but only where applicable
	if(is_home() || is_singular()) {
		if(!empty($keywords))
		echo '<meta name="keywords" content="'.esc_attr( $keywords ).'" />'."\n";
	}
}

/**
 * This function generates the index/follow code in the document <head>
 *
 * @uses genesis_get_seo_option, genesis_get_custom_field
 *
 * @since 0.1.3
 */
add_action('genesis_meta','genesis_index_follow_logic');
function genesis_index_follow_logic() {
	global $post;
	
	// if the user wants the blog private, then follow logic
	// is unnecessary. WP will insert noindex and nofollow
	if(get_option('blog_public') == 0) return;
	
	// defaults
	$index = 'index'; $follow = 'follow';
	
    // noindex all archives
	if(is_archive() || is_search()) :
		$index = 'noindex';
		// ... unless ...
		if(
			(is_category() && genesis_get_seo_option('index_cat_archive')) ||
			(is_tag() && genesis_get_seo_option('index_tag_archive')) ||
			(is_author() && genesis_get_seo_option('index_author_archive')) ||
			(is_date() && genesis_get_seo_option('index_date_archive')) ||
			(is_search() && genesis_get_seo_option('index_search_archive'))
		)
		{ $index = 'index'; }
    endif;
	
	// noindex pages/posts that are specified to noindex
	if(is_singular() && genesis_get_custom_field('_genesis_noindex'))
		$index = 'noindex';
	
	// nofollow posts/pages that are specified to nofollow
	if(is_singular() && genesis_get_custom_field('_genesis_nofollow'))
		$follow = 'nofollow';
	
	echo '<meta name="robots" content="'.esc_attr( $index ).','.esc_attr( $follow ).'" />'."\n";
}

/**
 * This function removes the canonical tag if disabled
 * by the user in the theme settings, or in the in-post SEO settings.
 *
 * @uses genesis_get_seo_option, genesis_get_custom_field
 *
 * @since 0.1.3
 */
add_action('get_header','genesis_canonical');
function genesis_canonical() {
	if(!genesis_get_seo_option('enable_canonical') || genesis_get_custom_field('_genesis_disable_canonical'))
		remove_action('wp_head', 'rel_canonical');
}

/**
 * This next section adds a new box to the post/page edit screen,
 * so that the user can set SEO options on a per-post/page basis.
 *
 * genesis_add_inpost_seo_box() is used to register the boxes.
 * @uses add_meta_box
 * genesis_inpost_seo_box() generates the content in the boxes.
 * @uses wp_create_nonce, checked, genesis_get_custom_field
 *
 * @since 0.1.3
 */
add_action('admin_menu', 'genesis_add_inpost_seo_box');
function genesis_add_inpost_seo_box() {
	add_meta_box('genesis_inpost_seo_box', __('SEO Options and Settings', 'genesis'), 'genesis_inpost_seo_box', 'post', 'normal', 'high');
	add_meta_box('genesis_inpost_seo_box', __('SEO Options and Settings', 'genesis'), 'genesis_inpost_seo_box', 'page', 'normal', 'high');
}
function genesis_inpost_seo_box() { ?>
	
	<input type="hidden" name="genesis_inpost_seo_nonce" value="<?php echo wp_create_nonce(plugin_basename(__FILE__)); ?>" />
	
	<p><label for="genesis_title"><b>Custom Document Title</b> (<code>&lt;title&gt;</code> Tag):</label></p>
	<p><input style="width: 99%;" type="text" name="genesis_seo[_genesis_title]" id="genesis_title" value="<?php echo esc_attr( genesis_get_custom_field('_genesis_title')); ?>" /></p>
	
	<p><label for="genesis_description"><b>Custom Post/Page Meta Description</b> (<code>&lt;meta name="description"&gt;</code>):</label></p>
	<p><textarea style="width: 99%;" name="genesis_seo[_genesis_description]" id="genesis_description"><?php echo htmlspecialchars(genesis_get_custom_field('_genesis_description')); ?></textarea></p>
	
	<p><label for="genesis_keywords"><b>Custom Post/Page Meta Keywords</b> (<code>&lt;meta name="keywords"&gt;</code>, comma separated):</label></p>
	<p><input style="width: 99%;" type="text" name="genesis_seo[_genesis_keywords]" id="genesis_keywords" value="<?php echo esc_attr(genesis_get_custom_field('_genesis_keywords')); ?>" /></p>
	
	<br />
	
	<p><b>Noindex this Post/Page</b> ( <a href="http://www.robotstxt.org/meta.html" target="_blank">more info</a> )</p>
	<p><input type="checkbox" name="genesis_seo[_genesis_noindex]" id="genesis_noindex" value="1" <?php checked(1, genesis_get_custom_field('_genesis_noindex')); ?> /> 
	<label for="genesis_noindex">Apply <code>noindex</code> to this post/page?</label></p>
	
	<br />
	
	<p><b>Nofollow this Post/Page</b> ( <a href="http://www.robotstxt.org/meta.html" target="_blank">more info</a> )</p>
	<p><input type="checkbox" name="genesis_seo[_genesis_nofollow]" id="genesis_nofollow" value="1" <?php checked(1, genesis_get_custom_field('_genesis_nofollow')); ?> /> 
	<label for="genesis_nofollow">Apply <code>nofollow</code> to this post/page?</label></p>
	
<?php if(!genesis_get_seo_option('disable_canonical')) : ?>
	<br />
	<p><b>Remove canonical tag</b> ( <a href="http://www.mattcutts.com/blog/canonical-link-tag/" target="_blank">more info</a> )</p>
	<p><input type="checkbox" name="genesis_seo[_genesis_disable_canonical]" id="genesis_disable_canonical" value="1" <?php checked(1, genesis_get_custom_field('_genesis_disable_canonical')); ?> /> 
	<label for="genesis_disable_canonical">Disable the <code>&lt;link rel="canonical" /&gt;</code> tag for this post/page?</label>
<?php endif;
}

/**
 * This function saves the SEO settings when we save a post/page.
 * It does so by grabbing the array passed in $_POST, looping through
 * it, and saving each key/value pair as a custom field.
 *
 * @uses wp_verify_nonce, plugin_basename, current_user_can
 * @uses add_post_meta, update_post_meta, delete_post_meta, get_custom_field
 *
 * @since 0.1.3
 */
add_action('save_post', 'genesis_inpost_seo_save', 1, 2);
function genesis_inpost_seo_save($post_id, $post) {
	
	//	verify the nonce
	if (!wp_verify_nonce($_POST['genesis_inpost_seo_nonce'], plugin_basename(__FILE__)))
		return $post->ID;
		
	//	don't try to save the data under autosave, ajax, or future post.
	if(defined(DOING_AUTOSAVE) && DOING_AUTOSAVE) return;
	if(defined(DOING_AJAX) && DOING_AJAX) return;
	if(defined(DOING_CRON) && DOING_CRON) return;

	//	is the user allowed to edit the post or page?
	if(('page' == $_POST['post_type'] && !current_user_can('edit_page', $post->ID)) || !current_user_can('edit_post', $post->ID ))
		return $post->ID;
		
	$genesis_seo = $_POST['genesis_seo'];
		
	//	gotta set these manually, unfortunately, since they're checkboxes
	$genesis_seo['_genesis_noindex'] = $_POST['genesis_seo']['_genesis_noindex'];
	$genesis_seo['_genesis_nofollow'] = $_POST['genesis_seo']['_genesis_nofollow'];
	$genesis_seo['_genesis_disable_canonical'] = $_POST['genesis_seo']['_genesis_disable_canonical'];
	
	//	store the custom fields
	foreach ($genesis_seo as $key => $value) {
		if($post->post_type == 'revision') return; // don't try to store data during revision save
		
		//	sanitize the title, description, and tags before storage
		if($key == '_genesis_title' || $key == '_genesis_description' || $key == '_genesis_keywords')
			$value = esc_html(strip_tags($value));
		
		//	save/update
		if(!genesis_get_custom_field($key))
			update_post_meta($post->ID, $key, $value);
		else
			add_post_meta($post->ID, $key, $value, TRUE);
		
		//delete if blank
		if(!$value) delete_post_meta($post->ID, $key);
	}
}

/**
 * Disable all SEO functions if the All-in-One SEO Pack or Headspace is active
 *
 * @since 0.1.3
 */
if (class_exists('All_in_One_SEO_Pack') || class_exists('HeadSpace_Plugin')) {
	remove_filter('wp_title', 'genesis_default_title', 10, 3);
	remove_action('genesis_meta','genesis_seo_meta');
	remove_action('genesis_meta','genesis_index_follow_logic');
	remove_action('genesis_meta','genesis_canonical');
	remove_action('admin_menu', 'genesis_add_inpost_seo_box');
	remove_action('save_post', 'genesis_inpost_seo_save', 1, 2);
}

/**
 * disable Genesis <title> generation if SEO Title Tag is active
 *
 * @since 0.1.3
 */
if (function_exists('seo_title_tag')) {
	remove_filter('wp_title', 'genesis_default_title', 10, 3);
	remove_action('genesis_title', 'wp_title');
	add_action('genesis_title', 'seo_title_tag');
}
?>