using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public HealthBase healthBase;
    public HealthFlame healthFlame;
    public Transform spawnPoint;
    //public GameObject telaFinal;

    [Header("Setup")]
    public SOPlayer soPlayerSetup;
    public SOInt lifeText;

    //public Animator animator;
    [Header("Shoot")]
    public UnityEngine.Rendering.Universal.Light2D light2D;
    public ParticleSystem particleSystemShoot;
    public bool isShooting;

    private float _curentSpeed;
    [SerializeField]private bool canJump = true;
    private bool canMove = true;

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
        healthFlame.animator = _currentPlayer.GetComponent<Animator>();

    }

    

    private void OnPlayerKill()
    {
        healthBase.onKill -= OnPlayerKill;
        //_currentPlayer.SetTrigger(soPlayerSetup.triggerDeath);
        


    }



    public void Update()
    {
        if (canMove)
        {
            HandleMoviment();
            HandleJump();
            HandleShoot();
        }
    }
    private void HandleShoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!isShooting)
            {
                particleSystemShoot.Play();
                StartCoroutine(fadeInAndOutRepeat(light2D, 1f, true));
               
                isShooting = true;
            }
            else
            { 
                particleSystemShoot.Stop();
                StartCoroutine(fadeInAndOutRepeat(light2D, 1f,false));
                isShooting = false;

            }
        }
        

    }
    IEnumerator fadeInAndOut(UnityEngine.Rendering.Universal.Light2D lightToFade, bool fadeIn, float duration)
    {
        float minLuminosity = 0; // min intensity
        float maxLuminosity = 2.22f; // max intensity

        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;

        if (fadeIn)
        {
            a = minLuminosity;
            b = maxLuminosity;
        }
        else
        {
            a = maxLuminosity;
            b = minLuminosity;
        }

        float currentIntensity = lightToFade.intensity;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);

            yield return null;
        }
    }
    IEnumerator fadeInAndOutRepeat(UnityEngine.Rendering.Universal.Light2D lightToFade, float duration,bool turn)
    {

        if(turn) yield return fadeInAndOut(lightToFade, true, duration);
        else yield return fadeInAndOut(lightToFade, false, duration);
    
        
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
        
        if (Input.GetKey(KeyCode.A))
        {
            myRigidBody.velocity = new Vector2(-_curentSpeed, myRigidBody.velocity.y);
            
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            myRigidBody.velocity = new Vector2(_curentSpeed, myRigidBody.velocity.y);
            
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
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && canJump == true)
        {
            _currentPlayer.SetBool(soPlayerSetup.triggerJump,true);
            if (randomShoot != null) randomShoot.PlayRandom();
            particleRun.gameObject.SetActive(false);
            myRigidBody.velocity = Vector2.up * soPlayerSetup.forceJump;
            myRigidBody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidBody.transform);

            canJump = false;
            HandleScaleJump();
            PlayJumpVFX();
        }
    }

    private void PlayJumpVFX()
    {
        //VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.JUMP, transform.position);
        
        //if (particlePulo != null) particlePulo.Play();
    }

    public void PlayDamageEffect(string position)
    {
        if (position == "Left")
        {
            StartCoroutine(RecoilPlayer(-5f));
        }
        else if (position == "Right")
        {
            StartCoroutine(RecoilPlayer(5f));
        }
        else if (position == "Up")
        {
            gameObject.transform.DOMoveY(transform.position.y + 5f, .5f).SetEase(Ease.OutBack);
            //Vector2 pulo = new Vector2(2, 0);
            //gameObject.transform.DOJump(pulo, 2f, 1, .5f);
        }
    }

    IEnumerator RecoilPlayer(float RecoilDirection)
    {
        canMove = false;
        gameObject.transform.DOMoveX(transform.position.x + RecoilDirection, .5f);
        yield return new WaitForSeconds(.5f);
        canMove = true;
    }
    private void PlayRunVFX()
    {
        particleRun.gameObject.SetActive(true);
    }
    private void PlayRespawnVFX()
    {
        VFXManager.Instantiate.PlayVFXByType(VFXManager.VFXType.RESPAWN, transform.position);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
            _currentPlayer.SetBool(soPlayerSetup.triggerJump, false);
           
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayRunVFX();
    }

    private void HandleScaleJump()
    {
        //myRigidBody.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2,LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        //myRigidBody.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2,LoopType.Yoyo).SetEase(soPlayerSetup.ease);
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
