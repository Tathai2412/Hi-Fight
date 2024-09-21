using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public GameObject maps;
    [SerializeField] private LongBehaviors longHair;
    [SerializeField] private ShortBehaviors shortHair;


    private void Awake()
    {
        shortHair.OnDead += CheckGameOver; //When short hair is dead, call game over method
        longHair.OnDead += CheckGameOver;
    }

    void Update()
    {
        
    }

    void CheckGameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    void Rematch()
    {
        
    }
}
