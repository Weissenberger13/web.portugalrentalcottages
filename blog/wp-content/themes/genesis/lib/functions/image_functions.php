<?php
/**
 * @todo Document this file
 *
 **/

/**
 * Activates post thumbnails/images in 2.9+
 *
 * @since 0.1
 */
if(function_exists('add_theme_support'))
	add_theme_support('post-thumbnails');

/**
 * Pulls an attachment ID from a post, if one exists
 *
 * @since 0.1
 */
function genesis_get_image_id($num = 0) {
	global $post;
	$children = get_children(array(
		'post_parent' => $post->ID,
		'post_type' => 'attachment',
		'post_mime_type' => 'image',
		'orderby' => 'menu_order',
		'order' => 'ASC'
	));

	$count = 0;
    foreach ((array)$children as $key => $value) {
        $images[$count] = $value;
        $count++;
    }
	if(isset($images[$num]))
		return $images[$num]->ID;
	else
		return false;
}

/**
 * Pulls an image from the media gallery and returns it
 *
 * @since 0.1
 */
// pulls an image URL from the media gallery
function genesis_get_image($args = array()) {
	global $post;
	
	$defaults = array(
		'format' => 'html',
		'size' => 'full',
		'num' => 0
	);
	$defaults = apply_filters('genesis_get_image_default_args', $defaults);
	
	$args = wp_parse_args($args, $defaults);

	// check for post image (native WP)
	if(function_exists('the_post_thumbnail') && has_post_thumbnail() && ($args['num'] === 0)) {
		$id = get_post_thumbnail_id();
		$html = wp_get_attachment_image($id, $args['size'], false, $args['attr']);
		list($url) = wp_get_attachment_image_src($id);
	}
	// else pull the first image attachment
	else {
		$id = genesis_get_image_id($args['num']);
		$html = wp_get_attachment_image($id, $args['size'], false, $args['attr']);
		list($url) = wp_get_attachment_image_src($id);
	}

	// determine output
	if(strtolower($args['format']) == 'html')
		$output = $html;
	elseif(strtolower($args['format']) == 'url')
		$output = $url;
	else
		$output = $src;
		
	// return FALSE if $src is blank
	if(empty($url)) $output = FALSE;
	
	// return FALSE if $src is invalid (file doesn't exist)
	$src = str_replace(get_bloginfo('siteurl'), '', $src);
	if(!file_exists(ABSPATH . $src)) $output = FALSE;
	
	// return data, filtered
	return apply_filters('genesis_get_image', $output, $args, $id, $html, $url, $src);
}
/**
 * Pulls an image from media gallery
 * and echos it
 *
 * @since 0.1
 */
function genesis_image($args = array()) {
	$image = genesis_get_image($args);
	
	if($image)
		echo $image;
	else
		return FALSE;
}

/**
 * Allows a child theme to add new image sizes
 *
 * @since 0.1.7
 */
function genesis_add_image_size($name, $width = 0, $height = 0, $crop = FALSE) {
	if(function_exists('add_image_size')) {
		add_image_size($name, $width, $height, $crop);
	}
	// for 2.8 backward compatibility
	else {
		global $_genesis_additional_image_sizes;
		$_genesis_additional_image_sizes[$name] = array('width' => absint($width), 'height' => absint($height), 'crop' => !!$crop );
		
		if(get_option($name.'size_w')) {
			update_option($name.'_size_w', $width);
			update_option($name.'_size_h', $height);
			update_option($name.'_crop', $crop);
		}
		else {
			add_option($name.'_size_w', $width);
			add_option($name.'_size_h', $height);
			add_option($name.'_crop', $crop);
		}
	}
}

/**
 * Filters intermediate sizes for 2.8 backward compatibility
 *
 * @since 0.1.7
 */
add_filter('intermediate_image_sizes', 'genesis_add_intermediate_sizes');
function genesis_add_intermediate_sizes($sizes) {
	global $_genesis_additional_image_sizes;
	
	if(version_compare(get_bloginfo('version'), '2.9', '<'))
	$sizes = array_merge((array)$sizes, array_keys((array)$_genesis_additional_image_sizes));
	
	return $sizes;
}

/**
 * Filters the attributes array in the wp_get_attachment_image function
 * For some reason, the wp_get_attachment_image function uses the caption
 * field value as the alt text, not the Alternate Text field value. Strange.
 *
 * @since 0.1.8
 */
add_filter('wp_get_attachment_image_attributes', 'genesis_filter_attachment_image_attributes', 10, 2);
function genesis_filter_attachment_image_attributes($attr, $attachment) {
	$alt = get_post_meta($attachment->ID, '_wp_attachment_image_alt', true);
	
	if($alt) $attr['alt'] = esc_attr( $alt );
	
	return $attr;
}

/**
 * Pulls additional image sizes
 *
 * @since 0.1.7
 * @return array
 */
function genesis_get_additional_image_sizes() {
	global $_wp_additional_image_sizes, $_genesis_additional_image_sizes;
	
	if($_wp_additional_image_sizes)
		$sizes = $_wp_additional_image_sizes;
	else
		$sizes = $_genesis_additional_image_sizes;
		
	return $sizes;
}

/**
 * Pulls all image sizes
 *
 * @since 1.0.2
 * @return array
 */
function genesis_get_image_sizes() {
	$sizes = array(
		'original'	=> array(),
		'full'		=> array(),
		'medium'	=> array(),
		'thumbnail'	=> array()
	);
	
	$additional_sizes = genesis_get_additional_image_sizes();
	
	return;
}