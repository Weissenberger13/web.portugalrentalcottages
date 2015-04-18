<?php
/*
 * @todo Document this file
 */

function genesis_purchase_themes_admin() { ?>
	
<div class="wrap purchase-themes">
	<?php screen_icon('themes'); ?>
	<h2><?php echo 'Genesis '; _e('- Purchase Themes', 'genesis'); ?></h2>
	
	<?php
		$store = get_transient('genesis-remote-store');
		
		if(!$store) {
			$request = new WP_Http;
			$store = $request->request('http://www.studiopress.com/store.php');
			set_transient('genesis-remote-store', $store, 60*60*12); // store for 12 hours
		}
		
		echo $store['body'];
	?>
	
</div>
<?php }