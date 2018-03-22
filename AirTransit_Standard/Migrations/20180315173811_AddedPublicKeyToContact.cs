using Microsoft.EntityFrameworkCore.Migrations;

namespace AirTransit_Core.Migrations
{
    public partial class AddedPublicKeyToContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublicKey",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicKey",
                table: "Contacts");
        }
    }
}
