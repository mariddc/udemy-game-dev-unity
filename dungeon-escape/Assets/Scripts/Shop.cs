using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject _shopPanel;
    private int currentItem;
    private int[] itemsCost = {200, 400, 100};
    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            _shopPanel.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (player != null)
        {
            UIManager.Instance.OpenShop(player.GetDiamonds());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int itemID)
    {
        currentItem = itemID;

        // 0 = flame sword
        // 1 = boots of flight
        // 2 = key to castle
        float[] yPositions = {74.0f, -31.0f, -136.0f};
        UIManager.Instance.UpdateShopSelection(yPositions[itemID]);
    }

    public void BuyItem()
    {
        int price = itemsCost[currentItem];
        if (player.GetDiamonds() >= price)
        {
            //award item
            switch (currentItem)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    GameManager.Instance.HasKeyToCastle = true;
                    break;
            }
            player.UpdateDiamonds(-1 * price);
        }
        else
        {
            Debug.Log("You do not have enough diamonds.");
        }
    }
}
