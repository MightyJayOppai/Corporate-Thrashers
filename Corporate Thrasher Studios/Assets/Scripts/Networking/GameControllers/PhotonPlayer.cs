using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GameSetUp.GS.spawnPoints.Length);

        if(PV.IsMine)
        {
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "P1"),
             GameSetUp.GS.spawnPoints[spawnPicker].position, GameSetUp.GS.spawnPoints[spawnPicker].rotation, 0);
        }
    }

    void Update()
    {
        
    }
}
