namespace BankingSystem;

public class AuthenticationService
{
    private Dictionary<string, string> userCredentials; // username, password (In a real system, use hashed passwords)

    public AuthenticationService()
    {
        userCredentials = new Dictionary<string, string>();
    }

    public void Register(string username, string password)
    {
        if (!userCredentials.ContainsKey(username))
        {
            userCredentials[username] = password;
            Console.WriteLine($"{username} registered successfully.");
        }
        else
        {
            Console.WriteLine($"Username {username} is already taken.");
        }
    }

    public bool Authenticate(string username, string password)
    {
        if (userCredentials.ContainsKey(username) && userCredentials[username] == password)
        {
            Console.WriteLine($"{username} authenticated successfully.");
            return true;
        }
        else
        {
            Console.WriteLine("Invalid credentials.");
            return false;
        }
    }
}