window.addEventListener('load', function () {

    Array.prototype.filter.call(document.getElementsByClassName('needs-validation'), function (form) {
        form.addEventListener('submit', function (event) {
            event.preventDefault();
            event.stopPropagation();

            if (form.checkValidity() === true) {

                let data = new FormData(form)

                $.ajax({
                    type: "POST",
                    url: "/",
                    data: data,
                    dataType: "json",
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        console.log('lo que sea')
                        if (response.status == 'valido')
                            window.location.href = '/Home/Index'
                    }
                });
            }
            form.classList.add('was-validated');
        }, false);
    });

}, false);