using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFlame : HealthBase
{
    public SOPlayer soPlayer;
    public Animator animator;
    public Transform spawnPoint;


    private void Start()
    {
        
    }
    public override void Damage(int damage)
    {
        base.Damage(damage);
        soPlayer.flameSize -= damage;
        if (soPlayer.flameSize <= 2)
        {
            StartCoroutine(DeathAnimation());
            Init();
            soPlayer.flameSize = 6;
        }
    }



    IEnumerator DeathAnimation()
    {
      
        if (animator != null)
        {
            Debug.Log("morto");
            animator.SetTrigger("Death");
            gameObject.GetComponent<Collider2D>().enabled = false;
            yield return new WaitForSeconds(2f);
            animator.SetTrigger("Live");
            gameObject.transform.position = spawnPoint.position;
            gameObject.GetComponent<Collider2D>().enabled = true;
            
        }

    }


}
