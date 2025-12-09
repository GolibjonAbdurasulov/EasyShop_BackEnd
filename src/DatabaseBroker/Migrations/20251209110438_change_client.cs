using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class change_client : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "clients",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "clients",
                newName: "inn");

            migrationBuilder.AddColumn<string>(
                name: "client_full_name",
                table: "clients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "company_name",
                table: "clients",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "client_full_name",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "company_name",
                table: "clients");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "clients",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "inn",
                table: "clients",
                newName: "email");
        }
    }
}
