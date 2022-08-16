using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public HealthBase healthBase;
    public GameObject spawnPoint;
    //public GameObject telaFinal;

    [Header("Setup")]
    public SOPlayer soPlayerSetup;
    public SOInt lifeText;

    //public Animator animator;

    private float _curentSpeed;
    private bool canJump = true;
    private bool canRun = true;

    private Animator _currentPlayer;
    public AudioRandomPlayClips randomShoot;

    [Header("Run Particle")]
    public ParticleSystem particleRun;

    private void Awake()
    {
        soPlayerSetup.life = 3;
        soPlayerSetup.enemiesKilled = 0;
        lifeText.value = soPlayerSetup.life;
        //telaFinal.SetActive(false);

        if(healthBase!=null)
        {
            healthBase.onKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(soPlayerSetup.player, transform);
        _currentPlayer.GetComponent<PlayerDestroyerHelper>().player = GameObject.FindObjectOfType<Player>();

    }

    

    private void OnPlayerKill()
    {
        healthBase.onKill -= OnPlayerKill;
        
        _currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
        
    }



    public void Update()
    {
        HandleMoviment();
        HandleJump();
    }

    private void HandleMoviment()
    {
        //verificar corrida
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _curentSpeed = soPlayerSetup.speedRun;
            _currentPlayer.speed = 2;
        }
        else
        {
            _curentSpeed = soPlayerSetup.speed;
            _currentPlayer.speed = 1;
        }
            

        //movimento
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidBody.velocity = new Vector2(-_curentSpeed, myRigidBody.velocity.y);
            if(myRigidBody.transform.localScale.x != -1)
            {
                myRigidBody.transform.DOScaleX(-1, soPlayerSetup.playerSwypeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidBody.velocity = new Vector2(_curentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != 1)
            {
                myRigidBody.transform.DOScaleX(1, soPlayerSetup.playerSwypeDuration);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }

        //eliminar fric��o
        if(myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity -= soPlayerSetup.friction; 
        }

        if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity += soPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            if (randomShoot != null) randomShoot.PlayRandom();
            canJump = false;
            particleRun.gameObject.SetActive(false);
            myRigidBody.velocity = Vector2.up * soPlayerSetup.forceJump;
            myRigidBody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidBody.transform);

            HandleScaleJump();
            PlayJumpVFX();
        }
    }

    private void PlayJumpVFX()
    {
        //VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.JUMP, transform.position);
        
        //if (particlePulo != null) particlePulo.Play();
    }
    private void PlayRunVFX()
    {
        particleRun.gameObject.SetActive(true);
    }
    private void PlayRespawnVFX()
    {
        VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.RESPAWN, transform.position);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
           
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayRunVFX();
    }

    private void HandleScaleJump()
    {
        myRigidBody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2,LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        myRigidBody.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2,LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        //fazer a anima��o de queda com a fun��o do DoTween para esperar a anterior acabar
    }
    public void SpawnPlayer()
    {
        gameObject.transform.position = spawnPoint.transform.position;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerLive);
        PlayRespawnVFX();
        
       
    }
    public void DestroyMe()
    { 
        if(soPlayerSetup.life==1)
        {
            Destroy(gameObject);
            //telaFinal.SetActive(true);

        }
        else
        {   
            Invoke("SpawnPlayer",1.6f);
            healthBase._isDead = false;
            healthBase._currentLife = 10; 
            healthBase.onKill += OnPlayerKill;
            soPlayerSetup.life--;
            lifeText.value = soPlayerSetup.life;
           
        }
       
    }


}
