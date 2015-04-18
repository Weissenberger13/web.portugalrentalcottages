<?php
/**
 * This function expedites the widget area registration process by taking
 * common things, before/after_widget, before/after_title, and doing them automatically.
 *
 * @uses wp_parse_args, register_sidebar
 * @since 1.0.1
 * @author Charles Clarkson
 * @author Nathan Rice
 */
function genesis_register_sidebar($args) {
	$defaults = array(
		'before_widget' => '<div id="%1$s" class="widget %2$s"><div class="widget-wrap">',
		'after_widget'  => "</div></div>\n",
		'before_title'  => '<h4 class="widgettitle">',
		'after_title'   => "</h4>\n"
	);

	$args = wp_parse_args($args, $defaults);

	return register_sidebar($args);
}


// defines the sidebars that are displayed in the WordPress widget screen
genesis_register_sidebar(array(
	'name'=>'Header Right',
	'description' => __('This is the right side of the header', 'genesis'),
	'id' => 'header-right'
));

genesis_register_sidebar(array(
	'name'=>'Sidebar',
	'description' => __('This is the main sidebar if you are using the Sidebar/Content or Content/Sidebar site layout option', 'genesis'),
	'id' => 'sidebar'
));

genesis_register_sidebar(array(
	'name'=>'Sidebar Alt',
	'description' => __('This is the left sidebar if you are using the Sidebar/Content/Sidebar site layout option', 'genesis'),
	'id' => 'sidebar-alt'
));