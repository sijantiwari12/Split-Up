using Microsoft.EntityFrameworkCore.Migrations;

namespace SplitUp.Web.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Gender", "Token" },
                values: new object[] { "M", "1aa07ae9-ca6b-49b7-8fdb-0b5eb14ca500" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Gender", "Token" },
                values: new object[] { "F", "accde4d4-4c64-4740-86c6-a97846dfa89b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Gender", "Token" },
                values: new object[] { null, "f8497185-903c-40d9-bbf5-a2b724798a11" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Gender", "Token" },
                values: new object[] { null, "3f6b8e6e-b22e-4751-80f4-23c5bda5400d" });
        }
    }
}
