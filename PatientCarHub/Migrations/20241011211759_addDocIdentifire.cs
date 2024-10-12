using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientCarHub.Migrations
{
    /// <inheritdoc />
    public partial class addDocIdentifire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentifierPath",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccountAcsepted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentifierPath",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "IsAccountAcsepted",
                table: "AspNetUsers");
        }
    }
}
