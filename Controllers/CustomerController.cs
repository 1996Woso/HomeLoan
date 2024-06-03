using Home_Loan_App.Data;
using Home_Loan_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Core.ExcelPackage;
using System.IO;
using System.Linq;

namespace Home_Loan_App.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CustomerController(ApplicationDbContext db)
        {
            _db = db;     
        }
        public IActionResult Index()
        {
            IEnumerable<Customer> objCustomerList = _db.Customers;
            return View(objCustomerList);
        }

        //BUTTON TO ADD NEW CUSTOMERS

        //GET
        public IActionResult Create()
        { 
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer obj)
        {
            double maximumInstallment = obj.MaximumInstallment();
            double PercentageApproved = obj.PercentageApproved();
            double deposit = obj.Deposit();
            double monthlyInstallment = obj.MonthlyInstallment();
            string loanStatus = obj.LoanStatus();
            if (loanStatus == "Declined")
            {
                PercentageApproved = 0;
                deposit = 0;
                monthlyInstallment = 0;
            }

            obj.Maximum_Installment_Amount = maximumInstallment;
            obj.Percentage_Home_Loan_Granted = PercentageApproved;
            obj.Deposit_Required = deposit;
            obj.Monthly_Installment = monthlyInstallment;
            obj.Loan_Application_Status = loanStatus;
            


            //ViewBag.MaximumInstallment = maximumInstallment;
            //ViewBag.LoanPercentage = PercentageApproved;
            //ViewBag.Deposit = deposit;
            //ViewBag.MonthlyInstallment = monthlyInstallment;
            //ViewBag.LoanStatus = loanStatus;


            //if (ModelState.IsValid)
            //{
				_db.Customers.Add(obj);
                _db.SaveChanges();
				// Redirect to another action or view after successful submission
				return RedirectToAction("Index");
            //}
			// If ModelState is not valid, return the view with validation errors
			return RedirectToAction("Create");
        }


        //EDIT BUTTON

        //GET
        public IActionResult Edit(int? id)//right-click to create a View
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var customerFromDb = _db.Customers.Find(id);
   
            if (customerFromDb == null)
            {
                return NotFound();
            }
            return View(customerFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer obj)
        {
            double maximumInstallment = obj.MaximumInstallment();
            double PercentageApproved = obj.PercentageApproved();
            double deposit = obj.Deposit();
            double monthlyInstallment = obj.MonthlyInstallment();
            string loanStatus = obj.LoanStatus();

            obj.Maximum_Installment_Amount = maximumInstallment;
            obj.Percentage_Home_Loan_Granted = PercentageApproved;
            obj.Deposit_Required = deposit;
            obj.Monthly_Installment = monthlyInstallment;
            obj.Loan_Application_Status = loanStatus;

            //if (ModelState.IsValid)
            //{
            _db.Customers.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //DELETE BUTTON

        //GET
        public IActionResult Delete(int? id)//right-click to create a View
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var customerFromDb = _db.Customers.Find(id);

            if (customerFromDb == null)
            {
                return NotFound();
            }
            return View(customerFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Customer obj)
        {
            if (ModelState.IsValid)
            {
                _db.Customers.Remove(obj);//Removes existing row from the database
                _db.SaveChanges();//Goes to the database and save the changes(store new row)
                return RedirectToAction("Index");

        }
            return View(obj);

        }

        //FIND LOAN STATUS

        //GET
        public IActionResult Status(int? id)//right-click to create a View
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var customerFromDb = _db.Customers.Find(id);

            if (customerFromDb == null)
            {
                return NotFound();
            }
            string Name = customerFromDb.Name;
            double maximumInstallment = customerFromDb.MaximumInstallment();
            double PercentageApproved = customerFromDb.PercentageApproved();
            double deposit = customerFromDb.Deposit();
            double monthlyInstallment = customerFromDb.MonthlyInstallment();
            string loanStatus = customerFromDb.LoanStatus();
            double LoanAmaountGranted = customerFromDb.LoanAmountGranted();
			//When status is declined because loan is too high
			double MaxAffordLoan = customerFromDb.MaxAffordLoan();
            double MaxDeposit = MaxAffordLoan * (100 - PercentageApproved) / 100;
            double MaxMonthlyInstall = (MaxAffordLoan - MaxDeposit) * 0.00785;

			//Vew Bags

			ViewBag.Name = Name;
            ViewBag.MaximumInstallment = maximumInstallment;
            ViewBag.LoanPercentage = PercentageApproved;
            ViewBag.Deposit = deposit;
            ViewBag.MonthlyInstallment = monthlyInstallment;
            ViewBag.LoanStatus = loanStatus;
            ViewBag.MaxAffordLoan = MaxAffordLoan;
            ViewBag.MaximumDeposit = MaxDeposit;
            ViewBag.MaximumMonthlyInstallment = MaxMonthlyInstall;
            ViewBag.LoanAmaountGranted = LoanAmaountGranted;


			return View("Status", customerFromDb);

        }
        public IActionResult Search(string sStatus)
        {
            var status = _db.Customers.Where(x => x.Loan_Application_Status == sStatus);
            return View(status); 
        }
        public IActionResult DownloadExcel()
        {
            var customers = _db.Customers.ToList();
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Customers"); // Name your worksheet "Customers"
                worksheet.Cells.LoadFromCollection(customers, true);
                byte[] fileContents = package.GetAsByteArray();
                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customers.xlsx");
            }
        }
    }
}
