$(document).ready(function () {
	$('#btn-excluir').on('click', function (e) {
		e.preventDefault();
		ExcluirRegistro(parseInt(document.getElementById('id-registro').value));
	});

	$('.dinheiro').mask('000.000.000.000.000,00', { reverse: true });
});