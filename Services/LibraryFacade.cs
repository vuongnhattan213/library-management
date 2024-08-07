using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class LibraryFacade
{
    private readonly string usersFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "users.json");
    private readonly string productsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "products.json");
    private readonly IFactory _factory;
    private readonly JsonFileManager _fileManager;

    public LibraryFacade(IFactory factory)
    {
        _factory = factory;
        _fileManager = JsonFileManager.Instance;
    }

    public List<User> GetAllUsers()
    {
        return _fileManager.LoadFromFile<User>(usersFilePath);
    }

    public void SaveUsers(List<User> users)
    {
        _fileManager.SaveToFile(usersFilePath, users);
    }

    public List<Product> GetAllProducts()
    {
        return _fileManager.LoadFromFile<Product>(productsFilePath);
    }

    public void SaveProducts(List<Product> products)
    {
        _fileManager.SaveToFile(productsFilePath, products);
    }

    public User CreateUser(int id, string fullName, string email, string username, string password)
    {
        return _factory.CreateUser(id, fullName, email, username, password);
    }

    public Product CreateProduct(int id, string bookName, string author)
    {
        return _factory.CreateProduct(id, bookName, author);
    }
}