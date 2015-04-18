<?php
require_once(TEMPLATEPATH.'/lib/init.php');
include('description_widget.php');

// Add support for custom background
if (function_exists('add_custom_background')) {
	add_custom_background();
}

//** Remove Site description (available in a widget)
//**************************************************************************//
remove_action('genesis_site_description', 'genesis_seo_site_description');


//** Setting up the Header Image
//**************************************************************************//
add_action('genesis_after_header', 'show_header_image', 5); 
function show_header_image() {
	global $post;
	if ( is_singular() &&
			has_post_thumbnail( $post->ID ) &&
			( /* $src, $width, $height */ $image = wp_get_attachment_image_src( get_post_thumbnail_id( $post->ID ), 'post-thumbnail' ) ) &&
			$image[1] >= HEADER_IMAGE_WIDTH ) :
		// Houston, we have a new header image!
		echo '<div id="header-image">';
		echo get_the_post_thumbnail( $post->ID, 'post-thumbnail' );
		echo '</div>';
	else : ?>
		<div id="header-image"><img src="<?php header_image(); ?>" width="<?php echo HEADER_IMAGE_WIDTH; ?>" height="<?php echo HEADER_IMAGE_HEIGHT; ?>" alt="" /></div>
	<?php endif;
}

define( 'HEADER_TEXTCOLOR', '' );
define( 'NO_HEADER_TEXT', true );
define( 'HEADER_IMAGE', CHILD_URL . '/images/headers/path.jpg' );
define( 'HEADER_IMAGE_WIDTH', apply_filters( 'twentyten_header_image_width', 940 ) );
define( 'HEADER_IMAGE_HEIGHT', apply_filters( 'twentyten_header_image_height', 198 ) );

set_post_thumbnail_size( HEADER_IMAGE_WIDTH, HEADER_IMAGE_HEIGHT, true );

add_custom_image_header( '', 'twentyten_admin_header_style' );

function twentyten_admin_header_style() { ?>
	<style type="text/css">
	#headimg {
		border-bottom: 1px solid #000;
		border-top: 4px solid #000;
	}
	</style>
<?php
}

register_default_headers( array(
	'berries' => array(
		'url' => CHILD_URL . '/images/headers/berries.jpg',
		'thumbnail_url' => CHILD_URL . '/images/headers/berries-thumbnail.jpg',
		/* translators: header image description */
		'description' => __( 'Berries', 'twentyten' )
	),
	'cherryblossom' => array(
		'url' => CHILD_URL . 'images/headers/cherryblossoms.jpg',
		'thumbnail_url' => CHILD_URL . '/images/headers/cherryblossoms-thumbnail.jpg',
		/* translators: header image description */
		'description' => __( 'Cherry Blossoms', 'twentyten' )
	),
	'concave' => array(
		'url' => CHILD_URL . '/images/headers/concave.jpg',
		'thumbnail_url' => CHILD_URL. '/images/headers/concave-thumbnail.jpg',
		/* translators: header image description */
		'description' => __( 'Concave', 'twentyten' )
	),
	'fern' => array(
		'url' => CHILD_URL . '/images/headers/fern.jpg',
		'thumbnail_url' => CHILD_URL . '/images/headers/fern-thumbnail.jpg',
		/* translators: header image description */
		'description' => __( 'Fern', 'twentyten' )
	),
	'forestfloor' => array(
		'url' => CHILD_URL . '/images/headers/forestfloor.jpg',
		'thumbnail_url' => CHILD_URL . '/images/headers/forestfloor-thumbnail.jpg',
		/* translators: header image description */
		'description' => __( 'Forest Floor', 'twentyten' )
	),
	'inkwell' => array(
		'url' => CHILD_URL . '/images/headers/inkwell.jpg',
		'thumbnail_url' => CHILD_URL . '/images/headers/inkwell-thumbnail.jpg',
		/* translators: header image description */
		'description' => __( 'Inkwell', 'twentyten' )
	),
	'path' => array(
		'url' => CHILD_URL . '/images/headers/path.jpg',
		'thumbnail_url' => CHILD_URL . '/images/headers/path-thumbnail.jpg',
		/* translators: header image description */
		'description' => __( 'Path', 'twentyten' )
	),
	'sunset' => array(
		'url' => CHILD_URL . '/images/headers/sunset.jpg',
		'thumbnail_url' => CHILD_URL . '/images/headers/sunset-thumbnail.jpg',
		/* translators: header image description */
		'description' => __( 'Sunset', 'twentyten' )
	)
) );

//** Post Info and Meta tweaks
//**************************************************************************//
add_filter('genesis_post_info', 'post_info_filter');
function post_info_filter($post_info) {
if (!is_page()) {
    $post_info = 'Posted on [post_date] by [post_author_posts_link] [post_edit]';
    return $post_info;
}}

add_filter('genesis_post_meta', 'post_meta_filter');
function post_meta_filter($post_meta) {
if (!is_page()) {
    $post_meta = '[post_categories sep=", " before="Posted in "] [post_tags sep=", " before="| Tagged "] | [post_comments]';
    return $post_meta;
}}

remove_action('genesis_after_post', 'genesis_do_author_box');
add_action('genesis_after_post_content', 'custom_genesis_do_author_box', 1);
function custom_genesis_do_author_box(){
	if ( is_single() && genesis_get_option( 'author_box_single' ) ) {
		genesis_author_box();
		return;
	}
}

//** Footer Tweaks
//**************************************************************************//
genesis_register_sidebar(array(
	'name'=>'Footer #1',
	'id'=>'footer-1',
	'description' => 'This is the first column of the footer section.',
	'before_title'=>'<h4 class="widgettitle">','after_title'=>'</h4>'
));
genesis_register_sidebar(array(
	'name'=>'Footer #2',
	'id'=>'footer-2',
	'description' => 'This is the second column of the footer section.',
	'before_title'=>'<h4 class="widgettitle">','after_title'=>'</h4>'
));
genesis_register_sidebar(array(
	'name'=>'Footer #3',
	'id'=>'footer-3',
	'description' => 'This is the third column of the footer section.',
	'before_title'=>'<h4 class="widgettitle">','after_title'=>'</h4>'
));
genesis_register_sidebar(array(
	'name'=>'Footer #4',
	'id'=>'footer-4',
	'description' => 'This is the fourth column of the footer section.',
	'before_title'=>'<h4 class="widgettitle">','after_title'=>'</h4>'
));

add_action('genesis_footer', 'genesis_footer_widgets', 6);
function genesis_footer_widgets() {
	get_template_part( 'footer-widgets' );
}


add_filter('genesis_footer_backtotop_text', 'custom_footer_backtotop_text');
function custom_footer_backtotop_text($backtotop) {
	$sitename = get_bloginfo('name');
	$siteurl = get_bloginfo('url');
    $backtotop = "<a href=\"$siteurl\">$sitename</a>";
    return $backtotop;
}

add_filter('genesis_footer_creds_text', 'custom_footer_creds_text');
function custom_footer_creds_text($creds) {
    $creds = '<a href="http://wordpress.org" class="wordpress">Proudly powered by WordPress</a> + <a href="http://www.jaredatchison.com/go/genesis/">Genesis Framework</a>';
    return $creds;
}