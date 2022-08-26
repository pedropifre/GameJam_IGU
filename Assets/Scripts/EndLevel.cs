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
            animator.SetTrigger("Fineshed");
            StartCoroutine(NextLevel());
        }
        
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene(level);
    }
}
