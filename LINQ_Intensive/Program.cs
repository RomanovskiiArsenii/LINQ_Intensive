
using System.Collections;

namespace LINQ_Intensive
{
    class Program
    {
        static void Main(string[] args)
        {
            #region ТЕСТОВЫЕ КОЛЛЕКЦИИ
            var range1 = Enumerable.Range(1, 10);
            var range2 = Enumerable.Range(11, 10);
            List<List<int>> listOfLists = new() { new List<int>() { 1, 2, 3 }, new List<int>() { 7, 8, 9 } };
            List<Employee> employees = new() { new Employee(1, "Chloe"), new Employee(2, "Mark") };
            List<Department> departments = new() { new Department(1, "Joinery"), new Department(2, "Management") };
            HashSet<int> set1 = new HashSet<int>() { 1, 2, 3, 4, 5, 6 };
            HashSet<int> set2 = new HashSet<int>() { 4, 5, 6, 7, 8, 9 };
            List<string> names = new List<string>() { "Chloe", "Mark", "Jimm", "Helen" };
            ArrayList nonparametrized = new ArrayList() { 1, 2, 3, 4 };
            #endregion

            #region МЕТОДЫ LINQ
            //фильтрация
            var quWhere = range1.Where(x => x % 3 == 0);            //where - фильтр элементов последовательности, требует наличия делегата
                                                                    //3 6 9
            var quTake = range1.Take(3);                            //take выбирает n первых элементов
                                                                    //1 2 3
            var quSkip = range1.Skip(7);                            //skip пропускает n первых элементов
                                                                    //8 9 10
            var quSkipWhile = range1.SkipWhile(x => x != 5);        //skipwhile пропускает элементы, пока выполняется условие
                                                                    //5 6 7 8 9 10
            var quTakeWhile = range1.TakeWhile(x => x != 5);        //takewhile выбирает элементы, пока выполняется условие
                                                                    //1 2 3 4 
            var quDistinct = range1.Distinct();                     //distinct выбирает все уникальные элементы, не допуская повторов
                                                                    //1 2 3 4 5 6 7 8 9 10

            //проецирование
            var quSelect = range1.Select(x => x * 10);              //select - проекция элементов последовательности, требует наличия делегата
                                                                    //10 20 30 40 50 60 70 80 90 100
            var quSelectMany = listOfLists.SelectMany(l => l);      //selectmany преобразует коллекцию коллекций в единую последовательность
                                                                    //1 2 3 7 8 9 

            //объединение
            var quJoin = from e in employees                        //join - объединение коллекций
                         join d in departments
                         on e.DepartmentId equals d.Id
                         select new
                         {
                             e.Name,                                //{ Name = Chloe, DepartmentName = Joinery }
                             d.DepartmentName                       //{ Name = Mark, DepartmentName = Management }
                         };

            //group join похож на join, но возвращает вложенные группы элементов, соответствующих условиям соответствия.
            //Это полезно, когда один элемент из первой коллекции может соответствовать нескольким элементам из второй коллекции.

            //упорядочивание    
            var quOrder1 = range1.OrderBy(x => x);                  //orderby - сортировка по возрастанию
                                                                    //1 2 3 4 5 6 7 8 9 10      
            var quOrder2 = range1.OrderBy(x => -x);                 //10 9 8 7 6 5 4 3 2 1 

            var quOrderDesc = range1.OrderByDescending(x => x);     //orderby descending - сортировка по убыванию
                                                                    //10 9 8 7 6 5 4 3 2 1 

            var quOrderDescFrom = from i in range1                  //orderby descending, но в конструкции from
                                  orderby i descending
                                  select i;                         //10 9 8 7 6 5 4 3 2 1

            //множества
            var quConcat = set1.Concat(set2);                       //concat - объединение множеств с повторяющимися значениями
                                                                    //1 2 3 4 5 6 4 5 6 7 8 9
            var quUnion = set1.Union(set2);                         //union - математическое объединение множеств (только уникальные значения)
                                                                    //1 2 3 4 5 6 7 8 9
            var quIntersect = set1.Intersect(set2);                 //intersect - пересечение множеств
                                                                    //4 5 6 
            var quExcept = set1.Except(set2);                       //except - разность множеств
                                                                    //1 2 3

            //экспорт
            int[] array = range1.ToArray();                          //toarray - экспорт в массив 
            List<int> list = range1.ToList();                        //tolist  - экспорт в список

            //локальный идентификатор запроса
            var quLet = from e in employees
                        let field = $"ID: {e.DepartmentId}, Name: {e.Name}"     //let - локальный идентификатор
                        orderby field descending                                //сортировка по убыванию
                        select field;                                           //ID: 2, Name: Mark
                                                                                //ID: 1, Name: Chloe

            //группировка
            var quGroup = from i in range1
                          group i by i % 2;                         //group - группировка по ключу - отсатку от деления на 2
                                                                    //Key: 1 Elements: 1 3 5 7 9 Key: 0 Elements: 2 4 6 8 10
                                                                    //foreach (var group in quGroup)
                                                                    //{
                                                                    //    Console.Write($"Key: {group.Key} Elements: ");
                                                                    //    foreach (var e in group) { Console.Write($"{e} "); }
                                                                    //}

            //Single
            //Возвращает единственный элемент из последовательности. Однако, если в последовательности
            //больше одного элемента, или в ней нет ни одного элемента, будет сгенерировано исключение.
            //var quSingle = range.Single();                          //InvalidOperationException

            #endregion

            #region ПРАКТИКА LINQ
            //1. Cоздание запроса
            var query = from i in range1
                        where i % 2 == 0
                        select i;                       //2 4 6 8 10 1 2 3 4 5 6 7 8 9 10

            //2. Экспорт запроса
            //каждое обращение к запросу заставит его выполниться снова
            //чтобы сохранить данные, полученные в запросе, нужно выполнить его экспорт
            int[] queryToArray = (from i in range1
                                  where i % 2 == 0
                                  select i).ToArray();  //2 4 6 8 10 1 2 3 4 5 6 7 8 9 10

            //3. Метод над результатом запроса
            //здесь получаем количество элементов в результате запроса, заключив его в скобки
            var queryCount = (from i in range1
                              where i % 2 == 0
                              select i).Count();        //5

            //4. Создание временной переменной, объединение запросов
            //получаем результат запроса и работаем с ним дальше через создание временной переменной
            var queryInto = from i in range1
                            where i % 2 == 0
                            select i                    //результат первого запроса
                            into even                   //создание временной переменной even
                            where even > 5              //проверка переменной на соответствие условию
                            orderby -even               //сортировка по убыванию
                            select even;                //10 8 6

            //аналогично запросу:
            var querySimilarToInto =
                            from even in (
                            from i in range1             //вложенный запрос
                            where i % 2 == 0
                            select i)
                            where even > 5
                            orderby -even
                            select even;                //10 8 6

            //5. Вложенные конструкции from
            //аналогичны вложенным foreach

            var queryFrom = from i in range1            //foreach(var i in range1)
                            where i % 4 == 0            // {
                            from j in range2            //foreach(var j in range2) {...}
                            where j % 4 == 0            // }
                            select new
                            { x = i, y = j };           //4 12  4 16  4 20  8 12  8 16  8 20

            //foreach (var i in queryFrom) Console.Write($"{i.x} {i.y}  ");

            //6. Работа с непараметризованными коллекциями
            //требуется явное указание типа в конструкции from

            var queryNonParametryzed = from int i in nonparametrized
                                       select i;
            #endregion

            #region СТРАТЕГИЯ СОЗДАНИЯ ЗАПРОСА
            //8. Стратегия создания запроса
            //A. Фильтры следует применять до изменяющих последовательность операторов.
            //Where использовать до GroupBy OrderBy.
            //когда операторы, изменяющие последовательность, обращаются к первоначальной последовательности
            //им приходится работать с большим объемом данных, нежели в последовательности, которая уже отфильтрована
            //B. При необходимости, можно всегда сохранить (экспортировать) результат запроса
            //С. Все сложные запросы желательно выносить во внешнюю часть, облегчая логику запроса
            #endregion
        }
    }
}