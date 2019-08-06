$(document).ready(function () {

});
$(function () {
    $("#datepicker").datepicker();
    $("#BeginPeriod").datepicker();
    $("#EndPeriod").datepicker();
});

$(document.body).on('change', '.ddl', function () {
    $("#progresfile").show();
    var selval = $('.ddl option:selected');
    $.ajax({
        type: 'get',
        url: $('#listpath').data('urllist'),
        data: { 'id': $(this).val() },
        success: function (data) {
            $('.subbl').html(data);
            $("#progresfile").hide();
        },
        error: function (ex, textStatus, errorThrown) {
            $("#progresfile").hide();
            alert('Помилка доступу до сервера ' + textStatus + '  ' + errorThrown, 'danger', false);
        }
    });
});


function Get_Filter() {
    var FilterModel = [];
    FilterModel =
        {
            start_data: $("#BeginPeriod").val(),
            end_data: $("#EndPeriod").val(),
            typeID: $('.ddl_type option:selected').val(),
            current_page: $(".pagination").find('.active').find('a').text(),
            subcatid: $('.sub_ddl option:selected').val(),
            catid: $('.ddl_cat option:selected').val()
        };
    return FilterModel;
}
$(document.body).on('click', '#filter_butt', function () {
    $('table').empty();
    $.ajax({
        type: 'get',
        url: $('#listpath').data('urllist_main'),
        data: { 'filter': JSON.stringify(Get_Filter()) },
        success: function (data) {
            $('table').append(data).html();
        },
        error: function (ex, textStatus, errorThrown) {
            alert('Помилка доступу до сервера ' + textStatus + '  ' + errorThrown, 'danger', false);
        }
    });
});
$(document.body).on('click', '.pagination a', function () {
    $('li').removeClass('active');
    $(this).closest('li').addClass('active');
    $('table').empty();
    $.ajax({
        type: 'get',
        url: $('#listpath').data('urllist_main'),
        data: { 'filter': JSON.stringify(Get_Filter()) },
        success: function (data) {
            $('table').append(data).html();
        },
        error: function (ex, textStatus, errorThrown) {
            alert('Помилка доступу до сервера ' + textStatus + '  ' + errorThrown, 'danger', false);
        }
    });
});

//var counter = 0;
//var c = 1;
//$(document.body).on('change', '.sub_ddl', function () {
//    var selval = $('.sub_ddl option:selected').val();
//    var seltex = $('.sub_ddl option:selected').text();
//    var ddl_selval = $('.ddl option:selected').val();

//    if (counter > 5) {
//        alert("Only 5 textboxes are allowed");
//        return false;
//    }
//    var newTextBoxDiv = $(document.createElement('div'))
//         .attr("id", 'TextBoxDiv' + counter);

//    newTextBoxDiv.after().html(
//     '<input type="text" class="form-control"' +
//     'id="textbox' + counter + '" value="" name="Finance[' + counter + '].amount" placeholder="' + seltex + '" >' +
//     '<input value="' + ddl_selval + '" type="hidden" id="Finance_' + counter + '__Category_CatID" name="Finance[' + counter + '].Category.CatID"/>'+
//     '<input type="hidden" value="' + selval + '" id="Finance_' + counter + '__Category_Subcategory_SubCatID" name="Finance[' + counter + '].Category.Subcategory.SubCatID"/>');
//    newTextBoxDiv.appendTo("#items");
//    counter++;
//    c++;
//});
