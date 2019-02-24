using Microsoft.EntityFrameworkCore.Migrations;

namespace TWork.Migrations
{
    public partial class ManageUsersRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64ef3d24-81fc-4849-9677-5c588bcfcaac");

            migrationBuilder.AddColumn<bool>(
                name: "CAN_MANAGE_USERS",
                table: "ROLEs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "71447a09-9b17-464c-b4fb-9522d8da52b8", "19c722b4-2beb-474f-a58f-5196e3c51f2d", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71447a09-9b17-464c-b4fb-9522d8da52b8");

            migrationBuilder.DropColumn(
                name: "CAN_MANAGE_USERS",
                table: "ROLEs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64ef3d24-81fc-4849-9677-5c588bcfcaac", "feecd8ed-9ba2-4c2b-9abc-c3d549de061c", "Admin", "ADMIN" });
        }
    }
}
