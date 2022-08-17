using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollactableBase : MonoBehaviour
{

    [Header("Sounds")]
    public AudioSource audioSource;

    public string compareTag = "Player";
    public float timeToHide=3;
    public GameObject graphicItem;

    private void Awake()
    {
        //if (particleSystem != null) particleSystem.transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }


    protected virtual void Collect() 
    {
        OnCollect();
        if (graphicItem != null) graphicItem.SetActive(false);
        Invoke(nameof(HideObject), timeToHide);
    }
    private void HideObject()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnCollect() 
    {
        
        //VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.COIN, transform.position);
        if (audioSource != null) audioSource.Play();
        
    }
    
        
}
