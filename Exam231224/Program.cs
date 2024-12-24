using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam231224
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book();
            book.ListDefault();    
            book.SaveListToFile();
            string strPic = "";
            const string logOut = "exit";
            string nameEn = "";
            string nameRu = "";

            int num;

            

            while(strPic.ToLower() != logOut)
            {
                Console.Clear();
                Console.Write("Программа Англо-Русский словарь\n1: Посмотреть весь словарь\n2: Добавить данные\n3: Найти данные" +
                "\n4: Удалить данные\n5: Удалить все данные\n9/exit: Выйти из программы\nВаш выбор -> ");

                strPic = Console.ReadLine();

                switch (strPic)
                {
                    case "1":

                        Console.Clear();
                        book.ShowList();
                        Console.WriteLine("Нажмите любую клавишу");
                        Console.ReadKey();

                        break;

                    case "2":

                        Console.Clear();
                        Console.Write("Введите слово на английском языке -> ");
                        nameEn = Console.ReadLine();
                        Console.Write("Введите перевод слова на русский -> ");
                        nameRu = Console.ReadLine();
                        Console.Write("Нажмите любую клавишу");
                        book.AddList(nameEn, nameRu);
                        book.SaveListToFile();
                        Console.ReadKey();

                        break;

                    case "3":

                        Console.Clear();
                        Console.Write("Введите слово для поиска - > ");
                        nameEn = Console.ReadLine();
                        book.Search(nameEn);
                        Console.ReadKey();

                        break;

                    case "4":

                        Console.Clear();
                        book.ShowList();
                        Console.Write("Введите номер для удаления -> ");
                        strPic = Console.ReadLine();

                        if (int.TryParse(strPic, out num))
                        {
                            book.RemuveForIndex(num - 1);
                            book.SaveListToFile();
                            Console.WriteLine("Нажмите любую клавишу");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Надо вводить тип данных Integer");
                            Console.WriteLine("Нажмите любую клавишу");
                            Console.ReadKey();
                        }
                        break;

                    case "5":

                        Console.Clear();
                        book.RemuveAllList();
                        book.SaveListToFile();
                        Console.WriteLine("Все данные удалены!!!\n\nНажмите любую клавишу");
                        Console.ReadKey();

                        break ;

                    case "9":

                        strPic = logOut;

                        break;

                    case logOut:

                        strPic = logOut;

                        break;

                    default:
                        Console.WriteLine("Что-то не так...");
                        Console.WriteLine("Нажмите любую клавишу");
                        Console.ReadKey();

                        break;


                }
            }
        }
    }

    class Words
    {
        private string name;

        private string nameTranslation;

        public string ReturnName() { return name; }
        public string ReturnNameTranslation() { return nameTranslation; }
        public void SetName(string name) { this.name = name; }
        public void SetNameTranslation(string nameTranslation) {  this.nameTranslation = nameTranslation; }

        public Words(string name, string nameTranslation)
        {
            this.name = name;
            this.nameTranslation = nameTranslation;
        }
    }

    class Book
    {
        public List<Words>_books = new List<Words>();

        string path = @"C:/txt/fileToTest.txt";

        public string OpenFile(string path)
        {
            using(FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public void ListDefault()
        {
            _books.Add(new Words("Aple", "Яблоко"));
            _books.Add(new Words("Milk", "Молоко"));
            _books.Add(new Words("Eggs", "Яйца"));
            _books.Add(new Words("Black", "Черный"));
            _books.Add(new Words("Car", "Машина"));
        }

        public void AddList(string str1, string str2)
        {
            _books.Add(new Words(str1, str2));
        }
        public void SaveListToFile()
        {            
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    for(int i = 0; i < _books.Count; i++)
                    {
                        sw.WriteLine(_books[i].ReturnName() + " " + _books[i].ReturnNameTranslation());
                    }
                }

            }            
        }  
        
        public void Search(string str)
        {
            foreach(Words it in _books)
            {
                if (it.ReturnName().ToLower() == str.ToLower())

                    Console.WriteLine("Результат поиска - " + it.ReturnName() + " " + it.ReturnNameTranslation());

                else if (it.ReturnNameTranslation().ToLower() == str.ToLower())

                    Console.WriteLine("Результат поиска - " + it.ReturnName() + " " + it.ReturnNameTranslation());

                else

                    Console.WriteLine($"По запросу ||{str.ToUpper()}|| ничего не найдено!!!");
            }
        }

        public void RemuveForIndex(int index)
        {
            if(_books.Count > index && index >= 0 && _books.Count != 0)

            _books.RemoveAt(index);

            else

                Console.WriteLine("Неверный диапазон");
        }

        public void ShowList()
        {
            for (int i = 0; i < _books.Count; ++i)
            {
                Console.WriteLine($"{i + 1} запись\t{_books[i].ReturnName()} --- {_books[i].ReturnNameTranslation()}");
            }
        }

        public void RemuveAllList()
        {
            _books.Clear();
        }
    }
}
