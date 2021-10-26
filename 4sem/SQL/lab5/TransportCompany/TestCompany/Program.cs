using System;
namespace TestCompany
{
    class Program
    {
        static void Main(string[] args)
        {

            string сonnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TransportCompanyConnectionString"].ConnectionString;
            menu(сonnectionString);

        }
        public static int showInfo()
        {
            int result;
            Console.WriteLine("\nВыберите пункт меню\n1.Инициализация базы данных\n2.Получить данные о сотрудниках и должностях," +
               " на которых они работают\n3.Получить полную информацию об автомобилях и их водителях\n4.Получить cписок вызовов водителей," +
               " фамилия которых начинается на букву «М»\n5.Параметрический запрос для отображения информации о сотрудниках, работающих на определенной должности\n6." +
               "Параметрический запрос для отображения информации об автомобилях определенного года выпуска\n7.Перекрестный запрос для отображения количества" +
               " вызовов по отдельным датам для каждого сотрудника\n8.Вычислить среднюю стоимость вызовов для каждого клиента\n9.Добавить автомобиль\n" +
               "10.Удалить автомобиль\n11.Обновить информацию об автомобиле\n12.Выход");
            string input = Console.ReadLine();
            int.TryParse(input, out result);

            return result;
        }
        public static void menu(string сonnectionString)
        {
            const int exit = 12;
            int result;
            while ((result = showInfo()) != exit)
            {
                Console.Clear();
                switch (result)
                {
                    case 1:
                        DbInitializer.InitializeStamps(сonnectionString);
                        DbInitializer.InitializePositions(сonnectionString);
                        DbInitializer.InitializeRates(сonnectionString);
                        DbInitializer.InitializeAuto(сonnectionString);
                        Console.WriteLine("Таблицы успешно инициализированы");
                        break;
                    case 2:
                        GetCompanyData.GetEmploye(сonnectionString);
                        break;
                    case 3:
                        GetCompanyData.GetAuto(сonnectionString);
                        break;
                    case 4:
                        GetCompanyData.GetDriversServicesM(сonnectionString);
                        break;
                    case 5:
                        GetCompanyData.GetEmplByParam(8, сonnectionString);
                        break;
                    case 6:
                        GetCompanyData.GetAutoByParam(new DateTime(2018, 7, 20), сonnectionString);
                        break;
                    case 7:
                        GetCompanyData.GetDate(сonnectionString);
                        break;
                    case 8:
                        GetCompanyData.GetAvgCost(сonnectionString);
                        break;
                    case 9:
                        try
                        {
                            Console.WriteLine("Введите номер марки авто");
                            var stamp = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Введите ID водителя");
                            var id = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Введите пробег");
                            var mileage = Int32.Parse(Console.ReadLine());
                            GetCompanyData.AddAuto(stamp, id, mileage, сonnectionString);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 10:
                        Console.WriteLine("Введите Id авто которое хотите удалить");
                        int res;
                        string input = Console.ReadLine();
                        int.TryParse(input, out res);
                        GetCompanyData.DelAuto(res, сonnectionString);
                        break;
                    case 11:
                        try
                        {
                            Console.WriteLine("Введите id автомобилия который хотите изменить");
                            var id = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Введите новый регистрационный номер");
                            var reg = Console.ReadLine();
                            Console.WriteLine("Введите пробег");
                            var mileage = Int32.Parse(Console.ReadLine());
                            Console.WriteLine("Введите дату последнего ТО");
                            var date = DateTime.Parse(Console.ReadLine());                         
                            GetCompanyData.UpdateAuto(id, date, reg, mileage, сonnectionString);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
