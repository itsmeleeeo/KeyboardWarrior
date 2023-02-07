using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTRigger : MonoBehaviour
{

    public GameObject colliderOBJ;
    public Dialogue dialogue; 

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
         FindObjectOfType<DialogManager>().StartDialogue(dialogue);  
         colliderOBJ.SetActive(false);
        }
    }
   
}
