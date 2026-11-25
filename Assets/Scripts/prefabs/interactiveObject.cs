using UnityEngine;
//ogólna klasa obiektu na planszy, zawiera podstawowe infomracje ktore będą potrzebne wszystkim obiektom
public abstract class inreractiveObject : MonoBehaviour
{
    public roomGenerator generator;
    public Vector2Int cords;
    public HexPoleScript pole;

    public interactiveObjectStats stats;

    public int travelCost = 0;

    private void OnMouseDown()
    {
        
        if (pole != null && pole.sprite.enabled) 
        { 
            pole.hexPoleChoosen();
        }
        // Tutaj wywołaj swoją funkcję
    }

    public void inicialiate(Vector2Int cord)
    {
        stats = Instantiate(stats);
        pole = generator.pola[cord].GetComponent<HexPoleScript>();
        transform.position = generator.pola[cord].transform.position;
        cords = cord;
        pole.currentStanding.Add(gameObject);
        pole.dostepneAkcje["Move"] = stats.Move;
        pole.dostepneAkcje["Occupated"] = stats.Occupated;
        pole.dostepneAkcje["Interact"] = stats.Interact;
        pole.dostepneAkcje["atackThrough"] = stats.atackThrough;
        pole.dostepneAkcje["Blocked"] = stats.Blocked;
        //pole.dostepneAkcje["Atack"] = stats.Attack;
        if (stats.Blocked)
        {
            travelCost = stats.hp / 100 + 1;
            pole.movementCost += travelCost;
        }

        OnPlaced();
    }

    public void takeDamage(int damage)
    {
        
        stats.hp -= damage;

        pole.movementCost -= travelCost;
        travelCost = stats.hp / 100 + 1;
        pole.movementCost += travelCost;
        if (stats.hp < 0)
        {
            pole.currentStanding.Remove(gameObject);
            pole.dostepneAkcje["Occupated"] = false;
            pole.dostepneAkcje["Move"] = true;
            pole.dostepneAkcje["Interact"] = false;
            pole.dostepneAkcje["atackThrough"] = true;
            pole.dostepneAkcje["Blocked"] = false;
            if (stats.Blocked)
            {
                pole.movementCost -= travelCost;
            }
            Destroy(gameObject);
        }
    }

    protected abstract void OnPlaced();
    public abstract void Interact();

    
}
