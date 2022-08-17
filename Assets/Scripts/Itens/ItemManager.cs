using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ebac.Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
   

    public SOInt flame;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        flame.value = 1;
    }

  
    
    public void AddFlame(int amount=1)
    {
        flame.value += amount;
    }
}
