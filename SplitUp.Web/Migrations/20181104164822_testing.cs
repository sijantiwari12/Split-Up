using Microsoft.EntityFrameworkCore.Migrations;

namespace SplitUp.Web.Migrations
{
    public partial class testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Gender", "Token" },
                values: new object[] { "M", "8ac79302-58e9-4f85-9fa2-0e663ccbc2f4" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Gender", "Token" },
                values: new object[] { "F", "e2aa62d8-72ba-4847-ac6e-287857a5bdcd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Gender", "Token" },
                values: new object[] { null, "1aa07ae9-ca6b-49b7-8fdb-0b5eb14ca500" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Gender", "Token" },
                values: new object[] { null, "accde4d4-4c64-4740-86c6-a97846dfa89b" });
        }
    }
}
