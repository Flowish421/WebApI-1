using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApI_1.Migrations
{
    /// <inheritdoc />
    public partial class GörEndTimeValfri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Workplaces_WorkplaceId",
                table: "Shifts");

            migrationBuilder.AlterColumn<int>(
                name: "WorkplaceId",
                table: "Shifts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Shifts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Workplaces_WorkplaceId",
                table: "Shifts",
                column: "WorkplaceId",
                principalTable: "Workplaces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Workplaces_WorkplaceId",
                table: "Shifts");

            migrationBuilder.AlterColumn<int>(
                name: "WorkplaceId",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Shifts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Workplaces_WorkplaceId",
                table: "Shifts",
                column: "WorkplaceId",
                principalTable: "Workplaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
