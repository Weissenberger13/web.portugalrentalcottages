<?php
/**
 * 
 */

add_action('genesis_after_header', 'genesis_do_nav');
function genesis_do_nav() {
	if( genesis_get_option('nav') ) {
		$args = array(
			'context' => 'nav',
			'show_home' => genesis_get_option('nav_home'),
			'type' => genesis_get_option('nav_type'),
			'sort_column' => genesis_get_option('nav_pages_sort'),
			'orderby' => genesis_get_option('nav_categories_sort'),
			'depth' => genesis_get_option('nav_depth'),
			'exclude' => genesis_get_option('nav_exclude'),
			'include' => genesis_get_option('nav_include'),
			'echo' => false
		);
		
		echo '<div id="nav"><div class="wrap">'.genesis_nav($args).'</div></div>';
	}
}
add_action('genesis_after_header', 'genesis_do_subnav');
function genesis_do_subnav() {
	if( genesis_get_option('subnav') ) {
		$args = array(
			'context' => 'subnav',
			'show_home' => genesis_get_option('subnav_home'),
			'type' => genesis_get_option('subnav_type'),
			'sort_column' => genesis_get_option('subnav_pages_sort'),
			'orderby' => genesis_get_option('subnav_categories_sort'),
			'depth' => genesis_get_option('subnav_depth'),
			'exclude' => genesis_get_option('subnav_exclude'),
			'include' => genesis_get_option('subnav_include'),
			'echo' => false
		);
		
		echo '<div id="subnav"><div class="wrap">'.genesis_nav($args).'</div></div>';
	}
}

add_filter('genesis_nav_items', 'genesis_nav_right', 10, 2);
function genesis_nav_right($menu, $args) {
	if( ($args['context'] != 'nav') )
		return $menu;
	
	if( genesis_get_option('nav_right') == 'rss' ) {
		$rss .= '<a rel="nofollow" href="'.get_bloginfo('rss_url').'">'.__('Posts', 'genesis').'</a>';
		$rss .= '<a rel="nofollow" href="'.get_bloginfo('comments_rss2_url').'">'.__('Comments', 'genesis').'</a>';
		
		$menu .= '<li class="right rss">'.$rss.'</li>';
	}
	elseif( genesis_get_option('nav_right') == 'search' ) {
		// I hate output buffering, but I have no choice
		ob_start();
		get_search_form();
		$search = ob_get_clean();
		
		$menu .= '<li class="right search">'.$search.'</li>';
	}
	elseif( genesis_get_option('nav_right') == 'twitter' ) {
		$twitter = '<a href="' . esc_url( 'http://twitter.com/'.genesis_get_option('twitter_id') ) .'">'.genesis_get_option('nav_twitter_text').'</a>';
		
		$menu .= '<li class="right twitter">'.$twitter.'</li>';
	}
	elseif( genesis_get_option('nav_right') == 'date' ) {
		$menu .= '<li class="right date">'.date(get_option('date_format')).'</li>';
	}
	
	return $menu;	
}

function genesis_nav($args = array()) {
	$defaults = array(
		'context' => '',
		'type' => 'pages',
		'sort_column' => 'menu_order, post_title',
		'menu_id' => false,
		'menu_class' => 'nav',
		'echo' => true,
		'link_before' => '',
		'link_after' => ''
	);
	
	$defaults = apply_filters('genesis_nav_default_args', $defaults);
	$args = wp_parse_args($args, $defaults);
	
	$menu = '';
	
	$list_args = $args;
	
	// Show Home in the menu (mostly copied from WP source)
	if ( isset($args['show_home']) && ! empty($args['show_home']) ) {
		if ( true === $args['show_home'] || '1' === $args['show_home'] || 1 === $args['show_home'] )
			$text = apply_filters('genesis_nav_home_text', __('Home', 'genesis'), $args);
		else
			$text = $args['show_home'];
		
		$class = '';
		
		if ( is_front_page() && !is_paged() )
			$class = 'class="home current_page_item"';
		else 
			$class = 'class="home"';
			
		$home = '<li ' . $class . '><a href="' . get_bloginfo('siteurl') . '" title="' . esc_attr($text) . '">' . $args['link_before'] . $text . $args['link_after'] . '</a></li>';
		
		$menu .= (genesis_get_seo_option('nofollow_home_link')) ? genesis_rel_nofollow($home) : $home;
		
		// If the front page is a page, add it to the exclude list
		if (get_option('show_on_front') == 'page' && $args['type'] == 'pages') {
			if ( !empty( $list_args['exclude'] ) ) {
				$list_args['exclude'] .= ',';
			} else {
				$list_args['exclude'] = '';
			}
			$list_args['exclude'] .= get_option('page_on_front');
		}
	}
	
	$list_args['echo'] = false;
	$list_args['title_li'] = '';
	
	if( $args['type'] == 'pages' )
		$menu .= str_replace( array( "\r", "\n", "\t" ), '', wp_list_pages($list_args) );
	elseif( $args['type'] == 'categories' )
		$menu .= str_replace( array( "\r", "\n", "\t" ), '', wp_list_categories($list_args) );
		
	// Apply filters to the nav items
	$menu = apply_filters('genesis_nav_items', $menu, $args);
		
	$menu_class = ( $args['menu_class'] ) ? ' class="'.esc_attr($args['menu_class']).'"' : '';
	$menu_id = ( $args['menu_id'] ) ? ' id="'.esc_attr($args['menu_id']).'"' : '';
	
	if( $menu )
		$menu = '<ul'. $menu_id . $menu_class . '>' . $menu . '</ul>';
	
	// Apply filters to the final nav output
	$menu = apply_filters('genesis_nav', $menu, $args);
	
	if( $args['echo'] )
		echo $menu;
	else
		return $menu;
}
?>