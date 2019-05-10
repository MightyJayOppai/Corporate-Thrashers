using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void OnClickCharacterPick(int whichCharacter)
    {
        //Check to make sure a player info singleton exists
        if(PlayerInfo.PI != null)
        {
            //set the mySelectedCharacter variable
            PlayerInfo.PI.mySelectedCharacter = whichCharacter;
            PlayerPrefs.SetInt("MyCharacter", whichCharacter);
        }
    }
}
