using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TWork.Migrations
{
    public partial class UpdateMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d2fc205-288e-411e-b63f-303d80e9528f");

            migrationBuilder.RenameColumn(
                name: "isReaded",
                table: "MESSAGEs",
                newName: "IS_READED");

            migrationBuilder.AddColumn<DateTime>(
                name: "SEND_DATE",
                table: "MESSAGEs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "64ef3d24-81fc-4849-9677-5c588bcfcaac", "feecd8ed-9ba2-4c2b-9abc-c3d549de061c", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64ef3d24-81fc-4849-9677-5c588bcfcaac");

            migrationBuilder.DropColumn(
                name: "SEND_DATE",
                table: "MESSAGEs");

            migrationBuilder.RenameColumn(
                name: "IS_READED",
                table: "MESSAGEs",
                newName: "isReaded");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d2fc205-288e-411e-b63f-303d80e9528f", "0831bff1-df37-4c6b-92b3-87cdb698160e", "Admin", "ADMIN" });
        }
    }
}
