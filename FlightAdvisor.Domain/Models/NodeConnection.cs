using System.Collections.Generic;

namespace FlightAdvisor.Domain.Models
{
    public class NodeConnection
    {
        public Node Target { get; private set; }
        public double Distance { get; private set; }

        public NodeConnection(Node target, double distance)
        {
            Target = target;
            Distance = distance;
        }
    }
}
