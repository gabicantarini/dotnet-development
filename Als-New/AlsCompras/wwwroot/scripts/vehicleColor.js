function setVehicleColor(vehicleColor) {
    
    localStorage.setItem('vehicleColorGlobal', vehicleColor);
    localStorage.setItem('vehicleColor', vehicleColor);
    $('#divMsgError').addClass('d-none');
} 

$('#formSelectEquipament').submit(function (e) {
    
    e.preventDefault();
    console.log('teste');
    let vehicleColorGlobal = localStorage.getItem('vehicleColorGlobal')
    if (vehicleColorGlobal) {
        localStorage.removeItem('vehicleColorGlobal')
        this.submit();
    }
    else {
        $('#divMsgError').removeClass('d-none');
    }
    
    return;

});

$(document).ready(function () {
    localStorage.removeItem('vehicleColorGlobal')
});






//function verifyFormColor(itens) {

//    debugger;
//    let item = $('#' + itens).is(":checked");

//    if (item) {
//        $('#formSelectEquipament').submit();
//    }

//    else {
//        $('#divMsgError').removeClass('d-none');
//    }
//}