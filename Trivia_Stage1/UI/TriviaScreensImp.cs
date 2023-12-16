using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trivia_Stage1.Models;

namespace Trivia_Stage1.UI
{
    public class TriviaScreensImp:ITriviaScreens
    {
        public User CurrentPlayer = null; // stores the current player and all of his attributes
        //Place here any state you would like to keep during the app life time
        //For example, player login details...


        //Implememnt interface here
        public bool ShowLogin() //set the public veriable currentPlayer to the user whoes email and password corespond to an email and password from the database
        {

            try
            {

                Console.WriteLine("Please Type your mail and Password: ");
                Console.Write("Mail: ");
                string email = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                TriviaGameDBContext db = new TriviaGameDBContext();
                while (!db.IsEmailExist(email) || !db.ISPasswordExist(password))//checks if current email and password are in the database
                {
                    Console.WriteLine("your Mail or password are wrong! Please try again:");
                    Console.Write("Mail: ");
                    email = Console.ReadLine();
                    Console.Write("Password: ");
                    password = Console.ReadLine();
                }
                Console.WriteLine("Connecting to Server...");
                Console.WriteLine("Click 'Enter' to start");
                Console.ReadKey(true);

                CurrentPlayer = db.Users.Where(u => u.UserMail == email && u.Password == password).FirstOrDefault();//set CurrentPlayer to the user attributes
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("We are Sorry! You lost connection to the server, Please try Again later...");
                return false;
            }


        }
        public bool ShowSignup() //gets user attributes from the current user, adds them to the data base and set the public veriable currentPlayer to the user 
        {
            TriviaGameDBContext db = new TriviaGameDBContext();
            //Logout user if anyone is logged in!
            CurrentPlayer = null;
            //A reference to the logged in user should be stored as a member variable
            //in this class! Example:
            //this.currentyPLayer == null

            //Loop through inputs until a user/player is created or 
            //user choose to go back to menu
            char c = ' ';
            while (c != 'B' && c != 'b' && CurrentPlayer == null) // checks if the sighen up is still in progress
            {
                //Clear screen
                CleareAndTtile("Signup");
                //gets user veriables from the user
                Console.Write("Please Type your email: ");
                string email = Console.ReadLine();
                while (!IsEmailValid(email))
                {
                    Console.Write("Bad Email Format! Please try again:");
                    email = Console.ReadLine();
                }

                Console.Write("Please Type your password: ");
                string password = Console.ReadLine();
                while (!IsPasswordValid(password))
                {
                    Console.Write("password must be at least 4 characters! Please try again: ");
                    password = Console.ReadLine();
                }

                Console.Write("Please Type your Name: ");
                string name = Console.ReadLine();
                while (!IsNameValid(name))
                {
                    Console.Write("name must be at least 3 characters! Please try again: ");
                    name = Console.ReadLine();
                }


                Console.WriteLine("Connecting to Server...");
                

                try
                {
                    //add the new user to the database
                    db.SignUp(email, password,name);//in ModelsEX
                    //makes the current player the newly created player
                    CurrentPlayer = db.Users.Where(u => u.UserMail == email).FirstOrDefault();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to signup! Email may already exist in DB!");
                    
                    return false;
                }
                

                
            }
            //return true if signup suceeded!
            return (false);
        }

