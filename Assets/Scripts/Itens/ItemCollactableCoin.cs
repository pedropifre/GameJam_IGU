using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollactableCoin : ItemCollactableBase
{    
    public Collider2D collider;
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddCoins();
        collider.enabled = false;
    }
}
