using System.Linq;
using DijkstraAlgorithm.Graphing;
using DijkstraAlgorithm.Pathing;
using Xunit;

namespace DijkstraAlgorithm.Tests
{
    public class UniDirectionalTests
    {
        private readonly Graph _graph;
        private readonly PathFinder _pathFinder;

        public UniDirectionalTests()
        {
            var builder = new GraphBuilder();

            builder
                .AddNode("A")
                .AddNode("B")
                .AddNode("C")
                .AddNode("D")
                .AddNode("E")
                .AddNode("F")
                .AddNode("G")
                .AddNode("H")
                .AddNode("I")
                .AddNode("J");

            builder
                .AddLink("A", "B", 8)
                .AddLink("A", "D", 5)
                .AddLink("A", "E", 7);

            builder
                .AddLink("B", "C", 7)
                .AddLink("B", "E", 1);

            builder
                .AddLink("C", "B", 10)
                .AddLink("C", "F", 7)
                .AddLink("C", "J", 2);

            builder
                .AddLink("D", "A", 2)
                .AddLink("D", "H", 6);

            builder
                .AddLink("E", "A", 6)
                .AddLink("E", "D", 9);

            builder
                .AddLink("F", "B", 2)
                .AddLink("F", "I", 6);

            builder
                .AddLink("G", "C", 9);

            builder
                .AddLink("H", "E", 3)
                .AddLink("H", "I", 8);

            builder
                .AddLink("I", "E", 9);

            builder
                .AddLink("J", "C", 4)
                .AddLink("J", "G", 3)
                .AddLink("J", "I", 8);

            _graph = builder.Build();
            _pathFinder = new PathFinder(_graph);
        }

        [Theory]
        [InlineData("A", "B", 8d)]
        [InlineData("A", "C", 15d)]
        [InlineData("A", "D", 5d)]
        [InlineData("A", "E", 7d)]
        [InlineData("A", "F", 22d)]
        [InlineData("A", "G", 20d)]
        [InlineData("A", "H", 11d)]
        [InlineData("A", "I", 19d)]
        [InlineData("A", "J", 17d)]
        [InlineData("B", "A", 7d)]
        [InlineData("B", "C", 7d)]
        [InlineData("B", "D", 10d)]
        [InlineData("B", "E", 1d)]
        [InlineData("B", "F", 14d)]
        [InlineData("B", "G", 12d)]
        [InlineData("B", "H", 16d)]
        [InlineData("B", "I", 17d)]
        [InlineData("B", "J", 9d)]
        [InlineData("C", "A", 16d)]
        [InlineData("C", "B", 9d)]
        [InlineData("C", "D", 19d)]
        [InlineData("C", "E", 10d)]
        [InlineData("C", "F", 7d)]
        [InlineData("C", "G", 5d)]
        [InlineData("C", "H", 25d)]
        [InlineData("C", "I", 10d)]
        [InlineData("C", "J", 2d)]
        [InlineData("D", "A", 2d)]
        [InlineData("D", "B", 10d)]
        [InlineData("D", "C", 17d)]
        [InlineData("D", "E", 9d)]
        [InlineData("D", "F", 24d)]
        [InlineData("D", "G", 22d)]
        [InlineData("D", "H", 6d)]
        [InlineData("D", "I", 14d)]
        [InlineData("E", "A", 6d)]
        [InlineData("E", "B", 14d)]
        [InlineData("E", "C", 21d)]
        [InlineData("E", "D", 9d)]
        [InlineData("E", "F", 28d)]
        [InlineData("E", "G", 26d)]
        [InlineData("E", "H", 15d)]
        [InlineData("E", "I", 23d)]
        [InlineData("F", "A", 9d)]
        [InlineData("F", "B", 2d)]
        [InlineData("F", "C", 9d)]
        [InlineData("F", "D", 12d)]
        [InlineData("F", "E", 3d)]
        [InlineData("F", "G", 14d)]
        [InlineData("F", "H", 18d)]
        [InlineData("F", "I", 6d)]
        [InlineData("G", "A", 25d)]
        [InlineData("G", "B", 18d)]
        [InlineData("G", "C", 9d)]
        [InlineData("G", "D", 28d)]
        [InlineData("G", "E", 19d)]
        [InlineData("G", "F", 16d)]
        [InlineData("G", "H", 34d)]
        [InlineData("G", "I", 19d)]
        [InlineData("G", "J", 11d)]
        [InlineData("H", "A", 9d)]
        [InlineData("H", "B", 17d)]
        [InlineData("H", "C", 24d)]
        [InlineData("H", "D", 12d)]
        [InlineData("H", "E", 3d)]
        [InlineData("H", "F", 31d)]
        [InlineData("H", "G", 29d)]
        [InlineData("H", "I", 8d)]
        [InlineData("H", "J", 26d)]
        [InlineData("I", "A", 15d)]
        [InlineData("I", "B", 23d)]
        [InlineData("I", "C", 30d)]
        [InlineData("I", "D", 18d)]
        [InlineData("I", "E", 9d)]
        [InlineData("I", "F", 37d)]
        [InlineData("I", "G", 35d)]
        [InlineData("I", "H", 24d)]
        [InlineData("I", "J", 32d)]
        [InlineData("J", "A", 20d)]
        [InlineData("J", "B", 13d)]
        [InlineData("J", "C", 4d)]
        [InlineData("J", "D", 23d)]
        [InlineData("J", "E", 14d)]
        [InlineData("J", "F", 11d)]
        [InlineData("J", "G", 3d)]
        [InlineData("J", "H", 29d)]
        [InlineData("J", "I", 8d)]
        public void Test(string origin, string destination, double weight)
        {
            var path = _pathFinder.FindShortestPath(
                _graph.Nodes.Single(node => node.Id == origin),
                _graph.Nodes.Single(node => node.Id == destination));

            Assert.Equal(path.Origin.Id, origin);
            Assert.Equal(path.Destination.Id, destination);
            Assert.Equal(path.Segments.Sum(s => s.Weight), weight);
        }
    }
}