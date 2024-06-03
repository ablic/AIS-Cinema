using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIS_Cinema.Migrations
{
    /// <inheritdoc />
    public partial class VisitorTelegramChatId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_AspNetUsers_VisitorId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_VisitorId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "VisitorId",
                table: "Tickets");

            migrationBuilder.AddColumn<long>(
                name: "TelegramChatId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                columns: new[] { "PasswordHash", "TelegramChatId" },
                values: new object[] { "AQAAAAIAAYagAAAAEFUkQJ2PcWF3n6eaIYlVZGZYVY+lggQGRuMLLQrXiUbmHTQ3AnMkxfjCquOdGKjc7Q==", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TelegramChatId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "VisitorId",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELpYr7rY0x5snGivTVFsTs8sDf4qxxbx6hXhp1Av15Kwj1txME3WJBimEUBwTtw1Kw==");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VisitorId",
                table: "Tickets",
                column: "VisitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_AspNetUsers_VisitorId",
                table: "Tickets",
                column: "VisitorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
