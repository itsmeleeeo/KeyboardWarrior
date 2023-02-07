using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionTrigger : MonoBehaviour
{
  
    public GameObject colliderOBJ;
    public Questions questions; 

      private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
        FindObjectOfType<DialogManager>().StartQuestions(questions);  
        Destroy(colliderOBJ);
        }
         
    }
}
