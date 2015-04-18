<?php
/**
 * undocumented 
 *
 */

/**
 * This function loads the stylesheet.
 * If a child theme is active, it loads the child theme's stylesheet,
 * otherwise, it loads the Genesis stylesheet.
 *
 */
add_action('genesis_meta', 'genesis_load_stylesheet');
function genesis_load_stylesheet() {
	echo "\n".'<!--The Main Stylesheet-->'."\n";
	echo '<link rel="stylesheet" href="'.get_bloginfo('stylesheet_url').'" type="text/css" media="screen" />'."\n";
}

/**
 * This function/filter adds new classes to the <body>
 * so that we can use psuedo-variables in our CSS file,
 * which helps us achieve multiple header layouts with minimal code
 *
 * @since 0.2.2
 */
add_filter('body_class', 'genesis_header_body_classes');
function genesis_header_body_classes($classes) {
	
	// add header classes to $classes array
	if(genesis_get_option('header_full'))
		$classes[] = 'header-full-width';
	if(genesis_get_option('blog_title') == 'image')
		$classes[] = 'header-image';
	
	// return filtered $classes
	return $classes;
}

/**
 * This function checks both the custom field and
 * the theme option to find the user-selected site
 * layout, and returns it.
 *
 * @since 0.2.2
 */
function genesis_site_layout() {
	
	// if is_singular, check for custom field,
	// fallback on theme option if no custom field not set
	if(is_singular()) {
		$custom_field = genesis_get_custom_field( '_genesis_layout' );
		$theme_option = genesis_get_option( 'site_layout' );
		$site_layout = ( $custom_field ) ? $custom_field : $theme_option;
	}
	// else pull the theme option
	else {
		$site_layout = genesis_get_option( 'site_layout' );
	}
	
	return esc_attr( $site_layout );
}

/**
 * This function/filter adds new classes to the <body>
 * so that we can use psuedo-variables in our CSS file,
 * which helps us achieve multiple site layouts with minimal code
 *
 * @since 0.2.2
 */
add_filter('body_class', 'genesis_layout_body_classes');
function genesis_layout_body_classes($classes) {
	
	// get the layout
	$site_layout = genesis_site_layout();
	
	// add new class to $classes array
	if($site_layout) $classes[] = $site_layout;
	
	// return filtered $classes
	return $classes;
}

/**
 * This function outputs the sidebar.php file
 * if specified in theme options or in-post options
 *
 * @since 0.2
 */
add_action('genesis_after_content', 'genesis_get_sidebar');
function genesis_get_sidebar() {
	
	// get the layout
	$site_layout = genesis_site_layout();

	// if full width content option is selected
	if($site_layout == 'full-width-content') return;
	// else, output the primary sidebar
	else get_sidebar();
}

/**
 * This function outputs the sidebar_alt.php file
 * if specified in theme options or in-post options
 *
 * @since 0.2
 */
add_action('genesis_after_content_sidebar_wrap', 'genesis_get_sidebar_alt');
function genesis_get_sidebar_alt() {
	
	// get the layout
	$site_layout = genesis_site_layout();
	
	// if we need the alternate sidebar
    if(	$site_layout == 'sidebar-content-sidebar' || 
		$site_layout == 'sidebar-sidebar-content' || 
		$site_layout == 'content-sidebar-sidebar')
    get_sidebar('alt');
	// else, return nothing
    else return;
}

/**
 * This next section adds a new box to the post/page edit screen,
 * so that the user can set layout options on a per-post/page basis.
 *
 * genesis_add_inpost_layout_box() is used to register the boxes.
 * @uses add_meta_box
 * genesis_inpost_layout_box() generates the content in the boxes.
 * @uses wp_create_nonce, checked, genesis_get_custom_field
 *
 * @since 0.2.2
 */
