function verifyDocumentForm() {
    let possuiFatura = $("input[type=radio][name=docsOption1]:checked");
    let possuiLivro = $("input[type=radio][name=docsOption2]:checked");

    let valid = true;
    
    if (possuiFatura.length == 0) {

        $('#divMsgError1').removeClass('d-none');
        valid = false;
    }

    if (possuiLivro.length == 0) {

        $('#divMsgError2').removeClass('d-none');
        valid = false;
    }

    if (valid) {

        localStorage.setItem('possuiFatura', possuiFatura.val());
        localStorage.setItem('possuiLivro', possuiLivro.val());

        $('#formVehiclePhotos').submit();
    }

    
}




/*function verifyForm() {

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

    
    $('#formVehiclePhotos').submit(function (e) {
               
        e.preventDefault();

        if ((option1 || option2) && (option3 || option4)) {
            localStorage.setItem('MaintenanceDocs', $('#option1' || '#option2' || '#option3' || '#option4').val());
            this.submit();
        }

        return;
    });

}*/