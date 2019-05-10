using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passwordField;

    public Button submitButton;
    [Header("Login Field")]
    public InputField inputUser;
    public InputField inputPassword;
    [Header("Create Field")]
    public UserNameObj username;
    public ScoreObj score;
    

    private string regUserURL = "http://localhost/battle_to_earn/RegisterUser.php";
    private string loginUserURL = "http://localhost/battle_to_earn/LoginUser.php";
    //Unity's inbuilt class to communicate and give information url's

    public void CallRegister()
    {
        StartCoroutine(Register(nameField.text, passwordField.text));
    }
    public void CallLogin()
    {
        StartCoroutine(Login(nameField.text, passwordField.text));
    }

    
    public IEnumerator Register(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        
        WWW www = new WWW(regUserURL, form);
        yield return www;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Debug.Log("Registration Successful");

        
        //Debug.Log(www.text);

    }

    public void Login()
    {
        if (inputUser.text != "" || inputPassword.text != "")
        {
            StartCoroutine(Login(inputUser.text, inputPassword.text));
            Debug.LogWarning("Started DBLogin");

            username.userName = inputUser.text;
            Debug.LogWarning("Username Stored");
        }
    }
    public IEnumerator Login(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);
        
        WWW www = new WWW(loginUserURL, form);
        yield return www;
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        Debug.Log("Login Successful");
        //Debug.Log(www.text);
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
