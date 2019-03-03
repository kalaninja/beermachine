using System;
using System.Collections.Generic;
using System.Globalization;
using Sibintek.BeerMachine.DataContracts;
using Sibintek.BeerMachine.Domain;

namespace Sibintek.BeerMachine.Services
{
    public class SessionService: ISessionService
    {
        private readonly IReadOnlyList<Session> _program;

        public SessionService()
        {
            _program = CreateProgram();
        }

        public IReadOnlyList<Session> GetProgram()
        {
            return _program;
        }
        
        private static IReadOnlyList<Session> CreateProgram()
        {   
            return new List<Session>
            {
                // 2019-03-14
                new Session("Сбор гостей",
                    DateTime.Parse("2019-03-14 9:00",CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 9:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),


                new Session("Приветствие гостей, вступительное слово",
                    DateTime.Parse("2019-03-14 9:30",CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 9:35",CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                //Пленарная сессия руководителей ССП ЦАУК 

                new Session("А.С. Петров: Комплексная цифровизация производственной деятельности ПАО \"НК \"Роснефть\"",
                    DateTime.Parse("2019-03-14 9:35", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 9:50", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session(
                    "В.В. Гайлунь: Концепция управления эффективностью в области ИТ, ПАМиКК и ИБ на всех стадиях жизненного цикла ИТ потребности",
                    DateTime.Parse("2019-03-14 9:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 10:05", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session(
                    "А.С. Филиппов, Т.И. Комкова, Ж.И. Елоза, С.П. Горячев: Контроль эффективности производственных процессов",
                    DateTime.Parse("2019-03-14 10:05", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 10:25", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("В.Н. Перевозный: Реализация Стратегии развития ООО ИК «СИБИНТЕК» 2022. Первые результаты",
                    DateTime.Parse("2019-03-14 10:25", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 10:40", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Е.В. Мартынова: Программа СЛОН: взаимодействие ЦАУК и ОГ",
                    DateTime.Parse("2019-03-14 10:40", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 10:50", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                //

                new Session("Кофе-брейк",
                    DateTime.Parse("2019-03-14 10:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 11:10", CultureInfo.InvariantCulture),
                    Array.Empty<Room>()),

                //Пленарная сессия руководителей от бизнеса

                new Session("Программа цифровизации РиД. Существующие и реализованные решения",
                    DateTime.Parse("2019-03-14 11:10", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 11:40", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Цифровой нефтеперерабатывающий завод",
                    DateTime.Parse("2019-03-14 11:40", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 12:10", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),


                new Session(" Примеры проектов региональных продаж",
                    DateTime.Parse("2019-03-14 12:10", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 12:40", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),


                //

                new Session("Обед",
                    DateTime.Parse("2019-03-14 12:40", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:10", CultureInfo.InvariantCulture),
                    Array.Empty<Room>()),

                //

                new Session("И.Г. Воронина: Внедрение типового решения для блоков РиД и Газ на базе SAP",
                    DateTime.Parse("2019-03-14 14:10", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:20", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),


                new Session("И.А. Баранов, А.М. Мальцев: Результаты внедрения корпоративного шаблона Службы Снабжения",
                    DateTime.Parse("2019-03-14 14:20", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("А.А. Куликов: Внедрение шаблона РУНО",
                    DateTime.Parse("2019-03-14 14:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:40", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("И.С. Якимов: КСЭД и Система обмена регулярной отчетностью между ОГ и ЦАУК",
                    DateTime.Parse("2019-03-14 14:40", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:50", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session(
                    "Р.Е. Калмыков, А.А. Гончаренко, И.В. Мерцалов, В.В. Раченков, В.В. Дмитриев: Разъяснения по порядку взаимодействия ОГ и СИБИНТЕК в формате ЕОС ИТ. ",
                    DateTime.Parse("2019-03-14 14:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 15:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                //

                new Session("Кофе-брейк, свободное общение",
                    DateTime.Parse("2019-03-14 15:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 19:30", CultureInfo.InvariantCulture),
                    Array.Empty<Room>()),

                //

                new Session("Е.В. Мартынова: Стратегическая сессия по дизайн мышлению: взаимодействие ЦАУК и ОГ",
                    DateTime.Parse("2019-03-14 15:45", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 18:45", CultureInfo.InvariantCulture),
                    new[]
                    {
                        Room.ConfHallA, Room.ConfHallB, Room.ConfHallC, Room.Don, Room.Danube, Room.Amur, Room.Neva
                    }),

                new Session("Гала-ужин",
                    DateTime.Parse("2019-03-14 19:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 23:00", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("",
                    DateTime.Parse("2019-03-14 14:20", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),
                
                
                // 2019-03-15
                
                
                new Session("Сбор гостей, открытие 2-го дня",
                    DateTime.Parse("2019-03-15 9:00", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 9:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),
                
                new Session("В.В. Сычев: Концепция центра по управлению ИБ, 187-ФЗ",
                    DateTime.Parse("2019-03-15 9:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),
                
                new Session("М.И. Тищенко, М.Н. Токарь: Процесс планирования и реализации потребностей по направлениям ИТ, ИБ и ПАМиКК",
                    DateTime.Parse("2019-03-15 9:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),
                
                new Session("Д.C. Решетников, В.О. Баврин: Итоги запуска корпоративного ЦОД и подход к организации сети региональных ЦОД",
                    DateTime.Parse("2019-03-15 9:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),
                
                new Session("Ж.Р. Лория: Цифровая лаборатория",
                    DateTime.Parse("2019-03-15 9:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:30", CultureInfo.InvariantCulture),
                    new[] {Room.Rhine}),
                
                new Session("А.А. Андриец, Г.П. Яцков: Контроль исполнения Программы обеспечения измерений внутри-производственных",
                    DateTime.Parse("2019-03-15 9:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:30", CultureInfo.InvariantCulture),
                    new[] {Room.Don}),
                
                new Session("А.В. Парасына, С.П. Горячев: Перспективные технологии на базе РН-Предикс",
                    DateTime.Parse("2019-03-15 9:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:30", CultureInfo.InvariantCulture),
                    new[] {Room.Danube}),
                
                new Session("А.Е. Рыжов: Повышение устойчивости ИТ ОГ в условиях НВК",
                    DateTime.Parse("2019-03-15 9:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:30", CultureInfo.InvariantCulture),
                    new[] {Room.Amur}),
                
                //
                new Session("Кофе-брейк",
                    DateTime.Parse("2019-03-15 10:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:50", CultureInfo.InvariantCulture),
                    Array.Empty<Room>()),
                
                
                //Секция ДОБЫЧА
                
                new Session("Программа цифровизации РиД - обзорная презентация Программа цифровизации Блока Газ - обзорная презентация",
                    DateTime.Parse("2019-03-15 10:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:20", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),
                
                new Session("Стратегия развития цифровизации службы Бурения ПАО «НК «Роснефть»",
                    DateTime.Parse("2019-03-15 11:20", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),
                
                new Session("Переход на комплексные услуги - «ГеоПАК»",
                    DateTime.Parse("2019-03-15 11:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:40", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),
                
                new Session("Подход к реализации цифровых инициатив - вызовы и пути решения (по результатам опроса ОГ о готовности к цифровизации)",
                    DateTime.Parse("2019-03-15 11:40", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:00", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),
                
                new Session("Инфраструктурная поддержка цифровизации",
                    DateTime.Parse("2019-03-15 12:00", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:15", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),
                
                new Session("Модель взаимодействия с Цифровым Кластером",
                    DateTime.Parse("2019-03-15 12:15", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:25", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),
                
                new Session("Программа мобильности - развитие системы удаленного доступа к корпоративным информационным системам Компании",
                    DateTime.Parse("2019-03-15 12:25", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:35", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),
                
                new Session("Управление ИТ-активами",
                    DateTime.Parse("2019-03-15 12:35", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:45", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),
                
                new Session("Вопросы и ответы по автоматизации, информатизации и цифровизации ОГ РиД/Газ",
                    DateTime.Parse("2019-03-15 12:45", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 13:15", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),
                
                
                //Секция ПЕРЕРАБОТКА
                
                new Session("-Обзор программы цифровизации, основные тренды и топ 5 активностей. -Искусственный интеллект для промышленности (А.С.Дмитриев)",
                    DateTime.Parse("2019-03-15 10:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:10", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),
                
                new Session("Подход к реализации цифровых инициатив компании BP",
                    DateTime.Parse("2019-03-15 11:10", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:20", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),
                
                new Session("Непрерывный контроль и управление технологическим процессом (СУУТП)",
                    DateTime.Parse("2019-03-15 11:20", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:35", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),
                
                new Session("Внедрение системы удаленного доступа к КИС и ее развитие в регионах",
                    DateTime.Parse("2019-03-15 11:35", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:45", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),
                
                new Session("Перспективы развития программных роботов",
                    DateTime.Parse("2019-03-15 11:45", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:55", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),
                
                new Session("Применение Blockchain технологий в нефтепереработке и региональных продажах",
                    DateTime.Parse("2019-03-15 11:55", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:05", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),
                
                new Session("Методика ускоренной реализации цифровых инициатив на основе подходов интеллектуального анализа производственных данных: как создавать решения, которые нужны",
                    DateTime.Parse("2019-03-15 12:05", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:15", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),
                
                new Session("Тренды будудщего, потенциальные варианты применения в нефтепереработке",
                    DateTime.Parse("2019-03-15 12:15", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:55", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),
                
                new Session("Ответы на вопросы ОГ",
                    DateTime.Parse("2019-03-15 12:55", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 13:20", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),
                
                
                //Секция СБЫТ
                
                new Session("Обзор цифровых трендов и технологий в блоке",
                    DateTime.Parse("2019-03-15 10:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:10", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),
                
                new Session("Перспективы развития программных роботов",
                    DateTime.Parse("2019-03-15 11:10", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:20", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),
                
                new Session("Программа цифровизации и текущие планы - обзорная презентация ",
                    DateTime.Parse("2019-03-15 11:20", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:40", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),
                
                new Session("Применение BLOCKCHAIN технологий в региональных продаж",
                    DateTime.Parse("2019-03-15 11:40", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:50", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),
                
                new Session("Подход к реализации цифровых инициатив - вызовы и пути решения (по результатам опроса ОГ)",
                    DateTime.Parse("2019-03-15 11:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:35", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),
                
                new Session("Внедрение системы удаленного доступа к КИС и ее развитие в регионах",
                    DateTime.Parse("2019-03-15 12:35", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:45", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),
                
                new Session("Восстановление работоспособности АЗС",
                    DateTime.Parse("2019-03-15 12:45", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:55", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),
                
                new Session("Методика ускоренной реализации цифровых инициатив на основе подходов интеллектуального анализа производственных данных: как создавать решения, которые нужны",
                    DateTime.Parse("2019-03-15 12:55", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 13:05", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),
                
                new Session("Модель взаимодействия с Цифровым Кластером",
                    DateTime.Parse("2019-03-15 13:05", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 13:20", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),
                
                //
                
                new Session("Е.В. Мартынова: Результаты стратегической сессии (стратегический дешборд)",
                    DateTime.Parse("2019-03-15 13:20", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 13:35", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),
                
                
                new Session("Заключительное слово",
                    DateTime.Parse("2019-03-15 13:35", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 13:45", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),
                
                
                new Session("Обед",
                    DateTime.Parse("2019-03-15 13:45", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 15:15", CultureInfo.InvariantCulture),
                    Array.Empty<Room>()),
                
            }.AsReadOnly();
        }
    }
}