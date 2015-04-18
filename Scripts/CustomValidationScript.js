        $(document).ready(function () {
           

            // Parse date in dd/mm/yyyy format
            function parseDate(input) {
                // Split the date, divider is '/'
                var parts = input.match(/(\d+)/g);

                // Build date (months are 0-based)
                return new Date(parts[2], parts[1] - 1, parts[0]);
            }

            $.validator.addMethod("FutureDate",
            function (value, element) { return parseDate(value.replace("-", "/")) > new Date().getTime(); },
            "Date must be in the future.");

            $.validator.addMethod("FutureBookingFormEndDate",
      function (value, element) { return parseDate(value.replace("-", "/")) > parseDate($("#StartDate").val().replace("-", "/")) &&  parseDate(value.replace("-", "/")) > new Date().getTime(); },
       "Date must be later than start date.");

            $.validator.addMethod("FutureBookingForm1EndDate",
      function (value, element) { return parseDate(value.replace("-", "/")) > parseDate($("#StartDate1").val().replace("-", "/")) && parseDate(value.replace("-", "/")) > new Date().getTime(); },
      "Date must be later than start date.");


            // Replace the builtin US date validation with UK date validation
$.validator.addMethod(
    "date",
    function ( value, element ) {
        var bits = value.match( /([0-9]+)/gi ), str;
        if ( ! bits )
            return this.optional(element) || false;
        str = bits[ 1 ] + '/' + bits[ 0 ] + '/' + bits[ 2 ];
        return this.optional(element) || !/Invalid|NaN/.test(new Date( str ));
    },
    "Please enter a date in the format dd/mm/yyyy"
);

            $('#tourForm').removeAttr('novalidate');
            $('#tourForm').validate({
                rules:
                {
                    ExtraRentalDate: {required: true, date: true, FutureDate: true}
                },
                submitHandler: function (form) {

                    $("#spinner").show();
                    form.submit();

                },
            });

            $('#airportTransferForm').removeAttr('novalidate');
            $('#airportTransferForm').validate({
                rules:
                {
                    ExtraRentalDate: { required: true, date: true, FutureDate: true },
                },
                submitHandler: function (form) {
                    $("#spinner").show();
                    form.submit();

                },
            });

            $('#carForm').removeAttr('novalidate');
            $('#carForm').validate({
                rules:
                {
                    ExtraRentalDate: { required: true, date: true, FutureDate: true },
                    ExtraReturnDate: { required: true, date: true, FutureDate: true }
                },
                submitHandler: function (form) {
                    $("#spinner").show();
                    form.submit();

                },
            });

            $('#wineTourForm').removeAttr('novalidate');
            $('#wineTourForm').validate({
                rules:
                {
                    ExtraRentalDate: { required: true, date: true, FutureDate: true },
                },
                submitHandler: function (form) {
                    $("#spinner").show();
                    form.submit();

                },
            });

            $('#BookingForm').removeAttr('novalidate');
            $('#BookingForm').validate({
                rules:
                {
                    StartDate: { required: true, date: true, FutureDate: true},
                    EndDate: { required: true, date: true, FutureDate: true },
                },
                submitHandler: function (form) {
                    $("#spinner").show();
                    form.submit();
                },
            });

            $('#BookingForm1').removeAttr('novalidate');
            $('#BookingForm1').validate({
                rules:
                {
                    StartDate1: { required: true, date: true, FutureDate: true },
                    EndDate1: { required: true, date: true, FutureBookingForm1EndDate: true },
                },
                submitHandler: function(form) {
                    $("#spinner").show();
                    form.submit();

               },
            });


            $('#propertyBookingForm').removeAttr('novalidate');
            $('#propertyBookingForm').validate({
                rules:
                {
                    bookingModalStartDatePicker: { required: true, date: true, FutureDate: true },
                    bookingModalEndDatePicker: { required: true, date: true, FutureDate: true },
                },
                submitHandler: function (form) {
                    $("#spinner").show();
                    form.submit();

                },
            });


     



            $('.aFormToValidate').removeAttr('novalidate');
            $('.aFormToValidate').validate({
                rules: {
                    FirstName: {
                        required: true
                    },
                    LastName: {
                        required: true
                    },
                    Country: {
                        required: true,
                    },
                    Password1: {
                        required: true,
                        minlength: 6,
                        maxlength: 15
                    },
                    Confirm_Password: {
                        equalTo: "#Password1",
                        minlength: 6,
                        maxlength: 15
                    },
                    securityQuestion: {
                        required: true,
                    },
                },
                submitHandler: function(form) {
                    $("#spinner").show();
                    form.submit();
                },
                messages: {
                    FirstName: {
                        required: "Please specify your First Name"
                    },
                    LastName: {
                        required: "Please specify your Last Name",
                    },
                    Password: {
                        required: "This field is required.",
                        password: "Must contain letters and numbers",
                    },
                    Confirm_Password: {
                        required: "This field is required.",
                        password: "Does not match password"
                    }
                }
            });


            jQuery.validator.addClassRules("makeValid", {
                required: true
            });

            jQuery.validator.addClassRules("makeValidFuture", {
                required: true,
                FutureDate: true
            });


        });
