using System;
using System.Collections.Generic;
using Entity.Models.Common;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "file_model",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "text", nullable: true),
                    content_type = table.Column<string>(type: "text", nullable: true),
                    path = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_model", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "food_product_tags",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_product_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "household_product_tags",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_product_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "oil_product_tags",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oil_product_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "translations",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    uz = table.Column<string>(type: "text", nullable: true),
                    en = table.Column<string>(type: "text", nullable: true),
                    ru = table.Column<string>(type: "text", nullable: true),
                    ger = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_translations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<int>(type: "integer", nullable: false),
                    IsSigned = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "water_and_drinks_tags",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_water_and_drinks_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "food_product_categories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    food_produc_category_name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    food_product_image_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_product_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_food_product_categories_file_model_food_product_image_id",
                        column: x => x.food_product_image_id,
                        principalTable: "file_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "household_product_categories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    house_hold_category_name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    house_hold_category_image_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_product_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_household_product_categories_file_model_house_hold_category~",
                        column: x => x.house_hold_category_image_id,
                        principalTable: "file_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "main_product_categories",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    main_category_name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    main_category_image_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_main_product_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_main_product_categories_file_model_main_category_image_id",
                        column: x => x.main_category_image_id,
                        principalTable: "file_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    products_id = table.Column<List<long>>(type: "bigint[]", nullable: true),
                    customer_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart", x => x.id);
                    table.ForeignKey(
                        name: "FK_cart_users_customer_id",
                        column: x => x.customer_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    products_id = table.Column<List<long>>(type: "bigint[]", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric", nullable: false),
                    order_status = table.Column<int>(type: "integer", nullable: false),
                    delivery_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    customer_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_users_customer_id",
                        column: x => x.customer_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "food_products",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    food_category_id = table.Column<long>(type: "bigint", nullable: false),
                    tag_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    about = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    product_image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    main_category_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_food_products_file_model_product_image_id",
                        column: x => x.product_image_id,
                        principalTable: "file_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_food_products_food_product_categories_food_category_id",
                        column: x => x.food_category_id,
                        principalTable: "food_product_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_food_products_food_product_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "food_product_tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_food_products_main_product_categories_main_category_id",
                        column: x => x.main_category_id,
                        principalTable: "main_product_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "household_products",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hose_hold_category_id = table.Column<long>(type: "bigint", nullable: false),
                    tag_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    about = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    product_image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    main_category_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_household_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_household_products_file_model_product_image_id",
                        column: x => x.product_image_id,
                        principalTable: "file_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_household_products_household_product_categories_hose_hold_c~",
                        column: x => x.hose_hold_category_id,
                        principalTable: "household_product_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_household_products_household_product_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "household_product_tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_household_products_main_product_categories_main_category_id",
                        column: x => x.main_category_id,
                        principalTable: "main_product_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "oil_products",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    about = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    product_image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    main_category_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oil_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_oil_products_file_model_product_image_id",
                        column: x => x.product_image_id,
                        principalTable: "file_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oil_products_main_product_categories_main_category_id",
                        column: x => x.main_category_id,
                        principalTable: "main_product_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oil_products_oil_product_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "oil_product_tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "water_and_drinks",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_id = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    about = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    product_image_id = table.Column<Guid>(type: "uuid", nullable: false),
                    main_category_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_water_and_drinks", x => x.id);
                    table.ForeignKey(
                        name: "FK_water_and_drinks_file_model_product_image_id",
                        column: x => x.product_image_id,
                        principalTable: "file_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_water_and_drinks_main_product_categories_main_category_id",
                        column: x => x.main_category_id,
                        principalTable: "main_product_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_water_and_drinks_water_and_drinks_tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "water_and_drinks_tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cart_customer_id",
                table: "cart",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_product_categories_food_product_image_id",
                table: "food_product_categories",
                column: "food_product_image_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_products_food_category_id",
                table: "food_products",
                column: "food_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_products_main_category_id",
                table: "food_products",
                column: "main_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_products_product_image_id",
                table: "food_products",
                column: "product_image_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_products_tag_id",
                table: "food_products",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_household_product_categories_house_hold_category_image_id",
                table: "household_product_categories",
                column: "house_hold_category_image_id");

            migrationBuilder.CreateIndex(
                name: "IX_household_products_hose_hold_category_id",
                table: "household_products",
                column: "hose_hold_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_household_products_main_category_id",
                table: "household_products",
                column: "main_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_household_products_product_image_id",
                table: "household_products",
                column: "product_image_id");

            migrationBuilder.CreateIndex(
                name: "IX_household_products_tag_id",
                table: "household_products",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_main_product_categories_main_category_image_id",
                table: "main_product_categories",
                column: "main_category_image_id");

            migrationBuilder.CreateIndex(
                name: "IX_oil_products_main_category_id",
                table: "oil_products",
                column: "main_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_oil_products_product_image_id",
                table: "oil_products",
                column: "product_image_id");

            migrationBuilder.CreateIndex(
                name: "IX_oil_products_tag_id",
                table: "oil_products",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_translations_code",
                table: "translations",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_water_and_drinks_main_category_id",
                table: "water_and_drinks",
                column: "main_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_water_and_drinks_product_image_id",
                table: "water_and_drinks",
                column: "product_image_id");

            migrationBuilder.CreateIndex(
                name: "IX_water_and_drinks_tag_id",
                table: "water_and_drinks",
                column: "tag_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "food_products");

            migrationBuilder.DropTable(
                name: "household_products");

            migrationBuilder.DropTable(
                name: "oil_products");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "translations");

            migrationBuilder.DropTable(
                name: "water_and_drinks");

            migrationBuilder.DropTable(
                name: "food_product_categories");

            migrationBuilder.DropTable(
                name: "food_product_tags");

            migrationBuilder.DropTable(
                name: "household_product_categories");

            migrationBuilder.DropTable(
                name: "household_product_tags");

            migrationBuilder.DropTable(
                name: "oil_product_tags");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "main_product_categories");

            migrationBuilder.DropTable(
                name: "water_and_drinks_tags");

            migrationBuilder.DropTable(
                name: "file_model");
        }
    }
}
