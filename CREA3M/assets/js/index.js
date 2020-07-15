function LoginSuccess(data) {
    if (data.status == 'success') {
        window.location = '/Home/index'
    } else if (data.status == 'captcha') {
        window.location = '/Login/Forbidden'
    } else {
        toastr.warning('Estas credeciales no existen en el servidor seleccionado!');
    }

}

grecaptcha.ready(function () {
    grecaptcha.execute('6Lf3xqsZAAAAAJzlBCq4aCK0EfPZeXX7f3jvV1vt', { action: 'homepage' }).then(function (token) {
        $('#token').val(token)
    });
});

$(document).ready(function () {
    const urlParams = new URLSearchParams(window.location.search);

    toastr.options = {
        timeOut: 3000,
        progressBar: true,
        showMethod: "slideDown",
        hideMethod: "slideUp",
        showDuration: 200,
        hideDuration: 200
    };

    if (urlParams.get('loggedout') == 1) toastr.success('Has cerrado tu sesión!');
    if (urlParams.get('nosession') == 1) toastr.warning('Debes iniciar sesión primero!');

    window.history.replaceState({}, document.title, "/Login/Index");

})