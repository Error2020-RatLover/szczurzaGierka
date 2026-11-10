using UnityEngine;

public class ChampionScript : MonoBehaviour
{

    public Vector2Int pozycja = new Vector2Int(0, 0);

    public roomGenerator generator;

    ItemManager itemManager;


    public string action = "Move";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemManager = FindFirstObjectByType<ItemManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void actionToTake(Vector2Int toCordinates)
    {
        SendMessage(action, toCordinates);
    }

    public void przeniesNaPozycje(Vector2Int pozycja)
    {
        if (generator.pola.ContainsKey(pozycja))
        {
            transform.position = generator.pola[pozycja].transform.position;
        }
    }

    public void Move(Vector2Int cords)
    {
        generator.resetBoard();
        przeniesNaPozycje(cords);
        pozycja = cords;
        generator.pola[cords].GetComponent<HexPoleScript>().setActiveHex(1);
        itemManager.SprawdzKolizje(cords);
    }
}
