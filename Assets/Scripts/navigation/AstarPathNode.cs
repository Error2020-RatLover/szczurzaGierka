using UnityEngine;

public class AstarPathNode
{
    public HexPoleScript tile;

    public int gCost;
    public int hCost;

    public int fCost => gCost + hCost;

    public AstarPathNode parent;

    public int direction = -1;

    public AstarPathNode(HexPoleScript tile)
    {
        this.tile = tile;
        gCost = int.MaxValue;
    }
}