using System.Collections.Generic;

namespace FlightAdvisor.Domain.Models
{
    public class Node
    {
        public List<NodeConnection> _connections;
        public string Name { get; private set; }
        public double DistanceFromStart { get; set; }
        public List<string> RouteNames { get; set; }
        public List<double> RoutePrices { get; set; }
        public List<string> Routes { get; set; }

        public IEnumerable<NodeConnection> Connections
        {
            get { return _connections; }
        }

        public Node(string name)
        {
            Name = name;
            _connections = new List<NodeConnection>();
            Routes = new List<string>();
            RouteNames = new List<string>();
            RoutePrices = new List<double>();
        }

        public void AddConnection(Node targetNode, double distance)
        {
            if (targetNode == null || targetNode == this || distance <= 0) return;

            _connections.Add(new NodeConnection(targetNode, distance));
        }
    }
}
