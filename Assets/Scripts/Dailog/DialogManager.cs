using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogManager : MonoBehaviour
{   
   
    
    public GameObject treelvl2_1,treelvl2_2;
     public GameObject treelvl3_1,treelvl3_2;
    private int questionTotal,questionsRight;
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;
    private Queue<string> sentences;
    private List<string> buttonTextQuestions;
    public GameObject dialogueUI,questionUI,player;
    public GameObject buttonQ1,buttonQ2,buttonQ3,buttonQ4;
    public TMP_Text textDialogueUI,nameDialogueUI;
    public TMP_Text textQuestionUI,nameQuestionUI;
    public TMP_Text[] buttonsQuestions;
    public Text playerScore;
    
 
    // Start is called before the first frame update
    void Start()
    {
        
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        playerMovement = player.GetComponent<PlayerMovement>();
        sentences = new Queue<string>();
        buttonTextQuestions = new List<string>();

        questionsRight = HighScoreSing.Instance.GetTotalPoints();
    }

    public void StartQuestions(Questions question){
        questionUI.SetActive(true);
        nameQuestionUI.text = question.name;
        
        Time.timeScale = 0f;

       textQuestionUI.text = question.questions;

       foreach(string option in question.answers){
        buttonTextQuestions.Add(option);
       }
       for(int i=0;i<buttonTextQuestions.Count;i++){
        buttonsQuestions[i].text = buttonTextQuestions[i];
       }
       buttonTextQuestions.Clear();
    }
   public void StartDialogue(Dialogue dialogue){
    dialogueUI.SetActive(true);
    nameDialogueUI.text = dialogue.name;
    Time.timeScale = 0f;
    sentences.Clear();

    foreach (string sentence in dialogue.sentences){
        sentences.Enqueue(sentence);
    }
    

    DisplayNextSentence();
   }
   public void DisplayNextSentence(){
    if(sentences.Count == 0){
        EndDialogue();
        return;
    }
    
    string sentence = sentences.Dequeue();
    textDialogueUI.text = sentence;
   }

  public void EndDialogue(){
    dialogueUI.SetActive(false);
    Time.timeScale = 1f;
    
   }
   public void RightAnswer(){
    Time.timeScale = 1f;
    HighScoreSing.Instance.QuestionRight();
    HighScoreSing.Instance.SaveDmgUp();
    textQuestionUI.text = "You got it right! Get ready for combat!";
    buttonQ1.SetActive(false);
    buttonQ2.SetActive(false);
    buttonQ3.SetActive(false);
    buttonQ4.SetActive(false);
    Invoke("ChangeScene", 2f);
    
   }
  public void WrongAnswer(){
    Time.timeScale = 1f;
    HighScoreSing.Instance.QuestionWrong();
    textQuestionUI.text = "You got it Wrong! Don`t let it get in your head Get ready for combat!";
    buttonQ1.SetActive(false);
    buttonQ2.SetActive(false);
    buttonQ3.SetActive(false);
    buttonQ4.SetActive(false);
    Invoke("ChangeScene", 2.5f);
   
   }
   public void ChangeScene(){
    questionUI.SetActive(false);
    
    SceneManager.LoadScene("GameScene_TurnBattleSystem");
   
   }
   
}
