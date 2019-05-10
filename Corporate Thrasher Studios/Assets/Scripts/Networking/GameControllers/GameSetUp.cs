using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    public static GameSetUp GS;
    public Transform[] spawnPoints;
    
    void OnEnable()
    {
        if(GameSetUp.GS == null)
        {
            GameSetUp.GS = this;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
