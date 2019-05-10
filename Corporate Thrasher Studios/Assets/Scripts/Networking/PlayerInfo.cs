using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo PI;
    public int mySelectedCharacter;
    public GameObject[] allCharacters;
    
    void OnEnable()
    {
        if(PlayerInfo.PI == null)
        {
           PlayerInfo.PI = this;
        }
        else
        {
            //Check if the current script does not equal to the current singleton
            if(PlayerInfo.PI != this)
            {
                //Reset the singleton and set the PlayerInfo to the new this
                Destroy(PlayerInfo.PI.gameObject);
                PlayerInfo.PI = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        //Check if playerprefs exist, if it does, set mySelectedCharacter equal to this value
        if(PlayerPrefs.HasKey("MyCharacter"))
        {
            mySelectedCharacter = PlayerPrefs.GetInt("MyCharacter");
        }
        else
        {
            mySelectedCharacter = 0;
            PlayerPrefs.SetInt("MyCharacter", mySelectedCharacter);
        }
    }
}
