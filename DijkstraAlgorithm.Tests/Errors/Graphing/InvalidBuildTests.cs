using DijkstraAlgorithm.Exceptions;
using DijkstraAlgorithm.Graphing;
using NUnit.Framework;

namespace DijkstraAlgorithm.Tests.Errors.Graphing
{
    [TestFixture]
    public class InvalidBuildTests
    {
        [Test]
        public void Cannot_add_same_link_twice()
        {
            Assert.Throws<GraphBuilderException>(() => new GraphBuilder().AddLink("A", "B", 0).AddLink("A", "B", 1));
        }

        [Test]
        public void Cannot_add_same_node_twice()
        {
            Assert.Throws<GraphBuilderException>(() => new GraphBuilder().AddNode("A").AddNode("A"));
        }

        [Test]
        public void Cannot_create_link_from_non_existing_node()
        {
            Assert.Throws<GraphBuilderException>(() => new GraphBuilder().AddLink("A", "B", 0).Build());
        }

        [Test]
        public void Cannot_create_link_to_non_existing_node()
        {
            Assert.Throws<GraphBuilderException>(() => new GraphBuilder().AddNode("A").AddLink("A", "B", 0).Build());
        }
    }
}