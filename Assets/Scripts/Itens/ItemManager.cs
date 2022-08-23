using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ebac.Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
   

    public SOPlayer flame;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        flame.flameSize = 1;
    }

  
    
    public void AddFlame(int amount=1)
    {
        flame.flameSize += amount;
    }
}
