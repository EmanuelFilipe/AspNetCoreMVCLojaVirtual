$(document).ready(function () {
	$('#btn-excluir-categoria').on('click', function (e) {
		e.preventDefault();
		ExcluirCategoria(parseInt(document.getElementById('id-ip').value));
	});
});