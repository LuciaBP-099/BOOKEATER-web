using Microsoft.AspNetCore.Identity;

namespace BookEater.Models;

public class MyRol : IdentityRole
{
    public string? Section{ get; set; }

    public DateTime AltaDate { get; set; }
}