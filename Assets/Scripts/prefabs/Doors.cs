using UnityEngine;

public class Doors : inreractiveObject
{
    protected override void OnPlaced()
    {
        //pole.dostepneAkcje["Move"] = false;
        if (pole.item != null)
        {
            Destroy(pole.item);
        }
    }

}
