using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyBase : MonoBehaviour
{
    public int damage = 10;

    public Animator animator;
    public string triggerAttack = "Attack";
    public string triggerDeath = "Death";

    public HealthBase healthBase;
    public float timeToDestroy = 1f;
    public AudioSource audioSource;
    
    [Header("Setup")]
    public SOPlayer soPlayerSetup;
    

    private void Awake()
    {
        if(healthBase!=null)
        {
            healthBase.onKill += OnEnemyKill;
        }
    }

    private void OnEnemyKill()
    {
        healthBase.onKill -= OnEnemyKill;
        PlayDeathAnimation();
        Destroy(gameObject,timeToDestroy);
        soPlayerSetup.enemiesKilled +=1;
        if(audioSource!=null)audioSource.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        var health = collision.gameObject.GetComponent<HealthBase>();

        if(health != null)
        {
            health.Damage(damage);
            PlayAttackAnimation();
        }
    }

    private void PlayAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }
    private void PlayDeathAnimation()
    {
        animator.SetTrigger(triggerDeath);
    }
   
    public void Damage(int damage)
    {
        healthBase.Damage(damage);
    }
}
