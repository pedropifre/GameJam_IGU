using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool isActive = false;
    public GameObject checkpoint;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && !isActive)
        {
            gameObject.GetComponent<Animator>().SetBool("CheckPoint",true);
            checkpoint.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            isActive = true;
        }
    }
}
