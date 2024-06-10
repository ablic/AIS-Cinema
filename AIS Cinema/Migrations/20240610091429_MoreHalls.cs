using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AIS_Cinema.Migrations
{
    /// <inheritdoc />
    public partial class MoreHalls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEDU2LwYi9LJAwb9XtlLTsvhOtvKdNF8N3Hgtp7HQaAIubVECWuN3ATCz07hFEq8IoA==");

            migrationBuilder.InsertData(
                table: "Halls",
                columns: new[] { "Id", "Schema" },
                values: new object[,]
                {
                    { 2, "[{\"Number\":1,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.7,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":6,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":2,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":6,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":7,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":3,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":6,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":7,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":8,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":4,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.5,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":6,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":7,\"LeftGap\":0.5,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":8,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":5,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":6,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":7,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":8,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":6,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":6,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":7,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":8,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":7,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":6,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":7,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":8,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":6,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":7,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]}]" },
                    { 3, "[{\"Number\":1,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":2,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":3,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":4,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]},{\"Number\":5,\"FrontGap\":0.0,\"BackGap\":0.0,\"Seats\":[{\"Number\":1,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":2,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":3,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":4,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0},{\"Number\":5,\"LeftGap\":0.0,\"RightGap\":0.0,\"PriceMultiplier\":1.0}]}]" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Halls",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa6c0c49-3d13-433f-bc24-fcf769b6e6e7",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEImV5D/mAsr2FvfWr10Af5/esSNQKGkD6I6MyoDOLliAexQx4VIrnzl+9i9zDGOPgg==");
        }
    }
}
