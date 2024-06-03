using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Home_Loan_App.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]

        //[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabetic characters are allowed.")]
        public string Name { get; set; }

        [Required]
        [Range(0,1000000,ErrorMessage ="Gross Salary must be positive")]
        [DisplayName("Gross Salary")]
        public double GrossSalary { get; set; }
        [Required]
        [Range(0,1200,ErrorMessage ="Credit Score ranges from 0 to 1200")]
        [DisplayName("Credit Score")]
        public int CreditScore { get; set; }
        [Required]
        [Range(0,5000000,ErrorMessage ="Purchase price is positive")]
        [DisplayName("Purchase Price")]
        public double PurchasePrice { get; set; }
        [DisplayName("Created Date & Time")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [DisplayName("Loan Application Status")]
        public string Loan_Application_Status { get; set; }

        [DisplayName("Maximum Installment Amount")]
        public double Maximum_Installment_Amount { get; set; }
        [DisplayName("Percentage Home Loan Granted")]

        public double Percentage_Home_Loan_Granted { get; set; }
        [DisplayName("Deposit Required")]
        public double Deposit_Required { get; set; }
        [DisplayName("Monthly Installment")]
        public double Monthly_Installment { get; set; }

        //METHODS
        public double MaximumInstallment()

        {
            return Math.Round(GrossSalary * 0.3,2);
        }
        public double LoanPercentage = 0;

        public double PercentageApproved()
        {
            if (CreditScore >= 800 && CreditScore <= 1200)
            {
                LoanPercentage = 100;
            }
            if (CreditScore >= 750 && CreditScore <= 799)
            {
                LoanPercentage = 97.5;
            }
            if (CreditScore >= 700 && CreditScore <= 749)
            {
                LoanPercentage = 95;
            }
            if (CreditScore >= 650 && CreditScore <= 699)
            {
                LoanPercentage = 90;
            }
            if (CreditScore >= 600 && CreditScore <= 649)
            {
                LoanPercentage = 85;
            }
            if (CreditScore >= 550 && CreditScore <= 599)
            {
                LoanPercentage = 80;
            }
            return LoanPercentage;
        }
        public double Deposit()
        {
            return (100 - LoanPercentage) * PurchasePrice / 100;
        }
        public double MonthlyInstallment()
        {
            double monthlyInstall = Math.Round((PurchasePrice - Deposit()) * 0.00785, 2);
            return monthlyInstall;
        }
        public string LoanStatus()
        {
            string status = "";
            if (MonthlyInstallment() <= 0.3 * GrossSalary && CreditScore >= 550 && CreditScore <= 1200)
            {
                status = "Granted";
            }
            else
            {
                status = "Declined";
            }
            return status;
        }

        public double MaxAffordLoan()
        {
            return MaximumInstallment() / (PercentageApproved() * 0.00785 / 100);
        }
        public double LoanAmountGranted()
        {
            return PercentageApproved() * PurchasePrice / 100;
        }


    }
}
