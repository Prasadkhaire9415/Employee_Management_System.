using Employee_ManageMent_System.DAL;
using Employee_ManageMent_System.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;

namespace Employee_ManageMent_System.Controllers
{
    public class EmployeeController1 : Controller
    {
        private readonly Employee_DAL dal;
        public EmployeeController1(Employee_DAL dal)
        {
            this.dal = dal;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employees =new List<Employee>();
            try
             {
                employees = dal.GetAll();
            }
	        catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            

            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                TempData["errorMessage"] = "Model data invalid";
            }
            bool result=dal.Insert(employee);
            if (!result) 
            {
                TempData["errorMessage"] = "Unable to save the data";
                return View();
            }
            TempData["SuccessMessage"] = "Employee details saved";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                Employee employee = dal.GetById(id);
                if (employee.Id == 00)
                {
                    TempData["errorMessage"] = "Employee not found";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            catch ( Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data invalid";
                    return View();
                }

               bool result=dal.Update(employee);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to update the data";
                    return View();
                }
                TempData["SuccessMessage"] = "Employee details updated";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
		[HttpGet]
		public IActionResult Delete(int id)
		{
			try
			{
				Employee employee = dal.GetById(id);
				if (employee.Id == 00)
				{
					TempData["errorMessage"] = "Employee not found";
					return RedirectToAction("Index");
				}
				return View(employee);
			}
			catch (Exception ex)
			{

				TempData["errorMessage"] = ex.Message;
				return View();
			}
		}
		[HttpPost]
		public IActionResult Delete(Employee employee)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					TempData["errorMessage"] = "Model data invalid";
					return View();
				}

				bool result = dal.Delete(employee.Id);
				if (!result)
				{
					TempData["errorMessage"] = "Unable to delete the data";
					return View();
				}
				TempData["SuccessMessage"] = "Employee details deleted";
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{

				TempData["errorMessage"] = ex.Message;
				return View();
			}
		}
	}
}
