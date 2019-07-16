using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        // GET: /<controller>/
        public string Index()
        {
            return _employeeRepository.GetEmployee(1).Name;
        }

        public ViewResult Details()
        {
            Employee model = _employeeRepository.GetEmployee(1);

            ViewBag.PageTitle = "Employee Details";

            //デフォルトでは規約に従って自分のコントローラー(Home)のアクション(Details)のViewに返す
            return View(model);

            //規約以外のファイルを指定する。拡張子もいらない。拡張子つけたらエラーになる。
            //return View("Test", model);

            //さらに、任意のフォルダの場合。拡張子が必要
            //絶対パスは/MyViews...でもMyViewsでも~/MyViewsでもよい。
            //プロジェクトルートが起点。
            //return View("MyViews/Test.cshtml", model);

            //相対パスでさかのぼることもできる
            //return View("../../MyViews/Test");
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = _employeeRepository.Add(employee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }
    }
}
