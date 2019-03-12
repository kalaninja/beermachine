using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sibintek.BeerMachine.DataContracts;
using Sibintek.BeerMachine.Domain;

// ReSharper disable StringLiteralTypo

namespace Sibintek.BeerMachine.Services
{
    public class SessionService : ISessionService
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
                new Session("Приветствие гостей. Открытие мероприятия.",
                    DateTime.Parse("2019-03-14 09:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 09:31", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Безопасность на мероприятии",
                    DateTime.Parse("2019-03-14 09:31", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 09:35", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Общая информация о мероприятии + информация о руководителях ССП",
                    DateTime.Parse("2019-03-14 09:35", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 09:39", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Представление А.С. Петрова и темы доклада",
                    DateTime.Parse("2019-03-14 09:39", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 09:40", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Комплексная цифровизация производственной деятельности ПАО \"НК \"Роснефть\"",
                    DateTime.Parse("2019-03-14 09:40", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 09:50", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Представление В.В. Гайлунь и темы доклада",
                    DateTime.Parse("2019-03-14 09:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 09:51", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session(
                    "Концепция управления эффективностью в области ИТ, ПАМиКК и ИБ на всех стадиях жизненного цикла ИТ потребности",
                    DateTime.Parse("2019-03-14 09:51", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 10:03", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Представление А.С. Филиппова, Т.И. Комкова, Ж.И. Елоза, С.П. Горячева и темы доклада",
                    DateTime.Parse("2019-03-14 10:03", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 10:06", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Контроль эффективности производственных процессов",
                    DateTime.Parse("2019-03-14 10:06", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 10:24", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Представление: - В.Н. Перевозный",
                    DateTime.Parse("2019-03-14 10:24", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 10:26", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Реализация Стратегии развития ООО ИК «СИБИНТЕК» 2022. Первые результаты.",
                    DateTime.Parse("2019-03-14 10:26", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 10:38", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Представление Е.В. Мартыновой",
                    DateTime.Parse("2019-03-14 10:38", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 10:39", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Программа СЛОН: взаимодействие ЦАУК и ОГ",
                    DateTime.Parse("2019-03-14 10:39", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 10:49", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Объявление кофе-брейка",
                    DateTime.Parse("2019-03-14 10:49", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 10:50", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Объявление о начале пленарной сессии руководителей от бизнеса",
                    DateTime.Parse("2019-03-14 11:10", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 11:11", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Объявление темы: Программа цифровизации РиД. Существующие и реализованные решения",
                    DateTime.Parse("2019-03-14 11:11", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 11:12", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Презентация результатов работ 2018 года",
                    DateTime.Parse("2019-03-14 11:12", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 11:20", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session(
                    "Демонстрация подхода к оперативному мониторингу и управлению месторождением через планшет ",
                    DateTime.Parse("2019-03-14 11:20", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 11:28", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Документальный фильм \"Цифровое месторождение \"Башнефть\"",
                    DateTime.Parse("2019-03-14 11:28", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 11:36", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session(
                    "Видео и рассказ о применении Продвинутой 3D аналитики для оперативного мониторинга и управления Цифровым месторождением",
                    DateTime.Parse("2019-03-14 11:36", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 11:44", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Объявление ",
                    DateTime.Parse("2019-03-14 11:44", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 11:45", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Объявление темы: Цифровой нефтеперерабатывающий завод",
                    DateTime.Parse("2019-03-14 11:45", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 11:46", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Презентация на 3-4 слайда (цели и задачи цифровизации)",
                    DateTime.Parse("2019-03-14 11:46", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 11:54", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Видео о цифровом заводе",
                    DateTime.Parse("2019-03-14 11:54", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 12:04", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("\"Живая\" трансляция работы дрона и компьютерного зрения",
                    DateTime.Parse("2019-03-14 12:04", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 12:14", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Объявление ",
                    DateTime.Parse("2019-03-14 12:14", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 12:15", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Объявление темы: Примеры проектов региональных продаж",
                    DateTime.Parse("2019-03-14 12:15", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 12:16", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Вводное слово (о проекте 371)",
                    DateTime.Parse("2019-03-14 12:16", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 12:18", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Презентация проекта Системы Электронной Пломбировк",
                    DateTime.Parse("2019-03-14 12:18", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 12:24", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Восстановление работы. Демонстрация использования планшета для обслуживания АЗС.",
                    DateTime.Parse("2019-03-14 12:24", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 12:31", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Заправка с использованием мобильного приложения",
                    DateTime.Parse("2019-03-14 12:31", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 12:37", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Объявление о завершении пленарной сессии руководителей от бизнеса",
                    DateTime.Parse("2019-03-14 12:37", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 12:38", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Объявление об обеде",
                    DateTime.Parse("2019-03-14 12:38", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 12:39", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Объявление о продолжении мероприятия (серия докладов)",
                    DateTime.Parse("2019-03-14 14:10", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:12", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Представление  И.Г. Ворониной",
                    DateTime.Parse("2019-03-14 14:12", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:13", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Внедрение типового решения для блоков РиД и Газ на базе SAP",
                    DateTime.Parse("2019-03-14 14:13", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:21", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Представление И.А. Баранова",
                    DateTime.Parse("2019-03-14 14:21", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:22", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Результаты внедрения корпоративного шаблона Службы Снабжения",
                    DateTime.Parse("2019-03-14 14:22", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Представление А.А. Куликова",
                    DateTime.Parse("2019-03-14 14:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:31", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Внедрение шаблона РУНО", DateTime.Parse(
                        "2019-03-14 14:31", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:39", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Представление Иванова Р. В.",
                    DateTime.Parse("2019-03-14 14:39", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:40", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("КСЭД и Система обмена регулярной отчетностью между ОГ и ЦАУК",
                    DateTime.Parse("2019-03-14 14:40", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:48", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Объявление ",
                    DateTime.Parse("2019-03-14 14:48", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:49", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session(
                    "Объявление о старте части мероприятия: Разъяснения по порядку взаимодействия ОГ и СИБИНТЕК в формате ЕОС ИТ",
                    DateTime.Parse("2019-03-14 14:49", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 14:50", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session(
                    "- Презентация подходов/ решений по внутренним вопросам- Презентация подходов/ решений в части поставок МТР - Презентация подходов/ решений в части поставок услуг/ проектов",
                    DateTime.Parse("2019-03-14 14:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 15:28", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session(
                    "Объявление завершении  части мероприятия: Разъяснения по порядку взаимодействия ОГ и СИБИНТЕК в формате ЕОС ИТ",
                    DateTime.Parse("2019-03-14 15:28", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 15:29", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Объявление кофе-брейке, свободное общение.",
                    DateTime.Parse("2019-03-14 15:29", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 15:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Вводная часть про дизайн-мышление",
                    DateTime.Parse("2019-03-14 15:40", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 16:10", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Вводная часть про дизайн-мышление",
                    DateTime.Parse("2019-03-14 16:10", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-14 18:45", CultureInfo.InvariantCulture),
                    new[] {Room.Rhine, Room.Don, Room.Danube, Room.Amur, Room.Neva}),


                // 2019-03-15
                new Session("Приветствие гостей. Открытие мероприятия.",
                    DateTime.Parse("2019-03-15 09:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 09:31", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),

                new Session("Концепция центра по управлению ИБ, 187-ФЗ",
                    DateTime.Parse("2019-03-15 09:31", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:29", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session("Процесс планирования и реализации потребностей по направлениям ИТ, ИБ и ПАМиКК",
                    DateTime.Parse("2019-03-15 09:31", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:31", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),

                new Session("Итоги запуска корпоративных ЦОД и развитие региональных ЦОД",
                    DateTime.Parse("2019-03-15 09:31", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:31", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),

                new Session("Цифровая лаборатория", DateTime.Parse("2019-03-15 09:31", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:31", CultureInfo.InvariantCulture),
                    new[] {Room.Rhine}),

                new Session("Контроль исполнения Программы обеспечения измерений внутри-произв. материальных потоков",
                    DateTime.Parse("2019-03-15 09:31", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:31", CultureInfo.InvariantCulture),
                    new[] {Room.Don}),

                new Session("Перспективные технологии на базе РН-Предикс",
                    DateTime.Parse("2019-03-15 09:31", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:31", CultureInfo.InvariantCulture),
                    new[] {Room.Danube}),

                new Session(" Повышение устойчивости ИТ ОГ в условиях НВК",
                    DateTime.Parse("2019-03-15 09:31", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:29", CultureInfo.InvariantCulture),
                    new[] {Room.Amur}),

                new Session("Объевление кофе-брейка",
                    DateTime.Parse("2019-03-15 10:29", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC}),


                new Session("Объявление о старте выступлений в секции  ДОБЫЧА: Нефтегазовое месторождение",
                    DateTime.Parse("2019-03-15 10:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:51", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),

                new Session("Программа цифровизации РиД - обзорная презентация",
                    DateTime.Parse("2019-03-15 10:51", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:06", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),

                new Session("Программа цифровизации Блока Газ - обзорная презентация",
                    DateTime.Parse("2019-03-15 11:06", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:21", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),

                new Session("Стратегия развития цифровизации службы Бурения",
                    DateTime.Parse("2019-03-15 11:21", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:31", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),

                new Session("Переход на комплексные услуги - «ГеоПАК»",
                    DateTime.Parse("2019-03-15 11:31", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:41", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),

                new Session(
                    "Подход к реализации цифровых инициатив - вызовы и пути решения (по результатам опроса ОГ о готовности к цифровизации)",
                    DateTime.Parse("2019-03-15 11:41", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:03", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),

                new Session("Инфраструктурная поддержка цифровизации",
                    DateTime.Parse("2019-03-15 12:03", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:18", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),

                new Session("Модель взаимодействия с Цифровым Кластером",
                    DateTime.Parse("2019-03-15 12:18", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),

                new Session(
                    "Программа мобильности - развитие системы удаленного доступа к корпоративным информационным системам Компании",
                    DateTime.Parse("2019-03-15 12:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:40", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),

                new Session("Управление ИТ-активами",
                    DateTime.Parse("2019-03-15 12:40", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:50", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),

                new Session("Вопросы и ответы по автоматизации, информатизации и цифровизации ОГ РиД/Газ",
                    DateTime.Parse("2019-03-15 12:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 13:15", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallB}),


                new Session(
                    "\"Объявление о старте выступлений в секции ПЕРЕРАБОТКА: Современное производство в нефтепереработке и нефтегазохимии",
                    DateTime.Parse("2019-03-15 10:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:51", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session(
                    "Обзор программы цифровизации, основные тренды и топ 5 активностей Искусственный интеллект для промышленности",
                    DateTime.Parse("2019-03-15 10:51", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:01", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session(
                    "Обзор программы цифровизации, основные тренды и топ 5 активностей Искусственный интеллект для промышленности",
                    DateTime.Parse("2019-03-15 11:01", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:11", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session("Подход к реализации цифровых инициатив компании BP",
                    DateTime.Parse("2019-03-15 11:11", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:18", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session("Подход к реализации цифровых инициатив компании BP",
                    DateTime.Parse("2019-03-15 11:18", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:25", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session("Непрерывный контроль и управление технологическим процессом (СУУТП)",
                    DateTime.Parse("2019-03-15 11:25", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:35", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session("Внедрение системы удаленного доступа к КИС и ее развитие в регионах",
                    DateTime.Parse("2019-03-15 11:35", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:45", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session("Перспективы развития программных роботов",
                    DateTime.Parse("2019-03-15 11:45", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:55", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session("Применение Blockchain технологий в нефтепереработке и региональных продажах",
                    DateTime.Parse("2019-03-15 11:55", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session(
                    "Методика ускоренной реализации цифровых инициатив на основе подходов интеллектуального анализа производственных данных: как создавать решения, которые нужны",
                    DateTime.Parse("2019-03-15 12:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:40", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session("Тренды  будущего, потенциальные варианты применения в нефтепереработке",
                    DateTime.Parse("2019-03-15 12:40", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 13:00", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session("Тренды  будущего, потенциальные варианты применения в нефтепереработке",
                    DateTime.Parse("2019-03-15 13:00", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 13:15", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallA}),

                new Session(
                    "Объявление о старте выступлений в секции СБЫТ:Цифровая АЗС, система региональных продаж, маркетинг",
                    DateTime.Parse("2019-03-15 10:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 10:51", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),

                new Session("Вводное слово",
                    DateTime.Parse("2019-03-15 10:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:00", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),

                new Session("Обзор цифровых трендов и технологий в блоке",
                    DateTime.Parse("2019-03-15 11:00", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:10", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),

                new Session("050 проект",
                    DateTime.Parse("2019-03-15 11:10", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:20", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),

                new Session("АИС \"НБХ\"",
                    DateTime.Parse("2019-03-15 11:20", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:30", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),

                new Session("CRM для ДПСН",
                    DateTime.Parse("2019-03-15 11:30", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:40", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),

                new Session("Применение BLOCKCHAIN технологий в региональных продаж",
                    DateTime.Parse("2019-03-15 11:40", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 11:50", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),

                new Session("Контроль сохранности материальных запасов",
                    DateTime.Parse("2019-03-15 11:50", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:00", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),

                new Session("Перспективы развития программных роботов",
                    DateTime.Parse("2019-03-15 12:00", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:10", CultureInfo.InvariantCulture),
                    new[] {Room.ConfHallC}),

                new Session(
                    "Подход к реализации цифровых инициатив - вызовы и пути решения (по результатам опроса ОГ о готовности к цифровизации)",
                    DateTime.Parse("2019-03-15 12:10", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:35", CultureInfo.InvariantCulture), new[] {Room.ConfHallC}),

                new Session("Восстановление работоспособности АЗС",
                    DateTime.Parse("2019-03-15 12:35", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:45", CultureInfo.InvariantCulture), new[] {Room.ConfHallC}),

                new Session("Внедрение системы удаленного доступа к КИС и ее развитие в регионах",
                    DateTime.Parse("2019-03-15 12:45", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 12:55", CultureInfo.InvariantCulture), new[] {Room.ConfHallC}),

                new Session(
                    "Методика ускоренной реализации цифровых инициатив на основе подходов интеллектуального анализа производственных данных: как создавать решения, которые нужны",
                    DateTime.Parse("2019-03-15 12:55", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 13:05", CultureInfo.InvariantCulture), new[] {Room.ConfHallC}),

                new Session("Сессия вопросов и ответов ",
                    DateTime.Parse("2019-03-15 13:05", CultureInfo.InvariantCulture),
                    DateTime.Parse("2019-03-15 13:15", CultureInfo.InvariantCulture), new[] {Room.ConfHallC}),
            }.AsReadOnly();
        }
    }

    public class MockSessionService : ISessionService
    {
        private readonly IReadOnlyList<Session> _program;

        public MockSessionService()
        {
            _program = CreateProgram().ToList().AsReadOnly();
        }

        public IReadOnlyList<Session> GetProgram() => _program;

        private static IEnumerable<Session> CreateProgram()
        {
            var today = DateTime.Today;

            var start = DateTime.Today;
            for (var i = 0; start.Date == today; i++)
            {
                var end = start.AddMinutes(15);
                yield return new Session($"Session{i}", start, end,
                    new[] {Room.ConfHallA, Room.ConfHallB, Room.ConfHallC});

                start = end.AddMinutes(5);
            }
        }
    }
}