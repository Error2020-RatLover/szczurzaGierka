// using System.Numerics;
// using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
public class HexPoleScript : MonoBehaviour
{


    public Vector2Int coordinates;
    public int movementCost = 1;
    public List<GameObject> neighbors = new();

    public CapsuleCollider2D button;

    public SpriteRenderer sprite;

    public roomGenerator generator;

    public Dictionary<string, bool> dostepneAkcje = new();

    public GameObject item;

    public int gameObjectId = -1;

    public List<GameObject> currentStanding;

    private void OnMouseDown()
    {
        // tutaj dodaje zeby sie nie dalo przez ui klikac w pola
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //DisplayCords();
        //setActiveHex();

        generator.takeAction(coordinates);

    }

    public void disable()
    {
        if (dostepneAkcje[generator.actionState])
        {
            if (generator.actionState == "Move" && dostepneAkcje["Occupated"])
            {
                return;
            }
            sprite.enabled = true;
            button.enabled = true;
        }

        //Debug.Log(coordinates);
    }

    public void enable()
    {
        sprite.enabled = false;
        button.enabled = false;
    }

    public void setActiveHex(int movementRange = 1, bool straightLine = true)
    {
        Queue<HexPoleScript> queue = new Queue<HexPoleScript>();

        HashSet<HexPoleScript> visited = new HashSet<HexPoleScript>();

        queue.Enqueue(this);
        visited.Add(this);

        int distance = 0;

        while (queue.Count > 0 && distance < movementRange)
        {
            int levelSize = queue.Count;

            for (int i = 0; i < levelSize; i++)
            {
                HexPoleScript current = queue.Dequeue();

                foreach (GameObject x in current.neighbors)
                {
                    HexPoleScript pole = x.GetComponent<HexPoleScript>();

                    if (pole == null || visited.Contains(pole))
                        continue;

                    visited.Add(pole);

                    if (!CanPassThrough(pole))
                        continue;

                    // Pokazujemy pole tylko wtedy,
                    // gdy droga w linii prostej jest wolna
                    if (!straightLine || HasClearLine(pole))
                    {
                        pole.disable();
                    }
                  
                    // Nadal przeszukujemy dalsze pola
                    queue.Enqueue(pole);
                }
            }

            distance++;
        }
    }


    public LayerMask lineOfSightMask;

    public bool HasClearLine(HexPoleScript target)
    {
        Vector2 start = transform.position;
        Vector2 end = target.transform.position;

        RaycastHit2D[] hits = Physics2D.LinecastAll(
            start,
            end,
            lineOfSightMask
        );

        foreach (RaycastHit2D hit in hits)
        {
            turnObject entity = hit.collider.GetComponentInParent<turnObject>();
            inreractiveObject interactive = hit.collider.GetComponentInParent<inreractiveObject>();

            Vector2Int cords = new Vector2Int(0, 0);

            if (entity != null)
            {
                
                cords = entity.pozycja;
            }
            else if (interactive != null)
            {
                cords = interactive.cords;
            }
            else
            {
                continue;
            }

            HexPoleScript pole = generator.pola[cords].GetComponent<HexPoleScript>();
            
            if (pole == null)
                continue;

            // Ignorujemy pole startowe i docelowe
            if (pole == this || pole == target)
                continue;

            if (generator.actionState == "Move" &&
              pole.dostepneAkcje["Occupated"])
            {
                return false;
            }

            // Sprawdzamy możliwość przejścia
            if (!CanPassThrough(pole))
                return false;

            // Sprawdzamy możliwość przeprowadzenia ataku przez przeszkodę
            if (generator.actionState == "Attack" &&
                !pole.dostepneAkcje["atackThrough"])
            {
                return false;
            }

          

        }

        return true;
    }

    public bool CanPassThrough(HexPoleScript pole)
    {
        

            if (!pole.dostepneAkcje[generator.actionState])
        {
            //Debug.Log($"============ \nAktualnie złe działanie\n {pole.dostepneAkcje[generator.actionState]}\n{generator.actionState}   ");
  
            return false;
        }
            

        if (generator.actionState == "Move" && pole.dostepneAkcje["Occupated"])
        {
            
            return false;
        }

        //Debug.Log("Precueż pozwalam na akcje");
        
        return true;
    }

    public void turnOffHex(int signalStrength)
    {
        signalStrength--;

        foreach (GameObject x in neighbors)
        {
            HexPoleScript pole = x.GetComponent<HexPoleScript>();

            pole.enable();
            if (signalStrength > 0)
            {
                pole.turnOffHex(signalStrength);
            }

        }
    }

    public void ZbierzItem()
    {
        if (item != null)
        {
            Destroy(item);
            Debug.Log("Usunięto item");

            //Dodać po id do listy z podniesionymi itemami
        }
        if (item != null)
        {
            ItemScript itemScript = item.GetComponent<ItemScript>();

            InventoryManager inventoryManager =
                FindFirstObjectByType<InventoryManager>();


            // Dodanie przedmiotu do ekwipunku
            bool added = inventoryManager.AddItem(itemScript.itemID);

            // Usunięcie przedmiotu z mapy tylko jeśli udało się go zebrać
            if (added)
            {
                ItemManager itemManager = FindFirstObjectByType<ItemManager>();

                if (itemManager != null)
                {
                    itemManager.items.Remove(itemScript);
                }

                Destroy(item);
                item = null;

                Debug.Log("Zebrano przedmiot!");
            }

        }
    }




    public GameObject buttonPrefab;
    public Transform buttonParent;

    private bool buttonClicked = false;


    public IEnumerator askForId()
    {
        // Czyścimy poprzednie przyciski
        ClearChoicePanel();

        if (currentStanding.Count == 1)
        {
            gameObjectId = 0;
            yield break;
        }
        else if (currentStanding.Count == 0)
        {
            gameObjectId = -1;
            yield break;
        }

        buttonClicked = false;

        // Pokazujemy panel
        buttonParent.gameObject.SetActive(true);

        for (int i = 0; i < currentStanding.Count; i++)
        {
            int id = i;

            GameObject newButton = Instantiate(buttonPrefab, buttonParent);

            chooseButton button = newButton.GetComponent<chooseButton>();

            button.Setup(id, "Obiekt " + id, OnButtonClicked);
        }

        // Czekamy na kliknięcie
        yield return new WaitUntil(() => buttonClicked);

        // Usuwamy przyciski i chowamy panel
        ClearChoicePanel();
    }

    private void ClearChoicePanel()
    {
        for (int i = buttonParent.childCount - 1; i >= 0; i--)
        {
            Destroy(buttonParent.GetChild(i).gameObject);
        }

        buttonParent.gameObject.SetActive(false);
    }


    private void OnButtonClicked(int id)
    {
        gameObjectId = id;
        buttonClicked = true;
    }

    public void askForRewriteAllowance()
    {
        dostepneAkcje.Add("Move", true);
        dostepneAkcje.Add("Occupated", false);
        dostepneAkcje.Add("Attack", true);
        dostepneAkcje.Add("Interact", false);
        dostepneAkcje.Add("atackThrough", true);
    }


}
