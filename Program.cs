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

            else if(initialchoice.Equals("delete"))
            {
                Console.WriteLine("What movie would you like to delete?");
                string deletechoice = Console.ReadLine();

                DeleteEntry mydeletion = new DeleteEntry(deletechoice);
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
            //user @ to denote a variable in SQL
            mycommand.CommandText = "Select Entry From List Where Entry LIKE @movie";
            mycommand.Connection = myconnection;
            //Added the Parameter to my command so it can be used when I execute the command. I have already defined the movie variable in the constructor
            mycommand.Parameters.AddWithValue("@movie", "%" + movie + "%");
            SQLiteDataReader myreader = mycommand.ExecuteReader();

            //First Check if it has rows. Use the "!" to negate the myreader.HasRows returned value.
            if (!myreader.HasRows)
            {
                Console.WriteLine("It Doesn't Exist!");
            }
            else
            {
                //Have to use a while loop to iterate the command until there are no more rows to read from
                //Have to use a while loop to iterate the command until there are no more rows to read from
                while (myreader.Read())
                {
                    Console.WriteLine(myreader.GetValue(0));
                }
            }
        }
    }

    class DeleteEntry
    {
        public DeleteEntry(string deletionchoice)
        {
            SQLiteConnection myconnection = new SQLiteConnection("Data Source=Organizerdb.sqlite");
            myconnection.Open();

            SQLiteCommand mycommand = new SQLiteCommand();
            mycommand.CommandText = "DELETE FROM List WHERE Entry =@movie";
            mycommand.Connection = myconnection;
            mycommand.Parameters.AddWithValue("@movie", deletionchoice);
            SQLiteDataReader myreader = mycommand.ExecuteReader();

            Console.WriteLine("Record has been deleted");
        }
    }

        
}
