using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyScript : turnObject
{
    //public Vector2Int pozycja = new Vector2Int(0, 0);


    

    public HexPoleScript currentPole;

    public HexPoleScript targetTile;

    protected List<HexPoleScript> currentPath;

    public int stamina = 0;

    public bool abillityAproved =false;

    /*siema tu bartek, mam takie info. Jeśli tu jesteś to pewnie szukasz jakiś zmiennych. Ogólnie niektóre zmienne są 
    zarówno dal przeciwników i graczy więc zrobiłem dla nich wspólną klasę, turnObject(bo oboje bazują na turach).
    Sprawdź tąk klasę jeśli nie jesteś w stanie znaleźć ważnej zmiennej. 
    */
    //szczerze? mega mocne


    private void OnMouseDown()
    {

        if (currentPole != null && currentPole.sprite.enabled)
        {
            currentPole.hexPoleChoosen();
        }
        // Tutaj wywołaj swoją funkcję
    }



    public IEnumerator FindPlayerPath()
    {
        //argument w WaitForSeconds, może nie dasz wiary, ale to liczba sekund ile czeka przed kontunuowaniem pracy
        yield return new WaitForSeconds(0.6f);
        
        if (stamina <= 0)
            {
                //koniec tury
                endYourTurn();
            }
        //jeśli droga istnieje to idziemy
        else
        {
            foreach(attackStats attack in abilities)
            {
                //Debug.Log(attack.attackName);
                SendMessage(attack.attackName,attack);
                if (abillityAproved)
                {
                  
                    abillityAproved=false;
                    yield break;
                }
            }
            if (currentPath != null && currentPath.Count >= 2)
            {
                generator.actionState = "Move";
                currentPath = Pathfinding.FindPath(
                    currentPole,
                    targetTile,
                    "Move"
                );
                stamina -= 1;
                changePosition(currentPath[0].coordinates);
                yield break;

            }
            //else
            //{
            //    //stoje przy celu, koniec na razie, potem zmienimy
            //    Debug.Log("Stoję przy celu");
            //    Attack(targetTile.coordinates);
            //}
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
        currentPole.dostepneAkcje["atackThrough"] = false;
        currentPole.currentStanding.Add(gameObject);
        targetTile = generator.pola[new Vector2Int(0, 10)].GetComponent<HexPoleScript>();
        pozycja = cords;
        for (int i = 0; i < weppons.Count; i++)
        {
            weppons[i] = Instantiate(weppons[i]);
            for (int j = 0; j < weppons[i].attackPossible.Count; j++)
            {
                weppons[i].attackPossible[j] = Instantiate(weppons[i].attackPossible[j])
            }
        }
        currentWeppon = weppons[0];
        stats.maxHp += currentWeppon.statsModifier.maxHp;
        stats.hp += currentWeppon.statsModifier.hp;
        currentWeppon.statsModifier.hp = 0;
        stats.strenght += currentWeppon.statsModifier.strenght;
        stats.magic += currentWeppon.statsModifier.magic;
        stats.armor += currentWeppon.statsModifier.armor;
        stats.magicArmor += currentWeppon.statsModifier.magicArmor;
        stats.stamina += currentWeppon.statsModifier.stamina;
        stats.speed += currentWeppon.statsModifier.speed;
        foreach(attackStats ability in currentWeppon.attackPossible)
        {
            abilities.Add(ability);
        }


    }
    //zmiana pozycji
    public void changePosition(Vector2Int cords)
    {

        currentPole.currentStanding.Remove(gameObject);
        currentPole.dostepneAkcje["Occupated"] = false;
        currentPole.dostepneAkcje["atackThrough"] = true;
        transform.position = generator.pola[cords].transform.position;
        currentPole = generator.pola[cords].GetComponent<HexPoleScript>();
        currentPole.currentStanding.Add(gameObject);
        currentPole.dostepneAkcje["Occupated"] = true;
        currentPole.dostepneAkcje["atackThrough"] = false;
        currentPole.enable();
        pozycja = cords;
        StartCoroutine(FindPlayerPath());
        targetTile = generator.player.currentPole;

    }


    public void Attack(Vector2Int cords, int dmg, attackStats.DmgType dmgType)
    {
        stamina--;
        HexPoleScript skrypt = generator.pola[cords].GetComponent<HexPoleScript>();
        //StartCoroutine(AskForId());
        GameObject target = null;
        if(skrypt.gameObjectId >= 0)
        {
            target = skrypt.currentStanding[0];
        }
        
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
            enemy.takeDamage(dmg, dmgType);
        }
        else if (target.GetComponent<ChampionScript>() != null)
        {
            ChampionScript playerScript = target.GetComponent<ChampionScript>();
            playerScript.takeDamage(dmg, dmgType);
        }
        else if (target.GetComponent<inreractiveObject>())
        {
            inreractiveObject interactive = target.GetComponent<inreractiveObject>();
            interactive.takeDamage(dmg);
        }
  
        StartCoroutine(FindPlayerPath());
        targetTile = generator.player.currentPole;

    }
    public void takeDamage(int damage, attackStats.DmgType dmgType)
    {
        if (dmgType == attackStats.DmgType.phisical)
        {
            damage /= stats.armor;
        }
        else if (dmgType == attackStats.DmgType.magical)
        {
            damage /= stats.magicArmor;
        }

        stats.hp -= damage;
        Debug.Log($"CO MNIE BIJESZ??   {stats.hp}");
        if (stats.hp < 0)
        {
            currentPole.currentStanding.Remove(gameObject);
            currentPole.dostepneAkcje["Occupated"] = false;
            currentPole.dostepneAkcje["atackThrough"] = true;
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
