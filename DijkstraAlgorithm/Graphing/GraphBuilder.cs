﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DijkstraAlgorithm.Graphing
{
    public class GraphBuilder
    {
        private readonly Dictionary<string, Dictionary<string, Func<Node, Link>>> _links;
        private readonly Dictionary<string, Func<Node>> _nodes;

        public GraphBuilder()
        {
            _nodes = new Dictionary<string, Func<Node>>();
            _links = new Dictionary<string, Dictionary<string, Func<Node, Link>>>();
        }

        public GraphBuilder AddNode(string id)
        {
            _nodes.Add(id, () => Node.Create(id));
            return this;
        }

        public GraphBuilder AddLink(string sourceId, string destinationId, double weight)
        {
            if (!_links.ContainsKey(sourceId))
                _links.Add(sourceId, new Dictionary<string, Func<Node, Link>>());

            _links[sourceId].Add(destinationId, node => Link.Create(weight, node));
            return this;
        }

        public Graph Build()
        {
            var nodes = _nodes.ToDictionary(node => node.Key, node => node.Value.Invoke());

            foreach (var source in _links)
            foreach (var destination in source.Value)
                nodes[source.Key].Add(destination.Value.Invoke(nodes[destination.Key]));

            var graph = Graph.Create();

            foreach (var node in nodes.Values)
                graph.Add(node);

            return graph;
        }
    }
}