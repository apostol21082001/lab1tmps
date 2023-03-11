using System;
using System.Collections.Generic;
using System.Text;
public class ProductService : IProductService
{
     private List<Product> products = new List<Product>();

     public void AddProduct(Product product)
     {
          // Atribuie un ID unic produsului
          product.Id = products.Count + 1;

          // Adaugă produsul în listă
          products.Add(product);
     }

     public void RemoveProduct(int productId)
     {
          // Găsește produsul după ID și îl șterge din listă
          products.RemoveAll(p => p.Id == productId);
          // Re-numere produsele rămase în listă
          for (int i = 0; i < products.Count; i++)
          {
               products[i].Id = i + 1;
          }
     }

     public List<Product> GetAllProducts()
     {
          // Returnează toate produsele din listă
          return products;
     }
}