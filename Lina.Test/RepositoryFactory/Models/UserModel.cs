namespace Lina.Test.RepositoryFactory.Models;

public class UserModel
{
    public UserModel(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}