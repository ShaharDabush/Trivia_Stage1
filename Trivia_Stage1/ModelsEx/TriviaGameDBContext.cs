﻿using System;
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

    public void changeName(string newname, int userId)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserId == userId).FirstOrDefault();
        Updateuser.UserName = newname;
        db.SaveChanges();
    }
    public void changeMail(string newmail, int userId)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserId == userId).FirstOrDefault();
        Updateuser.UserMail = newmail;
        db.SaveChanges();
    }
    public void changePassword(string newpassword, int userId)
    {
        TriviaGameDBContext db = new TriviaGameDBContext();
        User Updateuser = db.Users.Where(u => u.UserId == userId).FirstOrDefault();
        Updateuser.Password = newpassword;
        db.SaveChanges();
    }




}

