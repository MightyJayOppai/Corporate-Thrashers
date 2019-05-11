using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;

public class PhotonRoom : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static PhotonRoom room;
    private PhotonView PV;
    public int currentScene;
    public int multiScene;
    //public bool isGameLoaded;
    
    private void Awake()
    {
        //Set up singleton
        if(PhotonRoom.room == null)
        {
            PhotonRoom.room = this;
        }
        else
        {
            //If it equals to another instance of the script and not this one
            if(PhotonRoom.room != this)
            {
                Destroy(PhotonRoom.room.gameObject);
                PhotonRoom.room = this;
            }
        }
        //This gameobject persists into the next scene
        DontDestroyOnLoad(this.gameObject);
        PV = GetComponent<PhotonView>();
    }
    void Start()
    {
        PV = GetComponent<PhotonView>();
    }

    public override void OnEnable()
    {
        //Subscribe to the functions
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are now in a room");

        if(!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        else
        {
            StartGame();
        }
    }

    void StartGame()
    {
        Debug.Log("Loading Level");
        PhotonNetwork.LoadLevel(multiScene);        
    }
    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //Call when multiplayer scene is loaded
        currentScene = scene.buildIndex;
        if(currentScene == multiScene)
        {        
            RPC_CreatePlayer();        
        }
    }

    private void RPC_CreatePlayer()
    {
        //Creates player network controller but not the player character
        //First parameter is the file path to the prefab, Path.Combine
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
    }
    
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " has left the game");
    }
    void Update()
    {
        
    }
}
