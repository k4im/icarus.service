﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using icarus.projetos.data;

#nullable disable

namespace icarus.projetos.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230522182419_NovoCampo")]
    partial class NovoCampo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("icarus.projetos.models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataEntrega")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.HasKey("Id");

                    b.ToTable("Projetos");
                });

            modelBuilder.Entity("icarus.projetos.models.Project", b =>
                {
                    b.OwnsOne("icarus.projetos.models.ValueObject.ChapaUtilizada", "Chapa", b1 =>
                        {
                            b1.Property<int>("ProjectId")
                                .HasColumnType("int");

                            b1.Property<string>("Chapa")
                                .HasColumnType("longtext")
                                .HasColumnName("ChapaUtilizada");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Projetos");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.OwnsOne("icarus.projetos.models.ValueObject.DescricaoProjeto", "Descricao", b1 =>
                        {
                            b1.Property<int>("ProjectId")
                                .HasColumnType("int");

                            b1.Property<string>("Descricao")
                                .HasColumnType("longtext")
                                .HasColumnName("Descricao");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Projetos");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.OwnsOne("icarus.projetos.models.ValueObject.QuantidadeChapaUtilizada", "QuantidadeDeChapa", b1 =>
                        {
                            b1.Property<int>("ProjectId")
                                .HasColumnType("int");

                            b1.Property<int>("Quantidade")
                                .HasColumnType("int")
                                .HasColumnName("QtndChapa");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Projetos");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.OwnsOne("icarus.projetos.models.ValueObject.StatusProjeto", "Status", b1 =>
                        {
                            b1.Property<int>("ProjectId")
                                .HasColumnType("int");

                            b1.Property<string>("Status")
                                .HasColumnType("longtext")
                                .HasColumnName("Status");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Projetos");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.OwnsOne("icarus.projetos.models.ValueObject.ValorProjeto", "Valor", b1 =>
                        {
                            b1.Property<int>("ProjectId")
                                .HasColumnType("int");

                            b1.Property<double>("Valor")
                                .HasColumnType("double")
                                .HasColumnName("ValorDoProjeto");

                            b1.HasKey("ProjectId");

                            b1.ToTable("Projetos");

                            b1.WithOwner()
                                .HasForeignKey("ProjectId");
                        });

                    b.Navigation("Chapa")
                        .IsRequired();

                    b.Navigation("Descricao");

                    b.Navigation("QuantidadeDeChapa")
                        .IsRequired();

                    b.Navigation("Status")
                        .IsRequired();

                    b.Navigation("Valor")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
