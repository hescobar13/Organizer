using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;

namespace ListOrganizer
{
    class Program
    {               
       static void Main(string[] args)
        {
            //Create connection object to be used with SQLiteCommand
            SQLiteConnection myconnection = new SQLiteConnection("Data Source=Organizerdb.sqlite");            
            myconnection.Open();

            /* Creates a new table
            string createtable = "Create Table List (Entry string)";
            SQLiteCommand mycommand = new SQLiteCommand(createtable, myconnection);
            mycommand.ExecuteNonQuery();
            */

            /*Inserts into a table
            SQLiteCommand mycommand = new SQLiteCommand();
            string insertdata = "Insert Into List (Entry) Values ('Hello')";
            mycommand.CommandText = insertdata;
            //the next line i have to manually set connection for the class because there is no constructor tht only takes connection
            //you can also do SQLiteCommand mycommand = myconnection.CreateCommand()... this automatically sets the connection for the 
            //new command object that is created, instead of having to do it manually as I do it below
            mycommand.Connection = myconnection;
            Console.WriteLine(mycommand.CommandText);
            mycommand.ExecuteNonQuery();
            Console.ReadLine();
            */

            //Read from Table
            SQLiteCommand mycommand = new SQLiteCommand();
            mycommand.CommandText = "Select Entry From List";
            mycommand.Connection = myconnection;
            SQLiteDataReader myreader = mycommand.ExecuteReader();
            Console.WriteLine(myreader.Read());

            Console.ReadLine();











            /*
            Console.WriteLine("Choose what you want to do:");
            string initialchoice = Console.ReadLine();



            if (initialchoice.StartsWith("add"))
            {
                Console.WriteLine("Enter a list of movies");
                string movies = Console.ReadLine();

                InsertMovie newmovies = new InsertMovie(movies);
            }
            /*
            else if (initialchoice.StartsWith("delete"))
            {

            }
           

            Console.ReadLine();
            */
        }

    }
    /*
    class InsertMovie
    {
        public InsertMovie(string input)
        {
            string[] inputarray = input.Split();
           
            if (inputarray.Length > 10)
            {
                Console.WriteLine("You can only add 10 at a time");
            }
            else
            {
                foreach (string movie in inputarray)
                {
                    Console.WriteLine(movie);
                }

            }
        }*/
        
}
