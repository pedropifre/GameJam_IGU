using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public ProjectileBase prefabProjectile;

    public Transform positionToShoot;
    public float timeBetweenShot = 2f;
    public Transform playerSideReference;

    private Coroutine _currentCoroutine;
    public AudioRandomPlayClips randomShoot;
    private bool canShoot = true;
    public bool canShootLoad = false;
    public int ShootLoad = 3;
    public float ShootLoadBreak = .2f;
    
    
    
    IEnumerator StartShoot()
    {
        canShoot = false;
        Shoot();
        yield return new WaitForSeconds(timeBetweenShot);
        
        canShoot = true;
        
    }
    IEnumerator StartShootLoad()
    {
        canShoot = false;
        var controllerLoad = ShootLoad;
        while (controllerLoad >= 1)
        {
            Shoot();
            yield return new WaitForSeconds(ShootLoadBreak);
            controllerLoad--;
        }
        yield return new WaitForSeconds(timeBetweenShot);
        
        canShoot = true;
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canShoot && collision.transform.tag == "Player" && !canShootLoad)
        {
            StartCoroutine(StartShoot());
            
        }
        else if (canShoot && collision.transform.tag == "Player" && canShootLoad)
        {
            StartCoroutine(StartShootLoad());
        }
    }

    
    public void Shoot()
    {
        if (randomShoot != null) randomShoot.PlayRandom();
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.side = playerSideReference.transform.localScale.x; 
    }
}
