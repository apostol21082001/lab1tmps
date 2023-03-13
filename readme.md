Topic: SOLID Principles

Author: Apostol Mihail

SOLID este un set de cinci principii de proiectare orientate pe obiecte menite să facă proiectele de software mai ușor de întreținut, mai flexibile și mai ușor de înțeles. Acronimul reprezintă principiul responsabilității unice, principiul deschis-închis, principiul substituției Liskov, principiul segregației interfeței și principiul inversării dependenței. Fiecare principiu abordează un aspect specific al proiectării software, cum ar fi organizarea responsabilităților, gestionarea dependențelor și proiectarea interfețelor. Urmând aceste principii, dezvoltatorii de software pot crea cod mai modular, testabil și reutilizabil, care este mai ușor de modificat și extins în timp. Aceste principii sunt acceptate și adoptate pe scară largă în comunitatea de dezvoltare software și pot fi aplicate oricărui limbaj de programare orientat pe obiecte.

Codul meu se bazează pe un exemplu simplu de implementare a unui serviciu de gestionare a produselor, care respectă cele 5 principii SOLID.Proiectul meu a fost scris in C#.

Interfata IProductService:
Aceasta este o interfata care defineste contractul pentru orice serviciu care se ocupa de produse. Contine trei metode:

    AddProduct(Product product) - adauga un produs nou in lista de produse.
    RemoveProduct(int productId) - sterge un produs din lista de produse, pe baza ID-ului.
    GetAllProducts() - returneaza toate produsele din lista.
    
Clasa Product:
Aceasta este clasa care reprezinta un produs. Are trei proprietati:

    Id - ID-ul unic al produsului.
    Name - numele produsului.
    Price - pretul produsului.
    
Clasa ProductService:
Aceasta este clasa care implementeaza interfata IProductService. Contine o lista de produse si implementeaza cele trei metode din interfata:

    AddProduct(Product product) - adauga un produs nou in lista de produse. Atribuie un ID unic produsului si apoi il adauga in lista.
    RemoveProduct(int productId) - sterge un produs din lista de produse, pe baza ID-ului. Găsește produsul după ID și il sterge din lista. Apoi re-numere produsele rămase în listă, pentru a asigura ca toate ID-urile sunt unice si consecutive.
    GetAllProducts() - returneaza toate produsele din lista.

Despre clasa Program:

    Aceasta este clasa principală a aplicației noastre, care gestionează interacțiunea cu utilizatorul prin intermediul consolei.
    În metodă statică Main, se creează o instanță a serviciului de produse (ProductService) și se afișează un meniu de opțiuni pentru utilizator.
    În funcție de opțiunea aleasă de utilizator, se apelează metodele corespunzătoare din serviciul de produse pentru a adăuga, elimina sau afișa produsele.
    Metoda AddProduct solicită numele și prețul produsului de la utilizator prin intermediul consolei, creează o instanță a clasei Product și o adaugă în lista de produse prin apelarea metodei AddProduct din serviciul de produse.
    Metoda RemoveProduct solicită utilizatorului ID-ul produsului de eliminat, apoi apelează metoda RemoveProduct din serviciul de produse pentru a elimina produsul corespunzător.
    Metoda GetAllProducts apelează metoda GetAllProducts din serviciul de produse pentru a returna o listă de toate produsele și le afișează în consolă.

Dupa realizarea proiectului se pare ca se respecte principiile SOLID:

    Principiul responsabilității unice (SRP): Fiecare clasă și interfață din codul tău are o singură responsabilitate și îndeplinește un singur rol specific în cadrul aplicației.

    Principiul deschis-închis (OCP): Interfața IProductService și clasa ProductService sunt deschise pentru extindere, dar închise pentru modificare, deoarece pot fi extinse prin adăugarea de metode suplimentare sau modificarea comportamentului existent prin moștenire sau implementare, fără a modifica codul existent.

    Principiul substituției Liskov (LSP): Clasa ProductService poate fi folosită ca substitut pentru interfața IProductService fără a afecta comportamentul aplicației.

    Principiul segregării interfețelor (ISP): Interfața IProductService este specifică și conține numai metodele necesare pentru a gestiona produsele, iar clasa ProductService implementează aceste metode în mod corespunzător.

    Principiul inversiunii dependențelor (DIP): Clasa Program depinde de interfața IProductService în loc de clasa ProductService, ceea ce face posibilă înlocuirea implementării ProductService cu o altă implementare fără a afecta comportamentul aplicației.

În general, codul pare să fie bine structurat și organizat, iar utilizarea interfeței IProductService face ca implementarea să fie flexibilă și extensibilă.

O mica  explicatie pentru fiecare principiu in parte cu mici secvente de cod sau adaugari care putea fi posibile:
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
