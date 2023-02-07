using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoos : MonoBehaviour
{  
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            HighScoreSing.Instance.activateBoos();
        }
    }
}
