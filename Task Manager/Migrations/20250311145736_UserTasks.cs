using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Manager.Migrations
{
    /// <inheritdoc />
    public partial class UserTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
