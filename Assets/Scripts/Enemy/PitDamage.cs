using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitDamage : MonoBehaviour
{
    public HealthFlame healthFlame;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            healthFlame.Damage(100);
        }
    }
}
