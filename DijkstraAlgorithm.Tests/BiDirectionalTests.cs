using System.Linq;
using DijkstraAlgorithm.Graphing;
using DijkstraAlgorithm.Pathing;
using NUnit.Framework;

namespace DijkstraAlgorithm.Tests
{
    [TestFixture]
    public class BiDirectionalTests
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

        [Test]
        [TestCase("A", "B", 3.0d)]
        [TestCase("A", "C", 7.0d)]
        [TestCase("A", "D", 1.0d)]
        [TestCase("A", "E", 2.0d)]
        [TestCase("B", "A", 3.0d)]
        [TestCase("B", "C", 5.0d)]
        [TestCase("B", "D", 2.0d)]
        [TestCase("B", "E", 2.0d)]
        [TestCase("C", "A", 7.0d)]
        [TestCase("C", "B", 5.0d)]
        [TestCase("C", "D", 6.0d)]
        [TestCase("C", "E", 5.0d)]
        [TestCase("D", "A", 1.0d)]
        [TestCase("D", "B", 2.0d)]
        [TestCase("D", "C", 6.0d)]
        [TestCase("D", "E", 1.0d)]
        [TestCase("E", "A", 2.0d)]
        [TestCase("E", "B", 2.0d)]
        [TestCase("E", "C", 5.0d)]
        [TestCase("E", "D", 1.0d)]
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