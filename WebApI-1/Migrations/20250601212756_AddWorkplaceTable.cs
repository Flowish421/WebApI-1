using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApI_1.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkplaceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Shifts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkplaceId",
                table: "Shifts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Workplaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workplaces", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_WorkplaceId",
                table: "Shifts",
                column: "WorkplaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Workplaces_WorkplaceId",
                table: "Shifts",
                column: "WorkplaceId",
                principalTable: "Workplaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Workplaces_WorkplaceId",
                table: "Shifts");

            migrationBuilder.DropTable(
                name: "Workplaces");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_WorkplaceId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "WorkplaceId",
                table: "Shifts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Shifts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
