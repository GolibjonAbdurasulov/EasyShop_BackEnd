using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_customer_id",
                table: "orders");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_clients_customer_id",
                table: "orders",
                column: "customer_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_clients_customer_id",
                table: "orders");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_customer_id",
                table: "orders",
                column: "customer_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
