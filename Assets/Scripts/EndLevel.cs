using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public Animator animator;
    public int level;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            var player = collision.GetComponent<Player>();
            player.canMove = false;
            animator.SetTrigger("Fineshed");
            StartCoroutine(NextLevel());
            player.canMove = true;
        }
        
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(level);
        
    }
}
