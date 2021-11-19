$(document).ready(function () {
	MoverScrollOrdenacao();
	MudarOrdenacao();
	MudarImagemPrincipalProduto();
	MudarQuantidadeProdutoCarrinho();
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
		var fragmento = "#posicao-produto";

		var queryString = new URLSearchParams(window.location.search);

		if (queryString.has("pagina")) {
			pagina = queryString.get("pagina");
		}

		if (queryString.has("pesquisa")) {
			pesquisa = queryString.get("pesquisa");
		}

		if ($("#breadcrumb").length > 0) {
			fragmento = "";
		}

		var URL = window.location.protocol + "//" + window.location.host + window.location.pathname;
		var URLParametros = URL + "?pagina=" + pagina + "&pesquisa=" + pesquisa + "&ordenacao=" + ordenacao + "#posicao-produto";

		window.location.href = URLParametros;
	});
}

function MudarImagemPrincipalProduto() {
	$(".img-small-wrap img").click(function () {
		var caminho = $(this).attr("src");
		$(".img-big-wrap img").attr("src", caminho);
		$(".img-big-wrap a").attr("href", caminho);
	});

}

function numberToReal(numero) {
	var numero = numero.toFixed(2).split('.');
	numero[0] = 'R$ ' + numero[0].split(/(?=(?:...)*$)/).join('.');

	return numero.join(',')
}

function MudarQuantidadeProdutoCarrinho() {
	$("#order .btn-primary").click(function () {
		if ($(this).hasClass("diminuir")) {
			OrquestradorDeAcoesProduto("diminuir", $(this))
		}
		if ($(this).hasClass("aumentar")) {
			OrquestradorDeAcoesProduto("aumentar", $(this))
		}
	});
}

function OrquestradorDeAcoesProduto(operacao, botao) {
	// Carregamento dos valores
	var pai = botao.parent().parent();
	var produtoId = pai.find(".inputProdutoId").val();
	var quantidadeEstoque = parseInt(pai.find(".inputQuantidadeEstoque").val());
	var valorUnitario = parseFloat(pai.find(".inputValorUnitario").val().replace(",", "."));
	var campoQuantidadeProdutoCarrinho = pai.find(".inputQuantidadeProdutoCarrinho");
	var quantidadeProdutoCarrinho = campoQuantidadeProdutoCarrinho.val();
	var campoValor = botao.parent().parent().parent().parent().parent().find(".price");

	var produto = new ProdutoQuantidadeEValor(produtoId, quantidadeEstoque, valorUnitario, quantidadeProdutoCarrinho,
		                                      0, campoQuantidadeProdutoCarrinho, campoValor);

	// Chamada de métodos
	AlteracoesVisuaisProdutoCarrinho(produto, operacao);
}

function AlteracoesVisuaisProdutoCarrinho(produto, operacao) {
	if (operacao == "aumentar") {
		if (produto.quantidadeProdutoCarrinhoAntiga == produto.quantidadeEstoque)
			alert('Opps! Não possuímos estoque suficiente para a quantidade que vc deseja comprar!')
		else {
			produto.quantidadeProdutoCarrinhoNova = parseInt(produto.quantidadeProdutoCarrinhoAntiga) + 1;
			AtualizarQuantidadeEValor(produto);
		}
	}
	else if (operacao == "diminuir") {
		if (produto.quantidadeProdutoCarrinhoAntiga == 1)
			alert('Opps! Caso não deseje este produto clique no botão Remover')
		else {
			produto.quantidadeProdutoCarrinhoNova = parseInt(produto.quantidadeProdutoCarrinhoAntiga) - 1;
			AtualizarQuantidadeEValor(produto);
		}
	}
}

function AtualizarQuantidadeEValor(produto) {
	debugger
	produto.campoQuantidadeProdutoCarrinho.val(produto.quantidadeProdutoCarrinhoNova);

	var resultado = produto.valorUnitario * produto.quantidadeProdutoCarrinhoNova;
	produto.campoValor.text(numberToReal(resultado));
}

/* CLASSES */
class ProdutoQuantidadeEValor {
	constructor(produtoId, quantidadeEstoque, valorUnitario, quantidadeProdutoCarrinhoAntiga, quantidadeProdutoCarrinhoNova,
		campoQuantidadeProdutoCarrinho, campoValor) {
		this.produtoId = produtoId;
		this.quantidadeEstoque = quantidadeEstoque;
		this.valorUnitario = valorUnitario;
		this.quantidadeProdutoCarrinhoAntiga = quantidadeProdutoCarrinhoAntiga;
		this.quantidadeProdutoCarrinhoNova = quantidadeProdutoCarrinhoNova;
		this.campoQuantidadeProdutoCarrinho = campoQuantidadeProdutoCarrinho;
		this.campoValor = campoValor;
	}
}