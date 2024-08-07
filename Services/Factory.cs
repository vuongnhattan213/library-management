public interface IFactory
{
    User CreateUser(int id, string fullName, string email, string username, string password);
    Product CreateProduct(int id, string bookName, string author);
}

public class Factory : IFactory
{
    public User CreateUser(int id, string fullName, string email, string username, string password)
    {
        return new User { Id = id, FullName = fullName, Email = email, Username = username, Password = password };
    }

    public Product CreateProduct(int id, string bookName, string author)
    {
        return new Product { Id = id, BookName = bookName, Author = author };
    }
}
