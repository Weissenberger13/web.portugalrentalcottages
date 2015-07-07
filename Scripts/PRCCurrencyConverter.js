
//Disable currently selected currency
$(document).ready(function () {

    var currencyCurrency = window.CURRENT_CURRENCY; //THIS NEEDS TO BE SET IN LAYOUT
    var systemDefaultCurrency = window.DEFAULT_CURRENCY;
    // $("#currencyConverter option[value='" + currencyCurrency + "']").selected();


    $("#currencyConverter").val(currencyCurrency);

    //$("#currencyConverter option[value='" + currencyCurrency + "']").remove();

   
    $('#currencyConverter').on('change', function () {

        var newCurrency = $("#currencyConverter").val();
        console.log(currencyCurrency);
        console.log(systemDefaultCurrency);

        //get the choice
     
        //get the current currency
      

        var exchangeRatePattern = systemDefaultCurrency + '-' + newCurrency;

        //get price in GBP from original price

       

       /* if (newCurrency == "GBP") {*/

       if (newCurrency == systemDefaultCurrency) {

            $("*[id*='convertedCurrency']").each(function (i, el) {
                $(this).hide();
            });

        }
        else {
            if (convertCurrencyAjaxCall(exchangeRatePattern)) {

                //set global
                window.CURRENT_CURRENCY = newCurrency;
                //set dropdown to last choice
                $("#currencyConverter").val(newCurrency);
            }
        }


    });

});





function convertCurrencyAjaxCall(exchangeRatePattern) {
    //make ajax call to convert
    $.ajax({
        url: '/CurrencyConverter/GetExchangeRate',
        data: { "exchangeRatePattern": exchangeRatePattern },
        type: "GET",
        success: function (result) {




            $("*[id*='originalCurrency']").each(function (i, el) {

              

                var value = $('#' + el.id).text();


                //get the number
                var original = parseFloat(value).toFixed(2);

                //get the ID to change


                var matchingConverterID = 'convertedCurrency' + el.id.replace('originalCurrency', '');

              

                $('#' + matchingConverterID).text(exchangeRatePattern.split('-')[1] + ' ' + parseFloat(original * result).toFixed(2));
                $('#' + matchingConverterID).show();
                $('#' + matchingConverterID).css('color', '#ff3333');


            });


            /*          alert($('#originalCurrency0').text());

                      //change booking price
                      var original = parseFloat($('#originalCurrency0').text()).toFixed(2);

                      alert(original * result);

                      $('#convertedCurrency0').text(exchangeRatePattern.split('-')[1] + ' ' + original * result);
                      $('#convertedCurrency0').show();

                      alert('converted to:' +   $('#convertedCurrency0').text());*/


            //change extras price

            return true;


        },
        error: function () { alert('That did not work please try again'); }
    });




}


