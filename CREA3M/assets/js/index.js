function LoginSuccess(data) {
    if (data.status == 'success') {
        window.location = 'Home/index'
    } else {
        toastr.warning('Estas credeciales no existen en el servidor seleccionado!');
    }

}
function LoginFailure(data) {
    
}


setTimeout(function () {

    let data = new FormData()
    data.append('captcha', 'string')
    data.append('status', 'string_two')

    $.ajax({
        type: "POST",
        url: "/login/captcha",
        data: data,
        dataType: "JSON",
        contentType: false,
        processData: false,
        timeout: 10000,
        success: function (response) {
            console.log(response)
        }
    });
}, 1000)