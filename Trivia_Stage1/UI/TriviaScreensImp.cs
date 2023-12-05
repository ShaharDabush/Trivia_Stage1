using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trivia_Stage1.Models;

namespace Trivia_Stage1.UI
{
    public class TriviaScreensImp:ITriviaScreens
    {
        public User CurrentPlayer;
        //Place here any state you would like to keep during the app life time
        //For example, player login details...


        //Implememnt interface here
        public bool ShowLogin()
        {
            
            
            //    TriviaGameDBContext db = new TriviaGameDBContext();
            //    List<User> users = db.Users.ToList();
            //    foreach (User user in users)
            //    {
            //        Console.WriteLine(user.UserName);
            //    }
            //Console.ReadLine();
            
            
            //try
            //  {

            //    Console.Write("Please Type your email and Password: ");
            //    string email = Console.ReadLine();
            //    string password = Console.ReadLine();
            //    TriviaGameDBContext db = new TriviaGameDBContext();
            //    while (!db.IsEmailExist(email) && !db.ISPasswordExist(password))
            //    {
            //        Console.Write("your Email or password are wrong! Please try again:");
            //        email = Console.ReadLine();
            //        password = Console.ReadLine();
            //    }
            //    Console.WriteLine("Connecting to Server...");
            //    Console.ReadKey(true);
                //TriviaGameDBContext db = new TriviaGameDBContext();
                //List<User> users = db.Users.ToList();
                //foreach (User user in users)
                //{
                //    Console.WriteLine(user.UserName);
                //}
                //Console.ReadLine();


            try
            {

                Console.Write("Please Type your email and Password: ");
                string email = Console.ReadLine();
                string password = Console.ReadLine();
                TriviaGameDBContext db = new TriviaGameDBContext();
                while (!db.IsEmailExist(email) && !db.ISPasswordExist(password))
                {
                    Console.Write("your Email or password are wrong! Please try again:");
                    email = Console.ReadLine();
                    password = Console.ReadLine();
                }
                Console.WriteLine("Connecting to Server...");
                Console.ReadKey(true);

                CurrentPlayer = db.Users.Where(u => u.UserMail == email).FirstOrDefault();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("We are Sorry! You lost connection to the server, Please try Again later...");
                return false;
            }

          
        }
        public bool ShowSignup()
        {

            //Logout user if anyone is logged in!
            CurrentPlayer = null;
            //A reference to the logged in user should be stored as a member variable
            //in this class! Example:
            //this.currentyPLayer == null

            //Loop through inputs until a user/player is created or 
            //user choose to go back to menu
            char c = ' ';
            while (c != 'B' && c != 'b' && CurrentPlayer == null)
            {
                //Clear screen
                CleareAndTtile("Signup");

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
                    TriviaGameDBContext db = new TriviaGameDBContext();
                    //this.CurrentPlayer = db.Signup(email, password, name);
                    User u = new User
                    {
                        UserMail = email,
                        Password = password,
                        UserName = name,
                    };
                    db.Users.Add(u);
                    CurrentPlayer = db.Users.Where(u => u.UserMail == email).FirstOrDefault();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to signup! Email may already exist in DB!");
                    
                    return false;
                }
                /* Create instance of Business Logic and call the signup method
                 * For example:
                try
                {
                    TriviaDBContext db = new TriviaDBContext();
                    this.currentyPLayer = db.Signup(email, password, name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to signup! Email may already exist in DB!");
                }
                
                */

                
            }
            //return true if signup suceeded!
            return (false);
        }

        public void ShowAddQuestion()
        {
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }

        public void ShowPendingQuestions()
        {
            //TriviaGameDBContext db = new TriviaGameDBContext();
            //List<Question> questions = db.Questions.Include(p => p.Answers).ToList();

            //foreach (Question q in questions)
            //{
            //    Console.WriteLine(q.Question1.ToString());

            //    Menu please = new Menu("checking pending complete");

            //    //please.AddItem()

            //    //Console.WriteLine("do you accept the question?");

            //    Console.ReadLine();
                
            //}
            //Console.WriteLine("checking pending complete");
            Console.ReadKey(true);
        }
        public void ShowGame()
        {
            TriviaGameDBContext db = new TriviaGameDBContext();
            int QuesCount = db.Questions.Count();
            Random rnd = new Random();
            int QuesId = rnd.Next(1, QuesCount + 1);
            Question? Question = db.Questions.Where(q => q.QuestionId == QuesId).FirstOrDefault();
            Console.WriteLine(Question.Subject);
            Console.WriteLine(Question.Question1);
            List<Answer> answers = db.Answers.Where(a => a.QuestionId == QuesId).ToList();
            int AnsNum = 1;
            foreach (Answer answer in answers)
            {
                Console.WriteLine(AnsNum+"."+ "  " + answer.Answer1);
                AnsNum++;
            }
            Console.WriteLine("Input the number of the correct answer");
            AnsNum = int.Parse(Console.ReadLine());
            while (AnsNum <1 || AnsNum > 4)
            {
                Console.WriteLine("Invalid answer");
                AnsNum = int.Parse(Console.ReadLine());
            }
            if (answers[AnsNum-1].TrueFalse == true)
            {
                Console.WriteLine("You are correct!!!");
                CurrentPlayer.Score += 10;
            }
            else
            {
                Console.WriteLine("nuh uh");
                CurrentPlayer.Score += -5;
            }
            Console.ReadKey(true);
        }
        public void ShowProfile()
        {
            TriviaGameDBContext db = new TriviaGameDBContext();
            if(CurrentPlayer == null)
            {
                Console.WriteLine("Please Login First...");
            }
            else
            {
                Console.WriteLine("User Name:");
            }
           
             
            Console.ReadKey(true);
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
