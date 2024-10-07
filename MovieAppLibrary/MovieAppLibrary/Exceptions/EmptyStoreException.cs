using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAppLibrary.Exceptions
{
    public class EmptyStoreException:Exception
    {
        public EmptyStoreException(string message):base(message) { }
    }
}
