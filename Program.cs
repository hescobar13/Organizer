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
            /* 
            SQLiteConnection myconnection = new SQLiteConnection("Data Source=Organizerdb.sqlite");            
            myconnection.Open();
            */

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
            /*
            SQLiteCommand mycommand = new SQLiteCommand();
            mycommand.CommandText = "Select Entry From List";
            mycommand.Connection = myconnection;
            SQLiteDataReader myreader = mycommand.ExecuteReader();
            Console.WriteLine(myreader.Read());
            */

            /* Another Example of using parameters                  
            SQLiteConnection myconnection = new SQLiteConnection("Data Source=Organizerdb.sqlite");
            myconnection.Open();
            
            string movie = "thor";
            SQLiteCommand mycommand = new SQLiteCommand();
            mycommand.CommandText = "Select Entry From List Where Entry LIKE @movie";
            mycommand.Connection = myconnection;
            mycommand.Parameters.AddWithValue("@movie", movie);
            
            SQLiteDataReader myreader = mycommand.ExecuteReader();

            while (myreader.Read())
            {
                Console.WriteLine(myreader.GetValue(0));
            }
            Console.ReadLine();
            */





             SQLiteConnection myconnection = new SQLiteConnection("Data Source=Organizerdb.sqlite");
             myconnection.Open();


            Console.WriteLine("Choose what you want to do:");
            string initialchoice = Console.ReadLine();

            if (initialchoice.Equals("add"))
            {
                Console.WriteLine("Enter a list of movies seperated by a comma and no spaces");
                string movies = Console.ReadLine();

                InsertMovie newmovies = new InsertMovie(movies);
            }  
                    
            else if (initialchoice.Equals("list"))
            {
                List mylist = new List();
            }

            else if (initialchoice.Equals("search"))
            {
                Console.WriteLine("What movie do you want to search for?");
                string parameter = Console.ReadLine();

                Search mysearch = new Search(parameter);
            }
            


            Console.ReadLine();

        }

    }

    class InsertMovie
    {
        public InsertMovie(string input)
        {
            //splits on comma
            string[] inputarray = input.Split(',');

            //Creates connection string again for use inside this context
            SQLiteConnection myconnection = new SQLiteConnection("Data Source=Organizerdb.sqlite");
            myconnection.Open();
            //manually sets different properties of SQLitecommand; prepares it for execution
            SQLiteCommand mycommand = new SQLiteCommand();
            string insertdata = "Insert Into List (Entry) Values (@movie)";
            mycommand.CommandText = insertdata;
            mycommand.Connection = myconnection;
            
            //Limit amount of movies that can be entered at once            
            if (inputarray.Length > 10)
            {
                Console.WriteLine("You can only add 10 at a time");
            }
            else
            {
                foreach (string movie in inputarray)
                {
                    //set parameter here because movie variable doesn't exist before this
                    mycommand.Parameters.AddWithValue("movie", movie);
                    mycommand.ExecuteNonQuery();                  
                }
            }
        }

    }

    class List
    {
        //create constructor. Does not take any input. List command for now just lists everything. In the future might change it to take input for how much it should display
        public List()
        {
            SQLiteConnection myconnection = new SQLiteConnection("Data Source=Organizerdb.sqlite");
            myconnection.Open();

            SQLiteCommand mycommand = new SQLiteCommand();
            mycommand.CommandText = "Select Entry From List";
            mycommand.Connection = myconnection;
            SQLiteDataReader myreader = mycommand.ExecuteReader();
            while (myreader.Read())
            {
                Console.WriteLine(myreader.GetString(0));
            }           
        }
    }

    class Search
    {
        public Search(string movie)
        {           
            SQLiteConnection myconnection = new SQLiteConnection("Data Source=Organizerdb.sqlite");
            myconnection.Open();

            SQLiteCommand mycommand = new SQLiteCommand();
            mycommand.CommandText = "Select Entry From List Where Entry LIKE @movie";
            mycommand.Connection = myconnection;
            mycommand.Parameters.AddWithValue("@movie", "%" + movie + "%");
            SQLiteDataReader myreader = mycommand.ExecuteReader();

            while (myreader.Read())
            {
                Console.WriteLine(myreader.GetString(0));
                /*
                string exist = myreader.GetString(0);
                                
                if (exist.Equals(movie))
                {
                    Console.WriteLine("That movie exists in the colleciton");
                }
                else
                {
                    Console.WriteLine(myreader.GetValue(0));
                }
                */
                
            }
        }
    }
}
