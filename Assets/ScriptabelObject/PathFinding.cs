using System.Collections.Generic;
using UnityEngine;

public static class Pathfinding
{
    public static List<HexPoleScript> FindPath(
        HexPoleScript start,
        HexPoleScript target)
    {
        if (start == null || target == null)
            return null;

        // Wszystkie dane dotyczące aktualnego wyszukiwania
        Dictionary<HexPoleScript, AstarPathNode> nodes = new();

        // Pola oczekujące na sprawdzenie
        List<AstarPathNode> openSet = new();

        // Pola już sprawdzone
        HashSet<HexPoleScript> closedSet = new();

        // Tworzymy początkowy węzeł
        AstarPathNode startNode = new AstarPathNode(start);

        startNode.gCost = 0;
        startNode.hCost = GetDistance(start, target);

        nodes.Add(start, startNode);
        openSet.Add(startNode);

        // Główna pętla A*
        while (openSet.Count > 0)
        {
            // Znajdujemy pole z najmniejszym F Cost
            AstarPathNode currentNode = GetLowestFCost(openSet);

            // Dotarliśmy do celu
            if (currentNode.tile == target)
            {
                return RetracePath(startNode, currentNode);
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode.tile);

            // Sprawdzamy wszystkich sąsiadów
            foreach (GameObject x  in currentNode.tile.neighbors)
            {
                HexPoleScript neighbor = x.GetComponent<HexPoleScript>();
                // Pomijamy pola niedostępne
                if (!neighbor.dostepneAkcje["Move"])
                    continue;

                // Pomijamy już sprawdzone pola
                if (closedSet.Contains(neighbor))
                    continue;

                // Obliczamy koszt dotarcia do sąsiada
                int tentativeGCost =
                    currentNode.gCost + neighbor.movementCost;

                // Pobieramy lub tworzymy węzeł sąsiada
                if (!nodes.TryGetValue(neighbor, out AstarPathNode neighborNode))
                {
                    neighborNode = new AstarPathNode(neighbor);
                    nodes.Add(neighbor, neighborNode);
                }

                // Czy znaleźliśmy lepszą drogę?
                if (tentativeGCost < neighborNode.gCost)
                {
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.hCost = GetDistance(neighbor, target);
                    neighborNode.parent = currentNode;

                    // Dodajemy do OpenSet, jeśli jeszcze go tam nie ma
                    if (!openSet.Contains(neighborNode))
                    {
                        openSet.Add(neighborNode);
                    }
                }
            }
        }

        // Nie znaleziono drogi
        return null;
    }

    // Wybiera węzeł o najmniejszym F Cost
    private static AstarPathNode GetLowestFCost(List<AstarPathNode> nodes)
    {
        AstarPathNode lowest = nodes[0];

        foreach (AstarPathNode node in nodes)
        {
            if (node.fCost < lowest.fCost ||
                (node.fCost == lowest.fCost &&
                 node.hCost < lowest.hCost))
            {
                lowest = node;
            }
        }

        return lowest;
    }

    // Odtwarza drogę od celu do początku
    private static List<HexPoleScript> RetracePath(
        AstarPathNode startNode,
        AstarPathNode endNode)
    {
        List<HexPoleScript> path = new();

        AstarPathNode current = endNode;

        while (current != startNode)
        {
            path.Add(current.tile);
            current = current.parent;
        }

        path.Reverse();

        return path;
    }

    // Heurystyka dla siatki hexagonalnej axial
    private static int GetDistance(HexPoleScript a, HexPoleScript b)
    {
        int dq = a.coordinates.x - b.coordinates.x;
        int dr = a.coordinates.y - b.coordinates.y;

        return (
            Mathf.Abs(dq) +
            Mathf.Abs(dr) +
            Mathf.Abs(dq + dr)
        ) / 2;
    }
}
