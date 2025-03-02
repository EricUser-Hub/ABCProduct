using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductEntity",
                columns: table => new
                {
                    DIN = table.Column<string>(type: "varchar(8)", nullable: false),
                    Name = table.Column<string>(type: "varchar(30)", nullable: true),
                    Shape = table.Column<string>(type: "varchar(100)", nullable: true),
                    Strength = table.Column<string>(type: "varchar(100)", nullable: true),
                    LegalStatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEntity", x => x.DIN);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductEntity");
        }
    }
}
