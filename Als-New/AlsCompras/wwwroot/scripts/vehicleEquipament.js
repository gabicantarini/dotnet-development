function setVehicleEquipament(vehicleEquipament) {

    localStorage.setItem('vehicleEquipamentGlobal', vehicleEquipament);
    localStorage.setItem('vehicleEquipament', vehicleEquipament);
    $('#divMsgError').addClass('d-none');
}


$('#formSelectKilometer').submit(function (e) {
    
    e.preventDefault();    
    let vehicleEquipamentGlobal = localStorage.getItem('vehicleEquipamentGlobal')
    if (vehicleEquipamentGlobal) {
        localStorage.removeItem('vehicleEquipamentGlobal')
        this.submit();
    }
    else {
        $('#divMsgError').removeClass('d-none');
    }    
    return;
});

$(document).ready(function () {
    localStorage.removeItem('vehicleEquipamentGlobal')
});





//function verifyFormEquipament(itens) {
//
//    let item = $('#' + itens).is(":checked");
//
//    if (item) {
//        $('#formKilometer').submit();
//    }
//
//    else {
//        $('#divMsgError').removeClass('d-none');
//    }
//}
