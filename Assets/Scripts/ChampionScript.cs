using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ChampionScript : turnObject
{

    public Vector2Int pozycja = new Vector2Int(0, 0);

    public roomGenerator generator;

    public HexPoleScript currentPole;
    ItemManager itemManager;

    
    public int stamina = 0;

    // action będzie przechowywał akcję jaką wykonuję champion.
    public string action = "Move";


  
    /*siema tu bartek, mam takie info. Jeśli tu jesteś to pewnie szukasz jakiś zmiennych. Ogólnie niektóre zmienne są 
    zarówno dal przeciwników i graczy więc zrobiłem dla nich wspólną klasę, turnObject(bo oboje bazują na turach).
    Sprawdź tąk klasę jeśli nie jesteś w stanie znaleźć ważnej zmiennej. 
    */

    void Start()
    {
        stats = Instantiate(stats);
        itemManager = FindFirstObjectByType<ItemManager>();

    }

    void Update()
    {   //na razie klikając z na klawiaturze zmieniamy stan. Potem trzeba by zrobić do tego przycisk    
        if (Keyboard.current != null && Keyboard.current.zKey.wasPressedThisFrame && generator.moveOrder[generator.currentMoveId] == this)
        {
            if (action == "Move")
            {
                action = "Attack";
            }
            else
            {
                action = "Move";
            }
            generator.actionState = action;
            //potem zautomatycujemy aby było łatwiej ale na razie tutaj ręcznie 
            currentPole.turnOffHex(1);
            currentPole.setActiveHex(1);
        }
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
        currentPole.dostepneAkcje["Occupated"] = false;
        currentPole.currentStanding = null;
        przeniesNaPozycje(cords);
        pozycja = cords;
        //po poruszeniu się gracza wykonują się funkcje na polu
        currentPole = generator.pola[cords].GetComponent<HexPoleScript>();
        currentPole.setActiveHex(1);
        currentPole.currentStanding = gameObject;
        currentPole.ZbierzItem();
        currentPole.dostepneAkcje["Occupated"] = true;
        //currentPole.enable();
        //itemManager.SprawdzKolizje(cords);
        stamina--;
        if (stamina <= 0) {
            endYourTurn();
        }

    }

    public void Attack(Vector2Int cords)
    {
        stamina--;
       
        GameObject target = generator.pola[cords].GetComponent<HexPoleScript>().currentStanding;
        if (target == null) {
            if (stamina <= 0)
            {
                endYourTurn();
            }
            return;
        }
        if(target.GetComponent<enemyScript>() != null)
        {
            enemyScript enemy = target.GetComponent<enemyScript>();
            enemy.takeDamage(8);
        }
        else if(target.GetComponent<ChampionScript>() != null)
        {
            ChampionScript playerScript = target.GetComponent<ChampionScript>();
            playerScript.takeDamage(8);
        }
        else if (target.GetComponent<inreractiveObject>())
        {
            inreractiveObject interactive = target.GetComponent<inreractiveObject>();
            interactive.takeDamage(8);
        }
        if (stamina <= 0)
        {
            endYourTurn();
        }

        //Debug.Log(target);
    }
    //akcje na zaczęciu tury
    public override void startYourTurn()
    {
        stamina = stats.stamina;
        currentPole.setActiveHex(1);
        
    }

    public void takeDamage(int damage)
    {
        stats.hp -= damage;
        Debug.Log($"Zostałem uderzony: {stats.hp}hp");
        //if (stats.hp < 0)
        //{
        //    Destroy(gameObject);
        //    currentPole.currentStanding = null;
        //    currentPole.dostepneAkcje["Occupated"] = false;
        //    generator.moveOrder.Remove(this);
        //    Destroy(gameObject);
        //}
    }

    //akcje na zakończenie tury
    public override void endYourTurn()
    {
        currentPole.turnOffHex(1);
        generator.changeNextTurn();
    }
    

}

