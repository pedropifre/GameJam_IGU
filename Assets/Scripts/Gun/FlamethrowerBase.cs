using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerBase : MonoBehaviour
{
    public bool canDamage = true;
    public float dpsFlame = 1;


    private void OnTriggerStay2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<HealthBase>();

        if (enemy != null && collision.transform.tag == "Enemy" && canDamage)
        {
            Debug.Log("damege");
            enemy.Damage(1);
            StartCoroutine(canDamageTimer());
        }
    }

    IEnumerator canDamageTimer()
    {
        canDamage = false;
        yield return new WaitForSeconds(dpsFlame);
        canDamage = true;
    }
  
}
