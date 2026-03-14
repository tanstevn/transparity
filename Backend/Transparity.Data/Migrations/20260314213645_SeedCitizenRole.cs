using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transparity.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCitizenRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: ["Id", "Name", "Description", "CreatedAt", "DeletedAt"],
                values: [2L, "Citizen", "Citizen Role", DateTime.UtcNow, null]
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Roles", keyColumn: "Id", keyValue: 2L);
        }
    }
}
