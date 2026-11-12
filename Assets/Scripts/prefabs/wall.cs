using UnityEngine;

public class wall : inreractiveObject
{
    protected override void OnPlaced()
    {
        pole.dostepneAkcje["Move"] = false;
    }

}
