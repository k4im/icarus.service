﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using icarus.fornecedores.Data;

#nullable disable

namespace icarus.fornecedores.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("icarus.fornecedores.Models.Fornecedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.HasKey("Id");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("icarus.fornecedores.Models.Fornecedor", b =>
                {
                    b.OwnsOne("icarus.fornecedores.Models.ValueObjects.CadastroNacionalPessoaJurídica", "Cnpj", b1 =>
                        {
                            b1.Property<int>("FornecedorId")
                                .HasColumnType("int");

                            b1.Property<string>("Cnpj")
                                .HasColumnType("longtext")
                                .HasColumnName("CNPJ");

                            b1.HasKey("FornecedorId");

                            b1.ToTable("Fornecedores");

                            b1.WithOwner()
                                .HasForeignKey("FornecedorId");
                        });

                    b.OwnsOne("icarus.fornecedores.Models.ValueObjects.Endereco", "Endereco", b1 =>
                        {
                            b1.Property<int>("FornecedorId")
                                .HasColumnType("int");

                            b1.Property<string>("Bairro")
                                .HasColumnType("longtext")
                                .HasColumnName("Bairro");

                            b1.Property<string>("Cep")
                                .HasColumnType("longtext")
                                .HasColumnName("Cep");

                            b1.Property<string>("Cidade")
                                .HasColumnType("longtext")
                                .HasColumnName("Cidade");

                            b1.Property<int>("Numero")
                                .HasColumnType("int")
                                .HasColumnName("NumeroEndereco");

                            b1.Property<string>("Rua")
                                .HasColumnType("longtext")
                                .HasColumnName("Rua");

                            b1.HasKey("FornecedorId");

                            b1.ToTable("Fornecedores");

                            b1.WithOwner()
                                .HasForeignKey("FornecedorId");
                        });

                    b.OwnsOne("icarus.fornecedores.Models.ValueObjects.Telefone", "Telefone", b1 =>
                        {
                            b1.Property<int>("FornecedorId")
                                .HasColumnType("int");

                            b1.Property<string>("CodigoDeArea")
                                .HasColumnType("longtext")
                                .HasColumnName("CodigoDeArea");

                            b1.Property<string>("CodigoPais")
                                .HasColumnType("longtext")
                                .HasColumnName("CodigoDoPais");

                            b1.Property<string>("Numero")
                                .HasColumnType("longtext")
                                .HasColumnName("NumeroTelefone");

                            b1.HasKey("FornecedorId");

                            b1.ToTable("Fornecedores");

                            b1.WithOwner()
                                .HasForeignKey("FornecedorId");
                        });

                    b.Navigation("Cnpj")
                        .IsRequired();

                    b.Navigation("Endereco");

                    b.Navigation("Telefone")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
