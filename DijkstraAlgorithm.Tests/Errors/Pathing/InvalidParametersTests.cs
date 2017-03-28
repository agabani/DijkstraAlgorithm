﻿using System.Linq;
using DijkstraAlgorithm.Exceptions;
using DijkstraAlgorithm.Graphing;
using DijkstraAlgorithm.Pathing;
using NUnit.Framework;

namespace DijkstraAlgorithm.Tests.Errors.Pathing
{
    [TestFixture]
    public class InvalidParametersTests
    {
        [Test]
        public void Cannot_create_with_null_graph()
        {
            Assert.Throws<PathFinderException>(() => new PathFinder(null));
        }

        [Test]
        public void Cannot_path_to_self()
        {
            var graph = new GraphBuilder().AddNode("A").Build();
            var pathFinder = new PathFinder(graph);

            Assert.Throws<PathFinderException>(() => pathFinder.FindShortestPath(
                graph.Nodes.Single(node => node.Id == "A"),
                graph.Nodes.Single(node => node.Id == "A")));
        }

        [Test]
        public void Cannot_path_from_null()
        {
            var graph = new GraphBuilder().AddNode("A").Build();
            var pathFinder = new PathFinder(graph);

            Assert.Throws<PathFinderException>(() => pathFinder.FindShortestPath(
                null,
                graph.Nodes.Single(node => node.Id == "A")));
        }

        [Test]
        public void Cannot_path_to_null()
        {
            var graph = new GraphBuilder().AddNode("A").Build();
            var pathFinder = new PathFinder(graph);

            Assert.Throws<PathFinderException>(() => pathFinder.FindShortestPath(
                graph.Nodes.Single(node => node.Id == "A"),
                null));
        }
    }
}