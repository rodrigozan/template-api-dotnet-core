﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("api.Models.DictionaryModel", b =>
                {
                    b.Property<long>("codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("en")
                        .HasMaxLength(500)
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("en");

                    b.Property<string>("propiedade")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("propiedade");

                    b.Property<string>("pt_br")
                        .HasMaxLength(500)
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("pt_br");

                    b.HasKey("codigo");

                    b.ToTable("tb_cad_dictionary", (string)null);
                });

            modelBuilder.Entity("api.Models.LanguageModel", b =>
                {
                    b.Property<long>("codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("descricao");

                    b.HasKey("codigo");

                    b.ToTable("tb_stc_language", (string)null);
                });

            modelBuilder.Entity("api.Models.MenuModel", b =>
                {
                    b.Property<long>("codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<ulong>("ativo")
                        .HasColumnType("BIT")
                        .HasColumnName("ativo");

                    b.Property<long>("codigo_perfil")
                        .HasColumnType("BIGINT")
                        .HasColumnName("codigo_perfil");

                    b.Property<string>("icon")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("icon");

                    b.Property<string>("router")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("router");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("VARCHAR(250)")
                        .HasColumnName("title");

                    b.HasKey("codigo");

                    b.HasIndex("codigo_perfil");

                    b.ToTable("tb_cad_menu", (string)null);
                });

            modelBuilder.Entity("api.Models.PerfilModel", b =>
                {
                    b.Property<long>("codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR(150)")
                        .HasColumnName("descricao");

                    b.HasKey("codigo");

                    b.ToTable("tb_stc_perfil", (string)null);
                });

            modelBuilder.Entity("api.Models.SubMenuModel", b =>
                {
                    b.Property<long>("codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<ulong>("ativo")
                        .HasColumnType("BIT")
                        .HasColumnName("ativo");

                    b.Property<long>("codigo_menu")
                        .HasColumnType("BIGINT")
                        .HasColumnName("codigo_menu");

                    b.Property<long>("codigo_perfil")
                        .HasColumnType("BIGINT")
                        .HasColumnName("codigo_perfil");

                    b.Property<string>("icon")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("icon");

                    b.Property<string>("router")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("VARCHAR(500)")
                        .HasColumnName("router");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("VARCHAR(250)")
                        .HasColumnName("title");

                    b.HasKey("codigo");

                    b.HasIndex("codigo_menu");

                    b.HasIndex("codigo_perfil");

                    b.ToTable("tb_cad_submenu", (string)null);
                });

            modelBuilder.Entity("api.Models.UsuarioModel", b =>
                {
                    b.Property<long>("codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<ulong>("ativo")
                        .HasColumnType("BIT")
                        .HasColumnName("ativo");

                    b.Property<long>("codigo_language")
                        .HasColumnType("BIGINT")
                        .HasColumnName("codigo_language");

                    b.Property<long>("codigo_perfil")
                        .HasColumnType("BIGINT")
                        .HasColumnName("codigo_perfil");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR(150)")
                        .HasColumnName("nome");

                    b.Property<string>("senha")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR(150)")
                        .HasColumnName("senha");

                    b.Property<string>("usuario")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("VARCHAR(150)")
                        .HasColumnName("usuario");

                    b.HasKey("codigo");

                    b.HasIndex("codigo_language");

                    b.HasIndex("codigo_perfil");

                    b.HasIndex(new[] { "usuario" }, "IX_usuario")
                        .IsUnique();

                    b.ToTable("tb_cad_usuario", (string)null);
                });

            modelBuilder.Entity("api.Models.MenuModel", b =>
                {
                    b.HasOne("api.Models.PerfilModel", "Perfil")
                        .WithMany("Menu")
                        .HasForeignKey("codigo_perfil")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Perfil_Menu");

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("api.Models.SubMenuModel", b =>
                {
                    b.HasOne("api.Models.MenuModel", "Menu")
                        .WithMany("SubMenu")
                        .HasForeignKey("codigo_menu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Menu_SubMenu");

                    b.HasOne("api.Models.PerfilModel", "Perfil")
                        .WithMany("SubMenu")
                        .HasForeignKey("codigo_perfil")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Perfil_SubMenu");

                    b.Navigation("Menu");

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("api.Models.UsuarioModel", b =>
                {
                    b.HasOne("api.Models.LanguageModel", "Language")
                        .WithMany("Usuario")
                        .HasForeignKey("codigo_language")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Language_Usuario");

                    b.HasOne("api.Models.PerfilModel", "Perfil")
                        .WithMany("Usuario")
                        .HasForeignKey("codigo_perfil")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Perfil_Usuario");

                    b.Navigation("Language");

                    b.Navigation("Perfil");
                });

            modelBuilder.Entity("api.Models.LanguageModel", b =>
                {
                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("api.Models.MenuModel", b =>
                {
                    b.Navigation("SubMenu");
                });

            modelBuilder.Entity("api.Models.PerfilModel", b =>
                {
                    b.Navigation("Menu");

                    b.Navigation("SubMenu");

                    b.Navigation("Usuario");
                });
        }
    }
}
