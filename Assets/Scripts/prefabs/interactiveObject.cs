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
        pole.currentStanding = gameObject;
        pole.dostepneAkcje["Move"] = stats.Move;
        pole.dostepneAkcje["Occupated"] = stats.Occupated;
        //pole.dostepneAkcje["Atack"] = stats.Attack;
       

        OnPlaced();
    }

    public void takeDamage(int damage)
    {
        stats.hp -= damage;
        if (stats.hp < 0)
        {
            pole.currentStanding = null;
            pole.dostepneAkcje["Occupated"] = false;
            pole.dostepneAkcje["Move"] = true;
            Destroy(gameObject);
        }
    }

    protected abstract void OnPlaced();
}
