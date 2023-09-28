
$(document).ready(function () {
    localStorage.setItem('fileImgRigth', $('#fileImgRigth').val());
});

$(document).ready(function () {
    localStorage.setItem('fileImgLeft', $('#fileImgLeft').val());
});

$(document).ready(function () {
    localStorage.setItem('fileImgFront', $('#fileImgFront').val());
});

$(document).ready(function () {
    localStorage.setItem('fileImgBack', $('#fileImgBack').val());
});

$(document).ready(function () {
    localStorage.setItem('fileImgAbove', $('#fileImgAbove').val());
});


function verifyForm() {
    
    let patternTel = (/^(\d{3})[.,]?(\d{3})[.,]?(\d{3})*$/);

    let valid = false;

    let clientName = $("input[type=text][name=name]").val();
    let clientMail = $("input[type=email][name=mail]").val();
    let clientPhone = $("input[type=tel][name=phone]").val();
    let clientZipCode = $("input[type=text][name=zipCode]").val();
    let newVehicle = $("input[type=radio][name=flexRadioDefault]:checked");

    if (clientName == '') {
        $('#divMsgError1').removeClass('d-none');
        valid = true;
    }
    if (clientMail == '') {
        $('#divMsgError2').removeClass('d-none');
        valid = true;
    }
    if (!patternTel.test(clientPhone)) {
        $('#divMsgError3').removeClass('d-none');
        valid = true;
    }
    if (clientZipCode == '') {
        $('#divMsgError4').removeClass('d-none');
        valid = true;
    }
    if (newVehicle.length == 0) {
        $('#divMsgError5').removeClass('d-none');
        valid = true;
    }


    if (!valid) {
        localStorage.setItem('clientName', clientName.value);
        localStorage.setItem('clientMail', clientMail.value);
        localStorage.setItem('clientPhone', clientPhone.value);
        localStorage.setItem('clientZipCode', clientZipCode.value);
        localStorage.setItem('newVehicle', newVehicle.val());

        $('#formOffer').submit();

    }
}




/*function verifyForm() {

    
    let valid = true;

    let clientName = $("input[name=name]" == 1);
    let clientMail = $("input[name=mail]" == 1);
    let clientPhone = $("input[name=phone]" == 1);
    let clientZipCode = $("input[name=zipCode]" == 1);

    if (clientName.length == 0) {
        $('#divMsgError1').removeClass('d-none');
       
    }
    if (clientMail.length == 0) {
        $('#divMsgError2').removeClass('d-none');
        
    }
    if (clientPhone.length == 0) {
        $('#divMsgError3').removeClass('d-none');
        
    }
    if (clientZipCode.length == 0) {
        $('#divMsgError4').removeClass('d-none');
        
    }


    if (valid) {

        localStorage.setItem('clientName', clientName.val());
        localStorage.setItem('clientMail', clientMail.val());
        localStorage.setItem('clientPhone', clientPhone.val());
        localStorage.setItem('clientZipCode', clientZipCode.val());

        $('#formOffer').submit();
    }

}*/


/*$(document).ready(function () {
    
    if ($('#profileForm').length !== 0) {

        $("form[name='profileForm']").validate({

            rules: {

                name: "required",
                phone: "required",
                mail: {
                    required: true,
                    email: true
                },
                zipCode: {
                    required: true,
                    minlength: 7
                }
            },

            messages: {
                name: "Por favor escreva seu nome",
                phone: "Por favor escreva seu telefone",
                email: "Por favor escreva um email válido",
                zipCode: {
                    required: "Por favor escreva seu email",
                    minlength: "Seu código postal deve ter mais de 5 caracteres"
                },

            },

            submitHandler: function (form) {
                form.submit();
            }

        });

    }


});*/



/*const profileForm = document.querySelector("#formOffer");

profileForm.onsubmit = evento => {
    let name = document.querySelector('#clientName').value;
    console.log(name);

    if (name === ' ') {
        evento.preventDefault();
        document.getElementById("divMsgError1");
        alert('teste');
        return;
    }
}*/







