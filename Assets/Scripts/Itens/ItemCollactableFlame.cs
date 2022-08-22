using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollactableFlame : ItemCollactableBase
{    
    public Collider2D colliderp;
    protected override void OnCollect()
    {
        base.OnCollect();
        //Debug.Log("Coletado");
        ItemManager.Instance.AddFlame(3);
        colliderp.enabled = false;
    }
}
