using System.Collections.Generic;
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
            var mockData = new DashboardDataModel
            {
                TopSavers = new List<Customer>
                {
                    new Customer {Name = "Лев Тихонов", Balance = 10.0m},
                    new Customer {Name = "Кирилл Зыков", Balance = 9.0m},
                    new Customer {Name = "Максим Шарапов", Balance = 8.0m},
                    new Customer {Name = "Фёдор Евдокимов", Balance = 7.0m},
                    new Customer {Name = "Антон Куликов", Balance = 6.0m},
                    new Customer {Name = "Гавриил Коновалов", Balance = 5.0m},
                    new Customer {Name = "Станислав Селиверстов", Balance = 4.0m},
                    new Customer {Name = "Михаил Кононов", Balance = 3.0m},
                    new Customer {Name = "Анатолий Дроздов", Balance = 2.0m},
                    new Customer {Name = "Олег Никитин", Balance = 1.0m},

                },
                TopSpenders = new List<Customer>
                {
                    new Customer {Name = "Александр Дроздов", Balance = 10.0m},
                    new Customer {Name = "Дмитрий Блинов", Balance = 9.0m},
                    new Customer {Name = "Тимофей Якушев", Balance = 8.0m},
                    new Customer {Name = "Борис Зиновьев", Balance = 7.0m},
                    new Customer {Name = "Вадим Фомин", Balance = 6.0m},
                    new Customer {Name = "Денис Устинов", Balance = 5.0m},
                    new Customer {Name = "Алексей Белов", Balance = 4.0m},
                    new Customer {Name = "Эдуард Тимофеев", Balance = 3.0m},
                    new Customer {Name = "Григорий Федотов", Balance = 2.0m},
                    new Customer {Name = "Даниил Королёв", Balance = 1.0m},
                }
            };

            return Json(mockData);
        }
    }
}