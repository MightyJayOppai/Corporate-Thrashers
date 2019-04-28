using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;

    //Unity's inbuilt class to communicate and give information url's

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passwordField.text);
        
        WWW www = new WWW("http://localhost/battle_to_earn/PlayerRegister.php", form);
        yield return www;
        
        if(www.text == "0")
        {
            Debug.Log("User Creation was Successful");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            //If problem, return error or at least description as to what went wrong
            Debug.Log("User Creation has Failed. Error #" + www.text);
        }
    }

    public void VerifyInputs()
    {
        //This is to make it so if the input conditions are not met, the button will not be interactable
        submitButton.interactable = (nameField.text.Length >= 4 && passwordField.text.Length >= 4);
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
