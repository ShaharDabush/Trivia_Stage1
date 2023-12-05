using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Trivia_Stage1.Models;

public partial class TriviaGameDBContext : DbContext
{
    public bool IsEmailExist(string emailAddress)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User? u = db.Users.Where(u => u.UserMail == emailAddress).FirstOrDefault();
        if (u != null)
        {
            return true;
        }
        return false;

    }
    public bool ISPasswordExist(string Password)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User? u = db.Users.Where(u => u.Password == Password).FirstOrDefault();
        if (u != null)
        {
            return true;
        }
        return false;

    }
}

