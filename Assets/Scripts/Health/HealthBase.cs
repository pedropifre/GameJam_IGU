using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{

    public Action onKill;
    public int startLife = 10;

    public bool destroyOnKill = false;
    public float delayToKill = 3f;

    public int _currentLife;
    public bool _isDead = false;

    public FlashCollor flashColor;

    private void Awake()
    {
        Init();
        if(flashColor == null)
        {
            flashColor = GetComponent<FlashCollor>();
        }
    }
    
    private void Init()
    {
        _isDead = false;
        _currentLife = startLife;
    }
    public void Damage(int damage)
    {
        if (_isDead) return;
        _currentLife -= damage;

        if(_currentLife<=0)
        {
            Kill();
        }

        if(flashColor!=null)
        {
            flashColor.Flash();
        }
    }

    private void Kill()
    {
        _isDead = true;
       

        if (destroyOnKill)
        {
            Destroy(gameObject,delayToKill);
        }
        onKill?.Invoke();
    }
}
