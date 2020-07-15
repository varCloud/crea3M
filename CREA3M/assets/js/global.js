$(document).ready(function () {

    $.ajax({
        type: "GET",
        url: "/Session/Validate",
        dataType: "json",
        success: function (response) {
            if (response.status == 'invalid')
                window.location = "/Login/Index?nosession=1"
        }
    });

    $('#logout').click(() => {
        $.ajax({
            type: "GET",
            url: "/Session/Logout",
            dataType: "json",
            success: function (response) {
                if(response.status == "success")
                    window.location = "/Login/Index?loggedout=1"
            }
        });
    })
})