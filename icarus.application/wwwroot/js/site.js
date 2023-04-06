// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// função para paginação da tabela, lib utilizada DataTables
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

// =========================== 
// Função de limpeza de modal |
// =========================== 
function limparModal() {
    $('.name').val('');
    $('.data-incio').val('');
    $('.data-entrega').val('');
    $('.descricao').val('');
    $('.valor').val('');
    $(".validacaoName").text("");
};

// =========================== 
// validar form              |
// =========================== 
function validarForm(novoProjeto) {
    if(novoProjeto.Name.length < 1) $(".validacaoName").text("O projeto precisa conter um nome!");
    if(novoProjeto.Name.length > 1) $(".validacaoName").text("");
    if(novoProjeto.DataInicio.length < 1) $(".validacaoDataInicio").text("O projeto precisa conter uma data de incio!");
    if(novoProjeto.DataInicio.length > 1) $(".validacaoDataInicio").text("");
    if(novoProjeto.DataEntrega.length < 1) $(".validacaoDataEntrega").text("O projeto precisa conter uma data de Entrega!");
    if(novoProjeto.DataEntrega.length > 1) $(".validacaoDataEntrega").text("");
    if(novoProjeto.Valor.length < 1) $(".validacaoValor").text("O projeto precisa conter um Valor!");
    if(novoProjeto.Valor.length > 1) $(".validacaoValor").text("");
    if(novoProjeto.Name.length > 1 && novoProjeto.DataInicio.length > 1 && novoProjeto.DataEntrega.length > 1 && novoProjeto.Valor.length > 1) return true;
    
};

// ============================= 
// Função de criar novo projeto |
// ============================= 
function criarProjeto(novoProjeto) {
   // Requisição ajax para criar um novo projeto
    $.ajax({
        type: 'POST', 
        url: '/Project/Create', // Rota do controlador
        contentType: "application/json; charset=utf-8", // Definindo o tipo do conteudo
        data: JSON.stringify(novoProjeto), // transformando o objecto recebido em json.
        success: function(){
            $("#modalCriarProjeto").modal('hide'); // fechando a modal
            window.location.reload(); // atualizando a pagina
        }
    }); 
}




//=============================================================== 
//Requisição Ajax para o controlador recuperar o projeto pelo id|
//=============================================================== 
function pegarPeloId(projetoId)
{
    limparModal();
    // Requisição ajax para recuperar os dados e apresentar na modal
    $.ajax ({
         type: 'GET',
         url: `/Project/Update?id=${projetoId}`, // rota do controlador
         success : function(result) {
            $("#modalAtualizarProjeto").modal('show'); // abrindo a modal
            $("#modal-body").html(result);
         }
     });
}


// ============================================================================= 
// Abrindo a modal para criar um novo projeto e setando o valor do id para zero|
// ============================================================================= 
$(document).ready(function () {
    $("#btnCriarProjeto").click( () => {
        $("#modalCriarProjeto").modal('show'); // Abrindo a modal e adicionado a classe show
        limparModal(); // limpando o campo da modal
        $(".projetoId").val(0); // setando o valor do input ProjetoId para zero
    });

});


// ================================================= 
// Recuperando click na modal para criar o projeto |
// ================================================= 
$(document).ready(function(){
    $(".btn--novo-projeto").click((projeto) => {
        // Criação de novo objecto em js para mapear com o objeto do controlador
        var novoProjeto = {
            Id: $(".projetoId").val(),
            Name: $('.name').val(),
            Status: $('.status').val(),
            DataInicio: $('.data-inicio').val(),
            DataEntrega: $('.data-entrega').val(),
            Descricao: $('.descricao').val(),
            Valor: $('.valor').val()
    
        };
        if(validarForm(novoProjeto)) criarProjeto(novoProjeto); // chamando função que cria o projeto
    });
});

// =============================
// Recuperação de id do projeto |   
// ============================= 
$(document).ready(function(projeto){
    $('#table, tbody').on("click", ".btn-atualizar", function(){
        var projetoId = $(this).attr('projeto-id'); // Setando a variavel com o valor do data attr.
        pegarPeloId(projetoId); // Chamando a função para popular o modal
    });    
});


// ========================= 
// Delete recuperando click |
// ========================= 

$(document).ready(function() {
    $("#table tbody").on("click", ".btn--delete", function(){
        $("#modalDelete").modal('show');
        var id = $(this).attr('projeto-id');
        $("#btn-modal-delete").click(function(){
            $.ajax ({
                type: 'POST',
                url: `/Project/Delete?id=${id}`, // rota do controlador
                success : function(result) {
                    window.location.reload(); // atualizando a pagina
                }
            });
        });
    });
});
