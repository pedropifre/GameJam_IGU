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
    
    
    
    IEnumerator StartShoot()
    {
        canShoot = false;
        Shoot();
        yield return new WaitForSeconds(timeBetweenShot);
        
        canShoot = true;
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canShoot && collision.transform.tag == "Player")
        {
            StartCoroutine(StartShoot());
            
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
