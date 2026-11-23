
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
        // Hotbar: 5 slotów
        for (int i = 0; i < 5; i++)
        {
            GameObject obj = Instantiate(slotPrefab, hotbarContainer);
            InventorySlotView view = obj.GetComponent<InventorySlotView>();
            view.Initialize(i);

            hotbarSlots.Add(view);
        }

        // Inventory: wszystkie 20 slotów
        for (int i = 0; i < inventoryManager.inventory.Count; i++)
        {
            GameObject obj = Instantiate(slotPrefab, inventoryContainer);
            InventorySlotView view = obj.GetComponent<InventorySlotView>();
            view.Initialize(i);

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

        // Hotbar
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            hotbarSlots[i].UpdateSlot(
                inventoryManager.inventory[i],
                inventoryManager.selectedSlot == i, i + 1);
        }

        // Inventory
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].UpdateSlot(
                inventoryManager.inventory[i],
                inventoryManager.selectedSlot == i,
                i + 6);
        }
    }
}
