<?php
/**
 * undocumented 
 *
 * @package Genesis
 */

// Via Joost de Valk (attribute later)
function genesis_breadcrumb($home, $sep, $prefix = '', $suffix = '', $display = true) {
	global $wp_query, $post;
	
	$opt['prefix']				= __("You are here:",'genesis');
	$opt['archiveprefix'] 		= __("Archives for",'genesis');
	$opt['searchprefix'] 		= __("Search for",'genesis');
	
	function bold_or_not( $input ) {
		// return '<strong>'.$input.'</strong>';
		return $input;
	}
	// Copied and adapted from WP source
	function sp_get_category_parents($id, $link = FALSE, $separator = '/', $nicename = FALSE){
		$chain = '';
		$parent = &get_category($id);
		if ( is_wp_error( $parent ) )
		   return $parent;

		if ( $nicename )
		   $name = $parent->slug;
		else
		   $name = $parent->cat_name;

		if ( $parent->parent && ($parent->parent != $parent->term_id) )
		   $chain .= get_category_parents($parent->parent, true, $separator, $nicename);

		$chain .= bold_or_not($name);
		return $chain;
	}
	
	$on_front = get_option('show_on_front');
	
	if ($on_front == "page") {
		$homelink = '<a href="'.get_permalink(get_option('page_on_front')).'">'.$home.'</a>';
		$bloglink = $homelink.' '.$sep.' <a href="'.get_permalink(get_option('page_for_posts')).'">'.get_the_title(get_option('page_for_posts')).'</a>';
	} else {
		$homelink = '<a href="'.get_bloginfo('url').'">'.$home.'</a>';
		$bloglink = $homelink;
	}
		
	if ( ($on_front == "page" && is_front_page()) || ($on_front == "posts" && is_home()) ) {
		$output = $home;
	} elseif ( $on_front == "page" && is_home() ) {
		$output = $homelink.' '.$sep.' '.get_the_title(get_option('page_for_posts'));
	} elseif ( !is_page() ) {
		$output = $bloglink.' '.$sep.' ';
		if (is_single()) {
			$cats = get_the_category();
			$cat = $cats[0];
			if ($cat->parent != 0) {
				$output .= get_category_parents($cat->term_id, true, " ".$sep." ");
			} else {
				$output .= '<a href="'.get_category_link($cat->term_id).'">'.esc_html($cat->name).'</a> '.$sep.' '; 
			}
		}
		if ( is_category() ) {
			$cat = intval( get_query_var('cat') );
			$output .= sp_get_category_parents($cat, false, " ".$sep." ");
		} elseif ( is_tag() ) {
			$output .= bold_or_not($opt['archiveprefix']." ".single_cat_title('',false));
		} elseif (is_date()) { 
			$output .= bold_or_not($opt['archiveprefix']." ".single_month_title(' ',false));
		} elseif (is_author()) { 
			$user = get_userdatabylogin($wp_query->query_vars['author_name']);
			$output .= bold_or_not($opt['archiveprefix']." ".esc_html($user->display_name));
		} elseif (is_search()) {
			$output .= bold_or_not($opt['searchprefix'].' "'.esc_html(apply_filters('the_search_query', get_search_query())).'"');
		} else {
			$output .= bold_or_not(get_the_title());
		}
	} else {
		$post = $wp_query->get_queried_object();

		// If this is a top level Page, it's simple to output the breadcrumb
		if ( 0 == $post->post_parent ) {
			$output = $homelink." ".$sep." ".bold_or_not(get_the_title());
		} else {
			if (isset($post->ancestors)) {
				if (is_array($post->ancestors))
					$ancestors = array_values($post->ancestors);
				else 
					$ancestors = array($post->ancestors);				
			} else {
				$ancestors = array($post->post_parent);
			}

			// Reverse the order so it's oldest to newest
			$ancestors = array_reverse($ancestors);

			// Add the current Page to the ancestors list (as we need it's title too)
			$ancestors[] = $post->ID;

			$links = array();			
			foreach ( $ancestors as $ancestor ) {
				$tmp  = array();
				$tmp['title'] 	= strip_tags( get_the_title( $ancestor ) );
				$tmp['url'] 	= get_permalink($ancestor);
				$tmp['cur'] = false;
				if ($ancestor == $post->ID) {
					$tmp['cur'] = true;
				}
				$links[] = $tmp;
			}

			$output = $homelink;
			foreach ( $links as $link ) {
				$output .= ' '.$sep.' ';
				if (!$link['cur']) {
					$output .= '<a href="'.$link['url'].'">'.$link['title'].'</a>';
				} else {
					$output .= bold_or_not($link['title']);
				}
			}
		}
	}
	if ($opt['prefix'] != "") {
		$output = $opt['prefix']." ".$output;
	}
	if ($display) {
		echo $prefix.$output.$suffix;
	} else {
		return $prefix.$output.$suffix;
	}
}