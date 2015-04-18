<?php
/**
 * @todo Document this file
 *
 **/

/**
 * These functions can be used to easily and efficiently pull data from a
 * post/page custom field. Returns FALSE if field is blank or not set.
 *
 * @param string $field used to indicate the custom field key
 *
 * @since 0.1.3
 */
function genesis_custom_field($field) {
	echo genesis_get_custom_field($field);
}
function genesis_get_custom_field($field) {
	global $post;
	$custom_field = get_post_meta($post->ID, $field, true);
	
	if($custom_field) {
		// sanitize and return the value of the custom field
		return wp_kses_stripslashes(wp_kses_decode_entities($custom_field));
	}
	else {
		// return FALSE if custom field is empty
		return FALSE;
	}
}

function genesis_rel_nofollow($xhtml) {
	$xhtml = genesis_strip_attr($xhtml, array('a'), array('rel'));
	$xhtml = stripslashes(wp_rel_nofollow($xhtml));
	
	return $xhtml;
}

/**
 * This function accepts a string of xHTML, parses it for any elements in the
 * $elements array, then parses that element for any attributes in the $attributes
 * array, and strips the attribute and its value(s).
 * 
 * @example genesis_strip_attr('<a class="class" href="http://google.com/">Google</a>', array('a'), array('class'));
 *
 * @param string $xhtml A string of xHTML formatted code
 * @param array|string $elements Elements that $attributes should be stripped from
 * @param array|string $attributes Attributes that should be stripped from $elements
 * @param bool $two_passes Whether the function should allow two passes
 *
 * @return string
 *
 * @since 1.0
 */
function genesis_strip_attr($xhtml, $elements, $attributes, $two_passes = true) {

	// Cache elements pattern
	$elements_pattern = join('|', $elements);

	// Build patterns
	$patterns = array();
	foreach ((array) $attributes as $attribute) {

		// Opening tags
		$patterns[] = sprintf('~(<(?:%s)[^>]*)\s+%s=[\\\'"][^\\\'"]+[\\\'"]([^>]*[^>]*>)~', $elements_pattern, $attribute);

		// Self closing tags
		$patterns[] = sprintf('~(<(?:%s)[^>]*)\s+%s=[\\\'"][^\\\'"]+[\\\'"]([^>]*[^/]+/>)~', $elements_pattern, $attribute);

	}

	// First pass
	$xhtml = preg_replace($patterns, '$1$2', $xhtml);

	if ( $two_passes ) // Second pass
		$xhtml = preg_replace($patterns, '$1$2', $xhtml);

	return $xhtml;
}