using System.Linq;
using DijkstraAlgorithm.Exceptions;
using DijkstraAlgorithm.Graphing;
using DijkstraAlgorithm.Pathing;
using NUnit.Framework;

namespace DijkstraAlgorithm.Tests.Errors.Pathing
{
    [TestFixture]
    public class NoPathTests
    {
        private Graph _graph;
        private PathFinder _pathFinder;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _graph = new GraphBuilder()
                .AddNode("A").AddNode("B").AddNode("C").AddNode("D")
                .AddLink("A", "B", 1).AddLink("C", "D", 1)
                .Build();

            _pathFinder = new PathFinder(_graph);
        }

        [Test]
        [TestCase("A", "C")]
        [TestCase("A", "D")]
        [TestCase("B", "C")]
        [TestCase("B", "D")]
        [TestCase("B", "A")]
        public void Cannot_find_path(string origin, string destination)
        {
            Assert.Throws<NoPathFoundException>(() => _pathFinder.FindShortestPath(
                _graph.Nodes.Single(node => node.Id == origin),
                _graph.Nodes.Single(node => node.Id == destination)));
        }
    }
}