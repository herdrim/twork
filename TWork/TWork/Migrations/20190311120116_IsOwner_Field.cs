using Microsoft.EntityFrameworkCore.Migrations;

namespace TWork.Migrations
{
    public partial class IsOwner_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71447a09-9b17-464c-b4fb-9522d8da52b8");

            migrationBuilder.AddColumn<bool>(
                name: "IS_TEAM_OWNER",
                table: "ROLEs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "30b46aae-6c25-4e3a-bcc1-24226fefaa1b", "638c72e2-ecab-4bc8-9a9b-1d0a3fe2dd1f", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30b46aae-6c25-4e3a-bcc1-24226fefaa1b");

            migrationBuilder.DropColumn(
                name: "IS_TEAM_OWNER",
                table: "ROLEs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "71447a09-9b17-464c-b4fb-9522d8da52b8", "19c722b4-2beb-474f-a58f-5196e3c51f2d", "Admin", "ADMIN" });
        }
    }
}
