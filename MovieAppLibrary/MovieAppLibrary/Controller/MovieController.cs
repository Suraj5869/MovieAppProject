﻿using MovieAppLibrary.Exceptions;
using MovieAppLibrary.Models;
using MovieAppLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppLibrary.Controller
{
    public class MovieController
    {
        public static int size = 0;
        public static int maxSize = 5;
        static List<Movie> movies = new List<Movie>();

        public void DisplayMovies()
        {
            IsEmpty();//Checks if the movie list is empty or not if it is empty then it throws an exception
            foreach (var movie in movies)
            {
                Console.WriteLine(movie);
            }
        }

        public void AddNewMovie(int id, string name, string genre, int year)
        {
            var movie = new Movie(id, name, genre, year);
            movies.Add(movie);//Add the movie with data in list
            size++;
        }
        public void RemoveAll()
        {
            movies = new List<Movie> { };
        }

        public Movie GetMovie(int id)
        {
            var movie = movies.Where(movie => movie.MovieId == id).FirstOrDefault();//Get the movie with same id entered by user if not present then return null
            if (movie == null)
            {
                throw new InvalidMovieException("Movie does not exist!!\n");
            }
            return movie;
        }
        public Movie GetMovie(string name)
        {
            var movie = movies.Where(movie => movie.MovieName == name).FirstOrDefault();//Get the movie with same name entered by user if not present then return null
            if (movie == null)
            {
                throw new InvalidMovieException("Movie does not exist!!\n");
            }
            return movie;
        }

        public void DeleteMovie(Movie movie)
        {
            movies = movies.Where(element => element != movie).ToList();//Add movies which is not equal to movie user want to delete in list
        }

        public void CheckMovie(string name)
        {
            var movie = movies.Where(movie => movie.MovieName == name).FirstOrDefault();//Get the movie with same name entered by user if not present then return null
            if (movie != null) {
                throw new MovieAlreadyExistException($"{name} is already exist in the movie store!!\n");
            }
        }
        public void CheckMovie(int id)
        {
            var movie = movies.Where(movie => movie.MovieId == id).FirstOrDefault();//Get the movie with same id entered by user if not present then return null
            if (movie != null) 
            {
                throw new MovieAlreadyExistException($"movie with id {id} is already exist!!\n");
            }
        }
        public void Deserialize()
        {
            if (File.Exists(Serialization.filePath))
            {
                movies = Serialization.MovieDeserializer();
                size = movies.Count;
            }
            else
            {
                movies = new List<Movie>();
            }
        }

        public void Serialize()
        {
            Serialization.MovieSerializer(movies);
        }

        public void CheckSize()
        {
            if (size >= maxSize)
            {
                throw new MovieStoreFullException("Movie Store is full!!!!\n");
            }
        }

        public void IsEmpty()
        {
            if (movies.Count == 0)
            {
                throw new EmptyStoreException("Movie Store is Empty!!!\n");
            }
        }
    }
}
