// using System.Numerics;
// using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
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

    void Start()
    {


        //coordinates = new Vector2(transform.position.x, transform.position.y);

    }


    public void DisplayCords()
    {
        //Debug.Log(coordinates.x + ", " + coordinates.y);
        //foreach(GameObject x in neighbors)
        //{
        //    Debug.Log(x);

        //}

    }

    private void OnMouseDown()
    {

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

    public void setActiveHex(int signalStrength = 1)
    {

        signalStrength--;

        foreach (GameObject x in neighbors)
        {
            HexPoleScript pole = x.GetComponent<HexPoleScript>();

            pole.disable();
            if (signalStrength > 0)
            {
                pole.setActiveHex(signalStrength);
            }

        }


    }

    public void turnOffHex(int signalStrength) {
        signalStrength--;

        foreach (GameObject x in neighbors)
        {
            HexPoleScript pole = x.GetComponent<HexPoleScript>();

            pole.enable();
            if (signalStrength > 0)
            {
                pole.setActiveHex(signalStrength);
            }

        }
    }

    public void ZbierzItem()
    {
        // if (item != null)
        // {
        //     Destroy(item);
        //     Debug.Log("Usunięto item");

        //     //Dodać po id do listy z podniesionymi itemami
        // }
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


}
