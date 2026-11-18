
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotView : MonoBehaviour
{
    public int id { get; private set; }
    public Image background;

    public TMP_Text slotNumber;
    public TMP_Text itemName;
    public TMP_Text quantityText;

    public Color normalColor = Color.gray;
    public Color selectedColor = Color.yellow;
    public void Initialize(int slotId)
    {
        id = slotId;
    }

    // public void UpdateSlot(InventorySlot slot, bool selected)
    // {
    //     slotNumber.text = (slot.slotID + 1).ToString();


    //     if (slot.quantity > 0)
    //     {
    //         itemName.text = slot.itemID;
    //         quantityText.text = slot.quantity.ToString();
    //     }
    //     else
    //     {
    //         itemName.text = "";
    //         quantityText.text = "";
    //     }

    //     background.color = selected ? selectedColor : normalColor;
    // }
    public void UpdateSlot(InventorySlot slot, bool selected, int displayNumber)
    {
        slotNumber.text = displayNumber.ToString();

        if (slot.quantity > 0)
        {
            itemName.text = slot.itemID;
            quantityText.text = slot.quantity.ToString();
        }
        else
        {
            itemName.text = "";
            quantityText.text = "";
        }

        background.color = selected ? selectedColor : normalColor;
    }
    public void ButtonTest()
    {
        Debug.Log("Kliknieto w slot " + id);

        // InventorySlotView slot = GetComponent<InventorySlotView>();
        // int slotID = slot.id;

        // otwiera karte przedmiotu, rmb możliwość przeniesienia itemu w inne miejsce, double click używa itemu(na hotbarze jedyna opcja)

    }
}
// public class InventorySlot
// {
//     public int slotID;
//     public string itemID;
//     public int quantity;

//     public InventorySlot(int id)
//     {
//         slotID = id;
//         itemID = "";
//         quantity = 0;
//     }
// }
