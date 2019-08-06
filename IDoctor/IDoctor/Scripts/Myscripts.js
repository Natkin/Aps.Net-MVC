
    //$('.multi-field-wrapper').each(function () {
    //    var $wrapper = $('.multi-fields', this);
    //    var x = 1;
    //    var max = 5;
    //    $(".add-field", $(this)).click(function (e) {
    //        if (x < max) {
    //            x++;
    //            $('.multi-field:first-child', $wrapper).clone(true).appendTo($wrapper).find('input').val('').focus().hide().show(500);
    //        }
    //        else if (x == max)
    //            alert("You cannot input more than 5 fields");
    //    });
    //    $('.multi-field .remove-field', $wrapper).click(function () {
    //        if ($('.multi-field', $wrapper).length > 1)
    //            $(this).parent('.multi-field').remove();
    //    });
    //});


    $(function () {
        $("#fields").tabs();
    });