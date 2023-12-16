using System;
using System.Collections.Generic;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace Trivia_Stage1.Models;

public partial class TriviaGameDBContext : DbContext
{
    public bool IsEmailExist(string emailAddress)//checks if the mail exists in the database
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User? u = db.Users.Where(u => u.UserMail == emailAddress).FirstOrDefault();
        if (u != null)
        {
            return true;
        }
        return false;

    }
    public bool ISPasswordExist(string Password)//checks if the password exists in the database
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User? u = db.Users.Where(u => u.Password == Password).FirstOrDefault();
        if (u != null)
        {
            return true;
        }
        return false;

    }
    public void SignUp(string email, string password, string name)//create a new user and adds it to the database
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
        db.SaveChanges();//commit changes to the database
    }

    public void addquestion(string question, int subject, int userid)//adds a question to the database
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
        db.SaveChanges();//commit changes to the database
    }

    public void addanswer(string answer, int questionid, bool trueorfalse)//adds an answer to the database
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        Answer a = new Answer
        {
            Answer1 = answer,
            QuestionId = questionid,
            TrueFalse = trueorfalse,
        };
        db.Answers.Add(a);
        db.SaveChanges();//commit changes to the database
    }

    public void changeName(string newname , string UserMail)//change user name of user in the database
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserMail == UserMail).FirstOrDefault();
        Updateuser.UserName = newname;
        db.Entry(Updateuser).State = EntityState.Modified;//modify user in database
        db.SaveChanges();//commit changes to the database
    }
    public void changeMail(string newmail ,string UserMail)//change user mail of user in the database
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserMail ==  UserMail).FirstOrDefault();
        Updateuser.UserMail = newmail;
        db.Entry(Updateuser).State = EntityState.Modified;//modify user in database
        db.SaveChanges();//commit changes to the database
    }
    public void changePassword(string newpassword , string UserMail)//changes user password in the database
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserMail == UserMail).FirstOrDefault();
        Updateuser.Password = newpassword;
        db.Entry(Updateuser).State = EntityState.Modified;//modify user in the database
        db.SaveChanges();//commit changes to the database
    }
    public void changeScore(string UserMail)//on adding question detract points fron the user
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserMail == UserMail).FirstOrDefault();
        Updateuser.Score -= 100;
        db.Entry(Updateuser).State = EntityState.Modified;//modify user in the database
        db.SaveChanges();//commit changes in the database
    }

    public void IncorrectAnswer(string UserMail)//reduce the score of the user on answering a question wrong
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserMail == UserMail).FirstOrDefault();
        Console.WriteLine("Incorrect answer!");
        Updateuser.Score -= 5;
        Updateuser.TotalScore -= 5;
        if (Updateuser.Score < 0)
        {
            Updateuser.Score = 0;
        }
        if (Updateuser.TotalScore < 0)
        {
            Updateuser.TotalScore = 0;
        }
        Console.WriteLine("Your score is: " + Updateuser.Score);
        db.Entry(Updateuser).State = EntityState.Modified;//modify user in the database
        db.SaveChanges();//commit chnages to the database
    }
    public void CorrectAnswer(string UserMail)//increase the score of the user on ansewering a question correctly
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserMail == UserMail).FirstOrDefault();
        Console.WriteLine("You are correct!!!");
        if (Updateuser.Score >= 90)
        {
            Updateuser.Score = 100;
            Updateuser.TotalScore += 10;
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
        db.Entry(Updateuser).State = EntityState.Modified;//modify user in database
        db.SaveChanges();//commit changes to the database
    }


}

