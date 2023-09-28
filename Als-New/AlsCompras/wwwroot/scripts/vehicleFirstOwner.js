function verifyForm1() {
    let eProprietario = $("input[type=radio][name=vehicleOption1]:checked");
    let eImportado = $("input[type=radio][name=vehicleOption2]:checked");

    let valid = true;

    if (eProprietario.length == 0) {
        $('#divMsgError1').removeClass('d-none');
        valid = false;
    }
    if (eImportado.length == 0) {
        $('#divMsgError2').removeClass('d-none');
        valid = false;
    }

    if (valid) {

        localStorage.setItem('eProprietario', eProprietario.val());
        localStorage.setItem('eImportado', eImportado.val());

        $('#formMaintenanceDocs').submit();
    }

}






 //Minimal onclick info in html - Create a JS file for ErrorMsg
 // $(".btn-check").bind("click", function () {alert('olá')})



/*function verifyForm1() {

    let option1 = $('#option1').is(":checked");
    let option2 = $('#option2').is(":checked");
    let option3 = $('#option3').is(":checked");
    let option4 = $('#option4').is(":checked");
            
    if (!option1 || !option2) {

        $('#divMsgError1').removeClass('d-none');

    }
    if (!option3 || !option4) {

        $('#divMsgError2').removeClass('d-none');

    }


    $('#formMaintenanceDocs').submit(function (e) {


        e.preventDefault();

        if ((option1 || option2) && (option3 || option4)) {
            //localStorage.setItem('firstOwner', $('#option1' || '#option2').val() && $('#option3' || '#option4').val());
            localStorage.setItem('firstOwner', $('#option1' || '#option2').val());
            localStorage.setItem('firstOwner', $('#option3' || '#option4').val());
            this.submit();

        }
            
        return;
    });

}*/


