using ChessboardSteering.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessboardSteering.Helpers
{
    public class Pathfinder
    {
        public List<Node> FindPath(Node source, Node destination)
        {
            List<Node> visitedNodes = new List<Node>();
            Queue<List<Node>> pathToVisit = new Queue<List<Node>>();

            pathToVisit.Enqueue(new List<Node> { source });
            visitedNodes.Add(source);

            while (pathToVisit.Count != 0)
            {
                List<Node> currentPath = pathToVisit.Dequeue();
                Node lastNode = currentPath.ElementAt(currentPath.Count - 1);

                if (lastNode == destination)
                {
                    return currentPath;
                }

                foreach (var node in lastNode.ConnectedNodes.Where(t => t != null && t.isEmpty == true))
                {
                    if (visitedNodes.Contains(node) == false)
                    {
                        visitedNodes.Add(node);
                        List<Node> newPath = new List<Node>(currentPath);
                        newPath.Add(node);
                        pathToVisit.Enqueue(newPath);
                    }
                }          
            }

            return null;
        }
    }
}
