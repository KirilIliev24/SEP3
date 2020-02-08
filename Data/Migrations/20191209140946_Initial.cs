using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    SprintId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.SprintId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    username = table.Column<string>(maxLength: 20, nullable: false),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.username);
                });

            migrationBuilder.CreateTable(
                name: "UserStories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BackLogItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SprintId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Priority = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    WorksOnItusername = table.Column<string>(nullable: true),
                    EstimatedTime = table.Column<int>(nullable: false),
                    ActualTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackLogItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BackLogItems_Users_WorksOnItusername",
                        column: x => x.WorksOnItusername,
                        principalTable: "Users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitation_Projects_Name",
                        column: x => x.Name,
                        principalTable: "Projects",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invitation_Users_username",
                        column: x => x.username,
                        principalTable: "Users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkGroups",
                columns: table => new
                {
                    WorkGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Creatorusername = table.Column<string>(nullable: true),
                    ScrumMasterusername = table.Column<string>(nullable: true),
                    ProductOwnerusername = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkGroups", x => x.WorkGroupId);
                    table.ForeignKey(
                        name: "FK_WorkGroups_Users_Creatorusername",
                        column: x => x.Creatorusername,
                        principalTable: "Users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkGroups_Projects_Name",
                        column: x => x.Name,
                        principalTable: "Projects",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkGroups_Users_ProductOwnerusername",
                        column: x => x.ProductOwnerusername,
                        principalTable: "Users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkGroups_Users_ScrumMasterusername",
                        column: x => x.ScrumMasterusername,
                        principalTable: "Users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkGroupDevelopers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkGroupId = table.Column<int>(nullable: false),
                    Developerusername = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkGroupDevelopers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkGroupDevelopers_Users_Developerusername",
                        column: x => x.Developerusername,
                        principalTable: "Users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkGroupDevelopers_WorkGroups_WorkGroupId",
                        column: x => x.WorkGroupId,
                        principalTable: "WorkGroups",
                        principalColumn: "WorkGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BackLogItems_WorksOnItusername",
                table: "BackLogItems",
                column: "WorksOnItusername");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_Name",
                table: "Invitation",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_username",
                table: "Invitation",
                column: "username");

            migrationBuilder.CreateIndex(
                name: "IX_WorkGroupDevelopers_Developerusername",
                table: "WorkGroupDevelopers",
                column: "Developerusername");

            migrationBuilder.CreateIndex(
                name: "IX_WorkGroupDevelopers_WorkGroupId",
                table: "WorkGroupDevelopers",
                column: "WorkGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkGroups_Creatorusername",
                table: "WorkGroups",
                column: "Creatorusername");

            migrationBuilder.CreateIndex(
                name: "IX_WorkGroups_Name",
                table: "WorkGroups",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkGroups_ProductOwnerusername",
                table: "WorkGroups",
                column: "ProductOwnerusername");

            migrationBuilder.CreateIndex(
                name: "IX_WorkGroups_ScrumMasterusername",
                table: "WorkGroups",
                column: "ScrumMasterusername");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackLogItems");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "UserStories");

            migrationBuilder.DropTable(
                name: "WorkGroupDevelopers");

            migrationBuilder.DropTable(
                name: "WorkGroups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
