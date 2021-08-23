﻿// <auto-generated />
using System;
using BackEnd.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackEnd.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    [Migration("20210823150942_v1.1")]
    partial class v11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackEnd.Domain.Model.Cuestionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Activo")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Cuestionario");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.Pregunta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CuestionarioId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CuestionarioId");

                    b.ToTable("Pregunta");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.Respuesta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("EsCorrecta")
                        .HasColumnType("bit");

                    b.Property<int>("PreguntaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PreguntaId");

                    b.ToTable("Respuesta");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.RespuestaCuestionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Activo")
                        .HasColumnType("int");

                    b.Property<int>("CuestionarioId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("NombreParticipante")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CuestionarioId");

                    b.ToTable("RespuestaCuestionario");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.RespuestaCuestionarioDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RespuestaCuestionarioId")
                        .HasColumnType("int");

                    b.Property<int>("RespuestaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RespuestaCuestionarioId");

                    b.HasIndex("RespuestaId");

                    b.ToTable("RespuestaCuestionarioDetalle");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.Cuestionario", b =>
                {
                    b.HasOne("BackEnd.Domain.Model.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.Pregunta", b =>
                {
                    b.HasOne("BackEnd.Domain.Model.Cuestionario", "Cuestionario")
                        .WithMany("listPreguntas")
                        .HasForeignKey("CuestionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cuestionario");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.Respuesta", b =>
                {
                    b.HasOne("BackEnd.Domain.Model.Pregunta", "Pregunta")
                        .WithMany("listRespuestas")
                        .HasForeignKey("PreguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pregunta");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.RespuestaCuestionario", b =>
                {
                    b.HasOne("BackEnd.Domain.Model.Cuestionario", "Cuestionario")
                        .WithMany()
                        .HasForeignKey("CuestionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cuestionario");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.RespuestaCuestionarioDetalle", b =>
                {
                    b.HasOne("BackEnd.Domain.Model.RespuestaCuestionario", "RespuestaCuestionario")
                        .WithMany("ListRtaCuestionarioDetalle")
                        .HasForeignKey("RespuestaCuestionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Domain.Model.Respuesta", "Respuesta")
                        .WithMany()
                        .HasForeignKey("RespuestaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Respuesta");

                    b.Navigation("RespuestaCuestionario");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.Cuestionario", b =>
                {
                    b.Navigation("listPreguntas");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.Pregunta", b =>
                {
                    b.Navigation("listRespuestas");
                });

            modelBuilder.Entity("BackEnd.Domain.Model.RespuestaCuestionario", b =>
                {
                    b.Navigation("ListRtaCuestionarioDetalle");
                });
#pragma warning restore 612, 618
        }
    }
}