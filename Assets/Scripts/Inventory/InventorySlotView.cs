using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotView : MonoBehaviour, IPointerClickHandler
{
    public int id { get; private set; }

    public Image background;
    public TMP_Text slotNumber;
    public TMP_Text itemName;
    public TMP_Text quantityText;

    public Color normalColor = Color.gray;
    public Color selectedColor = Color.yellow;
    public Color moveSelectedColor = new Color(1f, 0.5f, 0f);

    private InventoryUI inventoryUI;
    private bool canRightClick;

    public void Initialize(int slotId, InventoryUI ui, bool allowRightClick)
    {
        id = slotId;
        inventoryUI = ui;
        canRightClick = allowRightClick;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canRightClick)
            return;

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventoryUI.OnSlotRightClick(id);
        }
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
    public void UpdateSlot(InventorySlot slot, bool selected, int displayNumber, bool moveSelected = false)
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

        if (moveSelected)
            background.color = moveSelectedColor;
        else
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
