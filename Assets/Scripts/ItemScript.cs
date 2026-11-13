using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Vector2Int itemCords;
    public string itemID;
    void Start()
    {
        ItemManager manager = FindFirstObjectByType<ItemManager>();

        manager.items.Add(this);
        Debug.Log("ID obiektu: " + itemID + " dodany na pozycji " + itemCords);
    }

}