add_action('admin_menu', 'genesis_add_inpost_layout_box');
function genesis_add_inpost_layout_box() {
	add_meta_box('genesis_inpost_layout_box', __('Layout Options', 'genesis'), 'genesis_inpost_layout_box', 'post', 'normal', 'high');
	add_meta_box('genesis_inpost_layout_box', __('Layout Options', 'genesis'), 'genesis_inpost_layout_box', 'page', 'normal', 'high');
}
function genesis_inpost_layout_box() { ?>

	<?php wp_nonce_field( plugin_basename(__FILE__), 'genesis_inpost_layout_nonce' ); ?>
	
	<?php $_genesis_layout = genesis_get_custom_field('_genesis_layout'); ?>
	
	<input type="radio" name="_genesis_layout" id="default-layout" value="" <?php checked('', $_genesis_layout); ?> /> <label class="default" for="default-layout"><?php _e('Default Layout', 'genesis'); ?> <?php _e('set in', 'genesis'); ?> <a href="<?php echo admin_url('admin.php?page=genesis'); ?>"><?php _e('Theme Settings', 'genesis'); ?></a></label>
	
	<br style="clear: both;" /><br />
	
	<label class="box"><input type="radio" name="_genesis_layout" id="content-sidebar" value="content-sidebar" <?php checked('content-sidebar', $_genesis_layout); ?> /> <img src="<?php echo GENESIS_ADMIN_IMAGES_URL; ?>/layouts/cs.gif" alt="" /></label>
	
	<label class="box"><input type="radio" name="_genesis_layout" id="sidebar-content" value="sidebar-content" <?php checked('sidebar-content', $_genesis_layout); ?> /> <img src="<?php echo GENESIS_ADMIN_IMAGES_URL; ?>/layouts/sc.gif" alt="" /></label>
	
	<label class="box"><input type="radio" name="_genesis_layout" id="content-sidebar-sidebar" value="content-sidebar-sidebar" <?php checked('content-sidebar-sidebar', $_genesis_layout); ?> /> <img src="<?php echo GENESIS_ADMIN_IMAGES_URL; ?>/layouts/css.gif" alt="" /></label>
	
	<label class="box"><input type="radio" name="_genesis_layout" id="sidebar-sidebar-content" value="sidebar-sidebar-content" <?php checked('sidebar-sidebar-content', $_genesis_layout); ?> /> <img src="<?php echo GENESIS_ADMIN_IMAGES_URL; ?>/layouts/ssc.gif" alt="" /></label>
	
	<label class="box"><input type="radio" name="_genesis_layout" id="sidebar-content-sidebar" value="sidebar-content-sidebar" <?php checked('sidebar-content-sidebar', $_genesis_layout); ?> /> <img src="<?php echo GENESIS_ADMIN_IMAGES_URL; ?>/layouts/scs.gif" alt="" /></label>
		
	<label class="box"><input style="float: left; margin-right: 5px;" type="radio" name="_genesis_layout" id="full-width-content" value="full-width-content" <?php checked('full-width-content', $_genesis_layout); ?> /> <img src="<?php echo GENESIS_ADMIN_IMAGES_URL; ?>/layouts/c.gif" alt="Full Width Content" /></label>
	
	<br style="clear: both;" />
	
<?php
}

/**
 * This function saves the layout options when we save a post/page.
 * It does so by grabbing the array passed in $_POST, looping through
 * it, and saving each key/value pair as a custom field.
 *
 * @uses wp_verify_nonce, plugin_basename, current_user_can
 * @uses add_post_meta, update_post_meta, delete_post_meta, get_custom_field
 *
 * @since 0.2.2
 */
add_action('save_post', 'genesis_inpost_layout_save', 1, 2);
function genesis_inpost_layout_save($post_id, $post) {
	
	//	verify the nonce
	if (!wp_verify_nonce($_POST['genesis_inpost_layout_nonce'], plugin_basename(__FILE__)))
		return $post_id;
		
	//	don't try to save the data under autosave, ajax, or future post.
	if(defined(DOING_AUTOSAVE) && DOING_AUTOSAVE) return;
	if(defined(DOING_AJAX) && DOING_AJAX) return;
	if(defined(DOING_CRON) && DOING_CRON) return;

	//	is the user allowed to edit the post or page?
	if(('page' == $_POST['post_type'] && !current_user_can('edit_page', $post_id)) || !current_user_can('edit_post', $post_id ))
		return $post_id;
		
	$genesis_layout = $_POST['_genesis_layout'];
	
	//	save/update
	if(!genesis_get_custom_field('_genesis_layout'))
		update_post_meta($post_id, '_genesis_layout', $genesis_layout);
	else
		add_post_meta($post_id, '_genesis_layout', $genesis_layout, TRUE);
		
	//delete if blank
	if(!$genesis_layout) delete_post_meta($post_id, '_genesis_layout');
	
}