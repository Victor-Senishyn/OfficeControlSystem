using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeControlSystemApi.Migrations
{
    /// <inheritdoc />
    public partial class updated_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_access_card_employee_EmployeeId",
                table: "access_card");

            migrationBuilder.DropForeignKey(
                name: "FK_visit_history_access_card_AccessCardId",
                table: "visit_history");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employee",
                table: "employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_visit_history",
                table: "visit_history");

            migrationBuilder.DropPrimaryKey(
                name: "PK_access_card",
                table: "access_card");

            migrationBuilder.RenameTable(
                name: "employee",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "visit_history",
                newName: "VisitHistory");

            migrationBuilder.RenameTable(
                name: "access_card",
                newName: "AccessCard");

            migrationBuilder.RenameIndex(
                name: "IX_visit_history_AccessCardId",
                table: "VisitHistory",
                newName: "IX_VisitHistory_AccessCardId");

            migrationBuilder.RenameIndex(
                name: "IX_access_card_EmployeeId",
                table: "AccessCard",
                newName: "IX_AccessCard_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitHistory",
                table: "VisitHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccessCard",
                table: "AccessCard",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessCard_Employee_EmployeeId",
                table: "AccessCard",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitHistory_AccessCard_AccessCardId",
                table: "VisitHistory",
                column: "AccessCardId",
                principalTable: "AccessCard",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessCard_Employee_EmployeeId",
                table: "AccessCard");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitHistory_AccessCard_AccessCardId",
                table: "VisitHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitHistory",
                table: "VisitHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccessCard",
                table: "AccessCard");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "employee");

            migrationBuilder.RenameTable(
                name: "VisitHistory",
                newName: "visit_history");

            migrationBuilder.RenameTable(
                name: "AccessCard",
                newName: "access_card");

            migrationBuilder.RenameIndex(
                name: "IX_VisitHistory_AccessCardId",
                table: "visit_history",
                newName: "IX_visit_history_AccessCardId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessCard_EmployeeId",
                table: "access_card",
                newName: "IX_access_card_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employee",
                table: "employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_visit_history",
                table: "visit_history",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_access_card",
                table: "access_card",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_access_card_employee_EmployeeId",
                table: "access_card",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_visit_history_access_card_AccessCardId",
                table: "visit_history",
                column: "AccessCardId",
                principalTable: "access_card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
