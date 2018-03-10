/*
 * Program written by David Kapanga, Rogers Mukuna Kashala and Jean Robert Leriche for the OOP class's project (INF731)
 * the program is called FactureGenerator, it reads a file from disk gave by the user, 
 * and from that file, it makes a list of Article and generate a bill in a text file
 * this is the ApplicationAPI containning all principal methods that are used in the main class
 * Created on Feb 28 2018
 */
using System;
using System.IO;

namespace FactureGenerator
{
    class ApplicationAPI
    {
        //this static method shows the welcome banner and has no parameters
        public static String WelcomeBan()
        {
            string msg = "\n";
            msg += new string(' ', 5);
            msg += new string('*', 50);
            msg += ("\n     *                                                *");
            msg += ("\n     *      WELCOME IN FACTUREGENERATOR Ver1.0        *");
            msg += ("\n     *       written by David K. And Roger K.         *");
            msg += ("\n     *            INF732 Class Project                *");
            msg += ("\n     *                                                *\n");
            msg += new string(' ', 5);
            msg += new string('*', 50);
            msg +="\n";
            return msg;
        }

        //this static method just shows the operations menu on the screen, it has 2 parameters (the name of file and the full path)
        public static String ShowMenu(string path, string nameOfFile)
        {
            string msg;
            msg = "\n";
            msg += "the source's full path is : " + path;
            msg += "\nthe file's name is : " + nameOfFile;
            msg += "\n---------------------------------------------------------------";
            msg += "\n";

            msg += "Choose an operation to do : ";
            msg += "\n1. Generate a text file and display on Console";
            msg += "\n2. Generate a text file without Display on console";
            msg += "\n3. Display on Console without generating a text file";
            msg += "\n4. Choose another file (if youchose the wrong file)";
            msg += "\n5. Exit";
            msg += "\nYour choice : ";
            return msg;
        }

        //choiceTreatment handle the different choices made by user 
        public static void UserChoiceHandler(ref short option, Facture facture)
        {
            string choice = Console.ReadLine();
            //we make sure that the user will give the correct status
            while ((!short.TryParse(choice, out option) || ((option > 5) || (option < 1))))
            {
                Console.Write("\nWrong status, please chose between 1 to 5 : ");
                choice = Console.ReadLine();
            }
            //handling different status made by user
            switch (option)
            {
                case 1:
                    facture.generateTextFile();                     
                    Console.WriteLine(facture.ToString());
                    Console.WriteLine("\nFile created with success \n");
                    break;
                case 2:
                    facture.generateTextFile();
                    Console.WriteLine("File created with success \n");                    
                    break;
                case 3:
                    Console.WriteLine(facture.ToString());
                    Console.Write("\nThis is a preview of the bill, to save it on disk press 1 or any other keys to choose another file source : ");
                    int save = int.Parse(Console.ReadLine());
                    //after printing the preview on console, we can propose to the user to save it on disk or discard
                    if (save == 1)
                    {
                        facture.generateTextFile();
                        Console.WriteLine("File saved with success \n");
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
                    Console.Write("\n Thanks for using FactureGenerator By David Kapanga, Rogers Mukuna Kashala and Jean Robert Leriche \n");
                    Environment.Exit(0);
                    break;
                default:
                    option = 0;
                    break;
            }
        }

        public static void FileCheckingHandler(ref string path)
        {
            if (path.Contains("\\bin\\Debug\\"))
            {
                String[] spliter = { "\\bin\\Debug" };
                string[] pathPart = path.Split(spliter, StringSplitOptions.RemoveEmptyEntries);
                path = pathPart[0] + pathPart[1];
            }
            //we verify that the path is correct and that the file exist
            if (String.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                throw new ExceptionFileDoesntExist();
            }
        }
    }
}
