using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
   [SerializeField] private string authenticationEndpoint = "http://localhost:13756/account";
   [SerializeField] private TMP_InputField _userName;
   [SerializeField] private TMP_InputField _password;
   [FormerlySerializedAs("_errorMessage")] [SerializeField] private TMP_Text _alertMessage;
   [SerializeField] private Button loginButton;


   public void PrintInputFields()
   {
      loginButton.interactable = false;
      if (!(_userName.text.Length <= 0) || !(_password.text.Length <= 0))
      {
         loginButton.interactable = false;
         _alertMessage.color = Color.green;
         _alertMessage.text = "Signing In.....";
         _alertMessage.gameObject.SetActive(false);
         StartCoroutine(TryLogin());
      }
      else
      {
         _alertMessage.color = Color.red;
         _alertMessage.text = "Please enter your Username and Password";
         _alertMessage.gameObject.SetActive(true);
         loginButton.interactable = true;
      }

      
      
   }

   private IEnumerator TryLogin()
   {
      string username = _userName.text;
      string password = _password.text;
      
      WWWForm loginForm = new WWWForm();
      
      loginForm.AddField("username", username);
      loginForm.AddField("password", password);
      
      UnityWebRequest request = UnityWebRequest.Post(authenticationEndpoint,loginForm);
      var handler = request.SendWebRequest();
      Debug.Log($"{username} : {password}");
      print(loginForm.data.Length);
      float startTime = 0.0f;
      while (!handler.isDone)
      {
         startTime += Time.deltaTime;
         if (startTime > 10.0f)
         {
            break;
         }

         yield return null;
      }

      if (request.result == UnityWebRequest.Result.Success)
      {
         if (request.downloadHandler.text != "Invalid Credentials")
         {
            _alertMessage.color = Color.green;
            _alertMessage.gameObject.SetActive(true);
            loginButton.interactable = true;
            GameAccount account = JsonUtility.FromJson<GameAccount>(request.downloadHandler.text);
            _alertMessage.text = $"Success: {account.username} ID: {account._id}";
         }
         else
         {
            _alertMessage.color = Color.red;
            _alertMessage.text = "Invalid Credentials";
            loginButton.interactable = true;
            _alertMessage.gameObject.SetActive(true);
         }
      }
      else
      {
         _alertMessage.color = Color.red;
         _alertMessage.text = "Unable to Connect to Server";
         loginButton.interactable = true;
      }

      yield return null;
   }
}
