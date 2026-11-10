using UnityEngine;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    public List<ItemScript> items = new List<ItemScript>();

    public void SprawdzKolizje(Vector2Int playerCoordinates)
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            ItemScript item = items[i];

            if (item == null)
            {
                items.RemoveAt(i);
                continue;
            }

            if (item.itemCords == playerCoordinates)
            {
                Destroy(item.gameObject);
                items.RemoveAt(i);

                Debug.Log("Zebrano przedmiot!");
            }
        }
    }
}