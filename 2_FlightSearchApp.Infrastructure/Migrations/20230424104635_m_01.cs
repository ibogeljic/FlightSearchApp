using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightSearchApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class m_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodeLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeLists", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FlightOffers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureAirportCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DestinationAirportCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransferNumbersDeparture = table.Column<int>(type: "int", nullable: false),
                    TransferNumbersReturn = table.Column<int>(type: "int", nullable: false),
                    PassengersNumber = table.Column<int>(type: "int", nullable: false),
                    ValueID = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightOffers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FlightOffers_CodeLists_ValueID",
                        column: x => x.ValueID,
                        principalTable: "CodeLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightOffers_ValueID",
                table: "FlightOffers",
                column: "ValueID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightOffers");

            migrationBuilder.DropTable(
                name: "CodeLists");
        }
    }
}
