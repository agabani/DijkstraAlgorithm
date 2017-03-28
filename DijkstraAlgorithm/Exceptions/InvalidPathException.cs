using System;

namespace DijkstraAlgorithm.Exceptions
{
    public class InvalidPathException : Exception
    {
        internal InvalidPathException(string message) : base(message)
        {
        }
    }
}