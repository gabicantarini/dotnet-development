function goToStep2() {

    $value = $('#vehicleKilometers').val();
    if (/^(\d{3})[.,]?(\d{3})*$/.test($value)) {
        localStorage.setItem('kilometers', $('#vehicleKilometers').val());
        $('#formVehicleStatus').submit();

    } else {
        $('#divMsgError1').removeClass('d-none');

    }

}

