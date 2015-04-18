<?php
/**
 * This file is all about compatibility.
 */

/**
 * Upgrading to 1.0.1. A bit sloppy, but future upgrades
 * will be cleaner.
 */
add_action('init', 'genesis_upgrade');
function genesis_upgrade() {
	
	// Don't do anything if we're on the latest version
	if(version_compare(genesis_get_option('theme_version'), PARENT_THEME_VERSION, '>='))
		return;

	#########################
#	UPGRADE TO VERSION 1.0.1
	#########################
	
	// Check to see if we need to upgrade to 1.0.1
	if(version_compare(genesis_get_option('theme_version'), '1.0.1', '<')) {
	
		$theme_settings = get_option(GENESIS_SETTINGS_FIELD);
		$new_settings = array(
			'nav_home' => 1,
			'nav_twitter_text' => 'Follow me on Twitter',
			'subnav_home' => 1,
			'theme_version' => '1.0.1'
		);
	
		$settings = wp_parse_args($new_settings, $theme_settings);
		update_option(GENESIS_SETTINGS_FIELD, $settings);
	
	}
	
	#########################
#	UPGRADE TO VERSION 1.0.2
	#########################

	// Check to see if we need to upgrade to 1.0.2
	if(version_compare(genesis_get_option('theme_version'), '1.0.2', '<')) {

		$theme_settings = get_option(GENESIS_SETTINGS_FIELD);
		$new_settings = array(
			'theme_version' => '1.0.2'
		);

		$settings = wp_parse_args($new_settings, $theme_settings);
		update_option(GENESIS_SETTINGS_FIELD, $settings);

	}
	
}