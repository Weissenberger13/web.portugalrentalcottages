<?php
/**
 * This function registers the default values for Genesis theme settings
 */
function genesis_theme_settings_defaults() {
	$defaults = array( // define our defaults
			'blog_title' => 'text',
			'header_full' => 0,
			'site_layout' => 'content-sidebar',
			'nav' => 1,
			'nav_home' => 1,
			'nav_type' => 'pages',
			'nav_pages_sort' => 'menu_order',
			'nav_categories_sort' => 'name',
			'nav_depth' => 0,
			'nav_right' => 'date',
			'nav_twitter_text' => 'Follow me on Twitter',
			'subnav' => 0,
			'subnav_home' => 1,
			'subnav_type' => 'categories',
			'subnav_pages_sort' => 'menu_order',
			'subnav_categories_sort' => 'name',
			'subnav_depth' => 0,
			'comments_pages' => 0,
			'comments_posts' => 1,
			'author_box' => 1,
			'breadcrumb_home' => 1,
			'breadcrumb_single' => 1,
			'breadcrumb_page' => 1,
			'breadcrumb_archive' => 1,
			'content_archive' => 'full',
			'thumbnail' => 0,
			'posts_nav' => 'older-newer',
			'blog_cat' => '',
			'blog_cat_num' => 10,
			'header_scripts' => '',
			'footer_scripts' => '',
			'theme_version' => PARENT_THEME_VERSION // <-- no comma after the last option
	);
	
	return apply_filters('genesis_theme_settings_defaults', $defaults);
}

/**
 * This registers the settings field and adds defaults to the options table
 */
add_action('admin_init', 'genesis_register_theme_settings');
function genesis_register_theme_settings() {
	register_setting(GENESIS_SETTINGS_FIELD, GENESIS_SETTINGS_FIELD);
	add_option(GENESIS_SETTINGS_FIELD, genesis_theme_settings_defaults(), '', 'yes');
}

/**
 * This is a necessary go-between to get our scripts and boxes loaded
 * on the theme settings page only, and not the rest of the admin
 */
add_action('admin_menu', 'genesis_theme_settings_init');
function genesis_theme_settings_init() {
	global $_genesis_theme_settings_pagehook;
	
	add_action('load-'.$_genesis_theme_settings_pagehook, 'genesis_theme_settings_scripts');
	add_action('load-'.$_genesis_theme_settings_pagehook, 'genesis_theme_settings_boxes');
}

function genesis_theme_settings_scripts() {	
	wp_enqueue_script('common');
	wp_enqueue_script('wp-lists');
	wp_enqueue_script('postbox');
}

function genesis_Theme_Settings_boxes() {
	global $_genesis_theme_settings_pagehook;
	
	add_meta_box('genesis-theme-settings-version', __('Information', 'genesis'), 'genesis_theme_settings_info_box', $_genesis_theme_settings_pagehook, 'column1');
	add_meta_box('genesis-theme-settings-general', __('General Settings', 'genesis'), 'genesis_theme_settings_general_box', $_genesis_theme_settings_pagehook, 'column1');
	add_meta_box('genesis-theme-settings-nav', __('Primary Navigation', 'genesis'), 'genesis_theme_settings_nav_box', $_genesis_theme_settings_pagehook, 'column1');
	add_meta_box('genesis-theme-settings-subnav', __('Secondary Navigation', 'genesis'), 'genesis_theme_settings_subnav_box', $_genesis_theme_settings_pagehook, 'column1');
	add_meta_box('genesis-theme-settings-comments', __('Comments', 'genesis'), 'genesis_theme_settings_comments_box', $_genesis_theme_settings_pagehook, 'column1');
	add_meta_box('genesis-theme-settings-single', __('Single Post', 'genesis'), 'genesis_theme_settings_single_box', $_genesis_theme_settings_pagehook, 'column2');
	add_meta_box('genesis-theme-settings-breadcrumb', __('Breadcrumbs', 'genesis'), 'genesis_theme_settings_breadcrumb_box', $_genesis_theme_settings_pagehook, 'column2');
	add_meta_box('genesis-theme-settings-posts', __('Post Content', 'genesis'), 'genesis_theme_settings_posts_box', $_genesis_theme_settings_pagehook, 'column2');
	add_meta_box('genesis-theme-settings-blogpage', __('Blog Page', 'genesis'), 'genesis_theme_settings_blogpage_box', $_genesis_theme_settings_pagehook, 'column2');
	add_meta_box('genesis-theme-settings-scripts', __('Header/Footer Scripts', 'genesis'), 'genesis_theme_settings_scripts_box', $_genesis_theme_settings_pagehook, 'column2');
}

