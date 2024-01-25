function verifyForm1() {
    
    let valid = true;

    let estadoGeral = $("input[type=radio][name=vehicleOption1]:checked");
    let estadoInterior = $("input[type=radio][name=vehicleOption2]:checked");
    let estadoMecanico = $("input[type=radio][name=vehicleOption3]:checked");

    if (estadoGeral.length == 0) {
        $('#divMsgError1').removeClass('d-none');
        valid = false;
    }
    if (estadoInterior.length == 0) {
        $('#divMsgError2').removeClass('d-none');
        valid = false;
    }
    if (estadoMecanico.length == 0) {
        $('#divMsgError3').removeClass('d-none');
        valid = false;
    }

    if (valid) {

        localStorage.setItem('estadoGeral', estadoGeral.val());
        localStorage.setItem('estadoInterior', estadoInterior.val());
        localStorage.setItem('estadoMecanico', estadoMecanico.val());

        $('#formSelectStatus').submit();
    }
}




/*function verifyForm1() {
      
    let option1 = $('#option1').is(":checked");
    let option2 = $('#option2').is(":checked");
    let option3 = $('#option3').is(":checked");
    let option4 = $('#option4').is(":checked");
    let option5 = $('#option5').is(":checked");
    let option6 = $('#option6').is(":checked");
    let option7 = $('#option7').is(":checked");
    let option8 = $('#option8').is(":checked");
    let option9 = $('#option9').is(":checked");

    
    if (!option1 || !option2 || !option3) {
       
        $('#divMsgError1').removeClass('d-none');

    }
    if (!option4 || !option5 || !option6) {

        $('#divMsgError2').removeClass('d-none');

    }    
    if (!option7 || !option8 || !option9) {

        $('#divMsgError3').removeClass('d-none');

    }        
    
    $('#formSelectDanos').submit(function (e) {

        
        e.preventDefault();
        
        if ((option1 || option2 || option3) && (option4 || option5 || option6) && (option7 || option8 || option9)) {
            localStorage.setItem('vehicleStatus', $('#option1' || '#option2' || '#option3').val() && $('#option4' || '#option5' || '#option6').val() && $('#option4' || '#option5' || '#option6').val());
            this.submit();
        }
       
        return;
        
    });

}*/