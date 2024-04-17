using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VShop.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)" +
                " values('Caderno',7.5, 'Caderno', 10, 'caderno1.jpg',1)");

            migrationBuilder.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)" +
                " values('Lápis',3.25, 'Lápis preto', 20, 'lapis1.jpg',1)");

            migrationBuilder.Sql("Insert into Products(Name, Price, Description, Stock, ImageUrl, CategoryId)" +
                " values('CLips',0.80, 'CLips para papel', 50, 'clips1.jpg',2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from products");
        }
    }
}
