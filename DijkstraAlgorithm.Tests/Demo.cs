using System.Linq;
using DijkstraAlgorithm.Graphing;
using DijkstraAlgorithm.Pathing;
using NUnit.Framework;

namespace DijkstraAlgorithm.Tests
{
    [TestFixture]
    public class Demo
    {
        [Test]
        public void Example()
        {
            // Create graph
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

            var graph = builder.Build();

            // Create path finder
            var pathFinder = new PathFinder(graph);

            // Find path
            const string origin = "A", destination = "C";

            var path = pathFinder.FindShortestPath(
                graph.Nodes.Single(node => node.Id == origin),
                graph.Nodes.Single(node => node.Id == destination));

            // Assert results
            Assert.That(path.Origin.Id == origin);
            Assert.That(path.Destination.Id == destination);
            Assert.That(path.Segments.Count, Is.EqualTo(3));
            Assert.That(path.Segments.Sum(s => s.Weight), Is.EqualTo(7));

            Assert.That(path.Segments.ElementAt(0).Origin.Id, Is.EqualTo("A"));
            Assert.That(path.Segments.ElementAt(0).Weight, Is.EqualTo(1));
            Assert.That(path.Segments.ElementAt(0).Destination.Id, Is.EqualTo("D"));

            Assert.That(path.Segments.ElementAt(1).Origin.Id, Is.EqualTo("D"));
            Assert.That(path.Segments.ElementAt(1).Weight, Is.EqualTo(1));
            Assert.That(path.Segments.ElementAt(1).Destination.Id, Is.EqualTo("E"));

            Assert.That(path.Segments.ElementAt(2).Origin.Id, Is.EqualTo("E"));
            Assert.That(path.Segments.ElementAt(2).Weight, Is.EqualTo(5));
            Assert.That(path.Segments.ElementAt(2).Destination.Id, Is.EqualTo("C"));
        }
    }
}