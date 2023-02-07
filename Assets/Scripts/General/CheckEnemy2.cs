using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemy2 : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other) {
    if(other.name == "treant-walk-front-1"){
        HighScoreSing.Instance.Level2_1();
    }
    if(other.name == "reant-walk-front-2"){
        HighScoreSing.Instance.Level2_2();
    }
   }
}
