using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AIS_Cinema.Migrations
{
    /// <inheritdoc />
    public partial class AgeLimitIdForMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AgeLimits_AgeLimitId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AgeLimitId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AgeLimits",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "0+" },
                    { 2, "6+" },
                    { 3, "12+" },
                    { 4, "16+" },
                    { 5, "18+" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFI5DKHtEO+EUyWBmjphYQwlpcvgQBU82bHv2ArUd/EDGVN2gMKQi1ZVY2C67tmBdw==");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AgeLimits_AgeLimitId",
                table: "Movies",
                column: "AgeLimitId",
                principalTable: "AgeLimits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_AgeLimits_AgeLimitId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions");

            migrationBuilder.DeleteData(
                table: "AgeLimits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AgeLimits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AgeLimits",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AgeLimits",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AgeLimits",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Sessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AgeLimitId",
                table: "Movies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEMX2+vM7xQzHqXohcqDeAmEXW8Ra/jltammW8vyVtGu6L54NtOGqeHujYDIiFYmMeQ==");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_AgeLimits_AgeLimitId",
                table: "Movies",
                column: "AgeLimitId",
                principalTable: "AgeLimits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Movies_MovieId",
                table: "Sessions",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
