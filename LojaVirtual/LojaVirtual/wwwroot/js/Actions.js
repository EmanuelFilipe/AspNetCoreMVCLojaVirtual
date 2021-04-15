$(document).ready(function () {
	$('#btn-excluir').on('click', function (e) {
		e.preventDefault();
		ExcluirRegistro(parseInt(document.getElementById('id-registro').value));
	});

	$('.dinheiro').mask('000.000.000.000.000,00', { reverse: true });

	AjaxUploadImagemProduto();
});

function AjaxUploadImagemProduto() {
	$(".img-upload").click(function () {
		//this = a propria imagem... parent é o pai da imagem... 
		//e procura pela classe ".input-file"...o ponto é pq é uma classe css
		$(this).parent().find(".input-file").click();
	});

	$(".btn-imagem-excluir").click(function () {

		var txt_imagem = $(this).parent().find("input[name=imagem]");
		var imagem = $(this).parent().find(".img-upload");
		var btnExcluir = $(this).parent().find(".btn-imagem-excluir");

		// precisa limpar o input de file, pois se selecionar o mesmo arquivo que foi excluído ela não será carregada
		var inputFile = $(this).parent().find(".input-file");

		$.ajax({
			type: "GET",
			url: "/Colaborador/Imagem/Excluir?path=" + txt_imagem.val(),
			success: function () {
				imagem.attr("src", "/img/imagem-padrao.png");
				btnExcluir.addClass("btn-ocultar");
				inputFile.val("");
			},
			error: function () {
				alert("Erro no envio do arquivo");
			}
		});
	});

	//é acionado toda vez que o usuário selecionar um arquivo
	$(".input-file").change(function () {
		var binary = $(this)[0].files[0]; // pegando o valor binário do arquivo selecionado
		var formulario = new FormData();

		var txt_imagem = $(this).parent().find("input[name=imagem]");
		var imagem = $(this).parent().find(".img-upload");
		var btnExcluir = $(this).parent().find(".btn-imagem-excluir");

		//file é o mesmo nome que a action de Armazenar recebe, os nomes devem ser os mesmos.
		formulario.append("file", binary);

		$.ajax({
			type: 'POST',
			url: "/Colaborador/Imagem/Armazenar",
			data: formulario,
			contentType: false,
			processData: false, //responsavel pór validar os dados do formulário
			success: function (data) {
				txt_imagem.val(data.path);
				imagem.attr("src", data.path);
				btnExcluir.removeClass("btn-ocultar");
			},
			error: function () {
				alert("Erro no envio do arquivo");
			}
		});
	});
}