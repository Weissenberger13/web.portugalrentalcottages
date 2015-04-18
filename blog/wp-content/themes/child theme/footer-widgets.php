<?php
/* The footer widget area is triggered if any of the areas
 * have widgets. So let's check that first.
 *
 * If none of the sidebars have widgets, then let's bail early.
 */
if (   ! is_active_sidebar( 'Footer #1'  )
	&& ! is_active_sidebar( 'Footer #2' )
	&& ! is_active_sidebar( 'Footer #3'  )
	&& ! is_active_sidebar( 'Footer $4' )
)
	return;
// If we get this far, we have widgets. Let do this.

?>

<div id="footer-widget-area">
	<?php if ( is_active_sidebar( 'Footer #1' ) ) : ?>
		<div id="first" class="widget-area">
				<?php dynamic_sidebar( 'Footer #1' ); ?>
		</div><!-- #first .widget-area -->
	<?php endif; ?>

	<?php if ( is_active_sidebar( 'Footer #2' ) ) : ?>
		<div id="second" class="widget-area">
				<?php dynamic_sidebar( 'Footer #2' ); ?>
		</div><!-- #second .widget-area -->
	<?php endif; ?>

	<?php if ( is_active_sidebar( 'Footer #3' ) ) : ?>
		<div id="third" class="widget-area">
				<?php dynamic_sidebar( 'Footer #3' ); ?>
		</div><!-- #third .widget-area -->
	<?php endif; ?>

	<?php if ( is_active_sidebar( 'Footer #4' ) ) : ?>
		<div id="fourth" class="widget-area">
				<?php dynamic_sidebar( 'Footer #4' ); ?>
		</div><!-- #fourth .widget-area -->
	<?php endif; ?>
	
</div>