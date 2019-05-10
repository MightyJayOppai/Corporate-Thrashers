using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
    #region Public Variables
    public InputField showUserField;
    // public InputField showScoreField;
    public UserNameObj user;
    [SerializeField]
    private bool scoreUploaded;
    #endregion

    #region Private Variables
    private string addScoreURL = "http://localhost/battle_to_earn/AddScore.php"; //External Database
    #endregion

    #region Unity Callbacks
    void Start()
    {
        showUserField.text = user.userName;
        // showScoreField.text = user.playerScore;
        scoreUploaded = false;
    }
    #endregion

    #region My Functions
    // public void UploadScore()
    // {
    //     if (!scoreUploaded)
    //     {
    //         //ScoreStats(showUserField.text, showScoreField.text);
    //         StartCoroutine(ScoreStats(showUserField.text, showScoreField.text));
    //         scoreUploaded = true;
    //     }
    // }

    /*public void ScoreStats(string playerName, string playerScore)
    {
        WWWForm scoreForm = new WWWForm();
        //Debug.LogWarning("WWWForm Created"); //Testing

        scoreForm.AddField("playerUsername", playerName);
        scoreForm.AddField("userScore", playerScore);
        //Debug.LogWarning("UserScore Field Added"); //Testing

        WWW dbLink = new WWW(addScoreURL, scoreForm);
        Debug.LogWarning("Score Uploaded to Database");
    }*/

    private IEnumerator ScoreStats(string username, string playerScore)
    {
        WWWForm scoreForm = new WWWForm();
        Debug.LogWarning("WWWForm Created"); //Testing

        scoreForm.AddField("playerUsername", username);
        scoreForm.AddField("userScore", playerScore);
        Debug.LogWarning("UserScore Field Added"); //Testing

        WWW dbLink = new WWW(addScoreURL, scoreForm);
        yield return dbLink;
        /*if (dbLink.text == "Working")
            Debug.LogWarning("Score Uploaded to Database");
        else if (dbLink.text == "Error")
            Debug.LogWarning("Score Upload Failed");*/
    }
    #endregion

}
