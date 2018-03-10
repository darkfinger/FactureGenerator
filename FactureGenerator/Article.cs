/*
 * Program written by David Kapanga, Rogers Mukuna Kashala and Jean Robert Leriche for the OOP class's project (INF731)
 * the program is called FactureGenerator, it reads a file from disk, and from that file, it makes a list of Article and generate a bill
 * this is the Article.cs class
 * Created on Feb 18 2018
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactureGenerator
{
    class Article
    {
        string no;
        char[] category = new char[2];
        int quantity;
        string description;
        float price;

        public string No {
            get{return this.no;}
            set
            {  
                //even thought is written that all No from file will be correct, we made a small chacking 
                //we replace the blank spaces and check if the imput is empty
                value = value.Replace(" ", "");
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.no = value;
                }
                else
                {
                    throw new ExceptionsOnArticleCreation(1);
                }                               
            }
        }
        public string Category {
            //the return value is a the char arrayconverted to string, from  attribut Category
            get { return this.category[0].ToString() + this.category[1].ToString(); }
            //we receive a string and we make sure it's the required character, if not set the default value
            set
            {
                string nt="nt",fp="fp";
                if (value.ToLower().Equals(nt) || value.ToLower().Equals(fp))
                {
                    this.category[0] = value.ToCharArray()[0];
                    this.category[1]=value.ToCharArray()[1];
                }
                else
                {
                    throw new ExceptionsOnArticleCreation(2);
                }
            }
        }
        public int Quantity {
            get
            {
                return this.quantity;
            }
            set
            {
                if (value >= 0)
                {
                    this.quantity = value;
                }
                else
                {
                    throw new ExceptionsOnArticleCreation(3);
                }
            }
        }
        public string Description {
            get { return this.description; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.description = value;
                }
                else
                {
                    throw new ExceptionsOnArticleCreation(4);
                }
                
            } 
        }
        public float Price {
            get { return this.price; }
            set
            {
                float f;
                //we try to parse the value to float, if success, the value is stored in f, and check if it's positif, 
                //otherwise set the default value
                if (float.TryParse(value.ToString(), out f) && f>=0)
                {
                    this.price = value;
                }
                else
                {
                    throw new ExceptionsOnArticleCreation(5);
                }
            }
        }        
        public float TotalByUnit
        {
            get
            {
                return this.Quantity * this.Price;
            }
        }
        public float TpsByUnit
        {
            get
            {
                float tpsByUnit = 0;
                //first we check if the article is taxable, if yes then compute the taxe and return it
                if (this.Category.Equals("fp")) { 
                tpsByUnit = this.TotalByUnit * ((float)5 / 100);
                }
                return tpsByUnit;
            }
        }
        public float TvqByUnit
        {
            get
            {
                float tvqByUnit = 0;
                //first we check if the article is taxable, if yes then compute the taxe and return it
                if (this.Category.Equals("fp"))
                {
                    tvqByUnit = this.TotalByUnit * ((float)9.975 / 100);
                }                   
                return tvqByUnit;
            }
        }
        public Article(string no, string category, int quantity, string description, float price)
        {
            this.No = no;
            this.Category = category;
            this.Quantity = quantity;
            this.Description = description;
            this.Price = price;
        }
        public Article(Article article)
        {
            this.No = article.No;
            this.Category = article.Category;
            this.Quantity = article.Quantity;
            this.Description = article.Description;
            this.Price = article.Price;
        }
        //overloaded method to print out the article details
        public override string ToString()
        {
            string detail="";
            detail += "The Article No is : "+this.No
                    + "\nThe Category is :  "+this.Category
                    + "\nThe Quantity is : "+this.Quantity
                    + "\nThe Description is : " + this.Description
                    + "\nThe Price is : " + this.Price
                    + "\nThe TPS is : " + this.TpsByUnit
                    + "\nThe TVQ is : " + this.TvqByUnit
                    + "\nThe Total Price for that quantity is : " + this.TotalByUnit;
            return detail;
        }
    }
}
