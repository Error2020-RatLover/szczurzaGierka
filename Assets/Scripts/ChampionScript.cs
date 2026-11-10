using UnityEngine;

public class ChampionScript : MonoBehaviour
{

    public Vector2Int pozycja = new Vector2Int(0, 0);

    public roomGenerator generator;

    public string action = "Move";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void actionToTake(Vector2Int toCordinates)
    {
       
    }

    public void przeniesNaPozycje(Vector2Int pozycja)
    {
        if (generator.pola.ContainsKey(pozycja))
        {
            transform.position = generator.pola[pozycja].transform.position;
        }
    }
}
