using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public Animator animator;


    public KeyCode keyTotrigger = KeyCode.A;
    public KeyCode keyToExit = KeyCode.S;
    public string triggerToPlay = "Fly";


    public void OnValidate()
    {
        if (animator==null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(keyTotrigger))
        {
            animator.SetBool(triggerToPlay,!animator.GetBool(triggerToPlay));
        }
        
    }
}
