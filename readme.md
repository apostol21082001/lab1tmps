Topic: SOLID Principles

Author: Apostol Mihail

SOLID este un set de cinci principii de proiectare orientate pe obiecte menite să facă proiectele de software mai ușor de întreținut, mai flexibile și mai ușor de înțeles. Acronimul reprezintă principiul responsabilității unice, principiul deschis-închis, principiul substituției Liskov, principiul segregației interfeței și principiul inversării dependenței. Fiecare principiu abordează un aspect specific al proiectării software, cum ar fi organizarea responsabilităților, gestionarea dependențelor și proiectarea interfețelor. Urmând aceste principii, dezvoltatorii de software pot crea cod mai modular, testabil și reutilizabil, care este mai ușor de modificat și extins în timp. Aceste principii sunt acceptate și adoptate pe scară largă în comunitatea de dezvoltare software și pot fi aplicate oricărui limbaj de programare orientat pe obiecte.

Codul meu se bazează pe un exemplu simplu de implementare a unui serviciu de gestionare a produselor, care respectă cele 5 principii SOLID.Proiectul meu a fost scris in C#.

1.	Single Responsibility Principle (SRP)
Clasa Product reprezintă o entitate a produselor și are o singură responsabilitate, aceea de a stoca informații despre un produs.

Acest principiu este respectat în mai multe locuri din codul prezentat, de exemplu:

•	Clasa Program are o singură responsabilitate, aceea de a inițializa aplicația și de a afișa meniul principal.

•	Clasa Product are o singură responsabilitate, aceea de a stoca informații despre un singur produs.

•	Clasa ProductService are o singură responsabilitate, aceea de a gestiona operațiile CRUD pe obiecte Product.

public class Product

{

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}

Clasa ProductRepository are responsabilitatea de a gestiona operațiile de stocare și preluare a produselor din/spre o sursă de date (de exemplu, o bază de date).

public class ProductRepository : IProductRepository

{

    private readonly List<Product> _products = new List<Product>();

    public void Add(Product product)
    {
        _products.Add(product);
    }

    public void Delete(Product product)
    {
        _products.Remove(product);
    }

    public IEnumerable<Product> GetAll()
    {
        return _products;
    }

    public Product GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public void Update(Product product)
    {
        var existingProduct = GetById(product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
        }
    }
}

2.	Open/Closed Principle (OCP)

Clasa ProductService are responsabilitatea de a gestiona operațiile CRUD (Create, Read, Update, Delete) pentru produse. 
Acest principiu este respectat prin utilizarea interfețelor. Interfețele sunt deschise pentru extensie, ceea ce înseamnă că putem crea noi implementări ale interfeței fără a modifica codul existent. De exemplu, putem adăuga o nouă clasă care implementează interfața IProductRepository pentru a stoca informațiile într-un alt tip de bază de date.
public interface IProductService.



{

    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);
    IEnumerable<Product> GetAll();
    Product GetById(int id);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void Add(Product product)
    {
        _productRepository.Add(product);
    }

    public void Delete(Product product)
    {
        _productRepository.Delete(product);
    }

    public IEnumerable<Product> GetAll()
    {
        return _productRepository.GetAll();
    }

    public Product GetById(int id)
    {
        return _productRepository.GetById(id);
    }

    public void Update(Product product)
    {
        _productRepository.Update(product);
    }
}

3.	Liskov Substitution Principle (LSP)

Acest principiu este respectat prin faptul că obiectele de tip Product pot fi înlocuite cu instanțe ale claselor derivate fără a afecta corectitudinea programului. De exemplu, dacă am crea o clasă DiscountedProduct care moștenește din clasa Product, putem folosi obiecte de tip DiscountedProduct în loc de obiecte de tip Product în codul existent și totul va funcționa corect.
Clasa ProductRepository implementează interfața IProductRepository. Astfel, clasa ProductService poate utiliza instanțe ale clasei ProductRepository sau ale oricărei alte clase care implementează interfața IProductRepository, fără a afecta corectitudinea programului.

public interface IProductRepository

{

    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);
    IEnumerable<Product> GetAll();
    Product GetById(int id);
}

public class ProductRepository : IProductRepository
{

    // implementare
}

4.	Interface Segregation Principle (ISP)

Acest principiu este respectat prin faptul că interfețele sunt specializate pentru comportamentul de care avem nevoie. De exemplu, interfața IProductRepository definește doar operațiile CRUD pentru obiecte Product, fără a include alte metode sau proprietăți care nu sunt necesare. Acest lucru face interfața mai ușor de înțeles și de implementat.

public interface IProductService

{

    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);
    IEnumerable<Product> GetAll();
    Product GetById(int id);
}

5.	Dependency Inversion Principle (DIP)

Acest principiu este respectat prin faptul că modulele de nivel superior nu depind de modulele de nivel inferior, ci de abstracțiunile acestora. 
Clasa ProductService depinde de interfața IProductRepository, nu de implementarea acesteia. Acest lucru face ca clasa ProductService să fie mai ușor de testat și de modificat, fără a afecta codul din alte părți ale aplicației.

Secventa cod:

public class ProductService : IProductService

{

     private List<Product> products = new List<Product>();
