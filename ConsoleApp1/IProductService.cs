using System;
using System.Collections.Generic;

public interface IProductService
{
     void AddProduct(Product product);
     void RemoveProduct(int productId);
     List<Product> GetAllProducts();
}