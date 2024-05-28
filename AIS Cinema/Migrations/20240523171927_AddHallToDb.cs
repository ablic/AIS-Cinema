using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AIS_Cinema.Migrations
{
    /// <inheritdoc />
    public partial class AddHallToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELpYr7rY0x5snGivTVFsTs8sDf4qxxbx6hXhp1Av15Kwj1txME3WJBimEUBwTtw1Kw==");

            migrationBuilder.InsertData(
                table: "Halls",
                columns: new[] { "Id", "Schema" },
                values: new object[] { 1, "[{\"Number\":1,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":2,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":3,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":4,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":5,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]}]" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEFI5DKHtEO+EUyWBmjphYQwlpcvgQBU82bHv2ArUd/EDGVN2gMKQi1ZVY2C67tmBdw==");
        }
    }
}
