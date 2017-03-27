namespace DijkstraAlgorithm.Graphing
{
    public class Link
    {
        private Link(double weight, Node destination)
        {
            Weight = weight;
            Destination = destination;
        }

        public double Weight { get; }
        public Node Destination { get; }

        internal static Link Create(double weight, Node destination)
        {
            return new Link(weight, destination);
        }
    }
}