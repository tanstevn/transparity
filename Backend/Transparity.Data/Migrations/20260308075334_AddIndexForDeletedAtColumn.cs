using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transparity.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexForDeletedAtColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_DeletedAt",
                table: "Users",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_DeletedAt",
                table: "Roles",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_RequestStatuses_DeletedAt",
                table: "RequestStatuses",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_DeletedAt",
                table: "Requests",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_RequestFields_DeletedAt",
                table: "RequestFields",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_RequestComments_DeletedAt",
                table: "RequestComments",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_RequestCategories_DeletedAt",
                table: "RequestCategories",
                column: "DeletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_RequestAttachments_DeletedAt",
                table: "RequestAttachments",
                column: "DeletedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_DeletedAt",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Roles_DeletedAt",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_RequestStatuses_DeletedAt",
                table: "RequestStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Requests_DeletedAt",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_RequestFields_DeletedAt",
                table: "RequestFields");

            migrationBuilder.DropIndex(
                name: "IX_RequestComments_DeletedAt",
                table: "RequestComments");

            migrationBuilder.DropIndex(
                name: "IX_RequestCategories_DeletedAt",
                table: "RequestCategories");

            migrationBuilder.DropIndex(
                name: "IX_RequestAttachments_DeletedAt",
                table: "RequestAttachments");
        }
    }
}
