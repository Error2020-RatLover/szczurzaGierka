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
            pole.dostepneAkcje["Move"] = !closed;
            pole.dostepneAkcje["Occupated"] = closed;
            pole.dostepneAkcje["atackThrough"] = !closed;
            if (closed)
            {
                animator.Play("doorClose");
            }
            else
            {
                animator.Play("doorOpen");
            }
        }
          
        else
        {
            Debug.Log("Przecież ktoś stoi w tych drzwiach");
        }
    }

}
