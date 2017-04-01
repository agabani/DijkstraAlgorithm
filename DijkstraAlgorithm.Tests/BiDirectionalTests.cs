using System.Linq;
using DijkstraAlgorithm.Graphing;
using DijkstraAlgorithm.Pathing;
using Xunit;

namespace DijkstraAlgorithm.Tests
{
    public class BiDirectionalTests
    {
        private readonly Graph _graph;
        private readonly PathFinder _pathFinder;

        public BiDirectionalTests()
        {
            var builder = new GraphBuilder();

            builder
                .AddNode("A")
                .AddNode("B")
                .AddNode("C")
                .AddNode("D")
                .AddNode("E");

            builder
                .AddLink("A", "B", 6)
                .AddLink("A", "D", 1);

            builder
                .AddLink("B", "A", 6)
                .AddLink("B", "C", 5)
                .AddLink("B", "D", 2)
                .AddLink("B", "E", 2);

            builder
                .AddLink("C", "B", 5)
                .AddLink("C", "E", 5);

            builder
                .AddLink("D", "A", 1)
                .AddLink("D", "B", 2)
                .AddLink("D", "E", 1);

            builder
                .AddLink("E", "B", 2)
                .AddLink("E", "C", 5)
                .AddLink("E", "D", 1);

            _graph = builder.Build();

            _pathFinder = new PathFinder(_graph);
        }

        [Theory]
        [InlineData("A", "B", 3.0d)]
        [InlineData("A", "C", 7.0d)]
        [InlineData("A", "D", 1.0d)]
        [InlineData("A", "E", 2.0d)]
        [InlineData("B", "A", 3.0d)]
        [InlineData("B", "C", 5.0d)]
        [InlineData("B", "D", 2.0d)]
        [InlineData("B", "E", 2.0d)]
        [InlineData("C", "A", 7.0d)]
        [InlineData("C", "B", 5.0d)]
        [InlineData("C", "D", 6.0d)]
        [InlineData("C", "E", 5.0d)]
        [InlineData("D", "A", 1.0d)]
        [InlineData("D", "B", 2.0d)]
        [InlineData("D", "C", 6.0d)]
        [InlineData("D", "E", 1.0d)]
        [InlineData("E", "A", 2.0d)]
        [InlineData("E", "B", 2.0d)]
        [InlineData("E", "C", 5.0d)]
        [InlineData("E", "D", 1.0d)]
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