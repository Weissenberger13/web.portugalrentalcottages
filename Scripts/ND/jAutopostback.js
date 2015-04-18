//this autopostsback any element with the data-autopostback="true" attribute

$(document).ready(function ($) {
    $("select:[data-autopostback=true]").change(function () {
        $(this).closest("form").submit();
    });
});