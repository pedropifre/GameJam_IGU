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
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
