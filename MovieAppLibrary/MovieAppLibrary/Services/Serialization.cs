using MovieAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieAppLibrary.Services
{
    public class Serialization
    {
        public static string filePath = @"C:\Users\Suraj\OneDrive\Desktop\internship\Day 20\MovieApplicationWithDll\MovieApp\MovieData.json";

        public static void MovieSerializer(List<Movie> movies)
        {
            string data = JsonSerializer.Serialize(movies);
            File.WriteAllText(filePath, data);
        }

        public static List<Movie> MovieDeserializer()
        {
            var moviesData = File.ReadAllText(filePath);
            var movies = JsonSerializer.Deserialize<List<Movie>>(moviesData);
            return movies;
        }
    }
}
