using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("Item Details")]
    public string itemName;
    public string desc;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStr;

    [Header("Weapon/Armor Details")]
    public int weaponStr;
    public int armorStr;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Use(int charToUseOn) {

        if (BattleManager.instance.battleActive) {
            charToUseOn = BattleManager.instance.currentActiveBattler;
        }

        CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];

        Debug.Log(selectedChar.charName);
        Debug.Log("current HP: " + selectedChar.currentHp);
        Debug.Log("max HP: " + selectedChar.maxHp);

        if (isItem) {
            Debug.Log("Is Item");
            if (selectedChar.currentHp != selectedChar.maxHp) {
                Debug.Log("Not max HP");
                if (affectHP) {
                    Debug.Log("Item affects HP");
                    Debug.Log("amount to change: " + amountToChange);
                    selectedChar.currentHp += amountToChange;

                    if (selectedChar.currentHp > selectedChar.maxHp) {
                        selectedChar.currentHp = selectedChar.maxHp;
                    }

                    if (BattleManager.instance.battleActive) {
                        BattleManager.instance.activeBattlers[charToUseOn].currentHP += amountToChange;
                        if (BattleManager.instance.activeBattlers[charToUseOn].currentHP > selectedChar.maxHp) {
                            BattleManager.instance.activeBattlers[charToUseOn].currentHP = selectedChar.maxHp;
                        }
                    }
                }
                GameManager.instance.RemoveItem(itemName);
            }

            if (selectedChar.currentMp != selectedChar.maxMp) {
                if (affectMP) {
                    selectedChar.currentMp += amountToChange;

                    if (selectedChar.currentMp > selectedChar.maxMp) {
                        selectedChar.currentMp = selectedChar.maxMp;
                    }

                    if (BattleManager.instance.battleActive) {
                        BattleManager.instance.activeBattlers[charToUseOn].currentMP += amountToChange;
                        if (BattleManager.instance.activeBattlers[charToUseOn].currentMP > selectedChar.maxMp) {
                            BattleManager.instance.activeBattlers[charToUseOn].currentMP = selectedChar.maxMp;
                        }
                    }
                }
                GameManager.instance.RemoveItem(itemName);
            }

            if (affectStr) {
                selectedChar.strength += amountToChange;
                GameManager.instance.RemoveItem(itemName);
            }
        }

        if (isWeapon) {
            if (selectedChar.equippedWpn != "") {
                GameManager.instance.AddItem(selectedChar.equippedWpn);
            }

            selectedChar.equippedWpn = itemName;
            selectedChar.wpnPwr = weaponStr;
            GameManager.instance.RemoveItem(itemName);
        }

        if (isArmor) {
            if (selectedChar.equippedArmr != "") {
                GameManager.instance.AddItem(selectedChar.equippedArmr);
            }

            selectedChar.equippedArmr = itemName;
            selectedChar.armrPwr = armorStr;
            GameManager.instance.RemoveItem(itemName);
        }
    }
}
