using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameSetUp : MonoBehaviour
{
    public static GameSetUp GS;
    public Text health;
    public Transform[] spawnPoints;
    
    private void OnEnable()
    {
        if(GameSetUp.GS == null)
        {
            GameSetUp.GS = this;
        }
    }
    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        //Once disconnected from master client, load in to the main menu scene but not to early // while()
        while(PhotonNetwork.IsConnected)
        {
            //Make the game wait until the player is disconnected from the master client
            yield return null;
        }
        SceneManager.LoadScene(0);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
