using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public class Node
    {
        public Vector2Int position; // position in the grid
        public Node parent; // parent node
        public float gCost; // cost from start to current node
        public float hCost; // cost from current node to end
        public float fCost => gCost + hCost; // total gCost + hCost
        public bool walkable; // is this node walkable
        public Node(Vector2Int pos, bool isWalkable) // constructor
        {
            position = pos;
            walkable = isWalkable;
        }
    }
    public List<Node> FindPath(Vector2Int startPos, Vector2Int tartgetPos, Node[,] grid) // find the path
    {
        List<Node> openSet = new List<Node>(); // list of nodes to be evaluated
        List<Node> closedSet = new List<Node>(); // list of nodes already evaluated
        Node startNode = grid[startPos.x, startPos.y]; // start node
        Node targetNode = grid[tartgetPos.x, tartgetPos.y]; // target node

        openSet.Add(startNode); // add the start node to the open set
        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0]; // get the first node in the open set
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) // if the fCost is lower or the fCost is the same but the hCost is lower
                {
                    currentNode = openSet[i]; // get the node with the lowest fCost
                }
            }
            openSet.Remove(currentNode); // remove the current node from the open set
            closedSet.Add(currentNode); // add the current node to the closed set
            if (currentNode == targetNode) // if the current node is the target node
            {
                return RetracePath(startNode, targetNode); // return the path
            }
            foreach (Node neighbour in GetNeighbours(currentNode, grid)) // for each neighbour of the current node
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) // if the neighbour is not walkable or the neighbour is in the closed set
                {
                    continue; // skip the neighbour
                }
                float newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour); // calculate the new movement cost to the neighbour
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) // if the new movement cost is lower than the neighbour's gCost or the neighbour is not in the open set
                {
                    neighbour.gCost = newMovementCostToNeighbour; // set the neighbour's gCost to the new movement cost
                    neighbour.hCost = GetDistance(neighbour, targetNode); // set the neighbour's hCost to the distance to the target node
                    neighbour.parent = currentNode; // set the neighbour's parent to the current node
                    if (!openSet.Contains(neighbour)) // if the neighbour is not in the open set
                    {
                        openSet.Add(neighbour); // add the neighbour to the open set
                    }
                }
            }
        }
        return null;
    }
    public List<Node> RetracePath(Node startNode, Node endNode) // retrace the path
    {
        List<Node> path = new List<Node>(); // list of nodes in the path
        Node currentNode = endNode; // start at the end node
        while (currentNode != startNode) // while the current node is not the start node
        {
            path.Add(currentNode); // add the current node to the path
            currentNode = currentNode.parent; // move to the parent node
        }
        path.Reverse(); // reverse the path
        return path; // return the path
    }
    List<Node> GetNeighbours(Node node, Node[,] grid)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.position.x + x;
                int checkY = node.position.y + y;

                if (checkX >= 0 && checkX < grid.GetLength(0) && checkY >= 0 && checkY < grid.GetLength(1))
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }
    float GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.position.x - nodeB.position.x);
        int dstY = Mathf.Abs(nodeA.position.y - nodeB.position.y);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }

}
