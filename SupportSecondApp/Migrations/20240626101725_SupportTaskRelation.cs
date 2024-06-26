using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupportSecondApp.Migrations
{
    /// <inheritdoc />
    public partial class SupportTaskRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportTask_Projects_ProjectId",
                table: "SupportTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupportTask",
                table: "SupportTask");

            migrationBuilder.RenameTable(
                name: "SupportTask",
                newName: "SupportTasks");

            migrationBuilder.RenameIndex(
                name: "IX_SupportTask_ProjectId",
                table: "SupportTasks",
                newName: "IX_SupportTasks_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupportTasks",
                table: "SupportTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTasks_Projects_ProjectId",
                table: "SupportTasks",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SupportTasks_Projects_ProjectId",
                table: "SupportTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SupportTasks",
                table: "SupportTasks");

            migrationBuilder.RenameTable(
                name: "SupportTasks",
                newName: "SupportTask");

            migrationBuilder.RenameIndex(
                name: "IX_SupportTasks_ProjectId",
                table: "SupportTask",
                newName: "IX_SupportTask_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupportTask",
                table: "SupportTask",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupportTask_Projects_ProjectId",
                table: "SupportTask",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
