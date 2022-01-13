/* LINQ com SQL */

/* >>> PROGRAMA PRINCIPAL <<< */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Aula231_LINQ_SQL.Entities;

namespace Aula231_LINQ_SQL
{
    class Program
    {
        static void Print<T>(string message, IEnumerable<T> collection) // Receve "string" e "IEnumerable" como parametros
        {
            Console.WriteLine(message);
            foreach (T obj in collection)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Category c1 = new Category() { Id = 1, Name = "Tools", Tier = 2 }; // Cria objeto "c1", tipo "Category"
            Category c2 = new Category() { Id = 2, Name = "Computers", Tier = 1 };
            Category c3 = new Category() { Id = 3, Name = "Electronics", Tier = 1 };

            List<Product> products = new List<Product>() // Cria lista de "Product" - DATA SOURCE
            {
                new Product(){Id = 1, Name = "Computer", Price = 1100.00, Category = c2},
                new Product(){Id = 2, Name = "Hammer", Price = 90.00, Category = c1},
                new Product(){Id = 3, Name = "TV", Price = 1700.00, Category = c3},
                new Product(){Id = 4, Name = "Notebook", Price = 1300.00, Category = c2},
                new Product(){Id = 5, Name = "Saw", Price = 80.00, Category = c1},
                new Product(){Id = 6, Name = "Tablet", Price = 700.00, Category = c2},
                new Product(){Id = 7, Name = "Camera", Price = 700.00, Category = c3},
                new Product(){Id = 8, Name = "Printer", Price = 350.00, Category = c3},
                new Product(){Id = 9, Name = "MacBook", Price = 1800.00, Category = c2},
                new Product(){Id = 10, Name = "Sound Bar", Price = 700.00, Category = c3},
                new Product(){Id = 11, Name = "Level", Price = 70.00, Category = c1}
            };

            //CONSULTAS COM LINQ USANDO EXPRESSOES LAMBDA
            // Onde o Tier da Categoria for igual a 1 e o Preco for menor que 900:
            //var r1 = products.Where(p => p.Category.Tier == 1 && p.Price < 900); // Expressao Lambda com "Where"
            var r1 =
                from p in products
                where p.Category.Tier == 1 && p.Price < 900.0
                select p;
            Print("TIER 1 AND PRICE < 900.00:", r1); // Imprime - Parametros "string" e "IEnumerable"

            // Onde for categoria "Tools", seleciona o nome:
            //var r2 = products.Where(p => p.Category.Name == "Tools").Select(p => p.Name);7
            var r2 =
                from p in products
                where p.Category.Name == "Tools"
                select p.Name;
            Print("NAME OF PRODUCTIS FROM TOOLS:", r2); // Imprime - Parametros "string" e "IEnumerable"

            // Onde a primeira letra do nome for C, seleciona Nome, Preco e Nome da Categoria, usando ALIAS
            // var r3 = products.Where(p => p.Name[0] == 'C').Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name });
            var r3 =
                from p in products
                where p.Name[0] == 'C'
                select new
                {
                    p.Name,
                    p.Price,
                    CategoryName = p.Category.Name
                };
            Print("NAMES STARTING WITH 'C' AND ANONYMOUS OBJECT:", r3);

            // Onde o Tier da Categoria for igual a 1, ordena por 
            //var r4 = products.Where(p => p.Category.Tier == 1).OrderBy(p => p.Price).ThenBy(p => p.Name);
            var r4 =
                from p in products
                where p.Category.Tier == 1
                orderby p.Name
                orderby p.Price
                select p;
            Print("TIER 1 ORDERED BY PRICE THEN BY NAME:", r4);

            // Skip e Take - Muito usado para Paginacao
            //var r5 = r4.Skip(2).Take(4); // Pula os dois primeiros e adiciona os proximos quatro elementos
            var r5 = (from p in r4 select p).Skip(2).Take(4);
            Print("TIER 1 ORDERED BY PRICE THEN BY NAME SKIP 2 TAKE 4:", r5);

            //var r6 = products.FirstOrDefault(); // Seleciona o primeiro elemento. Caso seja nulo, nao retorna nada (nem erro)
            var r6 = (from p in products select p).FirstOrDefault();
            Console.WriteLine("FIRST - TEST WITH RESULT: " + r6);

            //var r7 = products.Where(p => p.Price > 3000.00).FirstOrDefault(); // Evita o erro, retornando o Default (nada)
            var r7 = (from p in products where p.Price > 3000.00 select p).FirstOrDefault();
            Console.WriteLine("FIRST - TEST WITHOUT RESULT: " + r7);

            Console.WriteLine();

            // Agrupamento por Categoria
            // var r16 = products.GroupBy(p => p.Category);
            var r16 = from p in products group p by p.Category;
            foreach (IGrouping<Category, Product> group in r16)
            {
                Console.WriteLine("CATEGORY: " + group.Key.Name + ":");
                foreach (Product p in group)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }
        }
    }
}
