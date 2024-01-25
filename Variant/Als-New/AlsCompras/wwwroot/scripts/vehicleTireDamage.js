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


/*
function verifyForm2() {

    
    let valid = true;

    let pneusDianteiros = $("input[type=radio][name=vehicleOption1]:checked");
    let pneusTrasieiros = $("input[type=radio][name=vehicleOption2]:checked");

    if (pneusDianteiros.length == 0) {
        $('#divMsgError1').removeClass('d-none');
        valid = false;
    }
    if (pneusTrasieiros.length == 0) {
        $('#divMsgError2').removeClass('d-none');
        valid = false;
    }


    if (valid) {

        localStorage.setItem('pneusDianteiros', pneusDianteiros.val());
        localStorage.setItem('pneusTrasieiros', pneusTrasieiros.val());

        $('#formFirstOwner').submit();
    }

}*/








/*function verifyForm2() {

    let pneusDianteiros = $("input[type=radio][name=vehicleOption1]:checked" )
    let pneusTrasieiros = $("input[type=radio][name=vehicleOption2]:checked" )

    if (pneusDianteiros.length == 0) {
        $('#divMsgError1').removeClass('d-none');

    }
    if (pneusTrasieiros.length == 0) {
        $('#divMsgError2').removeClass('d-none');

    }

    $('#formFirstOwner').submit(function (e) {
        
        e.preventDefault();

        if ((pneusDianteiros.length == 1) && (pneusTrasieiros.length == 1)) {
            localStorage.setItem('tireDamage', `D: ${pneusDianteiros.val()} T: ${pneusTrasieiros.val()}`);
            this.submit();
        }

        return;
    });

}*/



/*function verifyForm2() {
      
    let option1 = $('#option1').is(":checked");
    let option2 = $('#option2').is(":checked");
    let option3 = $('#option3').is(":checked");
    let option4 = $('#option4').is(":checked");
    let option5 = $('#option5').is(":checked");
    let option6 = $('#option6').is(":checked");
   
       
    if (!option1 || !option2 || !option3) {
       
        $('#divMsgError1').removeClass('d-none');

    }
    if (!option4 || !option5 || !option6) {

        $('#divMsgError2').removeClass('d-none');

    }
    
    
    $('#formFirstOwner').submit(function (e) {

        
        e.preventDefault();
        
        if ((option1 || option2 || option3) && (option4 || option5 || option6)) {
           localStorage.setItem('tireDamage', $('#option1' || '#option2' || '#option3').val() && $('#option4' || '#option5' || '#option6').val());
            this.submit();
        }        

        return;
    });

}*/


/*function verifyForm2() {

    let option1 = $('.vehicleOption1').is(":checked");
    let option2 = $('.vehicleOption2').is(":checked");


    if (option1 == "") {

        $('#divMsgError1').removeClass('d-none');

    }
    if (option2 == "") {

        $('#divMsgError2').removeClass('d-none');

    }


    $('#formFirstOwner').submit(function (e) {


        e.preventDefault();

        if (option1) {

            localStorage.setItem('tireDamage', $('.vehicleOption1').val());

        }
            else {

            localStorage.setItem('tireDamage', $('.vehicleOption2').val());
        }
        this.submit();
        return;
    });

}*/