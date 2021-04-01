$(document).ready(function () {
	$('#btn-excluir').on('click', function (e) {
		e.preventDefault();
		ExcluirRegistro(parseInt(document.getElementById('id-ip').value));
	});
});