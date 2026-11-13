using UnityEngine;
// klasa dziedziczy po interactiveObject, blokuje ruch i usuwa item jeśli był na tym samym polu
public class wall : inreractiveObject
{
    protected override void OnPlaced()
    {
        //pole.dostepneAkcje["Move"] = false;
        if(pole.item != null)
        {
            Destroy(pole.item);
        }
    }

}
