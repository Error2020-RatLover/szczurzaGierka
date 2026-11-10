using UnityEngine;

public class ChampionScript : MonoBehaviour
{

    public Vector2Int pozycja = new Vector2Int(0, 0);

    public roomGenerator generator;
    // kochani moi, action będzie przechowywał akcję jaką wykonuję champion.
    public string action = "Move";

  
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void actionToTake(Vector2Int toCordinates)
    {
        //message odpala funkcję o nazwie akcje i przekazuje argument toCordinates
        SendMessage(action, toCordinates);
    }
    //interpretacja kordunaktow na pozycje, mozna troche zmienic jesli chcecie ale ja nie widze bledow 
    public void przeniesNaPozycje(Vector2Int pozycja)
    {
        if (generator.pola.ContainsKey(pozycja))
        {
            transform.position = generator.pola[pozycja].transform.position;
        }
    }
    //no i tutaj wykonuje sie akcja wyslana SendMessage, zmieniajac parametr action bedzie mozna zmienic logike zachowania pamietajac jakie pole zostalo wybrane
    public void Move(Vector2Int cords) {
        generator.resetBoard();
        przeniesNaPozycje(cords);
        pozycja = cords;
        generator.pola[cords].GetComponent<HexPoleScript>().setActiveHex(2);
    }
}

