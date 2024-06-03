using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Home_Loan_App.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnsToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Deposit_Required",
                table: "Customers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Loan_Application_Status",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Maximum_Installment_Amount",
                table: "Customers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Monthly_Installment",
                table: "Customers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Percentage_Home_Loan_Granted",
                table: "Customers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deposit_Required",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Loan_Application_Status",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Maximum_Installment_Amount",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Monthly_Installment",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Percentage_Home_Loan_Granted",
                table: "Customers");
        }
    }
}
