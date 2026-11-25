using UnityEngine;

public class Doors : inreractiveObject
{

    private Animator animator;
    public bool closed = true;
    protected override void OnPlaced()
    {
        //pole.dostepneAkcje["Move"] = false;
        animator = GetComponent<Animator>();
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
            stats.Move = !closed;
            stats.Occupated = closed;
            pole.dostepneAkcje["atackThrough"] = !closed;
            pole.dostepneAkcje["Blocked"] = closed;
            if (closed)
            {
                pole.movementCost += travelCost;
                animator.Play("doorClose");
            }
            else
            {
                pole.movementCost -= travelCost;
                animator.Play("doorOpen");
            }
        }
          
        else
        {
            Debug.Log("Przecież ktoś stoi w tych drzwiach");
        }
    }

}
