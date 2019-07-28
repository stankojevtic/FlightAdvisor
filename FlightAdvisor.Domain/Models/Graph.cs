using System.Collections.Generic;

namespace FlightAdvisor.Domain.Models
{
    public class Graph
    {
        public IDictionary<string, Node> Nodes { get; set; }

        public Graph()
        {
            Nodes = new Dictionary<string, Node>();
        }

        public void AddNode(string name)
        {
            var node = new Node(name);
            Nodes.Add(name, node);
        }

        public void AddConnection(string fromNode, string toNode, double distance)
        {
            Nodes[fromNode].AddConnection(Nodes[toNode], distance);
        }
    }
}
