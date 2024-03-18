using Microsoft.EntityFrameworkCore.Migrations;

namespace SystemSzwajcarski.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organizers",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Roleuser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizers", x => x.idUser);
                });

            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    idUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusCreatures = table.Column<int>(type: "int", nullable: false),
                    Ranking = table.Column<int>(type: "int", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Roleuser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.idUser);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    idTournament = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizeridUser = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Access = table.Column<int>(type: "int", nullable: false),
                    CurrentRound = table.Column<int>(type: "int", nullable: false),
                    NumberPlayers = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxRound = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.idTournament);
                    table.ForeignKey(
                        name: "FK_Tournaments_organizers_OrganizeridUser",
                        column: x => x.OrganizeridUser,
                        principalTable: "organizers",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelationOP",
                columns: table => new
                {
                    idRelation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ranking = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    OrganizerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationOP", x => x.idRelation);
                    table.ForeignKey(
                        name: "FK_RelationOP_organizers_OrganizerId",
                        column: x => x.OrganizerId,
                        principalTable: "organizers",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelationOP_players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "players",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelationTP",
                columns: table => new
                {
                    idRelation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RankingTournament = table.Column<int>(type: "int", nullable: false),
                    RankingPlayer = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationTP", x => x.idRelation);
                    table.ForeignKey(
                        name: "FK_RelationTP_players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "players",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelationTP_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "idTournament",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    idGame = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Round = table.Column<int>(type: "int", nullable: false),
                    Bye = table.Column<bool>(type: "bit", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: true),
                    BlackPlayerId = table.Column<int>(type: "int", nullable: true),
                    WhitePlayerId = table.Column<int>(type: "int", nullable: true),
                    Result = table.Column<int>(type: "int", nullable: false),
                    RelationTPidRelation = table.Column<int>(type: "int", nullable: true),
                    TournamentidTournament = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.idGame);
                    table.ForeignKey(
                        name: "FK_games_RelationTP_BlackPlayerId",
                        column: x => x.BlackPlayerId,
                        principalTable: "RelationTP",
                        principalColumn: "idRelation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_games_RelationTP_RelationTPidRelation",
                        column: x => x.RelationTPidRelation,
                        principalTable: "RelationTP",
                        principalColumn: "idRelation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_games_RelationTP_WhitePlayerId",
                        column: x => x.WhitePlayerId,
                        principalTable: "RelationTP",
                        principalColumn: "idRelation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_games_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "idTournament",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_games_Tournaments_TournamentidTournament",
                        column: x => x.TournamentidTournament,
                        principalTable: "Tournaments",
                        principalColumn: "idTournament",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_games_BlackPlayerId",
                table: "games",
                column: "BlackPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_games_RelationTPidRelation",
                table: "games",
                column: "RelationTPidRelation");

            migrationBuilder.CreateIndex(
                name: "IX_games_TournamentId",
                table: "games",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_games_TournamentidTournament",
                table: "games",
                column: "TournamentidTournament");

            migrationBuilder.CreateIndex(
                name: "IX_games_WhitePlayerId",
                table: "games",
                column: "WhitePlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationOP_OrganizerId",
                table: "RelationOP",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationOP_PlayerId",
                table: "RelationOP",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationTP_PlayerId",
                table: "RelationTP",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_RelationTP_TournamentId",
                table: "RelationTP",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_OrganizeridUser",
                table: "Tournaments",
                column: "OrganizeridUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "RelationOP");

            migrationBuilder.DropTable(
                name: "RelationTP");

            migrationBuilder.DropTable(
                name: "players");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "organizers");
        }
    }
}