/**
 * Tell WordPress that we want only 2 columns available for our meta-boxes
 */
add_filter('screen_layout_columns', 'genesis_theme_settings_layout_columns', 10, 2);
function genesis_theme_settings_layout_columns($columns, $screen) {
	global $_genesis_theme_settings_pagehook;
	if ($screen == $_genesis_theme_settings_pagehook) {
		// This page should only have 2 column options
		$columns[$_genesis_theme_settings_pagehook] = 2;
	}
	return $columns;
}

/**
 * This function is what actually gets output to the page. It handles the markup,
 * builds the form, outputs necessary JS stuff, and fires <code>do_meta_boxes()</code>
 */
function genesis_theme_settings_admin() { 
global $_genesis_theme_settings_pagehook, $screen_layout_columns;
if( $screen_layout_columns == 3 ) {
	$width = "width: 32.67%";
}
elseif( $screen_layout_columns == 2 ) {
	$width = "width: 49%;";
	$hide3 = " display: none;";
}
else {
	$width = "width: 99%;";
	$hide2 = $hide3 = " display: none;";
}
?>	
	<div id="genesis-theme-settings" class="wrap genesis-metaboxes">
	<form method="post" action="options.php">
		
		<?php wp_nonce_field('closedpostboxes', 'closedpostboxesnonce', false ); ?>
		<?php wp_nonce_field('meta-box-order', 'meta-box-order-nonce', false ); ?>
		<?php settings_fields(GENESIS_SETTINGS_FIELD); // important! ?>
		<input type="hidden" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[theme_version]>" value="<?php esc_attr(genesis_option('theme_version')); ?>" />
		
		<?php screen_icon('options-general'); ?>
		<h2><?php _e('Genesis - Theme Settings', 'genesis'); ?></h2>
		
		<div class="top-buttons">
			<input type="submit" class="button-primary" value="<?php _e('Save Settings', 'genesis') ?>" />
			<input type="submit" class="button-highlighted" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[reset]" value="<?php _e('Reset Settings', 'genesis'); ?>" />
		</div>
		
		<?php
		if(genesis_get_option('reset')) {
			update_option(GENESIS_SETTINGS_FIELD, genesis_theme_settings_defaults());
			echo '<div id="message" class="updated" id="message"><p><strong>'.__('Theme Settings Reset', 'genesis').'</strong></p></div>';
		}
		elseif( isset($_REQUEST['updated']) && $_REQUEST['updated'] == 'true') {  
			echo '<div id="message" class="updated" id="message"><p><strong>'.__('Theme Settings Saved', 'genesis').'</strong></p></div>';
		}
		?>
		
		<div class="metabox-holder">
			<div class="postbox-container" style="<?php echo $width; ?>">
				<?php do_meta_boxes($_genesis_theme_settings_pagehook, 'column1', null); ?>
			</div>
			<div class="postbox-container" style="<?php echo $width; echo $hide2; ?>">
				<?php do_meta_boxes($_genesis_theme_settings_pagehook, 'column2', null); ?>
			</div>
		</div>
		
		<div class="bottom-buttons">
			<input type="submit" class="button-primary" value="<?php _e('Save Settings', 'genesis') ?>" />
			<input type="submit" class="button-highlighted" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[reset]" value="<?php _e('Reset Settings', 'genesis'); ?>" />
		</div>
	</form>
	</div>
	<script type="text/javascript">
		//<![CDATA[
		jQuery(document).ready( function($) {
			// close postboxes that should be closed
			$('.if-js-closed').removeClass('if-js-closed').addClass('closed');
			// postboxes setup
			postboxes.add_postbox_toggles('<?php echo $_genesis_theme_settings_pagehook; ?>');
		});
		//]]>
	</script>

<?php
}

