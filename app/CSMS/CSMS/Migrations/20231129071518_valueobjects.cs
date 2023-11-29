using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMS.Migrations
{
    /// <inheritdoc />
    public partial class valueobjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxMoney",
                table: "Contracts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TaxMoney",
                table: "Contracts",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
