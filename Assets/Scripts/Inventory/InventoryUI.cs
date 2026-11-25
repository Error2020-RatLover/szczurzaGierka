
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public InventoryManager inventoryManager;

    // UI
    public GameObject inventoryPanel;

    public GameObject hotbar;
    public GameObject actionbar;

    public Transform hotbarContainer;
    public Transform inventoryContainer;

    //    prefab slotow
    public GameObject slotPrefab;

    private List<InventorySlotView> hotbarSlots = new();
    private List<InventorySlotView> inventorySlots = new();
    private int selectedMoveSlot = -1;

    void Start()
    {
        if (inventoryManager == null)
            inventoryManager = FindFirstObjectByType<InventoryManager>();

        if (inventoryManager == null)
        {
            Debug.LogError("Nie znaleziono InventoryManager!");
            return;
        }

        // Ekwipunek domyślnie zamknięty
        inventoryPanel.SetActive(false);

        CreateSlots();
        RefreshUI();
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            ToggleInventory();
        }
        if (Keyboard.current != null &&
          Keyboard.current.iKey.wasPressedThisFrame)
        {
            hotbar.SetActive(!hotbar.activeSelf);
            actionbar.SetActive(!actionbar.activeSelf);
        }
    }
    // void CreateSlots()
    // {
    //     // Pierwsze 5 slotów - hotbar
    //     for (int i = 0; i < 5; i++)
    //     {
    //         GameObject obj = Instantiate(slotPrefab, hotbarContainer);

    //         InventorySlotView view = obj.GetComponent<InventorySlotView>();

    //         hotbarSlots.Add(view);
    //     }

    //     // Pozostałe sloty - główny ekwipunek
    //     for (int i = 5; i < inventoryManager.inventory.Count; i++)
    //     {
    //         GameObject obj = Instantiate(slotPrefab, inventoryContainer);

    //         InventorySlotView view = obj.GetComponent<InventorySlotView>();

    //         inventorySlots.Add(view);
    //     }
    // }
    void CreateSlots()
    {
        // HOTBAR
        for (int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate(slotPrefab, hotbarContainer);

            InventorySlotView view = obj.GetComponent<InventorySlotView>();

            view.Initialize(i, this, false);

            hotbarSlots.Add(view);
        }

        // INVENTORY
        for (int i = 0; i < inventoryManager.inventory.Count; i++)
        {
            GameObject obj = Instantiate(slotPrefab, inventoryContainer);

            InventorySlotView view = obj.GetComponent<InventorySlotView>();

            view.Initialize(i, this, true);

            inventorySlots.Add(view);
        }
    }



    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        RefreshUI();
    }

    // public void RefreshUI()
    // {
    //     if (inventoryManager == null)
    //         return;

    //     // Aktualizacja hotbara
    //     for (int i = 0; i < hotbarSlots.Count; i++)
    //     {
    //         hotbarSlots[i].UpdateSlot(
    //             inventoryManager.inventory[i],
    //             inventoryManager.selectedSlot == i
    //         );
    //     }

    //     // Aktualizacja pozostałych slotów
    //     for (int i = 0; i < inventorySlots.Count; i++)
    //     {
    //         int inventoryIndex = i + 5;

    //         inventorySlots[i].UpdateSlot(
    //             inventoryManager.inventory[inventoryIndex],
    //             inventoryManager.selectedSlot == inventoryIndex
    //         );
    //     }
    // }
    public void RefreshUI()
    {
        if (inventoryManager == null) return;

        // HOTBAR
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            hotbarSlots[i].UpdateSlot(
                inventoryManager.inventory[i],
                inventoryManager.selectedSlot == i,
                i + 1
            );
        }

        // INVENTORY
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].UpdateSlot(
                inventoryManager.inventory[i],
                inventoryManager.selectedSlot == i,
                i + 6,
                selectedMoveSlot == i
            );
        }
    }
    public void OnSlotRightClick(int clickedSlot)
    {
        // rmb wybieramy przedmiot do przeniesienia
        if (selectedMoveSlot == -1)
        {
            // Nie można wybrać pustego slotu
            if (inventoryManager.inventory[clickedSlot].quantity <= 0)
                return;

            selectedMoveSlot = clickedSlot;

            RefreshUI();
            return;
        }

        // Kliknięcie tego samego slotu anuluje wybór
        if (selectedMoveSlot == clickedSlot)
        {
            selectedMoveSlot = -1;
            RefreshUI();
            return;
        }

        // rmb przenoszenie
        inventoryManager.MoveItem(selectedMoveSlot, clickedSlot);

        selectedMoveSlot = -1;

        RefreshUI();
    }
}
