namespace NullChecking;

public class User
{
    public string FullName { get; }

    public User(string fullName)
    {
        FullName = fullName;
    }
    
    public User Child { get; set; }
}