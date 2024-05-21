using System;

namespace Model.EntException
{
    public class UnreachableCodeException : Exception
    {
        public UnreachableCodeException() : base("This line of code should not be reached.")
        {

        }
    }
}
