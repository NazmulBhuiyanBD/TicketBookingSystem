using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddBusSeats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Seat",
                table: "Busses",
                newName: "StartingPoint");

            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "Busses",
                newName: "SeatsJson");

            migrationBuilder.AddColumn<string>(
                name: "EndingPoint",
                table: "Busses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndingPoint",
                table: "Busses");

            migrationBuilder.RenameColumn(
                name: "StartingPoint",
                table: "Busses",
                newName: "Seat");

            migrationBuilder.RenameColumn(
                name: "SeatsJson",
                table: "Busses",
                newName: "Destination");
        }
    }
}
