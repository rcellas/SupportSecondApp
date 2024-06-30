using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    

    public ApplicationUser()
    {
        // Constructor vacío requerido por Identity
    }

    public ApplicationUser(string userName, string email) : base(userName)
    {
        Email = email;
        UserName = userName;
    }
}