// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(document).ready( function () {
    $('#table').DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
} );


$(document).ready(function () {
    $("#btnCriarProjeto").click( () => {
        $("#modalCriarProjeto").modal('show');
        limparModal();
        $(".projetoId").val(0);
    });

});


function limparModal() {
    $('.name').val('');
    $('.data-incio').val('');
    $('.data-entrega').val('');
    $('.descricao').val('');
    $('.valor').val('');
};

$(".btn--novo-projeto").click((projeto) => {
    var novoProjeto = {
        Id: $(".projetoId").val(),
        Name: $('.name').val(),
        Status: $('.status').val(),
        DataInicio: $('.data-inicio').val(),
        DataEntrega: $('.data-entrega').val(),
        Descricao: $('.descricao').val(),
        Valor: $('.valor').val()

    };
    criarProjeto(novoProjeto);
});


function criarProjeto(novoProjeto) {
    $.ajax({
        type: 'POST', 
        url: '/Project/Create',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(novoProjeto),
        success: function(){
            $("#modalCriarProjeto").modal('hide');
            window.location.reload();
        }
    }); 
}
    
$('.btn-atualizar').click(function(){
    var projetoId = $(this).attr('projeto-id');
    pegarPeloId(projetoId);
})

function pegarPeloId(projetoId)
{
    console.log(pegarPeloId);
     $.ajax ({
         type: 'GET',
         url: `/Project/Update?id=${projetoId}`,
         success : function(projeto) {
             $("#modalAtualizarProjeto").modal('show');
             $('.projetoId').val(projetoId);
             $('.name').val(projetoId.Name);
             $('.status').val(projetoId.Status);
             $('.data-incio').val(projetoId.DataInicio);
             $('.data-entrega').val(projetoId.DataEntrega);
             $('.descricao').val(projetoId.Descricao);
             $('.valor').val(projetoId.Valor);
         }
     });
}