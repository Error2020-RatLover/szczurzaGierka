
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotView : MonoBehaviour
{
    public Image background;

    public TMP_Text slotNumber;
    public TMP_Text itemName;
    public TMP_Text quantityText;

    public Color normalColor = Color.gray;
    public Color selectedColor = Color.yellow;

    public void UpdateSlot(InventorySlot slot, bool selected)
    {
        slotNumber.text = (slot.slotID + 1).ToString();

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
}
    