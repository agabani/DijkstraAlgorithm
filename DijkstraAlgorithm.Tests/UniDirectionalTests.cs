using System.Linq;
using DijkstraAlgorithm.Graphing;
using DijkstraAlgorithm.Pathing;
using NUnit.Framework;

namespace DijkstraAlgorithm.Tests
{
    [TestFixture]
    public class UniDirectionalTests
    {
        private Graph _graph;
        private PathFinder _pathFinder;

        [OneTimeSetUp]
        public void OneTimeSetUp()
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

        [Test]
        [TestCase("A", "B", 8d)]
        [TestCase("A", "C", 15d)]
        [TestCase("A", "D", 5d)]
        [TestCase("A", "E", 7d)]
        [TestCase("A", "F", 22d)]
        [TestCase("A", "G", 20d)]
        [TestCase("A", "H", 11d)]
        [TestCase("A", "I", 19d)]
        [TestCase("A", "J", 17d)]
        [TestCase("B", "A", 7d)]
        [TestCase("B", "C", 7d)]
        [TestCase("B", "D", 10d)]
        [TestCase("B", "E", 1d)]
        [TestCase("B", "F", 14d)]
        [TestCase("B", "G", 12d)]
        [TestCase("B", "H", 16d)]
        [TestCase("B", "I", 17d)]
        [TestCase("B", "J", 9d)]
        [TestCase("C", "A", 16d)]
        [TestCase("C", "B", 9d)]
        [TestCase("C", "D", 19d)]
        [TestCase("C", "E", 10d)]
        [TestCase("C", "F", 7d)]
        [TestCase("C", "G", 5d)]
        [TestCase("C", "H", 25d)]
        [TestCase("C", "I", 10d)]
        [TestCase("C", "J", 2d)]
        [TestCase("D", "A", 2d)]
        [TestCase("D", "B", 10d)]
        [TestCase("D", "C", 17d)]
        [TestCase("D", "E", 9d)]
        [TestCase("D", "F", 24d)]
        [TestCase("D", "G", 22d)]
        [TestCase("D", "H", 6d)]
        [TestCase("D", "I", 14d)]
        [TestCase("E", "A", 6d)]
        [TestCase("E", "B", 14d)]
        [TestCase("E", "C", 21d)]
        [TestCase("E", "D", 9d)]
        [TestCase("E", "F", 28d)]
        [TestCase("E", "G", 26d)]
        [TestCase("E", "H", 15d)]
        [TestCase("E", "I", 23d)]
        [TestCase("F", "A", 9d)]
        [TestCase("F", "B", 2d)]
        [TestCase("F", "C", 9d)]
        [TestCase("F", "D", 12d)]
        [TestCase("F", "E", 3d)]
        [TestCase("F", "G", 14d)]
        [TestCase("F", "H", 18d)]
        [TestCase("F", "I", 6d)]
        [TestCase("G", "A", 25d)]
        [TestCase("G", "B", 18d)]
        [TestCase("G", "C", 9d)]
        [TestCase("G", "D", 28d)]
        [TestCase("G", "E", 19d)]
        [TestCase("G", "F", 16d)]
        [TestCase("G", "H", 34d)]
        [TestCase("G", "I", 19d)]
        [TestCase("G", "J", 11d)]
        [TestCase("H", "A", 9d)]
        [TestCase("H", "B", 17d)]
        [TestCase("H", "C", 24d)]
        [TestCase("H", "D", 12d)]
        [TestCase("H", "E", 3d)]
        [TestCase("H", "F", 31d)]
        [TestCase("H", "G", 29d)]
        [TestCase("H", "I", 8d)]
        [TestCase("H", "J", 26d)]
        [TestCase("I", "A", 15d)]
        [TestCase("I", "B", 23d)]
        [TestCase("I", "C", 30d)]
        [TestCase("I", "D", 18d)]
        [TestCase("I", "E", 9d)]
        [TestCase("I", "F", 37d)]
        [TestCase("I", "G", 35d)]
        [TestCase("I", "H", 24d)]
        [TestCase("I", "J", 32d)]
        public void Test(string origin, string destination, double weight)
        {
            var path = _pathFinder.FindShortestPath(
                _graph.Nodes.Single(node => node.Id == origin),
                _graph.Nodes.Single(node => node.Id == destination));

            Assert.That(path.Origin.Id == origin);
            Assert.That(path.Destination.Id == destination);
            Assert.That(path.Segments.Sum(s => s.Weight), Is.EqualTo(weight));
        }
    }
}