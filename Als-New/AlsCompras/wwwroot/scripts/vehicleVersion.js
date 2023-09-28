function verifyForm() {

    let option1 = $('#option1').is(":checked");
    let option2 = $('#option2').is(":checked");

    console.log('version', option1);
    if (option1 || option2) {

        if (option1) {
            localStorage.setItem('vehicleVersion', $('#option1').val());
        } else {
            localStorage.setItem('vehicleVersion', $('#option2').val());
        }


        $('#formSelectColor').submit();
    }
    else {
        $('#divMsgError').removeClass('d-none');
    }
}
