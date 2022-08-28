using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerBase : MonoBehaviour
{
    public AudioSource flames;
    public bool canDamage = true;
    public float dpsFlame = 1;
    private bool colliderOn;
    private bool damagePlayer = true;
    public HealthFlame healthFlame;

    private void Start()
    {
        colliderOn = false;
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        var enemy = collision.gameObject.GetComponent<HealthBase>();

        if (enemy != null && collision.transform.tag == "Enemy" && canDamage && colliderOn)
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

    IEnumerator DamagePlayerTimer()
    {
        if (damagePlayer)
        {
            damagePlayer = false;
            healthFlame.Damage(1);
            yield return new WaitForSeconds(2);
            damagePlayer = true;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (colliderOn)
            {
                flames.Stop();
                colliderOn = false;
            }
            else
            {
                flames.Play();
                colliderOn = true;
            }
        }
        Debug.Log(damagePlayer);
        if (colliderOn)
        {
            StartCoroutine(DamagePlayerTimer());
        }
    }
}
