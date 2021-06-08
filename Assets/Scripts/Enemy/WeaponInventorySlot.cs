using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventorySlot : MonoBehaviour
{
    PlayerInventory playerInventory;
    WeaponSlotManager weaponSlotManager;
    UIManager uiManager;

    public Image icon;
    WeaponItem item;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void AddWeaponItem(WeaponItem newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearInventorySlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void EquipThisItem()
    {
        if (uiManager.rightHandSlot01Selected)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponsInRightHandSlots[0]);
            playerInventory.weaponsInRightHandSlots[0] = item;
            playerInventory.weaponsInventory.Remove(item);
        }
        else if (uiManager.rightHandSlot02Selected)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponsInRightHandSlots[1]);
            playerInventory.weaponsInRightHandSlots[1] = item;
            playerInventory.weaponsInventory.Remove(item);
        }
        else if (uiManager.leftHandSlot01Selected)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponsInLeftHandSlots[0]);
            playerInventory.weaponsInLeftHandSlots[0] = item;
            playerInventory.weaponsInventory.Remove(item);
        }
        else if (uiManager.leftHandSlot02Selected)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponsInLeftHandSlots[1]);
            playerInventory.weaponsInLeftHandSlots[1] = item;
            playerInventory.weaponsInventory.Remove(item);
        }
        else
        {
            return;
        }

        if (playerInventory.currentRightWeaponIndex != -1)
        {
            playerInventory.rightWeapon = playerInventory.weaponsInRightHandSlots[playerInventory.currentRightWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
        }
        if (playerInventory.currentLeftWeaponIndex != -1)
        {
            playerInventory.leftWeapon = playerInventory.weaponsInLeftHandSlots[playerInventory.currentLeftWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);
        }

        uiManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
        uiManager.ResetAllSelectedSlots();
        uiManager.UpdateUI();
        uiManager.OpenCharacterEquipmentWindow();
    }
}
