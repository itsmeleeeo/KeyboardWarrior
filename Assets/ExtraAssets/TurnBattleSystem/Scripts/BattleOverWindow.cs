

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleOverWindow : MonoBehaviour {
  
    
    private static BattleOverWindow instance;
    public int levels=0;

    private void Awake() {
       
        instance = this;
        Hide();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show(string winnerString) {
        gameObject.SetActive(true);

        transform.Find("winnerText").GetComponent<Text>().text = winnerString;
        Invoke("BackToScene", 2.5f);
    }

    public static void Show_Static(string winnerString) {
        instance.Show(winnerString);
    }
    public void BackToScene(){
       HighScoreSing.Instance.ChangeLevel();
    }

}
