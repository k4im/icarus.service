using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using icarus.jwtManager.Models.ValueObjects;

namespace icarus.jwtManager.Models
{
    public class UsuarioDTO
    {
        public UsuarioDTO(Nome nome,
        string senha,
        CadastroPessoasFisica cpf, 
        Endereco endereco, 
        Telefone telefone, 
        Perfilamento role)
        {
            Nome = nome;
            Senha = senha;
            Cpf = cpf;
            Endereco = endereco;
            Telefone = telefone;
            Role = role;
        }

        public Nome Nome { get;}
        public string Senha { get;}
        public CadastroPessoasFisica Cpf { get;}
        public Endereco Endereco { get;}
        public Telefone Telefone { get;}
        public Perfilamento Role { get;}

    }
}