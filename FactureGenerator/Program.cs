/*
 * Program written by David Kapanga, Rogers Mukuna Kashala and Jean Robert Leriche for the OOP class's project (INF731)
 * the program is called FactureGenerator, it reads a file from disk gave by the user, 
 * and from that file, it makes a list of Article and generate a bill in a text file
 * this is the starting class  named Program.cs and contain the Main method
 * Created on Feb 18 2018
 * we test our program with I:\article_test.txt and marchandise.txt on different location
 */
using System;
using System.IO;

namespace FactureGenerator
{
    class Program
    {
        static short status = 0; //this is our program's status
        public static short Status
        {
            get
            {
                return status;
            }
            set
            {
                try
                {
                    Status = value;
                }
                catch(Exception)                
                {
                    throw new ExceptionStatus();
                }
            }
        }
        static void Main(string[] args)
        {
            string path;//will contain the full path to the source fille
            string nameOfFile;//will contain just the name of the file exctracted from the full path (with extension)            
            Facture facture; //the bill object that will generated or display the bill
            
            Console.WriteLine(ApplicationAPI.WelcomeBan());
            //while our program's state is 0, we will keep asking for a file to read, Except if the path is wrong or the file doesn't exist, in that case the program will exit
            while ((Status < 5) && (Status >= 0))
            {
                try
                {
                    Console.Write("Give the file's name and extension or the full path (if file are in different folader) (ex:I:\\article_test.txt): ");
                    path = Console.ReadLine();
                    path =Path.GetFullPath(path);                
                    ApplicationAPI.FileCheckingHandler(ref path);
                    String[] part = path.Split('\\');
                    nameOfFile = part[part.Length - 1];
                    //initialization of the variable facture, with output name, name of file and full path as parameters
                    facture = new Facture("Facture-" + nameOfFile, path, nameOfFile);
                    Console.WriteLine(ApplicationAPI.ShowMenu(path, nameOfFile));
                    ApplicationAPI.UserChoiceHandler(ref status,facture);
                }
                catch (ExceptionStatus e)
                {
                    Console.WriteLine(e.Message);
                    Environment.Exit(0);
                }
                catch (ExceptionFileDoesntExist e)
                {
                    Console.WriteLine(e.Message);
                    Environment.Exit(0);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    Environment.Exit(0);
                }
                catch (ExceptionsOnArticleCreation e)
                {
                    Console.WriteLine(e.Message);
                    Environment.Exit(0);
                }
                catch (ExceptionsOnFacture e)
                {
                    Console.WriteLine(e.Message);
                    Environment.Exit(0);
                }
                catch (ExceptionOnGeneratingFille e)
                {
                    Console.WriteLine(e.Message);
                    Environment.Exit(0);
                }
            }
        }
    }
}