using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ebac.Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
   

    public SOInt coins;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
    }

    public void AddCoins(int amount=1)
    {
        coins.value += amount;
    }
}
