using UnityEngine;
//ogólna klasa obiektu na planszy, zawiera podstawowe infomracje ktore będą potrzebne wszystkim obiektom
public abstract class inreractiveObject : MonoBehaviour
{
    public roomGenerator generator;
    public Vector2Int cords;
    public HexPoleScript pole;


    public void inicialiate(Vector2Int cord)
    {
        pole = generator.pola[cord].GetComponent<HexPoleScript>();
        transform.position = generator.pola[cord].transform.position;
        cords = cord;
        pole.currentStanding = gameObject;
        OnPlaced();


    }
    protected abstract void OnPlaced();
}
