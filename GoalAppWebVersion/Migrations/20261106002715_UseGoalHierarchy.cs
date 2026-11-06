using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalAppWebVersion.Migrations
{
    /// <inheritdoc />
    public partial class UseGoalHierarchy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Streak",
                table: "Goal");

            migrationBuilder.AddColumn<int>(
                name: "DaysSinceHabitRelapse",
                table: "Goal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Goal",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HabitStreak",
                table: "Goal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HowOften",
                table: "Goal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Goal",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ReminderFrequency",
                table: "Goal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ExerciseItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    ExerciseGoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseItem_Goal_ExerciseGoalId",
                        column: x => x.ExerciseGoalId,
                        principalTable: "Goal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseItem_ExerciseGoalId",
                table: "ExerciseItem",
                column: "ExerciseGoalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseItem");

            migrationBuilder.DropColumn(
                name: "DaysSinceHabitRelapse",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "HabitStreak",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "HowOften",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "ReminderFrequency",
                table: "Goal");

            migrationBuilder.AddColumn<int>(
                name: "Streak",
                table: "Goal",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
