<?php if (is_single()) { ?>
<?php include(CHILD_DIR."/includesposts.php");?>
<?php } ?><br />
<?php if (is_page()) { ?>
<?php include(CHILD_DIR."/includespages.php");?>
<?php } ?>
