using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using icarus.projetos.models.ValueObject;

namespace icarus.projetos.models
{
    public class Project
    {


        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("NVARCHAR(25)")]
        public string Nome { get; private set; }
        
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("NVARCHAR(25)")]
        public StatusProjeto Status { get; private set; }        

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("DATE")]
        public DateTime DataInicio { get; private set; }  

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("DATE")]
        public DateTime DataEntrega { get; private set; } 

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType("NVARCHAR(150)")]
        public ChapaUtilizada Chapa { get; private set; } 

        [DataType("NVARCHAR(150)")]
        public DescricaoProjeto Descricao { get; private set; } 

        [Required(ErrorMessage = "Campo obrigatório")]
        public QuantidadeChapaUtilizada QuantidadeDeChapa { get; private set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public ValorProjeto Valor { get; private set; }
    
        [Timestamp]
        public byte[] RowVersion { get; set; }
      
        protected Project()
        {}
        public Project(string nome, StatusProjeto status, 
        DateTime dataInicio, DateTime dataEntrega, 
        ChapaUtilizada chapa, DescricaoProjeto descricao, 
        QuantidadeChapaUtilizada quantidadeDeChapa, ValorProjeto valor)
        {
            Nome = nome;
            Status = status;
            DataInicio = dataInicio;
            DataEntrega = dataEntrega;
            Chapa = chapa;
            Descricao = descricao;
            QuantidadeDeChapa = quantidadeDeChapa;
            Valor = valor;
        }



        public void atualizarStatus(StatusProjeto novoStatus)
        {
            this.Status.AtualizacaoDoStatus(novoStatus.Status);
        }

    }
}