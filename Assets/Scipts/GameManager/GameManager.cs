using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public GameObject maps;
    
    void Update()
    {
        
    }

    void CheckGameOver()
    {
        if (ShortBehaviors.health <= 0 || LongBehaviors.health <= 0)
        {
            gameOverScreen.SetActive(true);
        }
    }

    void Rematch()
    {
        
    }
}
