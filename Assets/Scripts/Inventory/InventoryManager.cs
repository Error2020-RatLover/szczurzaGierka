using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class InventoryManager : MonoBehaviour
{
    public int maxSlots = 5; //maksymalna liczba slotów

    public List<InventorySlot> inventory = new List<InventorySlot>();

    public int selectedSlot = 0; // Aktualnie wybrany slot

    public InventoryUI inventoryUI;



    void Awake()
    {
        // Tworzenie pustych slotów
        for (int i = inventory.Count; i < maxSlots; i++)
        {
            inventory.Add(new InventorySlot(i));
        }

        // Ustawienie id slotów
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[i].slotID = i;
        }
    }
    //E pokazuje ekwipunek w konsoli, 1-5 wybiera slot(można dać potem więcej)(domyślnie slot 0), F używa przedmiot
    void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            SelectSlot(0);

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            SelectSlot(1);

        if (Keyboard.current.digit3Key.wasPressedThisFrame)
            SelectSlot(2);

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
            SelectSlot(3);

        if (Keyboard.current.digit5Key.wasPressedThisFrame)
            SelectSlot(4);

        // Używanie przedmiotu
        if (Keyboard.current.fKey.wasPressedThisFrame)
            UseItem();

        // Wyświetlanie ekwipunku (debug) do wywalenia później
        if (Keyboard.current.eKey.wasPressedThisFrame)
            DisplayInventory();
    }

    // Wybieranie slotu
    public void SelectSlot(int slotID)
    {
        if (slotID < 0 || slotID >= inventory.Count)
            return;

        selectedSlot = slotID;
        RefreshInventoryUI();

        InventorySlot slot = inventory[selectedSlot];

        Debug.Log("Wybrano slot: " + selectedSlot + " | ID: " + (slot.quantity > 0 ? slot.itemID : "Empty") + " | Ilość: " + slot.quantity);
    }

    // Używanie przedmiotu
    public void UseItem()
    {
        InventorySlot slot = inventory[selectedSlot];
        // Sprawdzenie, czy slot jest pusty
        if (slot.quantity <= 0 || string.IsNullOrEmpty(slot.itemID))
        {
            Debug.Log("Wybrany slot jest pusty! Nie można użyć przedmiotu.");
            return;
        }

        // Zapamiętanie ID przed usunięciem
        string usedItemID = slot.itemID;

        Debug.Log("Użyto " + usedItemID);

        // Zmniejszenie ilości przedmiotów
        slot.quantity--;

        // Jeśli nie ma już przedmiotów, wyczyść slot
        if (slot.quantity <= 0)
        {
            slot.quantity = 0;
            slot.itemID = "";

            Debug.Log("Slot " + slot.slotID + " jest teraz pusty.");
        }

        RefreshInventoryUI();
    }

    // Dodawanie przedmiotu
    public bool AddItem(string itemID)
    {
        if (string.IsNullOrEmpty(itemID))
        {
            Debug.LogWarning("Nieprawidłowe ID przedmiotu!");
            return false;
        }

        // Sprawdzenie, czy przedmiot już istnieje
        foreach (InventorySlot slot in inventory)
        {
            if (slot.itemID == itemID && slot.quantity > 0)
            {
                slot.quantity++;

                Debug.Log("Dodano " + itemID + " do slotu " + slot.slotID + ". Ilość: " + slot.quantity);
                RefreshInventoryUI();
                return true;
            }
        }

        // Znalezienie pierwszego pustego slotu
        foreach (InventorySlot slot in inventory)
        {
            if (slot.quantity == 0)
            {
                slot.itemID = itemID;
                slot.quantity = 1;

                Debug.Log("Dodano " + itemID + " do nowego slotu " + slot.slotID);
                RefreshInventoryUI();

                return true;
            }
        }

        Debug.Log("Ekwipunek jest pełny!");
        return false;
    }

    // Wyświetlanie ekwipunku
    public void DisplayInventory()
    {
        Debug.Log("======= EKWIPUNEK =======");

        foreach (InventorySlot slot in inventory)
        {
            Debug.Log(
                "Slot: " + slot.slotID + (slot.slotID == selectedSlot ? " [WYBRANY]" : "") + " | ID: " + (string.IsNullOrEmpty(slot.itemID) ? "Empty" : slot.itemID) + " | Ilość: " + slot.quantity
            );
        }

        Debug.Log("=========================");
    }
    void RefreshInventoryUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.RefreshUI();
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public int slotID;
    public string itemID;
    public int quantity;


    public InventorySlot(int id)
    {
        slotID = id;
        itemID = "";
        quantity = 0;
    }
}