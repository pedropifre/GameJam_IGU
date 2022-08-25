using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPlayer : MonoBehaviour
{
    private bool facingRight = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && facingRight)
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.D) && !facingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        if(facingRight) gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else gameObject.GetComponent<SpriteRenderer>().flipX = false;
        //Debug.Log(facingRight);
        facingRight = !facingRight;
    }
}
