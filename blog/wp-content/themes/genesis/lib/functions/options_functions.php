<?php
/**
 * These functions pull options/settings
 * from the options database.
 *
 * @since 0.1.3
 **/
function genesis_get_option($key, $setting = null) {
	$setting = ($setting) ? $setting : GENESIS_SETTINGS_FIELD;
	
	$options = apply_filters('genesis_options', get_option($setting), $setting);
	return wp_kses_stripslashes(wp_kses_decode_entities($options[$key]));
}
function genesis_option($key, $setting = null) {
	echo genesis_get_option($key, $setting);
}
function genesis_get_seo_option($key) {
	return genesis_get_option($key, GENESIS_SEO_SETTINGS_FIELD);
}
function genesis_seo_option($key) {
	genesis_option($key, GENESIS_SEO_SETTINGS_FIELD);
}