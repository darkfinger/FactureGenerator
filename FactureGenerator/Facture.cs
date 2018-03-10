/*
 * Program written by David Kapanga, Rogers Mukuna Kashala and Jean Robert Leriche for the OOP class's project (INF731) 
 * the program is called FactureGenerator, it reads a file from disk, and from that file, it makes a list of Article and generate a bill
 * this is the Facture.cs class
 * Created on Feb 18 2018
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FactureGenerator
{
    class Facture
    {
        const string DEFAULT_NOFACTURE = "notSet";
        const string DEFAULT_PATH = "..\\..\\";
        private static StreamReader fReader;
        private static StreamWriter fWriter;

        //every attribut and properties of this class are encapsulated, we can't set anything from outside
        //the only way to set value is by sending a file from the constructor
        string nameFacture;
        string pathOfArticleFile;
        string pathOfFactureFile;
        string nameOfArticleFille;
        List<Article> listArticle;

        private string NameFacture
        {
            get { return this.nameFacture; }
            set
            {
                value = value.Replace(" ", "");
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.nameFacture = value;
                }
                else
                {
                    throw new ExceptionsOnFacture(1);
                }

            }
        }
        private string PathOfArticleFile
        {
            get { return this.pathOfArticleFile; }
            set
            {
                // we verified the path of the file in the main methode, so that we considere that the path will be correct
                //but still we make sure we get something accurate
                value = value.Replace(" ", "");
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.pathOfArticleFile = value;
                }
                else
                {
                    throw new ExceptionsOnFacture(2);
                }
                
            }
        }
        private string PathOfFactureFile
        {
            get { return this.pathOfFactureFile; }
            set
            {
                value = value.Replace(" ", "");
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.pathOfFactureFile = value;
                }
                else
                {
                    throw new ExceptionsOnFacture(3);
                }
            }
        }
        private string NameOfArticleFille
        {
            get { return this.nameOfArticleFille; }
            set
            {
                value = value.Replace(" ", "");
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.nameOfArticleFille = value;
                }
                else
                {
                    throw new ExceptionsOnFacture(4);
                }
            }
        }
        private int NumberOfArticle
        {
            get { return this.ListArticle.Count; }
        }
        private List<Article> ListArticle
        {
            get
            {
                return this.listArticle;
            }
            set
            {
                try
                {
                    this.listArticle = value;
                }
                catch (Exception)
                {
                    throw new ExceptionsOnFacture(5);
                }                
            }
        }
        private float SubTotal
        {
            get
            {
                float subTotal=0;
                for (int i = 0; i < this.ListArticle.Count; ++i)
                {
                    subTotal += this.ListArticle[i].TotalByUnit;
                }
                    return subTotal;
            }
        }
        private float Tvq
        {
            get
            {
                float tvq = 0;
                for (int i = 0; i < this.ListArticle.Count; ++i)
                {
                    tvq += this.ListArticle[i].TvqByUnit;
                }
                return tvq;
            }
        }
        private float Tps
        {
            get
            {
                float tps = 0;
                for (int i = 0; i < this.ListArticle.Count; ++i)
                {
                    tps += this.ListArticle[i].TpsByUnit;
                }
                return tps;
            }
        }
        private float Total
        {
            get
            {
                float Total = this.SubTotal+this.Tps+this.Tvq;
                return Total;
            }
        }

        public Facture(string nameFacture, string pathOfArticleFile, string nameOfArticleFille)
        {
            this.NameFacture = nameFacture;
            this.PathOfArticleFile = pathOfArticleFile;
            this.NameOfArticleFille = nameOfArticleFille;
            this.PathOfFactureFile = DEFAULT_PATH;
            this.ListArticle = null;
            ReadFile();
        }

        //this method read the file passed througth the constructor, save data found in a list and
        //pass that lis to  the attribut listArticle
        private void ReadFile()
        {
            //we put it in a try catch, in case we have a trouble reading the file source (ex: if we don't have a reading permision)
            try
            {
                fReader = new StreamReader(this.PathOfArticleFile, Encoding.UTF7);                      
                string fileData;//will contain each line
                char[] spliter = new char[] { ';' };
                string[] item;//array that contain each part split by the spliter
                List<Article> list=new List<Article>();//dynamic list for articles, to be passed to listArticle
                while (!fReader.EndOfStream)  // read while file is not at the end
                {
                    fileData = fReader.ReadLine();
                    item = fileData.Split(spliter);
                    Article aArticle;
                    //we try to write in the array, if the file source has correct format (same number of index needed)
                    try
                    {
                        aArticle = new Article(item[0], item[1], int.Parse(item[2]), item[3], float.Parse(item[4]));
                        list.Add(aArticle);
                    }
                    catch (ExceptionsOnArticleCreation e)
                    {
                        throw new ExceptionsOnArticleCreation(e.Code);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new ExceptionsOnFacture(7);
                    }
                }
                this.ListArticle = list;
            }
            catch (ExceptionsOnFacture)
            {
                throw new ExceptionsOnFacture(5);
            }
            catch (IOException)
            {
                throw new ExceptionsOnFacture(6);
            }
            finally
            {
                fReader.Close();
            }                
        }
        //method read the details of each data of the listArticle and use it to generate a text file, 
        //if the text file exist, it will overide it
        public void generateTextFile()
        {
            //we put it in a try catch, in case we have a trouble writing on disk (ex: if we don't have a writing permision)
            try
            {
                fWriter = new StreamWriter(this.PathOfFactureFile + this.NameFacture, false, Encoding.UTF8);

                fWriter.WriteLine();
                fWriter.WriteLine(new String('*', 100));
                fWriter.WriteLine("*                                                                                                  *");
                fWriter.WriteLine("  Facture Produite pour le fichier " + this.NameOfArticleFille + " par la team David Kapanga, Rogers Mukuna Kashala and Jean Robert Leriche ");
                fWriter.WriteLine("*                                                                                                  *");
                fWriter.WriteLine(new String('*', 100));
                fWriter.WriteLine();
                //ToStringTable is a method from the API imported(Class TableParser.cs) that allows us to parse a list to a table view
                fWriter.WriteLine(ListArticle.ToStringTable(
                  new[] { "No Article", "Qte", "Description", "Price", "Category", "Total by article" },
                  a => a.No, a => a.Quantity, a => a.Description, a => a.Price, a => a.Category, a => a.TotalByUnit));
                fWriter.WriteLine("-----------------------------------------------------------------------------------");
                fWriter.WriteLine("                                                                 Sub Total : " + this.SubTotal);
                fWriter.WriteLine("                                                                 TPS : " + this.Tps);
                fWriter.WriteLine("                                                                 TVQ : " + this.Tvq);
                fWriter.WriteLine("                                                                 TOTAL : " + this.Total);
            }
            catch (Exception)
            {
                throw new ExceptionOnGeneratingFille();
            }
            finally
            {
                fWriter.Close();
            }
            
        }
        //toString is overrided to return the same bill format generated in the text file
        public override string ToString()
        {
            string detail = "";
            detail += "\n";
            detail +=new String('*', 100);
            detail += "\n*                                                                                                  *\n";
            detail += "  Facture Produite pour le fichier " + this.NameOfArticleFille + " par la team David Kapanga, Rogers Mukuna Kashala and Jean Robert Leriche ";
            detail += "\n*                                                                                                  *\n";
            detail += new String('*', 100);
            detail += "\n\n";
            detail += (ListArticle.ToStringTable(
              new[] { "No Article", "Qte", "Description", "Price", "Category", "Total by article" },
              a => a.No, a => a.Quantity, a => a.Description, a => a.Price, a => a.Category, a => a.TotalByUnit));
            detail += (" ----------------------------------------------------------------------------------");
            detail += ("\n                                                               Sub Total : " + this.SubTotal);
            detail += ("\n                                                               TPS : " + this.Tps);
            detail += ("\n                                                               TVQ : " + this.Tvq);
            detail += ("\n                                                               TOTAL : " + this.Total);
            return detail;
        }
    }
}
