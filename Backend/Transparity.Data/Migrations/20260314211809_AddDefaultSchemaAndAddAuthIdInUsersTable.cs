using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transparity.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultSchemaAndAddAuthIdInUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "core");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "UserInfos",
                newName: "UserInfos",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Roles",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "RequestStatuses",
                newName: "RequestStatuses",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "Requests",
                newName: "Requests",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "RequestLevels",
                newName: "RequestLevels",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "RequestHistory",
                newName: "RequestHistory",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "RequestFields",
                newName: "RequestFields",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "RequestComments",
                newName: "RequestComments",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "RequestCategories",
                newName: "RequestCategories",
                newSchema: "core");

            migrationBuilder.RenameTable(
                name: "RequestAttachments",
                newName: "RequestAttachments",
                newSchema: "core");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthId",
                schema: "core",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "core",
                table: "UserInfos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthId",
                schema: "core",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "core",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "UserInfos",
                schema: "core",
                newName: "UserInfos");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "core",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "RequestStatuses",
                schema: "core",
                newName: "RequestStatuses");

            migrationBuilder.RenameTable(
                name: "Requests",
                schema: "core",
                newName: "Requests");

            migrationBuilder.RenameTable(
                name: "RequestLevels",
                schema: "core",
                newName: "RequestLevels");

            migrationBuilder.RenameTable(
                name: "RequestHistory",
                schema: "core",
                newName: "RequestHistory");

            migrationBuilder.RenameTable(
                name: "RequestFields",
                schema: "core",
                newName: "RequestFields");

            migrationBuilder.RenameTable(
                name: "RequestComments",
                schema: "core",
                newName: "RequestComments");

            migrationBuilder.RenameTable(
                name: "RequestCategories",
                schema: "core",
                newName: "RequestCategories");

            migrationBuilder.RenameTable(
                name: "RequestAttachments",
                schema: "core",
                newName: "RequestAttachments");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserInfos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
