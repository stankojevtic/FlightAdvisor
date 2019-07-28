namespace FlightAdvisor.Domain.Models
{
    public class NodeConnection
    {
        public Node Target { get; set; }
        public double Distance { get; set; }

        public NodeConnection(Node target, double distance)
        {
            Target = target;
            Distance = distance;
        }
    }
}
