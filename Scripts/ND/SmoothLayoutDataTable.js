

function SmoothLayoutDatatableHeight(elementToTable,elementToResize, heightPercentInt) {
    $('#' + elementToResize).animate({ height: heightPercentInt + '%' }, 500);
};
