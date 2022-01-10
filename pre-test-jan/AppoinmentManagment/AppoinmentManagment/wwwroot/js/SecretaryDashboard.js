$(document).ready(function () {
    //Patient count Api
    $.ajax({
        type: 'GET',
        url: '/CountPendingAppointment',
        datatype: 'JSON',
        success: function (response) {
            console.log(response);
            let appointment = response.appointment;
            console.log(appointment);
            document.getElementById('appoinmentCount').innerHTML = appointment;
        }
    });
});

function Decline(url) {
    swal({
        title: "Are you sure you want to decline appoinment?",
        text: "You will not be able to restore !",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, Decline it",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'POST',
            url: url,
            success: function (data) {
                if (data.success) {
                    window.location.reload();
                    toastr["success"](data.message);
                    
                }
                else {
                    toastr["error"](data.message);
                }
            }
        });
    });
}

function Approve(url) {
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


