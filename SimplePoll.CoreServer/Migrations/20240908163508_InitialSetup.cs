using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimplePoll.CoreServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Polls");

            migrationBuilder.CreateTable(
                name: "Polls",
                schema: "Polls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Elements",
                schema: "Polls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Subtype = table.Column<int>(type: "int", nullable: false),
                    Min = table.Column<int>(type: "int", nullable: true),
                    Max = table.Column<int>(type: "int", nullable: true),
                    PollId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Elements_Polls_PollId",
                        column: x => x.PollId,
                        principalSchema: "Polls",
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                schema: "Polls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PollId = table.Column<int>(type: "int", nullable: false),
                    SubmissionUser = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_Polls_PollId",
                        column: x => x.PollId,
                        principalSchema: "Polls",
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Values",
                schema: "Polls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    PollElementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Values_Elements_PollElementId",
                        column: x => x.PollElementId,
                        principalSchema: "Polls",
                        principalTable: "Elements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                schema: "Polls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PollElementId = table.Column<int>(type: "int", nullable: false),
                    PollSubmissionId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Elements_PollElementId",
                        column: x => x.PollElementId,
                        principalSchema: "Polls",
                        principalTable: "Elements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Submissions_PollSubmissionId",
                        column: x => x.PollSubmissionId,
                        principalSchema: "Polls",
                        principalTable: "Submissions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_PollElementId",
                schema: "Polls",
                table: "Answers",
                column: "PollElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_PollSubmissionId",
                schema: "Polls",
                table: "Answers",
                column: "PollSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Elements_PollId",
                schema: "Polls",
                table: "Elements",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_PollId",
                schema: "Polls",
                table: "Submissions",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_Values_PollElementId",
                schema: "Polls",
                table: "Values",
                column: "PollElementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers",
                schema: "Polls");

            migrationBuilder.DropTable(
                name: "Values",
                schema: "Polls");

            migrationBuilder.DropTable(
                name: "Submissions",
                schema: "Polls");

            migrationBuilder.DropTable(
                name: "Elements",
                schema: "Polls");

            migrationBuilder.DropTable(
                name: "Polls",
                schema: "Polls");
        }
    }
}
