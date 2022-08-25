using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;

public class EnemyBase : MonoBehaviour
{
    public int damage = 2;

    public Animator animator;
    public string triggerAttack = "Attack";
    public string triggerDeath = "Death";

    public HealthBase healthBase;
    public float timeToDestroy = 1f;
    public AudioSource audioSource;
    public float EnemyKillYLimit;
    public bool isJumpable;

    [Header("Setup")]
    public SOPlayer soPlayerSetup;

    public EnemyScroll enemyScroll;

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
        if (collision.transform.tag == "Player")
        {

            var health = collision.gameObject.GetComponent<HealthBase>();
            var player = collision.gameObject.GetComponent<Player>();

            if(health != null)
            {
                health.Damage(damage);
                
                //PlayAttackAnimation();
            }

            if (collision.transform.position.x < transform.position.x &&
                collision.transform.position.y < transform.position.y + EnemyKillYLimit)
            {
                Debug.Log("Esquerdo");
                player.PlayDamageEffect("Left");

            }
            else if (collision.transform.position.x > transform.position.x &&
                collision.transform.position.y < transform.position.y + EnemyKillYLimit)
            {
                Debug.Log("Direito");
                player.PlayDamageEffect("Right");
            }
            else if (isJumpable &&
                collision.transform.position.y > transform.position.y + EnemyKillYLimit)
            {
                Debug.Log("Hit Jump");
                player.PlayDamageEffect("Up");
                enemyScroll.alive = false;
                OnEnemyKill();
                
            }
            else if (!isJumpable &&
                collision.transform.position.y > transform.position.y + EnemyKillYLimit)
            {
                Debug.Log("cima");
                player.PlayDamageEffect("Right");
            }

        }
    }


 
    private void PlayAttackAnimation()
    {
        animator.SetTrigger(triggerAttack);
    }
    private void PlayDeathAnimation()
    {
        animator.SetTrigger(triggerDeath);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
   
    public void Damage(int damage)
    {
        healthBase.Damage(damage);
    }

    
}
