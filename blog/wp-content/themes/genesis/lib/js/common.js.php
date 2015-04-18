<?php header("Content-Type: text/javascript"); ?>
jQuery(document).ready(function($) { 
	$('#nav ul, #subnav ul, #header ul.nav').superfish({ 
		delay:       100,								// .5 second delay on mouseout 
		animation:   {opacity:'show',height:'show'},	// fade-in and slide-down animation 
		dropShadows: false								// disable drop shadows 
	});

	$('form.searchform input.s').focus(function() {
		if( $(this).val() == 'Search this website...' ) {
			$(this).val('');
		}
	});
	$('form.searchform input.s').blur(function() {
		if( $(this).val() == '' ) {
			$(this).val('Search this website...');
		}
	});
	
});