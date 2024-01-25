(function (window, undefined) {

    "use strict";

    var $ = window.jQuery;
    var document = window.document;

    // Nome do parâmetro de SPHostUrl
    var SPHostUrlKey = "SPHostUrl";

    // Obtém o SPHostUrl da URL atual e o acrescenta como uma sequência de consulta para os links que apontam para o domínio atual na página.
    $(document).ready(function () {
        ensureSPHasRedirectedToSharePointRemoved();

        var spHostUrl = getSPHostUrlFromQueryString(window.location.search);
        var currentAuthority = getAuthorityFromUrl(window.location.href).toUpperCase();

        if (spHostUrl && currentAuthority) {
            appendSPHostUrlToLinks(spHostUrl, currentAuthority);
        }
    });

    // Acrescenta o SPHostUrl como uma sequência de consulta a todos os links que apontam para um domínio atual.
    function appendSPHostUrlToLinks(spHostUrl, currentAuthority) {
        $("a")
            .filter(function () {
                var authority = getAuthorityFromUrl(this.href);
                if (!authority && /^#|:/.test(this.href)) {
                    // Filtra as âncoras e as URLs com outros protocolos sem suporte.
                    return false;
                }
                return authority.toUpperCase() == currentAuthority;
            })
            .each(function () {
                if (!getSPHostUrlFromQueryString(this.search)) {
                    if (this.search.length > 0) {
                        this.search += "&" + SPHostUrlKey + "=" + spHostUrl;
                    }
                    else {
                        this.search = "?" + SPHostUrlKey + "=" + spHostUrl;
                    }
                }
            });
    }

    // Obtém o SPHostUrl da sequência de consulta fornecida.
    function getSPHostUrlFromQueryString(queryString) {
        if (queryString) {
            if (queryString[0] === "?") {
                queryString = queryString.substring(1);
            }

            var keyValuePairArray = queryString.split("&");

            for (var i = 0; i < keyValuePairArray.length; i++) {
                var currentKeyValuePair = keyValuePairArray[i].split("=");

                if (currentKeyValuePair.length > 1 && currentKeyValuePair[0] == SPHostUrlKey) {
                    return currentKeyValuePair[1];
                }
            }
        }

        return null;
    }

    // Obtém autoridade da URL fornecida quando for uma URL absoluta com protocolo http/https ou uma URL relativa de protocolo.
    function getAuthorityFromUrl(url) {
        if (url) {
            var match = /^(?:https:\/\/|http:\/\/|\/\/)([^\/\?#]+)(?:\/|#|$|\?)/i.exec(url);
            if (match) {
                return match[1];
            }
        }
        return null;
    }

    // Se SPHasRedirectedToSharePoint existe na sequência de consulta, remova-o.
    // Consequentemente, quando o usuário indica a URL, SPHasRedirectedToSharePoint não é incluído.
    // Observe que modificar window.location.search causará uma solicitação adicional ao servidor.
    function ensureSPHasRedirectedToSharePointRemoved() {
        var SPHasRedirectedToSharePointParam = "&SPHasRedirectedToSharePoint=1";

        var queryString = window.location.search;

        if (queryString.indexOf(SPHasRedirectedToSharePointParam) >= 0) {
            window.location.search = queryString.replace(SPHasRedirectedToSharePointParam, "");
        }
    }

})(window);
