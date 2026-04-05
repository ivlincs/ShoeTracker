using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoeTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsArchivedToShoe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Shoes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Shoes");
        }
    }
}
