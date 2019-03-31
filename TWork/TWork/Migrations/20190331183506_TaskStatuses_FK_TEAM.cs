using Microsoft.EntityFrameworkCore.Migrations;

namespace TWork.Migrations
{
    public partial class TaskStatuses_FK_TEAM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4e805ed0-2cc6-484e-8c7c-17b647b50fab");

            migrationBuilder.AddColumn<int>(
                name: "TEAM_ID",
                table: "TASK_STATUSes",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bf96c62a-fe9d-44be-900f-9ae9fc54ec2e", "0d6ae0ea-065e-4ab3-94da-fc2d0bbea122", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_TASK_STATUSes_TEAM_ID",
                table: "TASK_STATUSes",
                column: "TEAM_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TASK_STATUSes_TEAMs_TEAM_ID",
                table: "TASK_STATUSes",
                column: "TEAM_ID",
                principalTable: "TEAMs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TASK_STATUSes_TEAMs_TEAM_ID",
                table: "TASK_STATUSes");

            migrationBuilder.DropIndex(
                name: "IX_TASK_STATUSes_TEAM_ID",
                table: "TASK_STATUSes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf96c62a-fe9d-44be-900f-9ae9fc54ec2e");

            migrationBuilder.DropColumn(
                name: "TEAM_ID",
                table: "TASK_STATUSes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4e805ed0-2cc6-484e-8c7c-17b647b50fab", "2fb0ecc6-1cbf-4a14-9abd-4ad6d527ff50", "Admin", "ADMIN" });
        }
    }
}
