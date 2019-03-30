using Microsoft.EntityFrameworkCore.Migrations;

namespace TWork.Migrations
{
    public partial class RolesForTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30b46aae-6c25-4e3a-bcc1-24226fefaa1b");

            migrationBuilder.AddColumn<int>(
                name: "TEAM_ID",
                table: "ROLEs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4e805ed0-2cc6-484e-8c7c-17b647b50fab", "2fb0ecc6-1cbf-4a14-9abd-4ad6d527ff50", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_ROLEs_TEAM_ID",
                table: "ROLEs",
                column: "TEAM_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLEs_TEAMs_TEAM_ID",
                table: "ROLEs",
                column: "TEAM_ID",
                principalTable: "TEAMs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROLEs_TEAMs_TEAM_ID",
                table: "ROLEs");

            migrationBuilder.DropIndex(
                name: "IX_ROLEs_TEAM_ID",
                table: "ROLEs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e805ed0-2cc6-484e-8c7c-17b647b50fab");

            migrationBuilder.DropColumn(
                name: "TEAM_ID",
                table: "ROLEs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "30b46aae-6c25-4e3a-bcc1-24226fefaa1b", "638c72e2-ecab-4bc8-9a9b-1d0a3fe2dd1f", "Admin", "ADMIN" });
        }
    }
}
