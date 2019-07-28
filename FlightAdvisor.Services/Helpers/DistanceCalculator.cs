using FlightAdvisor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightAdvisor.Core.Helpers
{
    public class DistanceCalculator
    {
        public List<GraphResultModel> CalculateDistances(Graph graph, string startingNode)
        {
            InitialiseGraph(graph, startingNode);
            ProcessGraph(graph, startingNode);
            return ExtractDistances(graph);
        }

        private void InitialiseGraph(Graph graph, string startingNode)
        {
            foreach (Node node in graph.Nodes.Values)
                node.DistanceFromStart = double.PositiveInfinity;
            graph.Nodes[startingNode].DistanceFromStart = 0;
        }

        private void ProcessGraph(Graph graph, string startingNode)
        {
            bool finished = false;
            var queue = graph.Nodes.Values.ToList();
            while (!finished)
            {
                Node nextNode = queue.OrderBy(n => n.DistanceFromStart).FirstOrDefault(
                    n => !double.IsPositiveInfinity(n.DistanceFromStart));
                if (nextNode != null)
                {
                    ProcessNode(nextNode, queue);
                    queue.Remove(nextNode);
                }
                else
                {
                    finished = true;
                }
            }
        }

        private void ProcessNode(Node node, List<Node> queue)
        {
            var connections = node.Connections.Where(c => queue.Contains(c.Target));
            foreach (var connection in connections)
            {
                double distance = node.DistanceFromStart + connection.Distance;
                if (distance < connection.Target.DistanceFromStart)
                {
                    connection.Target.DistanceFromStart = distance;
                    connection.Target.RouteNames.Add(node.Name);
                    connection.Target.RoutePrices.Add(node.DistanceFromStart);
                }
            }
        }

        private List<GraphResultModel> ExtractDistances(Graph graph)
        {
            List<GraphResultModel> graphResults = new List<GraphResultModel>();
            foreach(var node in graph.Nodes)
            {
                graphResults.Add(new GraphResultModel
                {
                    Key = node.Key,
                    Distance = node.Value.DistanceFromStart,
                    RouteNames = node.Value.RouteNames,
                    RoutePrices = node.Value.RoutePrices
                });
            }
            return graphResults;
        }
    }
}
