using System;

namespace DijkstraAlgorithm.Exceptions
{
    public class NoPathFoundException : Exception
    {
        internal NoPathFoundException(string message) : base(message)
        {
        }
    }
}