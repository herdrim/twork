using Microsoft.EntityFrameworkCore.Migrations;

namespace TWork.Migrations
{
    public partial class UpdateTaskStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c63befd7-4996-4358-bd17-c9e033b9b9a5");

            migrationBuilder.AddColumn<int>(
                name: "NEXT_STATUS_ID",
                table: "TASK_STATUSes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PREV_STATUS_ID",
                table: "TASK_STATUSes",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b42a1d9c-3625-4234-80f7-727fbeda41af", "62c50947-f58e-42d9-ae68-dd5e8ac98281", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_TASK_STATUSes_NEXT_STATUS_ID",
                table: "TASK_STATUSes",
                column: "NEXT_STATUS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TASK_STATUSes_PREV_STATUS_ID",
                table: "TASK_STATUSes",
                column: "PREV_STATUS_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_TASK_STATUSes_TASK_STATUSes_NEXT_STATUS_ID",
                table: "TASK_STATUSes",
                column: "NEXT_STATUS_ID",
                principalTable: "TASK_STATUSes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TASK_STATUSes_TASK_STATUSes_PREV_STATUS_ID",
                table: "TASK_STATUSes",
                column: "PREV_STATUS_ID",
                principalTable: "TASK_STATUSes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TASK_STATUSes_TASK_STATUSes_NEXT_STATUS_ID",
                table: "TASK_STATUSes");

            migrationBuilder.DropForeignKey(
                name: "FK_TASK_STATUSes_TASK_STATUSes_PREV_STATUS_ID",
                table: "TASK_STATUSes");

            migrationBuilder.DropIndex(
                name: "IX_TASK_STATUSes_NEXT_STATUS_ID",
                table: "TASK_STATUSes");

            migrationBuilder.DropIndex(
                name: "IX_TASK_STATUSes_PREV_STATUS_ID",
                table: "TASK_STATUSes");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b42a1d9c-3625-4234-80f7-727fbeda41af");

            migrationBuilder.DropColumn(
                name: "NEXT_STATUS_ID",
                table: "TASK_STATUSes");

            migrationBuilder.DropColumn(
                name: "PREV_STATUS_ID",
                table: "TASK_STATUSes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c63befd7-4996-4358-bd17-c9e033b9b9a5", "f95890c3-6a4e-4acb-ae4e-281bbb37a4b7", "Admin", "ADMIN" });
        }
    }
}
