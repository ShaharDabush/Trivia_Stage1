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
        public User CurrentPlayer;
        //Place here any state you would like to keep during the app life time
        //For example, player login details...


        //Implememnt interface here
        public bool ShowLogin()
        {


            //TriviaGameDBContext db = new TriviaGameDBContext();
            //List<User> users = db.Users.ToList();
            //foreach (User user in users)
            //{
            //    Console.WriteLine(user.UserName);
            //}
            //Console.ReadLine();


            try
            {

                Console.WriteLine("Please Type your mail and Password: ");
                Console.Write("Mail: ");
                string email = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                TriviaGameDBContext db = new TriviaGameDBContext();
                while (!db.IsEmailExist(email) && !db.ISPasswordExist(password))
                {
                    Console.WriteLine("your Mail or password are wrong! Please try again:");
                    Console.Write("Mail: ");
                    Console.Write("Password: ");
                    email = Console.ReadLine();
                    password = Console.ReadLine();
                }
                Console.WriteLine("Connecting to Server...");
                Console.WriteLine("Click 'Enter' to start");
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
            Console.WriteLine("Not implemented yet! Press any key to continue...");
            Console.ReadKey(true);
        }
        public void ShowProfile()
        {
            TriviaGameDBContext db = new TriviaGameDBContext();
            AccessLv PlayerLevel = db.AccessLvs.Where(l => l.AccessId == CurrentPlayer.AccessId).FirstOrDefault();
            if (CurrentPlayer == null)
            {
                Console.WriteLine("Please Login First...");
            }
            else
            {
                Console.WriteLine("Hello " + CurrentPlayer.UserName + "!" );
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine("Your Mail is:" + CurrentPlayer.UserMail);
                Console.WriteLine("Your Password is:" + CurrentPlayer.Password);
                Console.WriteLine("You are in " + PlayerLevel.AccessLevel + " level");
                Console.WriteLine("Your score is:" + CurrentPlayer.Password);
                Console.WriteLine("Your Total score is:" + CurrentPlayer.TotalScore);
                Console.WriteLine("You added " + CurrentPlayer.QuastionsAdded + " quastions");
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
                        case 1:
                            Console.WriteLine("Enter your new name: ");
                                Exit = false;
                            break;
                        case 2:
                                Console.WriteLine("Enter your new Mail: ");
                                Exit = false;
                                break;
                        case 3:
                                Console.WriteLine("Enter your new password: ");
                                Exit = false;
                                break;
                        case 4:
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
