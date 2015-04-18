<?php
/**
 * This file controls customisations to the Prose Child Theme.
 *
 * @author StudioPress
 * @package Prose
 * @subpackage Customisations
 */

/**
 * Include Genesis theme files
 */
require_once(TEMPLATEPATH.'/lib/init.php');

/**
 * Include Prose theme files
 */
require_once(STYLESHEETPATH.'/lib/init.php');

// Add support for custom background
if (function_exists('add_custom_background')) {
    add_custom_background();
}

/**
 * Defines child theme name (do not remove)
 */  
define('CHILD_THEME_NAME', 'Prose Theme');

/**
 * Defines child theme URL (do not remove)
 */  
define('CHILD_THEME_URL', 'http://www.studiopress.com/themes/prose');



/**
 * Modify the size of the Gravatar in the author box
 * 
 * @param int $size
 */
function prose_gravatar_size($size) {
    return '60'; 
}
add_filter('genesis_author_box_gravatar_size', 'prose_gravatar_size');

/**
 * Add widgeted footer section
 */
function prose_include_footer_widgets() {
    require_once(CHILD_DIR . '/footer-widgeted.php');
}
add_action('genesis_before_footer', 'prose_include_footer_widgets'); 

// Reposition the footer
remove_action('genesis_footer', 'genesis_footer_markup_open', 5);
remove_action('genesis_footer', 'genesis_do_footer');
remove_action('genesis_footer', 'genesis_footer_markup_close', 15);
add_action('genesis_after', 'genesis_footer_markup_open', 5);
add_action('genesis_after', 'genesis_do_footer');
add_action('genesis_after', 'genesis_footer_markup_close', 15);

/**
 * Customize the footer section
 *
 * @param string $creds
 * @return string 
 */
function prose_footer_creds_text($creds) {
	$creds = __('Copyright', 'genesis') . ' [footer_copyright] [footer_childtheme_link] '. __('on', PROSE_DOMAIN) .' [footer_genesis_link] &middot; [footer_wordpress_link] &middot; [footer_loginout]';
	return $creds;
}
add_filter('genesis_footer_creds_text', 'prose_footer_creds_text');

// Register widget areas
genesis_register_sidebar(array(
	'name'=>'Footer #1',
	'description' => __('This is the first column of the footer section.', PROSE_DOMAIN),
	'before_title'=>'<h4 class="widgettitle">','after_title'=>'</h4>'
));
genesis_register_sidebar(array(
	'name'=>'Footer #2',
	'description' => __('This is the second column of the footer section.', PROSE_DOMAIN),
	'before_title'=>'<h4 class="widgettitle">','after_title'=>'</h4>'
));
genesis_register_sidebar(array(
	'name'=>'Footer #3',
	'description' => __('This is the third column of the footer section.', PROSE_DOMAIN),
	'before_title'=>'<h4 class="widgettitle">','after_title'=>'</h4>'
));
// Remove the post info function
remove_action('genesis_before_post_content', 'genesis_post_info');
remove_action('genesis_after_post_content', 'genesis_post_meta');

add_action('genesis_after_post', 'executive_include_bottom_widgets');
function executive_include_bottom_widgets() {
    require(CHILD_DIR.'/bottom-widgets.php');
}
add_action('genesis_after_post_title', 'executive_include_top_widgets');
function executive_include_top_widgets() {
    require(CHILD_DIR.'/top-widgets.php');
}
add_action('genesis_before_sidebar_widget_area', 'executive_include_top_adverts');
function executive_include_top_adverts() {
    require(CHILD_DIR.'/top-adverts.php');
}