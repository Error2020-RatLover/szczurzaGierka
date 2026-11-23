using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;


public class ChampionScript : turnObject
{

    //public Vector2Int pozycja = new Vector2Int(0, 0);



    public HexPoleScript currentPole;
    ItemManager itemManager;


    public int stamina = 0;

    // action będzie przechowywał akcję jaką wykonuję champion.
    public string action = "Move";

    public  int currentRange = 1;
  
    private int currentAttackId = 0;

    /*siema tu bartek, mam takie info. Jeśli tu jesteś to pewnie szukasz jakiś zmiennych. Ogólnie niektóre zmienne są 
    zarówno dal przeciwników i graczy więc zrobiłem dla nich wspólną klasę, turnObject(bo oboje bazują na turach).
    Sprawdź tąk klasę jeśli nie jesteś w stanie znaleźć ważnej zmiennej. 
    */

    void Start()
    {
        stats = Instantiate(stats);
        itemManager = FindFirstObjectByType<ItemManager>();
        for(int i = 0; i < weppons.Count; i++)
        {
            weppons[i] = Instantiate(weppons[i]);
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

        currenAbility = currentWeppon.attackPossible[0];
    }



    void Update()
    {   //na razie klikając z na klawiaturze zmieniamy stan. Potem trzeba by zrobić do tego przycisk    
        if (Keyboard.current != null && Keyboard.current.zKey.wasPressedThisFrame && generator.moveOrder[generator.currentMoveId] == this)
        {
            ClearChoicePanel();
            if (action == "Move")
            {
                action = "Attack";
                wepponInfoShow();
            }
            else if (action == "Attack")
            {
                action = "Interact";
            }
            else
            {
                action = "Move";
                currentRange = 1;
            }
            generator.actionState = action;
            //potem zautomatycujemy aby było łatwiej ale na razie tutaj ręcznie 
            currentPole.turnOffHex(currentRange+1);
            currentPole.setActiveHex(currentRange);
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
        //generator.resetBoard();
        currentPole.turnOffHex(currentRange + 1);
        currentPole.dostepneAkcje["Occupated"] = false;
        currentPole.currentStanding.Remove(gameObject);
        przeniesNaPozycje(cords);
        pozycja = cords;
        //po poruszeniu się gracza wykonują się funkcje na polu
        currentPole = generator.pola[cords].GetComponent<HexPoleScript>();
        //currentPole.setActiveHex(3);
        currentPole.setActiveHex(currentRange);
        currentPole.currentStanding.Add(gameObject);
        currentPole.ZbierzItem();
        currentPole.dostepneAkcje["Occupated"] = true;
        //currentPole.enable();
        //itemManager.SprawdzKolizje(cords);
        currentPole.enable();
        stamina--;
        if (stamina <= 0)
        {
            endYourTurn();
        }

    }

    public void Attack(Vector2Int cords)
    {
        StartCoroutine(AttackCoroutine(cords));
    }




    private IEnumerator AttackCoroutine(Vector2Int cords)
    {
        stamina--;

        HexPoleScript skrypt = generator.pola[cords]
            .GetComponent<HexPoleScript>();

        yield return StartCoroutine(skrypt.askForId());


        GameObject target = null;

        if (skrypt.gameObjectId >= 0)
        {
            target = skrypt.currentStanding[skrypt.gameObjectId];

        }

        if (target == null)
        {
            if (stamina <= 0)
            {
                endYourTurn();
            }
            yield break;
        }
        int dmg = currenAbility.dmg;
        if(currenAbility.damageMultipler == attackStats.DmgMultipler    .phisical)
        {
            dmg *= stats.strenght;
        }
        else if (currenAbility.damageMultipler == attackStats.DmgMultipler.magical)
        {
            dmg *= stats.magic;
        }
        if (target.GetComponent<enemyScript>() != null)
        {
            enemyScript enemy = target.GetComponent<enemyScript>();
            enemy.takeDamage(dmg, currenAbility.damageType);
            foreach(string funcName in currenAbility.additionalEffects)
            {
                SendMessage(funcName, enemy);
            }
            
        }
        else if (target.GetComponent<ChampionScript>() != null)
        {
            ChampionScript playerScript = target.GetComponent<ChampionScript>();
            playerScript.takeDamage(dmg, currenAbility.damageType);
        }
        else if (target.GetComponent<inreractiveObject>() != null)
        {
            inreractiveObject interactive = target.GetComponent<inreractiveObject>();
            interactive.takeDamage(dmg);
        }

        if (stamina <= 0)
        {
            endYourTurn();
        }
    }

    public void Interact(Vector2Int cords)
    {
        StartCoroutine(InteractCoroutine(cords));
    }

    public IEnumerator InteractCoroutine(Vector2Int cords)
    {
        HexPoleScript skrypt = generator.pola[cords].GetComponent<HexPoleScript>();
        yield return StartCoroutine(skrypt.askForId());
        GameObject target = null;
        if (skrypt.gameObjectId >= 0)
        {
            target = skrypt.currentStanding[skrypt.gameObjectId];
        }
        if (target.GetComponent<inreractiveObject>())
        {
            inreractiveObject interactive = target.GetComponent<inreractiveObject>();
            interactive.Interact();
        }
    }

    //akcje na zaczęciu tury
    public override void startYourTurn()
    {
        stamina = stats.stamina;
        //currentPole.setActiveHex(3);

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
        ClearChoicePanel();
        currentPole.turnOffHex(currentRange + 2);
        generator.changeNextTurn();
    }
    
    public void wepponInfoShow()
    {
        
        Debug.Log("halo proba");
        currentRange = currenAbility.range;
        currenAbility = currenAbility;
        askForId();


    }

    public void printAttackName(enemyScript target)
    {
        Debug.Log(currenAbility.attackName);
    }

    public GameObject buttonPrefab;
    public Transform buttonParent;

    public void askForId()
    {
        // Czyścimy poprzednie przyciski
        ClearChoicePanel();

        // Pokazujemy panel
        buttonParent.gameObject.SetActive(true);

        for (int i = 0; i < currentWeppon.attackPossible.Count; i++)
        {
            //Debug.Log(currentWeppon.attackPossible[i].attackName);  
            int id = i;

            GameObject newButton = Instantiate(buttonPrefab, buttonParent);

            chooseButton button = newButton.GetComponent<chooseButton>();

            button.Setup(id, currentWeppon.attackPossible[i].attackName, OnButtonClicked);
        }
    }

    private void ClearChoicePanel()
    {
        //Debug.Log("czyścimy");
        for (int i = buttonParent.childCount - 1; i >= 0; i--)
        {
            Destroy(buttonParent.GetChild(i).gameObject);
        }

        buttonParent.gameObject.SetActive(false);
    }


    private void OnButtonClicked(int id)
    {
        currentPole.turnOffHex(currentRange + 1);
        
        currenAbility = currentWeppon.attackPossible[id];
        currentAttackId = id;
        Debug.Log(id);
        wepponInfoShow();
        currentPole.setActiveHex(currentRange);
    }

}

    