/**
 * This next section defines functions that contain the content of the "boxes" that will be
 * output by default on the "Theme Settings" page. There's a bunch of them.
 *
 * FWIW, you can copy this syntax and load your own boxes on the theme settings page too.
 */
function genesis_theme_settings_info_box() { ?>
	<p><strong><?php _e('Version:', 'genesis'); ?></strong> <?php genesis_option('theme_version'); ?> &middot; <strong><?php _e('Released:', 'genesis'); ?></strong> <?php _e('February 15th, 2010', 'genesis'); ?></p>
	
	<p><input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[show_info]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[show_info]" value="1" <?php checked(1, genesis_get_option('show_info')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[show_info]"><?php _e('Display Theme Information in your document source', 'genesis'); ?></label></p>
	
	<p><span class="description"><?php _e('<b>Note:</b> This can be helpful for diagnosing problems with your theme when seeking support in the forums.', 'genesis'); ?></span></p>
<?php
}

function genesis_theme_settings_general_box() { ?>
	<p><?php _e("Use for blog title/logo:", 'genesis'); ?>
	<select name="<?php echo GENESIS_SETTINGS_FIELD; ?>[blog_title]">
		<option style="padding-right:10px;" value="text" <?php selected('text', genesis_get_option('blog_title')); ?>><?php _e("Dynamic text", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="image" <?php selected('image', genesis_get_option('blog_title')); ?>><?php _e("Image logo", 'genesis'); ?></option>
	</select></p>			
	<p><input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[header_full]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[header_full]" value="1" <?php checked(1, genesis_get_option('header_full')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[header_full]"><?php _e("Use a full-width header?", 'genesis'); ?></label>
	</select></p>
	<p><?php _e("Select site layout:", 'genesis'); ?>
	<select name="<?php echo GENESIS_SETTINGS_FIELD; ?>[site_layout]">
		<option style="padding-right:10px;" value="content-sidebar" <?php selected('content-sidebar', genesis_get_option('site_layout')); ?>><?php _e("Content/Sidebar", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="sidebar-content" <?php selected('sidebar-content', genesis_get_option('site_layout')); ?>><?php _e("Sidebar/Content", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="content-sidebar-sidebar" <?php selected('content-sidebar-sidebar', genesis_get_option('site_layout')); ?>><?php _e("Content/Sidebar/Sidebar", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="sidebar-sidebar-content" <?php selected('sidebar-sidebar-content', genesis_get_option('site_layout')); ?>><?php _e("Sidebar/Sidebar/Content", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="sidebar-content-sidebar" <?php selected('sidebar-content-sidebar', genesis_get_option('site_layout')); ?>><?php _e("Sidebar/Content/Sidebar", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="full-width-content" <?php selected('full-width-content', genesis_get_option('site_layout')); ?>><?php _e("Full Width Content", 'genesis'); ?></option>
	</select></p>
<?php
}

function genesis_theme_settings_nav_box() { ?>
	<p><input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav]" value="1" <?php checked(1, genesis_get_option('nav')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav]"><?php _e("Include Primary Navigation Menu?", 'genesis'); ?></label>
	</p>
	
	<p><input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav_home]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav_home]" value="1" <?php checked(1, genesis_get_option('nav_home')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav_home]"><?php _e('Display Home Link?', 'genesis'); ?></p>
		
	<p><?php _e("Display the following (left side):", 'genesis'); ?></p>
	
	<p>
	<label><input type="radio" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav_type]" value="pages" <?php checked('pages', genesis_get_option('nav_type')); ?> />
	<?php _e('List of Pages sorted by', 'genesis'); ?></label>
	<select name="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav_pages_sort]">
		<option style="padding-right:10px;" value="menu_order" <?php selected('menu_order', genesis_get_option('nav_pages_sort')); ?>>Menu Order</option>
		<option style="padding-right:10px;" value="post_title" <?php selected('post_title', genesis_get_option('nav_pages_sort')); ?>>Title</option>
		<option style="padding-right:10px;" value="ID" <?php selected('ID', genesis_get_option('nav_pages_sort')); ?>>ID</option>
		<option style="padding-right:10px;" value="post_date" <?php selected('post_date', genesis_get_option('nav_pages_sort')); ?>>Date Created</option>
		<option style="padding-right:10px;" value="post_modified" <?php selected('post_modified', genesis_get_option('nav_pages_sort')); ?>>Date Modified</option>
		<option style="padding-right:10px;" value="post_author" <?php selected('post_author', genesis_get_option('nav_pages_sort')); ?>>Author</option>
		<option style="padding-right:10px;" value="post_name" <?php selected('post_name', genesis_get_option('nav_pages_sort')); ?>>Slug</option>
	</select>
	
	<br />
	
	<label><input type="radio" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav_type]" value="categories" <?php checked('categories', genesis_get_option('nav_type')); ?> />
	<?php _e('List of Categories sorted by', 'genesis'); ?></label>
	<select name="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav_categories_sort]">
		<option style="padding-right:10px;" value="name" <?php selected('name', genesis_get_option('nav_categories_sort')); ?>>Name</option>
		<option style="padding-right:10px;" value="ID" <?php selected('id', genesis_get_option('nav_categories_sort')); ?>>ID</option>
		<option style="padding-right:10px;" value="slug" <?php selected('slug', genesis_get_option('nav_categories_sort')); ?>>Slug</option>
		<option style="padding-right:10px;" value="count" <?php selected('count', genesis_get_option('nav_categories_sort')); ?>>Count</option>
		<option style="padding-right:10px;" value="term_group" <?php selected('term_group', genesis_get_option('nav_categories_sort')); ?>>Term Group</option>
	</select>
	</p>
	
	<p><?php _e('Navigation Depth', 'genesis'); ?>:
	<select name="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav_depth]">
		<option style="padding-right: 10px;" value="0" <?php selected(0, genesis_get_option('nav_depth')); ?>>No Limit</option>
		<option style="padding-right: 10px;" value="1" <?php selected(1, genesis_get_option('nav_depth')); ?>>1</option>
		<option style="padding-right: 10px;" value="2" <?php selected(2, genesis_get_option('nav_depth')); ?>>2</option>
		<option style="padding-right: 10px;" value="3" <?php selected(3, genesis_get_option('nav_depth')); ?>>3</option>
		<option style="padding-right: 10px;" value="4" <?php selected(4, genesis_get_option('nav_depth')); ?>>4</option>
	</select>
	</p>
	
	<p><?php _e("Include the following ID's:", 'genesis'); ?><br />
	<input type="text" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav_include]" value="<?php echo esc_attr( genesis_get_option('nav_include') ); ?>" size="40" /><br />
	<small><strong><?php _e("Comma separated - 1,2,3 for example", 'genesis'); ?></strong></small></p>

	<p><?php _e("Exclude the following ID's:", 'genesis'); ?><br />
	<input type="text" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav_exclude]" value="<?php echo esc_attr( genesis_get_option('nav_exclude') ); ?>" size="40" /><br />
	<small><strong><?php _e("Comma separated - 1,2,3 for example", 'genesis'); ?></strong></small></p>

	<hr class="div" />
	
	<p><?php _e("Display the following (right side):", 'genesis'); ?>
	<select name="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav_right]">
		<option style="padding-right:10px;" value="date" <?php selected('date', genesis_get_option('nav_right')); ?>><?php _e("Today's date", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="rss" <?php selected('rss', genesis_get_option('nav_right')); ?>><?php _e("RSS feed links", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="search" <?php selected('search', genesis_get_option('nav_right')); ?>><?php _e("Search form", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="twitter" <?php selected('twitter', genesis_get_option('nav_right')); ?>><?php _e("Twitter Link", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="" <?php selected('', genesis_get_option('nav_right')); ?>><?php _e("Don't display anything", 'genesis'); ?></option>
	</select></p>
	<p><?php _e("Enter Twitter ID:", 'genesis'); ?>
	<input type="text" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[twitter_id]" value="<?php echo esc_attr( genesis_get_option('twitter_id') ); ?>" size="27" /></p>
	<p><?php _e("Twitter Link Text:", 'genesis'); ?>
	<input type="text" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[nav_twitter_text]" value="<?php echo esc_attr( genesis_get_option('nav_twitter_text') ); ?>" size="27" /></p>
<?php
}

function genesis_theme_settings_subnav_box() { ?>
	<p><input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav]" value="1" <?php checked(1, genesis_get_option('subnav')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav]"><?php _e("Include Secondary Navigation Menu?", 'genesis'); ?></label>
	</p>
	
	<p><input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav_home]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav_home]" value="1" <?php checked(1, genesis_get_option('subnav_home')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav_home]"><?php _e('Display Home Link?', 'genesis'); ?></p>
	
	<p><?php _e("Display the following:", 'genesis'); ?></p>
	<p>
	<label><input type="radio" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav_type]" value="pages" <?php checked('pages', genesis_get_option('subnav_type')); ?> />
	<?php _e('List of Pages sorted by', 'genesis'); ?></label>
	<select name="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav_pages_sort]">
		<option style="padding-right:10px;" value="menu_order" <?php selected('menu_order', genesis_get_option('subnav_pages_sort')); ?>>Menu Order</option>
		<option style="padding-right:10px;" value="post_title" <?php selected('post_title', genesis_get_option('subnav_pages_sort')); ?>>Title</option>
		<option style="padding-right:10px;" value="ID" <?php selected('ID', genesis_get_option('subnav_pages_sort')); ?>>ID</option>
		<option style="padding-right:10px;" value="post_date" <?php selected('post_date', genesis_get_option('subnav_pages_sort')); ?>>Date Created</option>
		<option style="padding-right:10px;" value="post_modified" <?php selected('post_modified', genesis_get_option('subnav_pages_sort')); ?>>Date Modified</option>
		<option style="padding-right:10px;" value="post_author" <?php selected('post_author', genesis_get_option('subnav_pages_sort')); ?>>Author</option>
		<option style="padding-right:10px;" value="post_name" <?php selected('post_name', genesis_get_option('subnav_pages_sort')); ?>>Slug</option>
	</select>
	
	<br />
	
	<label><input type="radio" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav_type]" value="categories" <?php checked('categories', genesis_get_option('subnav_type')); ?> />
	<?php _e('List of Categories sorted by', 'genesis'); ?></label>
	<select name="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav_categories_sort]">
		<option style="padding-right:10px;" value="name" <?php selected('name', genesis_get_option('subnav_categories_sort')); ?>>Name</option>
		<option style="padding-right:10px;" value="ID" <?php selected('id', genesis_get_option('subnav_categories_sort')); ?>>ID</option>
		<option style="padding-right:10px;" value="slug" <?php selected('slug', genesis_get_option('subnav_categories_sort')); ?>>Slug</option>
		<option style="padding-right:10px;" value="count" <?php selected('count', genesis_get_option('subnav_categories_sort')); ?>>Count</option>
		<option style="padding-right:10px;" value="term_group" <?php selected('term_group', genesis_get_option('subnav_categories_sort')); ?>>Term Group</option>
	</select>
	</p>
	
	<p><?php _e('Sub Navigation Depth', 'genesis'); ?>:
	<select name="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav_depth]">
		<option style="padding-right: 10px;" value="0" <?php selected(0, genesis_get_option('nav_depth')); ?>>No Limit</option>
		<option style="padding-right: 10px;" value="1" <?php selected(1, genesis_get_option('nav_depth')); ?>>1</option>
		<option style="padding-right: 10px;" value="2" <?php selected(2, genesis_get_option('nav_depth')); ?>>2</option>
		<option style="padding-right: 10px;" value="3" <?php selected(3, genesis_get_option('nav_depth')); ?>>3</option>
		<option style="padding-right: 10px;" value="4" <?php selected(4, genesis_get_option('nav_depth')); ?>>4</option>
	</select>
	</p>

	<p><?php _e("Include the following ID's:", 'genesis'); ?><br />
	<input type="text" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav_include]" value="<?php echo esc_attr( genesis_get_option('subnav_include') ); ?>" size="40" /><br />
	<small><strong><?php _e("Comma separated - 1,2,3 for example", 'genesis'); ?></strong></small></p>

	<p><?php _e("Exclude the following ID's:", 'genesis'); ?><br />
	<input type="text" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[subnav_exclude]" value="<?php echo esc_attr( genesis_get_option('subnav_exclude') ); ?>" size="40" /><br />
	<small><strong><?php _e("Comma separated - 1,2,3 for example", 'genesis'); ?></strong></small></p>