        public void ShowAddQuestion()//adds questions to the database
        {
            if (CurrentPlayer.Score < 100)//checks if you are allowed to enter a question (your score is bigger then 100)
            {
                Console.WriteLine("you do not have access\n" +
                    "press any key to preside");
                Console.ReadKey(true);
            }
            else
            {
                try
                {
                    TriviaGameDBContext db = new TriviaGameDBContext();
                    int subject = 0;
                    String? question = "";
                    String? Canswer = "";
                    String? Wanswer1 = "";
                    String? Wanswer2 = "";
                    String? Wanswer3 = "";

                    bool inwhile = true;
                    while (inwhile)// add question subject
                    {
                        try
                        {
                            Console.WriteLine("on which subject is your question?\n" +
                            "1.sport\n" +
                            "2.politics\n" +
                            "3.history\n" +
                            "4.science\n" +
                            "5.high School ramon");
                            subject = int.Parse(Console.ReadLine());
                            if (subject != null) { inwhile = false; }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("please imput a valid number");
                        }

                    }
                    inwhile = true;
                    while (inwhile)//add question
                    {
                        Console.WriteLine("Please write your question");
                        question = Console.ReadLine();
                        if (question != null) { inwhile = false; }
                    }
                    Console.Clear();
                    inwhile = true;
                    while (inwhile)// add the currect answer
                    {
                        Console.WriteLine("Please write the answer");
                        Canswer = Console.ReadLine();
                        if (Canswer != null) { inwhile = false; }
                    }
                    inwhile = true;
                    while (inwhile)//add wrong answer 1
                    {
                        Console.WriteLine("Please write a wrong answer");
                        Wanswer1 = Console.ReadLine();
                        if (Wanswer1 != null) { inwhile = false; }
                    }
                    inwhile = true;
                    while (inwhile)//add wrong answer 2
                    {
                        Console.WriteLine("Please write a wrong the answer");
                        Wanswer2 = Console.ReadLine();
                        if (Wanswer2 != null) { inwhile = false; }
                    }
                    inwhile = true;
                    while (inwhile)//add wrong answer 3
                    {
                        Console.WriteLine("Please write a wrong the answer");
                        Wanswer3 = Console.ReadLine();
                        if (Wanswer3 != null) { inwhile = false; }
                    }

                    db.addquestion(question, subject, CurrentPlayer.UserId);// add the question to the database (function in ModelsEX) 
                    int qustionid = 0; 
                    Question que = db.Questions.Where(p => p.Question1 == question).FirstOrDefault();//get the id of the question to link the answers to it
                    qustionid = que.QuestionId;

                    //add the answers to the database (function in ModelsEX) 
                    db.addanswer(Canswer, qustionid, true);
                    db.addanswer(Wanswer1, qustionid, false);
                    db.addanswer(Wanswer2, qustionid, false);
                    db.addanswer(Wanswer3, qustionid, false);
                    Console.WriteLine("question added");

                    db.changeScore(CurrentPlayer.UserMail);// decreace the score of the user by 100 to make them unable to add another question before answering more of them(function in ModelsEx)

                    Console.ReadKey(true);
                }
                catch (Exception e)
                {
                    Console.WriteLine("sorry, an error has accured");
                    Console.WriteLine(e.Message);
                    Console.ReadKey(true);
                }

            }

        }

