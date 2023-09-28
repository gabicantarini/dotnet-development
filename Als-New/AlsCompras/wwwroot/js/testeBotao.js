document.getElementsByClassName("teste_btn").addEventListener("click", proximapagina)

function proximapagina() {
    if (event.key === "teste_btn") {
        window.location.href = "SelecionaVersao.cshtml";
    }
}