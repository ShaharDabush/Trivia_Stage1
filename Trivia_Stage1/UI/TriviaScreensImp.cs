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
        public User CurrentPlayer = null;
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
            TriviaGameDBContext db = new TriviaGameDBContext();
            List<User> users = db.Users.ToList();
            foreach (User user in users)
            {
                Console.WriteLine(user.UserName);
            }
            Console.ReadLine();
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
                    //TriviaGameDBContext db = new TriviaGameDBContext();
                    db.SignUp(email, password,name);
                    
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
                while (inwhile)
                {
                    Console.WriteLine("on which subject is your question");
                    subject = int.Parse(Console.ReadLine());
                    if (question != null) { inwhile = false; }
                }
                while (inwhile)
                {
                    Console.WriteLine("Please write your question");
                    question = Console.ReadLine();
                    if (question != null) { inwhile = false; }
                }
                Console.Clear();
                inwhile = true;
                while (inwhile)
                {
                    Console.WriteLine("Please write the answer");
                    Canswer = Console.ReadLine();
                    if (Canswer != null) { inwhile = false; }
                }
                inwhile = true;
                while (inwhile)
                {
                    Console.WriteLine("Please write the answer");
                    Wanswer1 = Console.ReadLine();
                    if (Wanswer1 != null) { inwhile = false; }
                }
                while (inwhile)
                {
                    Console.WriteLine("Please write the answer");
                    Wanswer2 = Console.ReadLine();
                    if (Wanswer2 != null) { inwhile = false; }
                }
                while (inwhile)
                {
                    Console.WriteLine("Please write the answer");
                    Wanswer3 = Console.ReadLine();
                    if (Wanswer3 != null) { inwhile = false; }
                }

                db.addquestion(question, subject, CurrentPlayer.UserId);
                int qustionid = 0;
                Question que = db.Questions.Where(p => p.Question1 == question).FirstOrDefault();
                qustionid = que.QuestionId;


                db.addanswer(Canswer, qustionid, true);
                db.addanswer(Wanswer1, qustionid, false);
                db.addanswer(Wanswer2, qustionid, false);
                db.addanswer(Wanswer3, qustionid, false);
                Console.ReadKey(true);
            }
            catch (Exception e)
            {
                Console.WriteLine("sorry, an error has accured");
                Console.WriteLine(e.Message);
                throw;
            }
            
        }

        public void ShowPendingQuestions() // completed exept the comit to database
        {
            TriviaGameDBContext db = new TriviaGameDBContext();
            List<Question> questions = db.Questions.Where(p => p.StatusId == 2).Include(p => p.Answers).ToList();

            foreach (Question q in questions)
            {
                String[][] answers = new string[3][];
                int count = 0;
                foreach (Answer a in q.Answers)
                {
                    answers[count][0] = a.Answer1;
                    answers[count][1] = a.TrueFalse.ToString();
                    count++;
                }

                Console.WriteLine($"{q.Question1.ToString()}\n" +
                    $"1.{answers[0][0]}" + $"{answers[0][1]}" + "\n" +
                    $"2.{answers[1][0]}" + $"{answers[0][1]}" + "\n" +
                    $"3.{answers[2][0]}" + $"{answers[0][1]}" + "\n" +
                    $"4.{answers[3][0]}" + $"{answers[0][1]}" + "\n");
                
                bool inwhile = true;
                String? answer = "";
                while (inwhile)
                {
                    Console.WriteLine("\\n" + "accept (a) \n deny(d)");
                    answer = Console.ReadLine();
                    if (answer == "a" || answer == "d") { inwhile = false; }
                }
                if (answer == "a")
                {
                    q.StatusId= 1;
                }
                else
                {
                    q.StatusId = 3;
                }

                db.Entry(q).State = EntityState.Modified;
                db.SaveChanges();

            }
            Console.WriteLine("checking pending complete");
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
                                string NewName = Console.ReadLine();
                                db.changeName(NewName,CurrentPlayer.UserId);
                                CurrentPlayer = db.Users.Where(u => u.UserMail == email).FirstOrDefault();
                                Console.WriteLine("change name completed! new name "+ CurrentPlayer.UserName);
                                
                                //CurrentPlayer.UserName = NewName; Exלעשות פעולה ב
                                //db.Entry(u).State = EntityState.Modified;
                                //db.SaveChanges();
                                //did that so no deed now

                                Exit = false;
                            break;
                        case 2:
                                Console.WriteLine("Enter your new Mail: ");
                                string NewMail = Console.ReadLine();
                                db.changeMail(NewMail, CurrentPlayer.UserId);
                                CurrentPlayer = db.Users.Where(u => u.UserMail == email).FirstOrDefault();
                                Console.WriteLine("change mail completed! new mail " + CurrentPlayer.UserName);

                                Exit = false;
                                break;
                        case 3:
                                Console.WriteLine("Enter your new password: ");
                                string NewPassword = Console.ReadLine();
                                db.changePassword(NewPassword, CurrentPlayer.UserId);
                                CurrentPlayer = db.Users.Where(u => u.UserMail == email).FirstOrDefault();
                                Console.WriteLine("change password completed! new password " + CurrentPlayer.UserName);

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
