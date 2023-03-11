using System;
using System.Collections.Generic;
using System.Text;
public class Program
{
     private static IProductService productService = new ProductService();

     static void Main(string[] args)
     {
          // Afiseaza meniul
          while (true)
          {
               Console.WriteLine("1. Adauga produs");
               Console.WriteLine("2. Sterge produs");
               Console.WriteLine("3. Afiseaza toate produsele");
               Console.WriteLine("4. Iesire");

               int choice = int.Parse(Console.ReadLine());

               switch (choice)
               {
                    case 1:
                         AddProduct();
                         break;
                    case 2:
                         RemoveProduct();
                         break;
                    case 3:
                         GetAllProducts();
                         break;
                    case 4:
                         Environment.Exit(0);
                         break;
                    default:
                         Console.WriteLine("Optiune invalida!");
                         break;
               }

               Console.WriteLine();
          }
     }

     private static void AddProduct()
     {
          Console.Write("Nume produs: ");
          string name = Console.ReadLine();

          Console.Write("Pret produs: ");
          decimal price = decimal.Parse(Console.ReadLine());

          Product product = new Product
          {
               Name = name,
               Price = price
          };

          productService.AddProduct(product);

          Console.WriteLine("Produs adaugat cu succes!");
     }

     private static void RemoveProduct()
     {
          Console.Write("ID produs: ");
          int id = int.Parse(Console.ReadLine());

          productService.RemoveProduct(id);

          Console.WriteLine("Produs sters cu succes!");
     }

     private static void GetAllProducts()
     {
          List<Product> products = productService.GetAllProducts();

          foreach (Product product in products)
          {
               Console.WriteLine($"{product.Id} - {product.Name} ({product.Price} lei)");
          }
     }
}