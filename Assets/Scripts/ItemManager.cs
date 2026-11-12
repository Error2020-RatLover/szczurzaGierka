using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class ItemManager : MonoBehaviour
{

    // itemy przeniesione z room generator
    public GameObject hat;
    public GameObject yPotion;
    public List<ItemScript> items = new List<ItemScript>(); //Lista spawnowanych itemów

    public GameObject returnGameObject()//wybranie losowego itemu
    {
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            return hat;
        }
        else
        {
            return yPotion;
        }
    }
    void Update()//po kliknieciu P wyswietla w konsoli wszystkie itemy z listy(debug)
    {
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {
            Debug.Log("======= LISTA ITEMÓW =======");

            foreach (ItemScript item in items)
            {
                Debug.Log("ID: " + item.itemID + " | Pozycja: " + item.itemCords
                );
            }

            Debug.Log("===========================");
        }
    }



    //więkość logiki przeniosłem do hex pola(zabierz item) i ChampionScript (move)

    //public void SprawdzKolizje(Vector2Int playerCoordinates)
    //{
    //    for (int i = items.Count - 1; i >= 0; i--)
    //    {
    //        ItemScript item = items[i];

    //        if (item == null)
    //        {
    //            items.RemoveAt(i);
    //            continue;
    //        }

    //        if (item.itemCords == playerCoordinates)
    //        {
    //            Destroy(item.gameObject);
    //            items.RemoveAt(i);

    //            Debug.Log("Zebrano przedmiot!");
    //        }
    //    }
    //}
}