<?php
}

function genesis_theme_settings_comments_box() { ?>
	<p><input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[comments_posts]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[comments_posts]" value="1" <?php checked(1, genesis_get_option('comments_posts')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[comments_posts]"><?php _e("Enable on posts?", 'genesis'); ?></label>

	<input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[comments_pages]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[comments_pages]" value="1" <?php checked(1, genesis_get_option('comments_pages')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[comments_pages]"><?php _e("Enable on pages?", 'genesis'); ?></label>
	</p>
	<p><span class="description"><strong><?php _e("Note: ", 'genesis'); ?></strong><?php _e("Comments can also be disabled on a per post/page basis when creating new posts/pages in your dashboard.", 'genesis'); ?></span></p>
<?php
}

function genesis_theme_settings_single_box() { ?>
	<p>
	<input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[author_box]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[author_box]" value="1" <?php checked(1, genesis_get_option('author_box')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[author_box]"><?php _e('Enable Author Box on Single Posts', 'genesis'); ?></label>
	</p>
	
	<p><span class="description"><?php _e('<b>Note:</b> The author box displays information about the post author, including their Gravatar, Display Name, and User Bio.', 'genesis'); ?></span></p>

<?php
}

function genesis_theme_settings_breadcrumb_box() { ?>
	<p>
	<input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[breadcrumb_home]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[breadcrumb_home]" value="1" <?php checked(1, genesis_get_option('breadcrumb_home')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[breadcrumb_home]"><?php _e("Enable on Front Page", 'genesis'); ?></label><br />
	<input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[breadcrumb_single]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[breadcrumb_single]" value="1" <?php checked(1, genesis_get_option('breadcrumb_single')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[breadcrumb_single]"><?php _e("Enable on Posts", 'genesis'); ?></label><br />
	<input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[breadcrumb_page]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[breadcrumb_page]" value="1" <?php checked(1, genesis_get_option('breadcrumb_page')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[breadcrumb_page]"><?php _e("Enable on Pages", 'genesis'); ?></label><br />
	<input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[breadcrumb_archive]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[breadcrumb_archive]" value="1" <?php checked(1, genesis_get_option('breadcrumb_archive')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[breadcrumb_archive]"><?php _e("Enable on Archives", 'genesis'); ?></label>
	</p>
	
	<p><span class="description"><strong><?php _e("Note: ", 'genesis'); ?></strong><?php _e('Breadcrumbs are a great way of letting your visitors find out where they are on your site with just a glance. You can enable/disable them on certain areas of your site.', 'genesis'); ?></span></p>
<?php
}

