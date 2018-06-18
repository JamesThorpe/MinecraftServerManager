$(document).on('click', '[data-dialog]', function() {
    $.get($(this).attr('data-dialog'), (html) => {
        $('#dialog')
            .html(html)
            .modal({
                backdrop: 'static'
            }).modal('show');
    });
});