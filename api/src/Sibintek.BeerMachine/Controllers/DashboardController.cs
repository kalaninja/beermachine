using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sibintek.BeerMachine.Models;

namespace Sibintek.BeerMachine.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Cart()
        {
            return View();
        }

        public JsonResult Data()
        {
            var random = new Random();
            var mockData = new DashboardDataModel
            {
                TopSavers = new List<Customer>
                {
                    new Customer {Name = "Лев Тихонов", Balance = random.Next(0,11)},
                    new Customer {Name = "Кирилл Зыков", Balance = random.Next(0,11)},
                    new Customer {Name = "Максим Шарапов", Balance = random.Next(0,11)},
                    new Customer {Name = "Фёдор Евдокимов", Balance = random.Next(0,11)},
                    new Customer {Name = "Антон Куликов", Balance = random.Next(0,11)},
                    new Customer {Name = "Гавриил Коновалов", Balance = random.Next(0,11)},
                    new Customer {Name = "Станислав Селиверстов", Balance = random.Next(0,11)},
                    new Customer {Name = "Михаил Кононов", Balance = random.Next(0,11)},
                    new Customer {Name = "Анатолий Дроздов", Balance = random.Next(0,11)},
                    new Customer {Name = "Олег Никитин", Balance = random.Next(0,11)},

                }.OrderByDescending(x=>x.Balance).ToList(),
                TopSpenders = new List<Customer>
                {
                    new Customer {Name = "Александр Дроздов", Balance = random.Next(1,21)},
                    new Customer {Name = "Дмитрий Блинов", Balance = random.Next(1,21)},
                    new Customer {Name = "Тимофей Якушев", Balance = random.Next(1,21)},
                    new Customer {Name = "Борис Зиновьев", Balance = random.Next(1,21)},
                    new Customer {Name = "Вадим Фомин", Balance = random.Next(1,21)},
                    new Customer {Name = "Денис Устинов", Balance = random.Next(1,21)},
                    new Customer {Name = "Алексей Белов", Balance = random.Next(1,21)},
                    new Customer {Name = "Эдуард Тимофеев", Balance = random.Next(1,21)},
                    new Customer {Name = "Григорий Федотов", Balance = random.Next(1,21)},
                    new Customer {Name = "Даниил Королёв", Balance = random.Next(1,21)},
                }.OrderByDescending(x=>x.Balance).ToList()
            };

            return Json(mockData);
        }
    }
}