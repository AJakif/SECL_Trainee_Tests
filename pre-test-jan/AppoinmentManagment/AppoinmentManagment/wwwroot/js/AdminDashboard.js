$(document).ready(function () {
    //Patient count Api
    $.ajax({
        type: 'GET',
        url: '/CountPatient',
        datatype: 'JSON',
        success: function (response) {
            let patient = response.patient;
            document.getElementById('patientCount').innerHTML = patient;
        }
    });

    //Doctor Count Api
    $.ajax({
        type: 'GET',
        url: '/CountDoctor',
        datatype: 'JSON',
        success: function (response) {
            let doctor = response.doctor;
            document.getElementById('doctorCount').innerHTML = doctor;
        }
    });

    //Doctor Count Api
    $.ajax({
        type: 'GET',
        url: '/CountSpecialization',
        datatype: 'JSON',
        success: function (response) {
            let doctor = response.special;
            document.getElementById('specializationCount').innerHTML = doctor;
        }
    });

    $.ajax({
        type: 'GET',
        url: '/Balance',
        datatype: 'JSON',
        success: function (response) {
            let balance = response.balance;
            document.getElementById('balance').innerHTML = "$"+balance;
        }
    });
});