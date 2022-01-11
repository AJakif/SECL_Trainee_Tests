$(document).ready(function () {
    $('table .edit').on('click', function () {
        var id = $(this).data("id");

        $.ajax({
            type: 'POST',
            url: '/payment',
            data: { "id": id },
            success: function (response) {
                $('#EditRecord #amount').val(response.fee);
                $('#EditRecord #id').val(response.appointment);

            }
        })
    });
});