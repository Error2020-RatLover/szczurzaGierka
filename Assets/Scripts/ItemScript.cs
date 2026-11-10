using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Vector2Int itemCords;
    void Start()
    {
        ItemManager manager = FindFirstObjectByType<ItemManager>();

        manager.items.Add(this);
    }

}
