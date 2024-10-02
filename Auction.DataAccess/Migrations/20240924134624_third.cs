using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auction.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tangerines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Place = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    StartPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tangerines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TangerineEntityUserEntity",
                columns: table => new
                {
                    TangerinesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TangerineEntityUserEntity", x => new { x.TangerinesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_TangerineEntityUserEntity_Tangerines_TangerinesId",
                        column: x => x.TangerinesId,
                        principalTable: "Tangerines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TangerineEntityUserEntity_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TangerineEntityUserEntity_UsersId",
                table: "TangerineEntityUserEntity",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TangerineEntityUserEntity");

            migrationBuilder.DropTable(
                name: "Tangerines");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
