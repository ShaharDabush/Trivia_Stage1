﻿using System;
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
    public void SignUp(string email, string password, string name)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User u = new User
        {
            UserMail = email,
            Password = password,
            UserName = name,
            AccessId = 1,
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
}

