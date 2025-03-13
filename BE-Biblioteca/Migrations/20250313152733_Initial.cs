using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE_Biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prestiti",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataPrestito = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LimiteRestituzione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataRestituzione = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestiti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prestiti_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prestiti_BookId",
                table: "Prestiti",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prestiti");
        }
    }
}
