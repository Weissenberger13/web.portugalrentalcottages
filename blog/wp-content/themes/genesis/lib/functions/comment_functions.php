<?php
/**
 * @todo document this file
 */

/**
 * This function outputs the comment list to the <code>genesis_comment_list()</code> hook
 *
 * @since 1.0
 */
add_action('genesis_list_comments', 'genesis_default_list_comments');
function genesis_default_list_comments() {
	$args = array(
		'type' => 'comment',
		'avatar_size' => 48,
		'callback' => 'genesis_comment_callback'
	);
	
	$args = apply_filters('genesis_comment_list_args', $args);
	
	wp_list_comments($args);
}

/**
 * This function outputs the pint list to the <code>genesis_ping_list()</code> hook
 * 
 * @since 1.0
 */
add_action('genesis_list_pings', 'genesis_default_list_pings');
function genesis_default_list_pings() {
	$args = array(
		'type' => 'pings'
	);
	
	$args = apply_filters('genesis_ping_list_args', $args);
	
	wp_list_comments($args);
}

/**
 * This function is the comment callback for <code>genesis_default_comment_list()</code>
 * 
 * @since 1.0
 */
function genesis_comment_callback( $comment, $args, $depth ) {
	$GLOBALS['comment'] = $comment; ?>
	
	<li <?php comment_class(); ?> id="comment-<?php comment_ID() ?>">
			
		<div class="comment-author vcard">
			<?php echo get_avatar( $comment, $size = $args['avatar_size'] ); ?>
			<?php printf(__('<cite class="fn">%s</cite> <span class="says">says:</span>', 'genesis'), get_comment_author_link()); ?>
     	</div><!-- end .comment-author -->

		<div class="comment-meta commentmetadata">
			<a href="<?php echo esc_attr( get_comment_link( $comment->comment_ID ) ); ?>"><?php printf(__('%1$s at %2$s', 'genesis'), get_comment_date(),  get_comment_time()); ?></a>
			<?php edit_comment_link(__('Edit', 'genesis'), '&bull; ', ''); ?>
		</div><!-- end .comment-meta -->

		<div class="comment-content">
			<?php if ($comment->comment_approved == '0') : ?>
				<p class="alert"><?php _e('Your comment is awaiting moderation.', 'genesis'); ?></p>
			<?php endif; ?>
			
			<?php comment_text(); ?>
		</div><!-- end .comment-content -->

		<div class="reply">
			<?php comment_reply_link(array_merge( $args, array('depth' => $depth, 'max_depth' => $args['max_depth']))); ?>
		</div>
		
		<?php genesis_comment(); ?>
		
	<?php // no ending </li> tag because of comment threading
}

/**
 * This function defines the comment form, hooked to <code>genesis_comments_form()</code>
 *
 * @since 1.0
 */
add_action('genesis_comments_form', 'genesis_default_comments_form');
function genesis_default_comments_form() {
?>
<form action="<?php bloginfo('wpurl') ?>/wp-comments-post.php" method="post" id="commentform">
	
<div class="cancel-comment-reply">
	<?php cancel_comment_reply_link(); ?>
</div>

<?php if ( $user_ID ) : ?>

<p><?php _e('Logged in as', 'genesis'); ?> <a href="<?php bloginfo('wpurl') ?>/wp-admin/profile.php"><?php echo $user_identity; ?></a>. <a href="<?php echo wp_logout_url(get_permalink()); ?>" title="Log out of this account"><?php _e('Log out', 'genesis'); ?> &raquo;</a></p>

<?php else : ?>

<div id="authorinput" class="commentinput"><input type="text" name="author" id="author" value="<?php echo $comment_author; ?>" tabindex="1" <?php if ($req) echo "aria-required='true'"; ?> />
<label for="author"><?php _e('Name', 'genesis'); ?> <?php if ($req) echo '<span class="req">*</span>'; ?></label></div>

<div id="emailinput" class="commentinput"><input type="text" name="email" id="email" value="<?php echo $comment_author_email; ?>" tabindex="2" <?php if ($req) echo "aria-required='true'"; ?> />
<label for="email"><?php _e('Email', 'genesis'); ?> <?php if ($req) echo '<span class="req">*</span>'; ?></label></div>

<div id="urlinput" class="commentinput"><input type="text" name="url" id="url" value="<?php echo $comment_author_url; ?>" tabindex="3" />
<label for="url"><?php _e('Website', 'genesis'); ?></label></div>

<?php endif; // endif user is logged in ?>

<textarea name="comment" id="comment" tabindex="4"></textarea>

<button type="submit" name="submit" id="submit" class="button" tabindex="5"><?php _e('Submit Comment', 'genesis'); ?></button>
<?php comment_id_fields(); ?>
<?php do_action('comment_form', $post->ID); ?>

</form>
<?php	
}