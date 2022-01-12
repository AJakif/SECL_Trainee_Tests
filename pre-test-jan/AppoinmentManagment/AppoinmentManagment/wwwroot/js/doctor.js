$(document).ready(function () {
    $('table .prescription').on('click', function () {
        var id = $(this).data("id");

        $('#Record #id').val(id);
    })

});

function Visit(url) {
    $.ajax({
        type: 'POST',
        url: url,
        success: function (data) {
            if (data.success) {
                toastr["success"](data.message)
                window.location.reload();
            }
            else {
                toastr["error"](data.message);
            }
        }
    });
}

function Patient(url) {
    document.Alert("Hello")
        //$.ajax({
        //    type: 'POST',
        //    url: url,
        //    success: function (data) {
        //        console.log(data);
        //        if (data.success) {
        //            swal({
        //                title: "Appointment Details",
        //                html: 'All safe! Here is the answer from the tool: ' + data['answer'],
        //                showCancelButton: true
        //            })
        //        }
        //        else {
        //            toastr["error"](data.message);
        //        }
        //    }
        //});
};


function Details(url) {

    $.ajax({
        type: 'POST',
        url: url,
        success: function (data) {
            if (data.success) {
                swal({
                    title: "Appointment Details",
                    html: 'All safe! Here is the answer from the tool: ' + data['answer'],
                    showCancelButton: true
                })
            }
            else {
                toastr["error"](data.message);
            }
        }
    });
};
