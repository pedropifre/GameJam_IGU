using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public GameObject endGame;
    [Header("Setup")]
    public SOPlayer soPlayerSetup;

   
    public void Update() 
    {
        if(soPlayerSetup.enemiesKilled == 3)
        {
            CallEndGame();  
        }
    }

    public void CallEndGame()
    {
        endGame.SetActive(true);
    }
}
