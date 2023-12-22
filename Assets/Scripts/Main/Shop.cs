using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] string productName;
    [SerializeField] int price;
    [SerializeField] Text productPrice;

    [SerializeField] private PlayerStats playerStats;

    void Awake()
    {
        productPrice.text = price.ToString();
    }

    public void OnButtonDown()
    {
        int coins = PlayerPrefs.GetInt("coins");
        print(coins);
        if (coins >= price)
        {
           PlayerPrefs.SetInt("coins", coins - price);
           playerStats.IncreaseFood(price);
           playerStats.Tiredness(price);
        }
    }
}
