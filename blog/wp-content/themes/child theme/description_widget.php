<?php
/**
 * new WordPress Widget format
 * Wordpress 2.8 and above
 * @see http://codex.wordpress.org/Widgets_API#Developing_Widgets
 */
class site_description_Widget extends WP_Widget {
	
    /**
     * Constructor
     *
     * @return void
     **/
	function site_description_Widget() {
		$widget_ops = array( 'classname' => 'sitedescription', 'description' => 'Displays Site Description' );
		$this->WP_Widget( 'sitedescription', 'Site Description', $widget_ops );
	}

    /**
     * Outputs the HTML for this widget.
     *
     * @param array  An array of standard parameters for widgets in this theme 
     * @param array  An array of settings for this widget instance 
     * @return void Echoes it's output
     **/
	function widget( $args, $instance ) {
		echo '<p class="description">' . get_bloginfo('description') . '</p>';
	}

    /**
     * Deals with the settings when they are saved by the admin. Here is
     * where any validation should be dealt with.
     *
     * @param array  An array of new settings as submitted by the admin
     * @param array  An array of the previous settings 
     * @return array The validated and (if necessary) amended settings
     **/
	function update( $new_instance, $old_instance ) {
		// Nothing to update :)
		// $updated_instance = $new_instance;
		// return $updated_instance;
	}

    /**
     * Displays the form for this widget on the Widgets page of the WP Admin area.
     *
     * @param array  An array of the current settings for this widget
     * @return void Echoes it's output
     **/
	function form( $instance ) {
		// No form needed for this widget!
		//$instance = wp_parse_args( (array) $instance, array( array of option_name => value pairs ) );
		// display field names here using:
		// $this->get_field_id('option_name') - the CSS ID
		// $this->get_field_name('option_name') - the HTML name
		// $instance['option_name'] - the option value
	}
}

add_action( 'widgets_init', create_function( '', "register_widget('site_description_Widget');" ) );	
?>