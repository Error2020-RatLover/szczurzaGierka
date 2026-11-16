using UnityEngine;

public class Doors : inreractiveObject
{
    public bool closed = true;
    protected override void OnPlaced()
    {
        //pole.dostepneAkcje["Move"] = false;
        if (pole.item != null)
        {
            Destroy(pole.item);
        }
    }

    public override void Interact()
    {

        if(pole.currentStanding.Count == 1)
        {
            closed = !closed;
            //Debug.Log(closed);

            pole.dostepneAkcje["Move"] = !closed;
            pole.dostepneAkcje["Occupated"] = closed;
        }
          
        else
        {
            Debug.Log("Przecież ktoś stoi w tych drzwiach");
        }
    }

}
