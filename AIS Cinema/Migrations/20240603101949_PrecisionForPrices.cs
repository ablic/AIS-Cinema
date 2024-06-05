using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIS_Cinema.Migrations
{
    /// <inheritdoc />
    public partial class PrecisionForPrices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAENIBKPxijWzfSlja/j3iQi1sWFqeiwUzYjV0AJrTGbiDQcyefsyHTBKog3wdF/FUrg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFUkQJ2PcWF3n6eaIYlVZGZYVY+lggQGRuMLLQrXiUbmHTQ3AnMkxfjCquOdGKjc7Q==");
        }
    }
}
