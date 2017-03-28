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
        [TestCase("A", "B", 8)]
        [TestCase("A", "C", 15)]
        [TestCase("A", "D", 5)]
        [TestCase("A", "E", 7)]
        [TestCase("A", "F", 22)]
        [TestCase("A", "G", 20)]
        [TestCase("A", "H", 11)]
        [TestCase("A", "I", 19)]
        [TestCase("A", "J", 17)]
        [TestCase("B", "A", 7)]
        [TestCase("B", "C", 7)]
        [TestCase("B", "D", 10)]
        [TestCase("B", "E", 1)]
        [TestCase("B", "F", 14)]
        [TestCase("B", "G", 12)]
        [TestCase("B", "H", 16)]
        [TestCase("B", "I", 17)]
        [TestCase("B", "J", 9)]
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