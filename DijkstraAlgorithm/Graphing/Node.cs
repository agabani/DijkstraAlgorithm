using System.Collections.Generic;

namespace DijkstraAlgorithm.Graphing
{
    public class Node
    {
        private readonly List<Link> _links;

        private Node(string id)
        {
            Id = id;
            _links = new List<Link>();
        }

        public string Id { get; }

        public IReadOnlyList<Link> Links => _links;

        internal static Node Create(string id)
        {
            return new Node(id);
        }

        internal void Add(Link link)
        {
            _links.Add(link);
        }
    }
}