using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthFlame : HealthBase
{
    public SOPlayer soPlayer;
    public Animator animator;
    public Animator animatorUI;
    public Transform spawnPoint;
    public Image imagem;


    private void Start()
    {
        imagem.fillAmount = .1f;
    }
    private void Update()
    {
        if (soPlayer.flameSize > 12)
        {
            soPlayer.flameSize = 12;
            imagem.fillAmount = 1;
        }
        else
        {
            imagem.fillAmount = soPlayer.flameSize * .1f;
        }

        

    }
  
    public override void Damage(int damage)
    {
        StartCoroutine(UIDamage());
        base.Damage(damage);
        
        soPlayer.flameSize -= damage;


        if (soPlayer.flameSize <= 2)
        {
            StartCoroutine(DeathAnimation());
            Init();
            soPlayer.flameSize = 6;
        }

       
    }

    IEnumerator UIDamage()
    {
        animatorUI.SetTrigger("Damage");
        yield return new WaitForSeconds(.37f);
        if (soPlayer.flameSize > 3)
        {
            animatorUI.SetTrigger("Idle");
        }
        else
        {
            animatorUI.SetTrigger("Danger");
        }
    }

    IEnumerator DeathAnimation()
    {
      
        if (animator != null)
        {
            Debug.Log("morto");
            animator.SetTrigger("Death");
            gameObject.transform.tag = "Batata";
            yield return new WaitForSeconds(2f);
            animator.SetTrigger("Live");
            gameObject.transform.position = spawnPoint.position;
            gameObject.transform.tag = "Player";
            //gameObject.GetComponent<Collider2D>().enabled = true;
            
        }

    }


}
