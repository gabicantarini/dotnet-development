$('#imgRight').click(() => {

    $('#divImageRight').removeClass('d-none');

});

$('#imgLeft').click(() => {

    $('#divImageLeft').removeClass('d-none');

});

$('#imgFront').click(() => {

    $('#divImageFront').removeClass('d-none');

});

$('#imgBack').click(() => {

    $('#divImageBack').removeClass('d-none');

});

$('#imgAbove').click(() => {

    $('#divImageAbove').removeClass('d-none');

});



function verifyForm() {
  
    let valid = false;

    let imgRight = $("input[type=file][name=fileImgRigth]").val();
    let imgLeft = $("input[type=file][name=fileImgLeft]").val();
  

    if (imgRight.length == 0) {
        $('#divMsgError1').removeClass('d-none');
        valid = true;
    }
    if (imgLeft.length == 0) {
        $('#divMsgError2').removeClass('d-none');
        valid = true;
    }

 
    if (!valid) {
   
        $('#frmDamage').submit();
    }

   /* $('#imgRight').bind('change', function (e) {
        var data = e.originalEvent.target.files[0];
        // Exibe o tamanho no console
        console.log(data.size + "is my file's size");
        // Ou faz a validação
        if (data.size > 500 * 1024) {
            console.log(data)
        }
    }*/
}



/*// Esconder e mostrar os inputs quando clica na imagem
function showDiv(targetId) {
    
    let a = $(`#${targetId}`)
    console.log(targetId, a)

    if (a.hasClass('d-none')) {
        a.removeClass('d-none')
    } else {
        a.addClass('d-none')
    }
}

// Chamar a função pra verificar erro toda vez que adicionar um arquivo
let inputs = document.querySelectorAll('input[type=file]')
inputs.forEach(element => {
    element.addEventListener('change', verifyImg)
});

// verificar se o arquivo tem menos de 4mb
function verifyImg(e) {
    var size = e.target.files[0].size;
    if (size > 4194304) {
        // Muito grande
        e.target. // input
            parentNode. // divImage
            parentNode. // div pai da divImage
            lastElementChild. // div Error
            classList.remove('d-none') // lista de classes da div Error
        e.target.value = ""; //Limpa o campo          
    }
    e.preventDefault()
}

function verifyForm(e) {
    

    let valid = true

    const semDanos = $("#defaultCheck1:checked")
    if (semDanos.length != 0) {
        $('#frmDamage').submit();
    } else {

        // validar se as imagens todas existem??        
        // let imgRight = $("input[type=file][name=fileImgRight]").val();
        // let imgLeft = $("input[type=file][name=fileImgLeft]").val();
        // let imgFront = $("input[type=file][name=fileImgFront]").val();
        // let imgBack = $("input[type=file][name=fileImgBack]").val();
        // let imgAbove = $("input[type=file][name=fileImgAbove]").val();


        // colocar os dados convertidos no localstorage
        // localStorage.setItem('imgRight', imgRightConvertida)
        // localStorage.setItem('imgLeft', imgLeftConvertida)
        // localStorage.setItem('imgFront', imgFrontConvertida)
        // localStorage.setItem('imgBack', imgBackConvertida)
        // localStorage.setItem('imgAbove', imgAboveConvertida)

                
        $('#frmDamage').submit();
    }
}

*/

