$(document).ready(function () {
	MoverScrollOrdenacao();
	MudarOrdenacao();
});

function MoverScrollOrdenacao() {
	if (window.location.hash.length > 0) {
		var hash = window.location.hash;

		if (hash == "#posicao-produto") {
			window.scrollBy(0, 475);
		}
	}
}

function MudarOrdenacao() {
	$("#ordenacao").change(function () {
		var pagina = 1;
		var pesquisa = "";
		var ordenacao = $(this).val();

		var queryString = new URLSearchParams(window.location.search);

		if (queryString.has("pagina")) {
			pagina = queryString.get("pagina");
		}

		if (queryString.has("pesquisa")) {
			pesquisa = queryString.get("pesquisa");
		}

		var URL = window.location.protocol + "//" + window.location.host + window.location.pathname;
		var URLParametros = URL + "?pagina=" + pagina + "&pesquisa=" + pesquisa + "&ordenacao=" + ordenacao + "#posicao-produto";

		window.location.href = URLParametros;
	});
}