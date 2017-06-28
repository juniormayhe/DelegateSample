using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateSample
{
    class Program
    {
        //delegate for processing book count
        public delegate void ProcessBookCountCallback(string message, decimal bookPrice);

        class Book {
            public string Name { get; set; }
            //for money always use decimal!
            public decimal Price { get; set; }
        }

        class BookStore {
            //a library has a list of books
            List<Book> list = new List<Book>();
            decimal total = 0;
            
            //add a book to list
            //and call delegate with int argument representing total of books
            public void AddBook(string name, decimal price, ProcessBookCountCallback processAveragePrice) {
                list.Add(new Book { Name = name, Price = price});
                //sum price
                total += price;
                decimal average = total / list.Count;
                processAveragePrice($"BookStore.Add(\"{name}\", {price})", average);
            }
            
        }
        static Random r = new Random();
        static void Main(string[] args)
        {
            const int MAXBOOKS = 10;
            Console.WriteLine($"Books to add in bookstore: {MAXBOOKS}\n");

            //test delegate
            BookStore l = new BookStore();
            
            //add a book with a custom callback
            l.AddBook("C# Delegate Sample", 25.99m, delegate (string message, decimal averagePrice) {
                Console.WriteLine("Custom callback. Message: {0}, Average Price {1:C2}", message, averagePrice);
            });
            //add many books with a common callback
            ProcessBookCountCallback commonCallback = new ProcessBookCountCallback(delegate (string message, decimal averagePrice) {
                Console.WriteLine($"Common callback. Message: {message}, Average Price: {averagePrice.ToString("C2")}");
            });
            for (int i = 0; i < 10; i++) {
                decimal randomPrice = r.Next(0, 11) * 0.99m;
                l.AddBook($"C# Delegate Sample {i}", randomPrice, commonCallback);
            }
            

            Console.ReadLine();
        }
    }
}
