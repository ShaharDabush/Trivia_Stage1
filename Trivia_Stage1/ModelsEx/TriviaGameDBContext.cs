using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

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
    public void SignUp(string email, string password, string name)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User u = new User
        {
            UserMail = email,
            Password = password,
            UserName = name,
            AccessId = 1,
            Score = 0,
            TotalScore = 0
        };
        db.Users.Add(u);
        db.SaveChanges();
    }

    public void addquestion(string question, int subject, int userid)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        Question q = new Question
        {
            Question1 = question,
            StatusId = 2,
            SubjectId = subject,
            UserId = userid
        };
        db.Questions.Add(q);
        db.SaveChanges();
    }

    public void addanswer(string answer, int questionid, bool trueorfalse)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        Answer a = new Answer
        {
            Answer1 = answer,
            QuestionId = questionid,
            TrueFalse = trueorfalse,
        };
        db.Answers.Add(a);
        db.SaveChanges();
    }

    public void changeName(string newname , string UserMail)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserMail == UserMail).FirstOrDefault();
        Updateuser.UserName = newname;
        db.Entry(Updateuser).State = EntityState.Modified;
        db.SaveChanges();
    }
    public void changeMail(string newmail ,string UserMail)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserMail ==  UserMail).FirstOrDefault();
        Updateuser.UserMail = newmail;
        db.Entry(Updateuser).State = EntityState.Modified;
        db.SaveChanges();
    }
    public void changePassword(string newpassword , string UserMail)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserMail == UserMail).FirstOrDefault();
        Updateuser.Password = newpassword;
        db.Entry(Updateuser).State = EntityState.Modified;
        db.SaveChanges();
    }
    public void changeScore(string UserMail)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserMail == UserMail).FirstOrDefault();
        Updateuser.Score -= 100;
        db.Entry(Updateuser).State = EntityState.Modified;
        db.SaveChanges();
    }

    public void IncorrectAnswer(string UserMail)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserMail == UserMail).FirstOrDefault();
        Console.WriteLine("Incorrect answer!");
        if (Updateuser.Score < 5)
        {
            Updateuser.Score = 0;
            Console.WriteLine("Your score is: " + Updateuser.Score);
        }

        else
        {
            Updateuser.Score += -5;
            Updateuser.TotalScore -= 5;
            Console.WriteLine("Your score is: " + Updateuser.Score);
        }
        db.Entry(Updateuser).State = EntityState.Modified;
        db.SaveChanges();
    }
    public void CorrectAnswer(string UserMail)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserMail == UserMail).FirstOrDefault();
        Console.WriteLine("You are correct!!!");
        if (Updateuser.Score > 90)
        {
            Updateuser.Score = 100;
            Console.WriteLine("Your score is: " + Updateuser.Score);
            Console.WriteLine();
            Console.WriteLine("You can now add a question");
            Console.WriteLine();
        }

        else
        {
            Updateuser.Score += 10;
            Updateuser.TotalScore += 10;
            Console.WriteLine("Your score is: " + Updateuser.Score);
        }
        db.Entry(Updateuser).State = EntityState.Modified;
        db.SaveChanges();
    }


}

