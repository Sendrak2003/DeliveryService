using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryService.Migrations
{
    /// <inheritdoc />
    public partial class SeedOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Weight", "District", "DeliveryTime" },
                values: new object[,]
                {
                { 5.0, "Центральный", DateTime.Now.AddHours(1) },
                { 3.5, "Северный", DateTime.Now.AddHours(2) },
                { 4.0, "Северный", DateTime.Now.AddHours(2) },
                { 2.5, "Северный", DateTime.Now.AddHours(2) },
                { 6.0, "Северный", DateTime.Now.AddHours(2) }
            });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удалите данные, если это необходимо
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
