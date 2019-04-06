using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TWork.Migrations
{
    public partial class UpdateCommentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_COMMENTs_COMMENTs_ANSWER_ID",
                table: "COMMENTs");

            migrationBuilder.DropForeignKey(
                name: "FK_COMMENTs_COMMENTs_PARENT_COMMENT_ID",
                table: "COMMENTs");

            migrationBuilder.DropIndex(
                name: "IX_COMMENTs_ANSWER_ID",
                table: "COMMENTs");

            migrationBuilder.DropIndex(
                name: "IX_COMMENTs_PARENT_COMMENT_ID",
                table: "COMMENTs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf96c62a-fe9d-44be-900f-9ae9fc54ec2e");

            migrationBuilder.DropColumn(
                name: "ANSWER_ID",
                table: "COMMENTs");

            migrationBuilder.DropColumn(
                name: "PARENT_COMMENT_ID",
                table: "COMMENTs");

            migrationBuilder.AddColumn<DateTime>(
                name: "CREATED",
                table: "COMMENTs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c63befd7-4996-4358-bd17-c9e033b9b9a5", "f95890c3-6a4e-4acb-ae4e-281bbb37a4b7", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c63befd7-4996-4358-bd17-c9e033b9b9a5");

            migrationBuilder.DropColumn(
                name: "CREATED",
                table: "COMMENTs");

            migrationBuilder.AddColumn<int>(
                name: "ANSWER_ID",
                table: "COMMENTs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PARENT_COMMENT_ID",
                table: "COMMENTs",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bf96c62a-fe9d-44be-900f-9ae9fc54ec2e", "0d6ae0ea-065e-4ab3-94da-fc2d0bbea122", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTs_ANSWER_ID",
                table: "COMMENTs",
                column: "ANSWER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTs_PARENT_COMMENT_ID",
                table: "COMMENTs",
                column: "PARENT_COMMENT_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_COMMENTs_COMMENTs_ANSWER_ID",
                table: "COMMENTs",
                column: "ANSWER_ID",
                principalTable: "COMMENTs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_COMMENTs_COMMENTs_PARENT_COMMENT_ID",
                table: "COMMENTs",
                column: "PARENT_COMMENT_ID",
                principalTable: "COMMENTs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
