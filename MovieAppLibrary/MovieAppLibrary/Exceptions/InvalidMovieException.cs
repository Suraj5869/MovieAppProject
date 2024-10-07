using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppLibrary.Exceptions
{
    public class InvalidMovieException:Exception
    {
        public InvalidMovieException(string message):base(message)
        {
            
        }
    }
}
