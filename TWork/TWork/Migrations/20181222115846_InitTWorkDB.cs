using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TWork.Migrations
{
    public partial class InitTWorkDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MESSAGE_TYPEs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGE_TYPEs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLEs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<int>(nullable: false),
                    DESCRIPTION = table.Column<int>(nullable: false),
                    IS_REQUIRED = table.Column<bool>(nullable: false),
                    CAN_CREATE_TASK = table.Column<bool>(nullable: false),
                    CAN_ASSIGN_TASK = table.Column<bool>(nullable: false),
                    CAN_COMMENT = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLEs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TASK_STATUSes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASK_STATUSes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TEAMs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEAMs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TASKs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TITLE = table.Column<string>(nullable: true),
                    DESCRIPTION = table.Column<string>(nullable: true),
                    DEATHLINE = table.Column<DateTime>(nullable: true),
                    CREATE_TIME = table.Column<DateTime>(nullable: true),
                    START_TIME = table.Column<DateTime>(nullable: true),
                    END_TIME = table.Column<DateTime>(nullable: true),
                    TASK_STATUS_ID = table.Column<int>(nullable: false),
                    TEAM_ID = table.Column<int>(nullable: false),
                    USER_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASKs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TASKs_TASK_STATUSes_TASK_STATUS_ID",
                        column: x => x.TASK_STATUS_ID,
                        principalTable: "TASK_STATUSes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TASKs_TEAMs_TEAM_ID",
                        column: x => x.TEAM_ID,
                        principalTable: "TEAMs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TASKs_AspNetUsers_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USER_TEAM_ROLEs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    USER_ID = table.Column<string>(nullable: true),
                    TEAM_ID = table.Column<int>(nullable: false),
                    ROLE_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_TEAM_ROLEs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_TEAM_ROLEs_ROLEs_ROLE_ID",
                        column: x => x.ROLE_ID,
                        principalTable: "ROLEs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_TEAM_ROLEs_TEAMs_TEAM_ID",
                        column: x => x.TEAM_ID,
                        principalTable: "TEAMs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_TEAM_ROLEs_AspNetUsers_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USERS_TEAMs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TEAM_ID = table.Column<int>(nullable: false),
                    USER_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS_TEAMs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USERS_TEAMs_TEAMs_TEAM_ID",
                        column: x => x.TEAM_ID,
                        principalTable: "TEAMs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USERS_TEAMs_AspNetUsers_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "COMMENTs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CONTENT = table.Column<string>(nullable: true),
                    ANSWER_ID = table.Column<int>(nullable: true),
                    PARENT_COMMENT_ID = table.Column<int>(nullable: true),
                    USER_ID = table.Column<string>(nullable: true),
                    TASK_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMMENTs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COMMENTs_COMMENTs_ANSWER_ID",
                        column: x => x.ANSWER_ID,
                        principalTable: "COMMENTs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_COMMENTs_COMMENTs_PARENT_COMMENT_ID",
                        column: x => x.PARENT_COMMENT_ID,
                        principalTable: "COMMENTs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_COMMENTs_TASKs_TASK_ID",
                        column: x => x.TASK_ID,
                        principalTable: "TASKs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COMMENTs_AspNetUsers_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MESSAGEs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TEXT = table.Column<string>(nullable: true),
                    MESSAGE_TYPE_ID = table.Column<int>(nullable: true),
                    TEAM_ID = table.Column<int>(nullable: true),
                    COMMENT_ID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGEs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MESSAGEs_COMMENTs_COMMENT_ID",
                        column: x => x.COMMENT_ID,
                        principalTable: "COMMENTs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MESSAGEs_MESSAGE_TYPEs_MESSAGE_TYPE_ID",
                        column: x => x.MESSAGE_TYPE_ID,
                        principalTable: "MESSAGE_TYPEs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MESSAGEs_TEAMs_TEAM_ID",
                        column: x => x.TEAM_ID,
                        principalTable: "TEAMs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTs_ANSWER_ID",
                table: "COMMENTs",
                column: "ANSWER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTs_PARENT_COMMENT_ID",
                table: "COMMENTs",
                column: "PARENT_COMMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTs_TASK_ID",
                table: "COMMENTs",
                column: "TASK_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMENTs_USER_ID",
                table: "COMMENTs",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGEs_COMMENT_ID",
                table: "MESSAGEs",
                column: "COMMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGEs_MESSAGE_TYPE_ID",
                table: "MESSAGEs",
                column: "MESSAGE_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGEs_TEAM_ID",
                table: "MESSAGEs",
                column: "TEAM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TASKs_TASK_STATUS_ID",
                table: "TASKs",
                column: "TASK_STATUS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TASKs_TEAM_ID",
                table: "TASKs",
                column: "TEAM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TASKs_USER_ID",
                table: "TASKs",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_TEAM_ROLEs_ROLE_ID",
                table: "USER_TEAM_ROLEs",
                column: "ROLE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_TEAM_ROLEs_TEAM_ID",
                table: "USER_TEAM_ROLEs",
                column: "TEAM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_TEAM_ROLEs_USER_ID",
                table: "USER_TEAM_ROLEs",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_TEAMs_TEAM_ID",
                table: "USERS_TEAMs",
                column: "TEAM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_TEAMs_USER_ID",
                table: "USERS_TEAMs",
                column: "USER_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MESSAGEs");

            migrationBuilder.DropTable(
                name: "USER_TEAM_ROLEs");

            migrationBuilder.DropTable(
                name: "USERS_TEAMs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "COMMENTs");

            migrationBuilder.DropTable(
                name: "MESSAGE_TYPEs");

            migrationBuilder.DropTable(
                name: "ROLEs");

            migrationBuilder.DropTable(
                name: "TASKs");

            migrationBuilder.DropTable(
                name: "TASK_STATUSes");

            migrationBuilder.DropTable(
                name: "TEAMs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
