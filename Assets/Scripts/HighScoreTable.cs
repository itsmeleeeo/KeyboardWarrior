using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoreTable : MonoBehaviour
{
    public Transform entryContainer;
    public Transform entryTemplate;

    
    void Start() {
       // StartCoroutine(GetRequest("http://localhost/sqlconnect/GetScores.php"));
    }
    private void Awake(){
        entryContainer = transform.Find("highScoreEntryContainer");
        entryTemplate = entryContainer.Find("highScoreEntryTemplate");

        //entryTemplate.gameObject.SetActive(false);

        StartCoroutine(GetRequest("http://localhost/sqlconnect/GetScores.php"));
    }

    void Update() {
        
    }

    IEnumerator GetRequest(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch(webRequest.result) {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.Log(pages[page] + " : HTTP Error: " + webRequest.error);
                    break;
                
                case UnityWebRequest.Result.Success:
                    string rawResponse = webRequest.downloadHandler.text;
                    string[] users = rawResponse.Split('*');

                    for(int i = 0; i < users.Length; i++) {
                        if(users[i] != "") {
                            Transform entryTransform = Instantiate(entryTemplate,entryContainer);
                            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
                            entryRectTransform.anchoredPosition = new Vector2(0, -20f * i);
                            entryTransform.gameObject.SetActive(true);
                            string[] userInfo = users[i].Split(',');
                            Debug.Log("Username: " + userInfo[0] + " Score: " + userInfo[1]);
                        }
                    }

                    break;
            }
        }
    }
}
