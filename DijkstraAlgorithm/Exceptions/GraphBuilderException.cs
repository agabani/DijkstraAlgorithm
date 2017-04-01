using System;

namespace DijkstraAlgorithm.Exceptions
{
    public class GraphBuilderException : Exception
    {
        internal GraphBuilderException(string message) : base(message)
        {
        }
    }
}