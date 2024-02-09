﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SystemSzwajcarski;

namespace SystemSzwajcarski.Migrations
{
    [DbContext(typeof(DbContextSS))]
    [Migration("20240209085618_Playerimprovement")]
    partial class Playerimprovement
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SystemSzwajcarski.Models.Main.Game", b =>
                {
                    b.Property<int>("idGame")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Color")
                        .HasColumnType("bit");

                    b.Property<int?>("OpponentId")
                        .HasColumnType("int");

                    b.Property<int>("RelationId")
                        .HasColumnType("int");

                    b.Property<int>("Result")
                        .HasColumnType("int");

                    b.Property<int>("Round")
                        .HasColumnType("int");

                    b.Property<int?>("TournamentidTournament")
                        .HasColumnType("int");

                    b.HasKey("idGame");

                    b.HasIndex("OpponentId");

                    b.HasIndex("RelationId");

                    b.HasIndex("TournamentidTournament");

                    b.ToTable("games");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.Main.Tournament", b =>
                {
                    b.Property<int>("idTournament")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Access")
                        .HasColumnType("int");

                    b.Property<int>("CurrentRound")
                        .HasColumnType("int");

                    b.Property<int?>("MaxRound")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberPlayers")
                        .HasColumnType("int");

                    b.Property<int?>("OrganizeridUser")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("idTournament");

                    b.HasIndex("OrganizeridUser");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.Organizer", b =>
                {
                    b.Property<int>("idUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Roleuser")
                        .HasColumnType("int");

                    b.HasKey("idUser");

                    b.ToTable("organizers");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.Player", b =>
                {
                    b.Property<int>("idUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ranking")
                        .HasColumnType("int");

                    b.Property<int>("Roleuser")
                        .HasColumnType("int");

                    b.Property<int>("StatusCreatures")
                        .HasColumnType("int");

                    b.HasKey("idUser");

                    b.ToTable("players");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.Relation.RelationTP", b =>
                {
                    b.Property<int>("idRelation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Black")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("RankingPlayer")
                        .HasColumnType("int");

                    b.Property<int>("RankingTournament")
                        .HasColumnType("int");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("idRelation");

                    b.HasIndex("PlayerId");

                    b.HasIndex("TournamentId");

                    b.ToTable("RelationTP");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.RelationOP", b =>
                {
                    b.Property<int>("idRelation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrganizerId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Ranking")
                        .HasColumnType("int");

                    b.HasKey("idRelation");

                    b.HasIndex("OrganizerId");

                    b.HasIndex("PlayerId");

                    b.ToTable("RelationOP");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.Main.Game", b =>
                {
                    b.HasOne("SystemSzwajcarski.Models.Player", "Opponent")
                        .WithMany()
                        .HasForeignKey("OpponentId");

                    b.HasOne("SystemSzwajcarski.Models.Relation.RelationTP", "Relation")
                        .WithMany("Games")
                        .HasForeignKey("RelationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemSzwajcarski.Models.Main.Tournament", null)
                        .WithMany("Games")
                        .HasForeignKey("TournamentidTournament");

                    b.Navigation("Opponent");

                    b.Navigation("Relation");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.Main.Tournament", b =>
                {
                    b.HasOne("SystemSzwajcarski.Models.Organizer", "Organizer")
                        .WithMany("Tournament")
                        .HasForeignKey("OrganizeridUser");

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.Relation.RelationTP", b =>
                {
                    b.HasOne("SystemSzwajcarski.Models.Player", "Player")
                        .WithMany("Tournament")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemSzwajcarski.Models.Main.Tournament", "Tournament")
                        .WithMany("Players")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.RelationOP", b =>
                {
                    b.HasOne("SystemSzwajcarski.Models.Organizer", "Organizer")
                        .WithMany("Players")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SystemSzwajcarski.Models.Player", "Player")
                        .WithMany("Organizers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizer");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.Main.Tournament", b =>
                {
                    b.Navigation("Games");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.Organizer", b =>
                {
                    b.Navigation("Players");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.Player", b =>
                {
                    b.Navigation("Organizers");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("SystemSzwajcarski.Models.Relation.RelationTP", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
