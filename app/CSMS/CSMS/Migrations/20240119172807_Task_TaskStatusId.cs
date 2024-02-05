using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSMS.Migrations
{
    /// <inheritdoc />
    public partial class TaskTaskStatusId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TaskStatusId",
                table: "Task",
                type: "bigint",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Task_TaskStatusId",
                table: "Task",
                column: "TaskStatusId");

            migrationBuilder.AddForeignKey(
                name: "TaskStatus_fkey",
                table: "Task",
                column: "TaskStatusId",
                principalTable: "TaskStatus",
                principalColumn: "TaskStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "TaskStatus_fkey",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_TaskStatusId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "TaskStatusId",
                table: "Task");
        }
    }
}