        public void ShowPendingQuestions() // an admin and master function used to accept newly created questions
        {
            if (CurrentPlayer.AccessId == 1) // if the user is not tan admin or a master denied access
            {
                Console.WriteLine("you do not have access\n" +
                    "press any key to preside");
                Console.ReadKey(true);
            }
            else
            {
                TriviaGameDBContext db = new TriviaGameDBContext();
                List<Question> questions = db.Questions.Where(p => p.StatusId == 2).Include(p => p.Answers).ToList();// gets all of the questions that require accepting

                foreach (Question q in questions)
                {
                    String[,] answers = new string[999, 999];
                    int count = 0;
                    foreach (Answer a in q.Answers)
                    {
                        answers[count, 0] = a.Answer1;
                        answers[count, 1] = a.TrueFalse.ToString();
                        count++;
                    }

                    Console.WriteLine($"{q.Question1.ToString()}\n" +
                        $"1.{answers[0, 0]} " + $"{answers[0, 1]}" + "\n" +
                        $"2.{answers[1, 0]} " + $"{answers[1, 1]}" + "\n" +
                        $"3.{answers[2, 0]} " + $"{answers[2, 1]}" + "\n" +
                        $"4.{answers[3, 0]} " + $"{answers[3, 1]}" + "\n");

                    bool inwhile = true;
                    String? answer = "";
                    while (inwhile)
                    {
                        Console.WriteLine("\n" + "accept (a) \ndeny(d)");
                        answer = Console.ReadLine();
                        if (answer == "a" || answer == "d") { inwhile = false; }
                    }
                    if (answer == "a")// if the question is accepted change its status the be accepted
                    {
                        q.StatusId = 1;
                    }
                    else// if the question is denide change it status to be denide
                    {
                        q.StatusId = 3;
                    }

                    db.Entry(q).State = EntityState.Modified;// commit to the database
                    db.SaveChanges();

                }
                Console.WriteLine("checking pending complete");
                Console.ReadKey(true);
            }
            
        }
        public void ShowGame()//A function that shows the questions and answers so you can answer them
        {   
            TriviaGameDBContext db = new TriviaGameDBContext();
            int QuesCount = db.Questions.Count();
            Random rnd = new Random();
            string c = " ";
            int privQues = 0;
            while (c != "e" && c != "E")// used to exit the game
            {
                Console.Clear();
                int QuesId = rnd.Next(1, QuesCount + 1);// gets a random question from the database
                while (QuesId == privQues)//if the question is the same as the previus one it will chose a new one
                    QuesId = rnd.Next(1, QuesCount + 1);
                privQues = QuesId;
                Question? Question = db.Questions.Where(q => q.QuestionId == QuesId).FirstOrDefault();// gets the question from the database
                QuestionSubject? QuesSubj = db.QuestionSubjects.Where(s => s.SubjectId == Question.SubjectId).FirstOrDefault();// gets the question subject from the database
                Console.WriteLine("Question subject is: " + QuesSubj.Subject );
                Console.WriteLine(Question.Question1);
                List<Answer> answers = db.Answers.Where(a => a.QuestionId == QuesId).ToList();//get the answers to the question from the database
                int AnsNum = 1;
                foreach (Answer answer in answers)
                {
                    Console.WriteLine(AnsNum + "." + "  " + answer.Answer1);
                    AnsNum++;
                }
                Console.WriteLine("Input the number of the correct answer");
                AnsNum = int.Parse(Console.ReadLine());//request user input to answer the question
                while (AnsNum < 1 || AnsNum > 4)
                {
                    Console.WriteLine("Invalid answer");
                    AnsNum = int.Parse(Console.ReadLine());
                }
                if (answers[AnsNum - 1].TrueFalse == true)//if user answerd correctly
                {
                   db.CorrectAnswer(CurrentPlayer.UserMail);//adds 10 points to the users score and total score (function in ModalsEx)

                }
                else
                {
                    db.IncorrectAnswer(CurrentPlayer.UserMail);//detracts 5 points from the users score and total score (function in ModalsEx)

                }
                Console.WriteLine("Input e to exit, input anything else to continue");
                c = Console.ReadLine();
            }
           
            
        }
        public void ShowProfile()//allow a user to view and change his profile
        {
            Console.Clear();
            TriviaGameDBContext db = new TriviaGameDBContext();
            AccessLv PlayerLevel = db.AccessLvs.Where(l => l.AccessId == CurrentPlayer.AccessId).FirstOrDefault();//gets the player access level
            if (CurrentPlayer == null)//if the user is not logged in
            {
                Console.WriteLine("Please Login First...");
            }
            else
            {
                Console.WriteLine("Hello " + CurrentPlayer.UserName + "!" );//shows the username
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine("Your Mail is: " + CurrentPlayer.UserMail);//shows the user's mail
                Console.WriteLine("Your Password is: " + CurrentPlayer.Password);//shows the user's password
                Console.WriteLine("You are in " + PlayerLevel.AccessLevel + " level");//shows the user's access leve;
                Console.WriteLine("Your score is: " + CurrentPlayer.Score);//shows the user's current score
                Console.WriteLine("Your Total score is: " + CurrentPlayer.TotalScore);//shows the user's total score
                Console.WriteLine("You added " + CurrentPlayer.QuastionsAdded + " questions");//shows how many questions the user added
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine("If you want to Update your data enter Y if else enter N");
                string YesOrNo = Console.ReadLine();
                if(YesOrNo == "Y")
                {
                    bool Exit = true;
                    while (Exit)
                    { 
                        Console.WriteLine();
                    Console.WriteLine("Update:");
                    Console.WriteLine("1.  Name");
                    Console.WriteLine("2.  Email");
                    Console.WriteLine("3.  Password");
                    Console.WriteLine("4.  Exit");
                    Console.WriteLine("Choose to update:");
                    int UpdateChoice = int.Parse(Console.ReadLine());
                    switch (UpdateChoice)
                    {
                        case 1://updating name
                            Console.WriteLine("Enter your new name: ");
                                string NewName = Console.ReadLine();
                                while (!IsNameValid(NewName))
                                {
                                    Console.Write("name must be at least 3 characters! Please try again: ");
                                     NewName = Console.ReadLine();
                                }
                                db.changeName(NewName, CurrentPlayer.UserMail);//changes the user's username in the database(function in ModelsEx)
                                CurrentPlayer = db.Users.Where(u => u.UserMail == CurrentPlayer.UserMail).FirstOrDefault();//chenges the current player's name in the game
                                Console.WriteLine("change name completed! new name " + CurrentPlayer.UserName);

                                ShowProfile();
                                Exit = false;
                                break;
                        case 2://updating mail
                                Console.WriteLine("Enter your new Mail: ");
                                string NewMail = Console.ReadLine();
                                while (!IsEmailValid(NewMail))
                                {
                                    Console.Write("Bad Email Format! Please try again:");
                                    NewMail = Console.ReadLine();
                                }
                                db.changeMail(NewMail, CurrentPlayer.UserMail)//changes the user's mail in the database(function in ModelsEx)
                                CurrentPlayer = db.Users.Where(u => u.UserMail == NewMail).FirstOrDefault();//changes the user's mail in the game
                                Console.WriteLine("change mail completed! new mail " + CurrentPlayer.UserMail);

                                ShowProfile();
                                Exit = false;
                                break;
                        case 3://updating password
                                Console.WriteLine("Enter your new password: ");
                                string NewPassword = Console.ReadLine();
                                while (!IsPasswordValid(NewPassword))
                                {
                                    Console.Write("password must be at least 4 characters! Please try again: ");
                                    NewPassword = Console.ReadLine();
                                }
                                db.changePassword(NewPassword, CurrentPlayer.UserMail);//changes the user's password in the database(function in ModelsEx)
                                CurrentPlayer = db.Users.Where(u => u.UserMail == CurrentPlayer.UserMail).FirstOrDefault();//changes the user's password in the game
                                Console.WriteLine("change password completed! new password " + CurrentPlayer.UserMail);

                                ShowProfile();
                                Exit = false;
                                break;
                        case 4://exit show Profile
                                Exit = false;
                                break;
                            default: 
                                Console.WriteLine("You didnt enter a valied numbers, Please renter a number");
                                break;
                    }

                    }

                }


            }
           
             
            
        }

        //Private helper methodfs down here...
        private void CleareAndTtile(string title)
        {
            Console.Clear();
            Console.WriteLine($"\t\t\t\t\t{title}");
            Console.WriteLine();
        }

        private bool IsEmailValid(string emailAddress)
        {
            var pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

            var regex = new Regex(pattern);
            return regex.IsMatch(emailAddress);
        }

        private bool IsPasswordValid(string password)
        {
            return password != null && password.Length >= 3;
        }

        private bool IsNameValid(string name)
        {
            return name != null && name.Length >= 3;
        }

    }
}