function genesis_theme_settings_posts_box() { ?>
	<p><?php _e("Select one of the following:", 'genesis'); ?>
	<select name="<?php echo GENESIS_SETTINGS_FIELD; ?>[content_archive]">
		<option style="padding-right:10px;" value="full" <?php selected('full', genesis_get_option('content_archive')); ?>><?php _e("Display full posts", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="excerpts" <?php selected('excerpts', genesis_get_option('content_archive')); ?>><?php _e("Display post excerpts", 'genesis'); ?></option>
	</select></p>			
	<p><input type="checkbox" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[thumbnail]" id="<?php echo GENESIS_SETTINGS_FIELD; ?>[thumbnail]" value="1" <?php checked(1, genesis_get_option('thumbnail')); ?> /> <label for="<?php echo GENESIS_SETTINGS_FIELD; ?>[thumbnail]"><?php _e("Include the post image with excerpts?", 'genesis'); ?></label>
	</p>
	
	<p><?php _e("Image Size", 'genesis'); ?>:
	<?php $sizes = genesis_get_additional_image_sizes(); ?>
	<select name="<?php echo GENESIS_SETTINGS_FIELD; ?>[image_size]">
		<option style="padding-right:10px;" value="thumbnail">thumbnail (<?php echo get_option('thumbnail_size_w'); ?>x<?php echo get_option('thumbnail_size_h'); ?>)</option>
		<?php
		foreach((array)$sizes as $name => $size) :
		echo '<option style="padding-right: 10px;" value="'.$name.'" '.selected($name, genesis_get_option('image_size'), FALSE).'>'.$name.' ('.$size['width'].'x'.$size['height'].')</option>';
		endforeach;
		?>
	</select></p>

	<p><?php _e("Select Post Navigation Technique:", 'genesis'); ?>
	<select name="<?php echo GENESIS_SETTINGS_FIELD; ?>[posts_nav]">
		<option style="padding-right:10px;" value="older-newer" <?php selected('older-newer', genesis_get_option('posts_nav')); ?>><?php _e("Older / Newer", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="prev-next" <?php selected('prev-next', genesis_get_option('posts_nav')); ?>><?php _e("Previous / Next", 'genesis'); ?></option>
		<option style="padding-right:10px;" value="numeric" <?php selected('numeric', genesis_get_option('posts_nav')); ?>><?php _e("Numeric", 'genesis'); ?></option>
	</select></p>
	
	<p><span class="description"><strong><?php _e("Note: ", 'genesis'); ?></strong><?php _e("The post content option will affect any blog listings page, including archive, author, blog, category, search and tag pages.", 'genesis'); ?></span></p>
<?php
}

function genesis_theme_settings_blogpage_box() { ?>
	<p><?php _e("Display which category:", 'genesis'); ?>
	<?php wp_dropdown_categories(array('selected' => genesis_get_option('blog_cat'), 'name' => GENESIS_SETTINGS_FIELD.'[blog_cat]', 'orderby' => 'Name' , 'hierarchical' => 1, 'show_option_all' => __("All Categories", 'genesis'), 'hide_empty' => '0' )); ?></p>
	
	<p><?php _e("Number of Posts to Show"); ?>:
	<input type="text" name="<?php echo GENESIS_SETTINGS_FIELD; ?>[blog_cat_num]" value="<?php genesis_option('blog_cat_num'); ?>" size="2" /></p>
<?php
}

function genesis_theme_settings_scripts_box() { ?>
	<p><?php _e("Enter scripts/code you would like output to <code>wp_head()</code>:", 'genesis'); ?><br />
	<textarea name="<?php echo GENESIS_SETTINGS_FIELD; ?>[header_scripts]" cols=39 rows=5><?php genesis_option('header_scripts'); ?></textarea><br />
	<span class="description"><?php _e('<strong>Note:</strong> The <code>wp_head()</code> hook executes immediately before the closing <code>&lt;/head&gt;</code> tag in the document source', 'genesis'); ?></span></p>
	
	<p><?php _e("Enter scripts/code you would like output to <code>wp_footer()</code>:", 'genesis'); ?><br />
	<textarea name="<?php echo GENESIS_SETTINGS_FIELD; ?>[footer_scripts]" cols=39 rows=5><?php genesis_option('footer_scripts'); ?></textarea><br />
	<span class="description"><?php _e('<strong>Note:</strong> The <code>wp_footer()</code> hook executes immediately before the closing <code>&lt;/body&gt;</code> tag in the document source', 'genesis'); ?></span></p>
<?php
}