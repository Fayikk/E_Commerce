using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceForUdemy_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationForPublisherField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "OrderDetails",
                newName: "Publisher");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Publisher",
                table: "OrderDetails",
                newName: "Size");
        }
    }
}
