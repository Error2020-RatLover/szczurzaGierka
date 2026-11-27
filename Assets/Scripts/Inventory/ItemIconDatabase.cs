using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ItemIconEntry
{
    public string itemID;
    public Sprite icon;
}

public class ItemIconDatabase : MonoBehaviour
{
    public List<ItemIconEntry> items = new List<ItemIconEntry>();

    public Sprite GetIcon(string itemID)
    {
        foreach (ItemIconEntry entry in items)
        {
            if (entry.itemID == itemID)
                return entry.icon;
        }

        return null;
    }
}