using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Home_Loan_App.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrossSalary = table.Column<double>(type: "float", nullable: false),
                    CreditScore = table.Column<int>(type: "int", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
