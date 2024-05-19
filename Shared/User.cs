using System;
using System.Collections.Generic;

namespace StellarJadeManager.Shared;

public  class User
{
    public User(string name, string email, string salt, string hash)
    {
        Name = name;
        Email = email;
        Salt = salt;
        Hash = hash;
        LastActive= DateTime.Now;
    }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime LastActive { get; set; }

    public string Salt { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();
}
