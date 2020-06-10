function LoginSuccess(data) {
    if (data.status == 'success') {
        window.location = 'Home/index'
    } else {
        toastr.warning('Estas credeciales no existen en el servidor seleccionado!');
    }

}
function LoginFailure(data) {
    
}

//grecaptcha.ready(function () {
//    grecaptcha.execute('6LcvquQUAAAAABYNncZkaz54wYGlkdZDucb4f1oS', { action: 'homepage' }).then(function (token) {
//        $.ajax({
//            type: "POST",
//            url: "DAO/captcha.php",
//            data: {
//                token
//            },
//            dataType: "JSON",
//            success: function (response) {
//                if (response.success != true) window.open('forbidden.html', "_self")
//            }
//        });
//    });
//});

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
        success: function (response) {
            console.log(response)
        }
    });
}, 1000)