using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace icarus.projetos.models.ValueObject
{
    public class StatusProjeto
    {
        public string Status { get; private set; }

        protected StatusProjeto() {}
        public StatusProjeto(string status)
        {
            ValidarStatus(status);
            Status = status;
        }

        void ValidarStatus(string status)
        {
            if(string.IsNullOrWhiteSpace(status)) throw new Exception("O status não pode estar nulo!");
            if (!Regex.IsMatch(Status, @"^[a-zA-Z ]+$")) throw new Exception("A rua não pode conter caracteres especiais");
        }

        public void AtualizacaoDoStatus(string novoStatus)
        {
            this.Status = novoStatus;
        }
    }
}