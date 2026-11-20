using UnityEngine;
//ogólna klasa obiektu na planszy, zawiera podstawowe infomracje ktore będą potrzebne wszystkim obiektom
public abstract class inreractiveObject : MonoBehaviour
{
    public roomGenerator generator;
    public Vector2Int cords;
    public HexPoleScript pole;

    public interactiveObjectStats stats;

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
        //pole.dostepneAkcje["Atack"] = stats.Attack;


        OnPlaced();
    }

    public void takeDamage(int damage)
    {
        Debug.Log("halo");
        stats.hp -= damage;
        if (stats.hp < 0)
        {
            pole.currentStanding.Remove(gameObject);
            pole.dostepneAkcje["Occupated"] = false;
            pole.dostepneAkcje["Move"] = true;
            pole.dostepneAkcje["Interact"] = false;
            pole.dostepneAkcje["atackThrough"] = true;
            Destroy(gameObject);
        }
    }

    protected abstract void OnPlaced();
    public abstract void Interact();
}
