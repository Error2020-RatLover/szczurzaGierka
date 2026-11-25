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
                SendMessage(attack.functionToCall, attack);
                currenAbility = attack;
                if (abillityAproved)
                {
                    yield return new WaitForSeconds(0.6f);
                    Attack(targetTile.coordinates);
                    abillityAproved =false;
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
                
                if (currentPath[0].dostepneAkcje["Blocked"])
                {
                    yield return new WaitForSeconds(0.6f);
                    Debug.Log("WALE ŚCIANĘ AAAAAAA");
                    currenAbility = abilities[0];
                    Attack(currentPath[0].coordinates);

                }
                else
                {
                    stamina -= 1;
                    changePosition(currentPath[0].coordinates);
                }
                
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
                weppons[i].attackPossible[j] = Instantiate(weppons[i].attackPossible[j]);
            }
        }
        stats = Instantiate(stats);
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



    public IEnumerator MoveEnemyToPosition(Vector2Int cords)
    {
        if (!generator.pola.ContainsKey(cords))
            yield break;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = generator.pola[cords].transform.position;

        float duration = 1.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            transform.position = Vector3.Lerp(
                startPosition,
                targetPosition,
                t
            );

            yield return null;
        }

        transform.position = targetPosition;
    }


    //zmiana pozycji

    public void changePosition(Vector2Int cords)
    {
        StartCoroutine(ChangePositionCoroutine(cords));
    }

    private IEnumerator ChangePositionCoroutine(Vector2Int cords)
    {
        if (!generator.pola.ContainsKey(cords))
            yield break;

        // Zwolnienie starego pola
        currentPole.currentStanding.Remove(gameObject);
        currentPole.dostepneAkcje["Occupated"] = false;
        currentPole.dostepneAkcje["atackThrough"] = true;

        // Płynne przemieszczanie przeciwnika
        yield return MoveEnemyToPosition(cords);
        
        // Aktualizacja nowego pola
        currentPole = generator.pola[cords].GetComponent<HexPoleScript>();

        currentPole.currentStanding.Add(gameObject);
        currentPole.dostepneAkcje["Occupated"] = true;
        currentPole.dostepneAkcje["atackThrough"] = false;

        currentPole.enable();

        pozycja = cords;
        TriggerMoveEffects();
        // Aktualizacja celu
        targetTile = generator.player.currentPole;

        // Ponowne wyszukanie ścieżki
        StartCoroutine(FindPlayerPath());
    }




public void Attack(Vector2Int cords)
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
        int dmg = currenAbility.dmg;
        if (currenAbility.damageMultipler == attackStats.DmgMultipler.phisical)
        {
            dmg *= stats.strenght;
        }
        else if (currenAbility.damageMultipler == attackStats.DmgMultipler.magical)
        {
            dmg *= stats.magic;
        }
        //Debug.Log(dmg);
        if (target.GetComponent<enemyScript>() != null)
        {
            enemyScript enemy = target.GetComponent<enemyScript>();
            enemy.takeDamage(dmg, currenAbility.damageType);
            List<string> onHit = new List<string>(currenAbility.effectsOnHit);
            onHit.AddRange(currentWeppon.effectsOnHit);

            foreach (string funcName in onHit)
            {
                
                
              SendMessage(funcName, enemy);
                
                    
            }

            List<effectData> effects = new List<effectData>(currenAbility.effects);
            effects.AddRange(currentWeppon.effects);
            //efekty jak poison itp.
            foreach (effectData effect in effects)
            {
                if (UnityEngine.Random.value <= effect.effectChance)
                {
                    if (UnityEngine.Random.value <= effect.effectChance)
                    {
                        enemy.giveEffect(effect);

                    }
                        
                }
                    
            }
        }
        else if (target.GetComponent<ChampionScript>() != null)
        {
            ChampionScript playerScript = target.GetComponent<ChampionScript>();
            playerScript.takeDamage(dmg, currenAbility.damageType);

            List<string> onHit = new List<string>(currenAbility.effectsOnHit);
            onHit.AddRange(currentWeppon.effectsOnHit);

            foreach (string funcName in onHit)
            {
                SendMessage(funcName, playerScript);
            }
            List<effectData> effects = new List<effectData>(currenAbility.effects);
            effects.AddRange(currentWeppon.effects);
            //efekty jak poison itp.
            foreach (effectData effect in effects)
            {
                playerScript.giveEffect(effect);
            }
        }
        else if (target.GetComponent<inreractiveObject>())
        {
            inreractiveObject interactive = target.GetComponent<inreractiveObject>();
            interactive.takeDamage(dmg);
        }
        changeToNextWeppon();
        TriggerAttackEffects();
        StartCoroutine(FindPlayerPath());
        targetTile = generator.player.currentPole;

    }
    public override void takeDamage(int damage, attackStats.DmgType dmgType)
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
        if (stats.hp <= 0)
        {
            currentPole.currentStanding.Remove(gameObject);
            currentPole.dostepneAkcje["Occupated"] = false;
            currentPole.dostepneAkcje["atackThrough"] = true;
            generator.moveOrder.Remove(this);
            Destroy(gameObject);
        }
        TriggerDmgTakenEffects();
    }
    //początek tury
    public override void startYourTurn()
    {
        stamina = stats.stamina;
        targetTile = generator.player.currentPole;
        StartCoroutine(FindPlayerPath());
        TriggerStartEffects();
    }

    //koniec tury
    public override void endYourTurn()
    {
        TriggerEndEffects();
        generator.changeNextTurn();
    }


    public void tryHitAttack(attackStats attack)
    {
        int range = attack.range;
        generator.actionState = "Attack";
        currentPath = Pathfinding.FindPath(
            currentPole,
            targetTile,
            "Attack"
        );
        bool isInCleanLine = currentPole.HasClearLine(targetTile);
        if (currentPath.Count <= range && isInCleanLine)
        {
            abillityAproved = true;



        }
    }

    public void changeWeppon(wepponInfoStats newWeapon)
    {
        stats.maxHp -= currentWeppon.statsModifier.maxHp;
        stats.strenght -= currentWeppon.statsModifier.strenght;
        stats.magic -= currentWeppon.statsModifier.magic;
        stats.armor -= currentWeppon.statsModifier.armor;
        stats.magicArmor -= currentWeppon.statsModifier.magicArmor;
        stats.stamina -= currentWeppon.statsModifier.stamina;
        stats.speed -= currentWeppon.statsModifier.speed;
        foreach (attackStats ability in currentWeppon.attackPossible)
        {
            abilities.Remove(ability);
        }
        currentWeppon = newWeapon;
        foreach (attackStats ability in currentWeppon.attackPossible)
        {
            abilities.Add(ability);
        }
        stats.maxHp += currentWeppon.statsModifier.maxHp;
        stats.hp += currentWeppon.statsModifier.hp;
        currentWeppon.statsModifier.hp = 0;
        stats.strenght += currentWeppon.statsModifier.strenght;
        stats.magic += currentWeppon.statsModifier.magic;
        stats.armor += currentWeppon.statsModifier.armor;
        stats.magicArmor += currentWeppon.statsModifier.magicArmor;
        stats.stamina += currentWeppon.statsModifier.stamina;
        stats.speed += currentWeppon.statsModifier.speed;

    }

    public void changeToNextWeppon()
    {
        int curIndex = weppons.IndexOf(currentWeppon);
        curIndex++;
        if (curIndex >= weppons.Count)
        {
            curIndex = 0;
        }
        Debug.Log(curIndex);
        changeWeppon(weppons[curIndex]);
    }

}
