using Microsoft.EntityFrameworkCore.Migrations;

namespace SplitUp.Web.Migrations
{
    public partial class addedping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ping",
                table: "Creditors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Gender", "Token" },
                values: new object[] { "M", "afc392f9-d1da-4ac2-98f1-f6dd1912a999" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Gender", "Token" },
                values: new object[] { "F", "c4fdc9d3-408a-4462-ac84-33ca2494c311" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ping",
                table: "Creditors");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Gender", "Token" },
                values: new object[] { null, "8ac79302-58e9-4f85-9fa2-0e663ccbc2f4" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Gender", "Token" },
                values: new object[] { null, "e2aa62d8-72ba-4847-ac6e-287857a5bdcd" });
        }
    }
}
