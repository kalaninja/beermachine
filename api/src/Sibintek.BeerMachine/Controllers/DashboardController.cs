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
                TopSavers = new List<CustomerModel>
                {
                    new CustomerModel {Name = "Лев Тихонов", Balance = random.Next(0, 11)},
                    new CustomerModel {Name = "Кирилл Зыков", Balance = random.Next(0, 11)},
                    new CustomerModel {Name = "Максим Шарапов", Balance = random.Next(0, 11)},
                    new CustomerModel {Name = "Фёдор Евдокимов", Balance = random.Next(0, 11)},
                    new CustomerModel {Name = "Антон Куликов", Balance = random.Next(0, 11)},
                    new CustomerModel {Name = "Гавриил Коновалов", Balance = random.Next(0, 11)},
                    new CustomerModel {Name = "Станислав Селиверстов", Balance = random.Next(0, 11)},
                    new CustomerModel {Name = "Михаил Кононов", Balance = random.Next(0, 11)},
                    new CustomerModel {Name = "Анатолий Дроздов", Balance = random.Next(0, 11)},
                    new CustomerModel {Name = "Олег Никитин", Balance = random.Next(0, 11)},
                }.OrderByDescending(x => x.Balance).ToList(),
                TopSpenders = new List<CustomerModel>
                {
                    new CustomerModel {Name = "Александр Дроздов", Balance = random.Next(1, 21)},
                    new CustomerModel {Name = "Дмитрий Блинов", Balance = random.Next(1, 21)},
                    new CustomerModel {Name = "Тимофей Якушев", Balance = random.Next(1, 21)},
                    new CustomerModel {Name = "Борис Зиновьев", Balance = random.Next(1, 21)},
                    new CustomerModel {Name = "Вадим Фомин", Balance = random.Next(1, 21)},
                    new CustomerModel {Name = "Денис Устинов", Balance = random.Next(1, 21)},
                    new CustomerModel {Name = "Алексей Белов", Balance = random.Next(1, 21)},
                    new CustomerModel {Name = "Эдуард Тимофеев", Balance = random.Next(1, 21)},
                    new CustomerModel {Name = "Григорий Федотов", Balance = random.Next(1, 21)},
                    new CustomerModel {Name = "Даниил Королёв", Balance = random.Next(1, 21)},
                }.OrderByDescending(x => x.Balance).ToList(),

                EarnedLostDataSet = Enumerable.Range(0, 2).Select(x => random.Next(0, 100)).ToArray(),
                SpendAccumulateDataSet = Enumerable.Range(0, 2).Select(x => random.Next(0, 100)).ToArray(),
                
                Transactions = Enumerable.Range(0,10).Select(x=> new TransactionModel
                {
                   WalletId = Guid.NewGuid().ToString("N"),
                   Sum = random.Next(0,100),
                   Type = (TransactionType)random.Next(0,2),
                   TransactionDate = DateTime.Now
                }).ToList()
            };

            return Json(mockData);
        }
    }
}