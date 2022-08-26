using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
   public void NextLevelManager(int levelCount)
    {
        SceneManager.LoadScene(levelCount);
    }
}
