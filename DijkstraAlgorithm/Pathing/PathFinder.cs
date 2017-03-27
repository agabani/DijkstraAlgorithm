﻿using System.Collections.Generic;
using System.Linq;
using DijkstraAlgorithm.Graphing;

namespace DijkstraAlgorithm.Pathing
{
    public class PathFinder
    {
        private readonly Graph _graph;

        public PathFinder(Graph graph)
        {
            _graph = graph;
        }

        public Path FindShortestPath(Node origin, Node destination)
        {
            return Build(origin, destination, Process(origin, _graph));
        }

        private static List<Record> Process(Node origin, Graph graph)
        {
            var visited = new List<Node>();

            var records = CreateInitialRecords(origin, graph);

            var currentRecord = NextRecordToProcess(records, visited);

            do
            {
                foreach (var link in currentRecord.Vertex.Links)
                {
                    var weight = currentRecord.Weight + link.Weight;
                    var nextRecord = NextRecord(link, records);

                    if (weight < nextRecord.Weight)
                        nextRecord.Update(weight, currentRecord.Vertex);
                }

                visited.Add(currentRecord.Vertex);

                currentRecord = NextRecordToProcess(records, visited);
            } while (currentRecord != null);

            return records;
        }

        private static List<Record> CreateInitialRecords(Node node, Graph graph)
        {
            var records = graph.Nodes
                .Select(Record.Create)
                .ToList();

            const double initalWeight = 0;
            const Node initialNode = null;

            records
                .Single(r => r.Vertex == node)
                .Update(initalWeight, initialNode);

            return records;
        }

        private static Record NextRecordToProcess(IEnumerable<Record> records, ICollection<Node> visited)
        {
            return records
                .Where(record => !visited.Contains(record.Vertex))
                .OrderBy(r => r.Weight)
                .FirstOrDefault();
        }

        private static Record NextRecord(Link link, IEnumerable<Record> records)
        {
            return records.Single(record => record.Vertex == link.Destination);
        }

        private static Path Build(Node origin, Node destination, List<Record> records)
        {
            var path = Path.Create(origin);

            foreach (var segment in PathSegments(destination, records))
                path.AddSegment(segment);

            return path;
        }

        private static IEnumerable<PathSegment> PathSegments(Node destination, List<Record> records)
        {
            var segments = new List<PathSegment>();

            var currentRecord = Destination(destination, records);

            do
            {
                segments.Add(CreateSegment(currentRecord));
                currentRecord = PreviousRecord(currentRecord, records);
            } while (!IsOriginRecord(currentRecord));

            return segments.AsEnumerable().Reverse();
        }

        private static Record Destination(Node destination, IEnumerable<Record> records)
        {
            return records.Single(record => record.Vertex == destination);
        }

        private static PathSegment CreateSegment(Record currentRecord)
        {
            return PathSegment.Create(currentRecord.PreviousVertex, currentRecord.Vertex,
                currentRecord.PreviousVertex.Links.Single(link => link.Destination == currentRecord.Vertex).Weight);
        }

        private static Record PreviousRecord(Record currentRecord, IEnumerable<Record> records)
        {
            return records.Single(record => record.Vertex == currentRecord.PreviousVertex);
        }

        private static bool IsOriginRecord(Record currentRecord)
        {
            return currentRecord.PreviousVertex == null;
        }

        private class Record
        {
            private Record(Node vertex)
            {
                Vertex = vertex;
                Weight = double.MaxValue;
                PreviousVertex = null;
            }

            public Node Vertex { get; }
            public double Weight { get; private set; }
            public Node PreviousVertex { get; private set; }

            public static Record Create(Node vertex)
            {
                return new Record(vertex);
            }

            public void Update(double weight, Node previousVertex)
            {
                Weight = weight;
                PreviousVertex = previousVertex;
            }
        }
    }
}