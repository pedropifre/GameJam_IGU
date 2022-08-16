using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShot = .3f;
    public Transform playerSideReference;

    private Coroutine _currentCoroutine;
    public AudioRandomPlayClips randomShoot;



    // Update is called once per frame
    
    private void Awake() 
    {
        playerSideReference = GameObject.FindObjectOfType<Player>().transform;    
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
        } 
    }

    IEnumerator StartShoot()
    {
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShot);
            Debug.Log("shoot");
        }
    }

    public void Shoot()
    {
        if(randomShoot!=null)randomShoot.PlayRandom();
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.side = playerSideReference.transform.localScale.x;
    }

    
}
