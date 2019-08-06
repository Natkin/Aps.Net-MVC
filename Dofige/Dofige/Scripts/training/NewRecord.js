$(document).ready(function () {
    $('.data').datepicker();
    $("#accordion").accordion({ header: "h3", collapsible: true, active: false });
});

$(function () {
    $("#accordion").accordion();
});

var counter = 0;
$(document.body).on('click', "#rtList a", function (e) {

    var current = $(this).closest('.adding');
    var temp = current.find($('.temp_bl'));
    var items = current.find('.items');
    var data_block = temp.find('.data_bl');

    var cat_id = temp.find('input[id="category"]').val();
    var hidden_cat = temp.find('input[id="category"]');
    var sub_cat_id = $(this).data('linkid');
    var amount_input = temp.find('input[id=summ]');
    var curr_data = data_block.find('input').val();


    if ((items).is(':empty')) {
        current = current;
    }
    else {
        counter--;
        items.empty();
    }
    var new_amount_input = '<input type="text" class="form-control" data-val-number="The field amount must be a number." data-val-required="Требуется поле amount."' +
    'id="summ" value="' + amount_input.val() + '" name="Finance[' + counter + '].amount" placeholder="' + $(this).text() + '" >';
    amount_input.replaceWith(new_amount_input);

    var fields = '<input data-list="1" value="' + cat_id + '" type="hidden" id="Finance_' + counter + '__Category_CatID" name="Finance[' + counter + '].Category.CatID"/>' +
    '<input type="hidden" value="' + sub_cat_id + '" id="Finance_' + counter + '__Category_Subcategory_SubCatID" name="Finance[' + counter + '].Category.Subcategory.SubCatID"/>';

    var dates = '<div id="data_bl[' + counter + ']" class="input-group data_bl">' +
                        '<input value="' + curr_data + '" type="text" name="Finance[' + counter + '].data" placeholder="Date" id="data[' + counter + ']" class="data form-control">' +
                    '</div>';
    var add_butt = '<span class="input-group-btn"><button style="background-color:none" class="btn btn-primary" id="add" type="button"><span class="glyphicon glyphicon-repeat" aria-hidden="true"></span></button></span>';
    var valid = '<span class="field-validation-valid" data-valmsg-for="Finance[' + counter + '].amount" data-valmsg-replace="true"></span>'

    temp.empty();
    temp.append(hidden_cat);
    temp.append(new_amount_input);
    temp.append(dates);
    temp.append(add_butt);
    items.append(fields);
    current.append(valid);
    counter++;
    temp.find('.data').datepicker();
});


$(document.body).on('click', "#add_sub_cat", function (e) {
    var a = confirm("Все изменения будут утеряны, продожить?");
    if (a == true) {
        var selval = $('.ddl option:selected').val();
        $.ajax({
            type: 'get',
            url: $('#listpath').data('urllist_subcat'),
            data: { 'id': selval, name: $('#new_sub_cat_name').val() },
            success: function (data) {
                window.location.reload();
            },
            error: function (ex, textStatus, errorThrown) {
                alert('Помилка доступу до сервера ' + textStatus + '  ' + errorThrown, 'danger', false);
            }
        });
    }
});

$(document.body).on('click', "#add_cat", function (e) {
    var selval = $('input[id="type"]:checked').val();
    var a = confirm("Все изменения будут утеряны, продожить?");
    if (a == true) {

        $.ajax({
            type: 'get',
            url: $('#listpath').data('urllist_cat'),
            data: { 'id': selval, name: $('#new_cat_name').val() },
            success: function (data) {
                window.location.reload();
            },
            error: function (ex, textStatus, errorThrown) {
                alert('Помилка доступу до сервера ' + textStatus + '  ' + errorThrown, 'danger', false);
            }
        });
    }
});

$(document.body).on('click', "#add", function (e) {
    var current = $(this).closest('.adding');
    var temp = current.find($('.temp_bl'));
    var items = current.find('.items');
    var cat_id = temp.find('input[id="category"]').val();
    var curr = current.find('input[id=current_counter]').val();
    var dd = current.find('input[id=current_date]').val();

    var default_value = '<input type="hidden" value="' + cat_id + '" id="category">' +
    '<input type="text" id="summ" class="form-control">' +
    '<div id="data_bl[' + curr + ']" class="input-group data_bl">' +
        '<input type="text" value="'+dd+'" placeholder="дата" id="data['+curr+']" class="data form-control hasDatepicker"></div>' +
    '<span class="input-group-btn">' +
        '<button style="background-color:none" class="btn btn-primary" id="add" type="button"><span class="glyphicon glyphicon-repeat" aria-hidden="true"></span></button>' +
    '</span>';

    temp.empty();
    items.empty();
    temp.append(default_value);
    counter--;
});