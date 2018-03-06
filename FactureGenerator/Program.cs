/*
 * Program written by David Kapanga and Roger Kashala for the OOP class project (INF731) 
 * the program is called FactureGenerator, it reads a file from disk, and from that file, it makes a list of Article and generate a bill
 * this is the starting class  named Program.cs and contain the main method
 * Created on Feb 18 2018
 * we test our program with I:\article_test.txt
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactureGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            const string DEFAULT_PATH = "..\\..\\";
            string path;//will contain the full path to the source fille
            string nameOfFile;//will contain the name of the file exctracted from the full path
            short option = 0; //this is our program's status 
            Facture facture; //the bill that will be generated
                             
            WelcomeBan();

            //whil our program's state is 0, we will keep asking for a file to read, Except if the path is wrong or the file doesn't exist, in that case the program will exit
            while (option == 0)
            {
                Console.Write("Give the full path with the name of the file (ex:I:\\article_test.txt): ");
                path = Console.ReadLine();
                path = DEFAULT_PATH + path;
                //whe verify that the path is correct and that the file exist
                if (String.IsNullOrWhiteSpace(path) || !File.Exists(path))
                {
                    Console.WriteLine("incorrect path name or file doesn't exists, the program will close ");
                    Environment.Exit(0);
                }
                String[] part = path.Split('\\');
                nameOfFile = part[part.Length - 1];
                //initialisation of the facture variable, with output name, name of file and full path as parameters
                facture = new Facture("Facture-" + nameOfFile, path, nameOfFile);
                ShowMenu(path, nameOfFile);                                
                ChoiceTreatment(ref option,facture);
            }
        }

        //this static method shows the welcome banner and has no parameters
        private static void WelcomeBan()
        {
            Console.WriteLine();
            Console.Write(new string(' ', 5));
            Console.WriteLine(new string('*',50));
            Console.WriteLine("     *                                                *");
            Console.WriteLine("     *      WELCOME IN FACTUREGENERATOR Ver1.0        *");
            Console.WriteLine("     *       written by David K. And Roger K.         *");
            Console.WriteLine("     *            INF732 Class Project                *");
            Console.WriteLine("     *                                                *");            
            Console.Write(new string(' ', 5));
            Console.WriteLine(new string('*', 50));
            Console.WriteLine();
        }

        //this static method just shows the operations menu on the screen, it has 2 parameters (the name of file and the full path)
        private static void ShowMenu(string path, string nameOfFile)
        {
            Console.WriteLine();
            Console.WriteLine("the source's full path is : " + path);
            Console.WriteLine("the file's name is : " + nameOfFile);
            Console.WriteLine("---------------------------------------------------------------");

            Console.WriteLine("Choose an operation to do : ");
            Console.WriteLine("1. Generate a text file and display on Console");
            Console.WriteLine("2. Generate a text file without Display on console");
            Console.WriteLine("3. Display on Console without generating a text file");
            Console.WriteLine("4. Choose another file (if youchose the wrong file)");
            Console.WriteLine("5. Exit");
            Console.Write("\nYour choice : ");
        }

        //choiceTreatment handle different choices made the by user, and has 2 parameters. 
        public static void ChoiceTreatment(ref short option, Facture facture)
        {
            string choice = Console.ReadLine();
            //we make sure that the user will give the correct option
            while ((!short.TryParse(choice, out option) || ((option > 5) || (option < 1))))
            {
                Console.Write("\nWrong option, please chose between 1 to 5 : ");
                choice = Console.ReadLine();
            }
            //handling different option made by user
            switch (option)
            {
                case 1:
                    if (facture.generateTextFile() == 1)
                    {
                        Console.WriteLine(facture.ToString());
                        Console.WriteLine("\nFile created with success \n");
                        option = 0;
                    }
                    else
                    {
                        Console.WriteLine("Operation failed, an unknown Error accured (Try verified that you have permission to write on disk) \n");
                        option = 0;
                    }
                    break;
                case 2:
                    if (facture.generateTextFile() == 1)
                    {
                        Console.WriteLine("File created with success \n");
                        option = 0;
                    }
                    else
                    {
                        Console.WriteLine("Operation failed, an unknown Error accured (Try verified that you have permission to write on disk) \n");
                        option = 0;
                    }
                    break;
                case 3:
                    Console.WriteLine(facture.ToString());
                    Console.Write("\nThis is a preview of the bill, to save it on disk press 1 or any other keys to choose another file source : ");
                    int save = int.Parse(Console.ReadLine());
                    //after printing the preview on console, we can propose to the user to save it on disk or discard
                    if (save == 1)
                    {
                        if (facture.generateTextFile() == 1)
                        {
                            Console.WriteLine("File saved with success \n");
                            option = 0;
                        }
                        else
                        {
                            Console.WriteLine("Operation failed, an unknown Error accured (Try verified that you have permission to write on disk) \n");
                            option = 0;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Preview was not saved");
                        option = 0;
                    }
                    break;
                case 4:
                    Console.Write("Try with another file. ");
                    option = 0;
                    break;
                case 5:
                    Console.Write("\n Thanks for using FactureGenerator By David Kapanga and Roger Kashala \n");
                    Environment.Exit(0);
                    break;
                default:
                    option = 0;
                    break;
            }
        }
    }
}

