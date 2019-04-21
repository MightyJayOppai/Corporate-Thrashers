using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MainGameManager : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";
    
    void Start()
    {
        Connect();
    }

    
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        
    }
    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
            {
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // #Critical, we must first and foremost connect to Photon Online Server.
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
    }
}
