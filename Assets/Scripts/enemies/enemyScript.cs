using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyScript : turnObject
{
    public Vector2Int pozycja = new Vector2Int(0, 0);


    public roomGenerator generator;

    public HexPoleScript currentPole;

    public HexPoleScript targetTile;

    private List<HexPoleScript> currentPath;

    public int stamina = 0;

    /*siema tu bartek, mam takie info. Jeśli tu jesteś to pewnie szukasz jakiś zmiennych. Ogólnie niektóre zmienne są 
    zarówno dal przeciwników i graczy więc zrobiłem dla nich wspólną klasę, turnObject(bo oboje bazują na turach).
    Sprawdź tąk klasę jeśli nie jesteś w stanie znaleźć ważnej zmiennej. 
    */
    //szczerze? mega mocne



    public IEnumerator FindPlayerPath()
    {
        //argument w WaitForSeconds, może nie dasz wiary, ale to liczba sekund ile czeka przed kontunuowaniem pracy
        yield return new WaitForSeconds(0.6f);
        currentPath = Pathfinding.FindPath(
            currentPole,
            targetTile
        );
        if (stamina <= 0)
            {
                //koniec tury
                endYourTurn();
            }
        //jeśli droga istnieje to idziemy
        else
        {
            if (currentPath != null && currentPath.Count >= 2)
            {
                stamina -= 1;
                changePosition(currentPath[0].coordinates);

            }
            else
            {
                //stoje przy celu, koniec na razie, potem zmienimy
                Debug.Log("Stoję przy celu");
                Attack(targetTile.coordinates);
            }
        }
        




    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stats = Instantiate(stats);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //inicjakizacja ważnych danych przez generator
    public void inicialiate(Vector2Int cords)
    {
        currentPole = generator.pola[cords].GetComponent<HexPoleScript>();
        transform.position = generator.pola[cords].transform.position;
        currentPole.dostepneAkcje["Occupated"] = true;
        targetTile = generator.pola[new Vector2Int(0, 10)].GetComponent<HexPoleScript>();



    }
    //zmiana pozycji
    public void changePosition(Vector2Int cords)
    {

        currentPole.currentStanding = null;
        currentPole.dostepneAkcje["Occupated"] = false;
        transform.position = generator.pola[cords].transform.position;
        currentPole = generator.pola[cords].GetComponent<HexPoleScript>();
        currentPole.currentStanding = gameObject;
        currentPole.dostepneAkcje["Occupated"] = true;
        currentPole.enable();
        StartCoroutine(FindPlayerPath());
        targetTile = generator.player.currentPole;

    }


    public void Attack(Vector2Int cords)
    {
        stamina--;

        GameObject target = generator.pola[cords].GetComponent<HexPoleScript>().currentStanding;
        if (target == null)
        {
            if (stamina <= 0)
            {
                endYourTurn();
            }
            return;
        }
        if (target.GetComponent<enemyScript>() != null)
        {
            enemyScript enemy = target.GetComponent<enemyScript>();
            enemy.takeDamage(8);
        }
        else if (target.GetComponent<ChampionScript>() != null)
        {
            ChampionScript playerScript = target.GetComponent<ChampionScript>();
            playerScript.takeDamage(8);
        }
        else if (target.GetComponent<inreractiveObject>())
        {
            Debug.Log("Inny obiekt");
        }
  
        StartCoroutine(FindPlayerPath());
        targetTile = generator.player.currentPole;

    }
    public void takeDamage(int damage)
    {
        stats.hp -= damage;
        if (stats.hp < 0)
        {
            currentPole.currentStanding = null;
            currentPole.dostepneAkcje["Occupated"] = false;
            generator.moveOrder.Remove(this);
            Destroy(gameObject);
        }
    }
    //początek tury
    public override void startYourTurn()
    {
        stamina = stats.stamina;
        targetTile = generator.player.currentPole;
        StartCoroutine(FindPlayerPath());
    }

    //koniec tury
    public override void endYourTurn()
    {
        generator.changeNextTurn();
    }



}
