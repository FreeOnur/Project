using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    // Reference to the Shop UI GameObject
    [SerializeField] GameObject ShopUI;

    // Keeps track of the shop UI state (open or closed)
    private bool isShopOpen = false;

    private void Awake()
    {
        // Ensure the instance is set properly
        instance = this;
    }

    // Method to toggle the Shop UI
    public void ToggleUI()
    {
        isShopOpen = !isShopOpen; // Switch the state
        ShopUI.SetActive(isShopOpen); // Set UI active or inactive based on the state
    }
}
