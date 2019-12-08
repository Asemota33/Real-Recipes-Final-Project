using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Comp229_301052117_Assign3.Models;


namespace Comp229_301052117_Assign3.Controllers
{
    public class OrderController : Controller
    {
        [HttpPost]
        public IActionResult CustomerForm(Customer cust)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Order", "Crud");

            }
            else
            {
                return View("CustomerForm", cust);
            }
        }
    }
}