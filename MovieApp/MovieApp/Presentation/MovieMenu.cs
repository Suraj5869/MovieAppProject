using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MovieAppLibrary.Controller;
using MovieAppLibrary.Exceptions;
using MovieAppLibrary.Models;
namespace MovieApp.Presentation
{
    internal class MovieMenu
    {
        static MovieController manager = new MovieController();
        public static void MovieMenus()
        {
            Console.WriteLine("========= Welcome to the Movie App =========");
            manager.Deserialize();
            while (true)
            {
                Console.WriteLine($"Select option:\n" +
                    $"1. Display Movies\n" +
                    $"2. Add Movies\n" +
                    $"3. Edit Movie\n" +
                    $"4. Find Movie by Id\n" +
                    $"5. Find Movie by Name\n" +
                    $"6. Remove Movie by Id\n" +
                    $"7. Remove Movie by Name\n" +
                    $"8. Remove all\n" +
                    $"9. Exit\n");

                int choice = int.Parse(Console.ReadLine());

                SwitchMenu(choice);
            }
        }

        static void SwitchMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    Display();
                    break;
                case 2:
                    AddMovie();
                    break;
                case 3:
                    EditMovie();
                    break;
                case 4:
                    FindMovieById();
                    break;
                case 5:
                    FindMovieByName();
                    break;
                case 6:
                    RemoveById();
                    break;
                case 7:
                    RemoveByName();
                    break;
                case 8:
                    RemoveAllMovies();
                    break;
                case 9:
                    manager.Serialize();
                    Environment.Exit(0);
                    break;
            }
        }

        static void Display()
        {
            try
            {
                manager.DisplayMovies();//checks if there is any movie or not
            }
            catch (EmptyStoreException es)
            {
                Console.WriteLine(es.Message);
            }
        }

        static void AddMovie()
        {
            try
            {
                manager.CheckSize();//checks if the movie store is full or not
                Console.WriteLine("Enter Movie ID:");
                int id = int.Parse(Console.ReadLine());
                manager.CheckMovie(id);//checks if the movie with same id is present or not

                Console.WriteLine("Enter Movie Name");
                string name = Console.ReadLine();
                manager.CheckMovie(name);//checks if the movie with same name is present or not

                Console.WriteLine("Enter Movie Genre:");
                string genre = Console.ReadLine();

                Console.WriteLine("Enter Movie Release Year:");
                int year = int.Parse(Console.ReadLine());

                manager.AddNewMovie(id, name, genre, year);
            }
            catch(MovieStoreFullException mf)
            {
                Console.WriteLine(mf.Message);
            }
            catch(MovieAlreadyExistException me)
            {
                Console.WriteLine(me.Message);
            }
        }

        static void EditMovie()
        {
            try
            {
                manager.IsEmpty();//checks f the movie store is empty or not
                Console.WriteLine("Enter Movie Id:");
                int id = int.Parse(Console.ReadLine());
                var movie = manager.GetMovie(id);//get the movie with id if not present then throws an exception
                Console.WriteLine($"What you have to edit:\n" +
                    $"1. Id\n" +
                    $"2. Name\n" +
                    $"3. Genre\n" +
                    $"4. Year\n");
                int choice = int.Parse(Console.ReadLine());
                EditSwitch(choice, movie);
            }
            catch (InvalidMovieException me) 
            {
                Console.WriteLine(me.Message);    
            }
            catch(EmptyStoreException es)
            {
                Console.WriteLine(es.Message);
            }
        }

        private static void EditSwitch(int choice, Movie movie)
        {
            switch (choice)
            {
                case 1:
                    EditId(movie);
                    break;
                case 2:
                    EditName(movie);
                    break;
                case 3:
                    EditGenre(movie);
                    break;
                case 4:
                    EditYear(movie);
                    break;
            };
        }

        public static void EditId(Movie movie)
        {
            Console.WriteLine("Enter New movie Id:");
            int id = int.Parse(Console.ReadLine());
            try
            {
                manager.CheckMovie(id);//checks the movie store with same movie id present or not if present then throws an exception
                movie.MovieId = id;
                Console.WriteLine("Movie Id Updated!\n");
            }
            catch(MovieAlreadyExistException me)
            {
                Console.WriteLine(me.Message);
            }
        }

        public static void EditName(Movie movie) 
        {
            Console.WriteLine("Enter New movie name:");
            string name = Console.ReadLine();
            try
            {
                manager.CheckMovie(name);//checks the movie store with same movie name present or not if present then throws an exception
                movie.MovieName = name;
                Console.WriteLine("Movie Name Updated!!\n");
            }
            catch(MovieAlreadyExistException me)
            {
                Console.WriteLine(me.Message);
            }
        }

        public static void EditGenre(Movie movie) 
        {
            Console.WriteLine("Enter New Genre of movie:");
            string genre = Console.ReadLine();
            movie.MovieGenre = genre;
            Console.WriteLine("Movie Genre Updated!!\n");
        }

        public static void EditYear(Movie movie) {
            Console.WriteLine("Enter New movie Year:");
            int year = int.Parse(Console.ReadLine());
            movie.MovieYear = year;
            Console.WriteLine("Movie Year Updated!!\n");
        }

        public static void FindMovieById()
        {
            try
            {
                manager.IsEmpty(); //Checks if the store is empty or not if it is empty then it throws an exception
                Console.WriteLine("Enter Movie Id:");
                int id = int.Parse(Console.ReadLine());
                var movie = manager.GetMovie(id);
                Console.WriteLine(movie);
            }
            catch(InvalidMovieException me)
            {
                Console.WriteLine(me.Message);
            }
            catch(EmptyStoreException es)
            {
                Console.WriteLine(es.Message);
            }
        }

        public static void FindMovieByName()
        {
            try
            {
                manager.IsEmpty();//Checks if the store is empty or not if it is empty then it throws an exception
                Console.WriteLine("Enter Movie Name:");
                string name = Console.ReadLine();
                var movie = manager.GetMovie(name);//get the movie with name if not present then throws an exception
                Console.WriteLine(movie);
            }
            catch (InvalidMovieException me)
            {
                Console.WriteLine(me.Message);
            }
            catch(EmptyStoreException es)
            {
                Console.WriteLine(es.Message);
            }
        }

        public static void RemoveById()
        {
            try
            {
                manager.IsEmpty();//Checks if the store is empty or not if it is empty then it throws an exception
                Console.WriteLine("Enter Movie Id:");
                int id = int.Parse(Console.ReadLine());
                var movie = manager.GetMovie(id);//get the movie with id if not present then throws an exception
                manager.DeleteMovie(movie);
                Console.WriteLine($"Movie {id} deleted successfully.");
            }
            catch (InvalidMovieException me)
            {
                Console.WriteLine(me.Message);
            }
            catch (EmptyStoreException es)
            {
                Console.WriteLine(es.Message);
            }
        }

        public static void RemoveByName() 
        {
            try
            {
                manager.IsEmpty();//Checks if the store is empty or not if it is empty then it throws an exception
                Console.WriteLine("Enter Movie Name:");
                string name = Console.ReadLine();
                var movie = manager.GetMovie(name);//get the movie with name if not present then throws an exception
                manager.DeleteMovie(movie);
                Console.WriteLine($"Movie {name} deleted successfully.");
            }
            catch (InvalidMovieException me)
            {
                Console.WriteLine(me.Message);
            }
            catch(EmptyStoreException es)
            {
                Console.WriteLine(es.Message);
            }
        }

        public static void RemoveAllMovies()
        {
            try
            {
                manager.IsEmpty();//Checks if the store is empty or not if it is empty then it throws an exception
                manager.RemoveAll();
            }
            catch(EmptyStoreException es)
            {
                Console.WriteLine(es.Message);
            }
        }
    }
}
