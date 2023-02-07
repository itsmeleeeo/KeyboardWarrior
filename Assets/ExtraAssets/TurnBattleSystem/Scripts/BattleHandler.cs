

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour {

    private static BattleHandler instance;
   
    public static BattleHandler GetInstance() {
        return instance;
    }


    [SerializeField] private Transform pfCharacterBattle;
    private int level=0;
    public Texture2D playerSpritesheet;
   // private int randomPlayerAtck = Random.Range(0,2);
    public Texture2D healerSpritesheet;
    public Texture2D tankSpritesheet;
    public Texture2D enemySpritesheet;
    public Texture2D enemySpritesheet2;
    public Texture2D enemySpritesheet3;
    public Texture2D boosSpritesheet;
   
    private bool healerUnlocked = true, tankUnlocked = true;
    public static bool healerMoving = false,tankMoving = false,enemys2 = true,enemys3 = true;
    private bool boosLevel = false;
    private CharacterBattle playerCharacterBattle;
    private CharacterBattle healerCharacterBattle;
    private CharacterBattle tankCharacterBattle;
    private CharacterBattle enemyCharacterBattle;
    private CharacterBattle enemyCharacterBattle2;
    private CharacterBattle enemyCharacterBattle3;
    private CharacterBattle boosCharacterBattle;
    private CharacterBattle activeCharacterBattle;
    private State state;
    private List<CharacterBattle> characterBattleList = new List<CharacterBattle>();


    private enum State {
        WaitingForPlayer,
        Busy,
    }

    private void Awake() {
       boosLevel = HighScoreSing.Instance.BoosTime();
        characterBattleList = new List<CharacterBattle>();
        instance = this;
    }

    private void Start() {
        playerCharacterBattle = SpawnCharacter(true,false,false,false,false,false);
        enemyCharacterBattle = SpawnCharacter(false,false,false,false,false,false);
        if(healerUnlocked){
            healerCharacterBattle = SpawnCharacter(true,true,false,false,false,false);
        }if(tankUnlocked){
            tankCharacterBattle = SpawnCharacter(true,false,true,false,false,false);
        }
        if(enemys2){
            enemyCharacterBattle2 = SpawnCharacter(false,false,false,true,false,false);
        }
        if(enemys3){
             enemyCharacterBattle3 = SpawnCharacter(false,false,false,false,true,false);
        }if(boosLevel){
            boosCharacterBattle = SpawnCharacter(false,false,false,false,false,true);
        }
        SetActiveCharacterBattle(playerCharacterBattle);
        state = State.WaitingForPlayer;
    }

    private void Update() {
        if (state == State.WaitingForPlayer) {
            if(healerMoving){
                if(healerCharacterBattle.IsDead()){
                SetActiveCharacterBattle(enemyCharacterBattle2);
                state = State.Busy;
                healerMoving = false;
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                SetActiveCharacterBattle(enemyCharacterBattle2);
                state = State.Busy;
                healerMoving = false;
                if (enemyCharacterBattle2.IsDead() && enemyCharacterBattle.IsDead() && enemyCharacterBattle3.IsDead()){
                    healerCharacterBattle.Attack(boosCharacterBattle, () => {
                    ChooseNextActiveCharacter();
                });}
                else if (enemyCharacterBattle2.IsDead() && enemyCharacterBattle.IsDead()){
                    healerCharacterBattle.Attack(enemyCharacterBattle3, () => {
                    ChooseNextActiveCharacter();
                });
                }else if(enemyCharacterBattle2.IsDead()){
                    healerCharacterBattle.Attack(enemyCharacterBattle, () => {
                    ChooseNextActiveCharacter();
                });
                }
                else{
                healerCharacterBattle.Attack(enemyCharacterBattle2, () => {
                ChooseNextActiveCharacter();
                });
            }
            }
            if (Input.GetKeyDown(KeyCode.R)) {
                state = State.Busy;
                SetActiveCharacterBattle(enemyCharacterBattle2);
                healerCharacterBattle.SpecialAttackHealer(playerCharacterBattle, () => {
                    ChooseNextActiveCharacter();
                });
                healerMoving = false;
            }
            }else if(tankMoving){
                if(tankCharacterBattle.IsDead()){
                     SetActiveCharacterBattle(enemyCharacterBattle3);
                    state = State.Busy;
                    tankMoving = false;
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                SetActiveCharacterBattle(enemyCharacterBattle3);
                state = State.Busy;
                tankMoving = false;
                if (enemyCharacterBattle2.IsDead() && enemyCharacterBattle.IsDead() && enemyCharacterBattle3.IsDead()){
                    tankCharacterBattle.Attack(boosCharacterBattle, () => {
                    ChooseNextActiveCharacter();
                });
                }
                else if (enemyCharacterBattle2.IsDead() && enemyCharacterBattle3.IsDead()){
                    tankCharacterBattle.Attack(enemyCharacterBattle, () => {
                    ChooseNextActiveCharacter();
                });
                }else if (enemyCharacterBattle3.IsDead()){
                    tankCharacterBattle.Attack(enemyCharacterBattle2, () => {
                    ChooseNextActiveCharacter();
                });
                }
                else{
                    tankCharacterBattle.Attack(enemyCharacterBattle3, () => {
                    ChooseNextActiveCharacter();
                });
            }
            }
            if (Input.GetKeyDown(KeyCode.R)) {
                state = State.Busy;
                SetActiveCharacterBattle(enemyCharacterBattle3);
                tankCharacterBattle.SpecialAttackTank(enemyCharacterBattle,enemyCharacterBattle2,enemyCharacterBattle3, () => {
                    ChooseNextActiveCharacter();
                });
                tankMoving = false;
            }
            }else{
                if(playerCharacterBattle.IsDead()){
                    state = State.Busy;
                    ChooseNextActiveCharacter();
                }
            if (Input.GetKeyDown(KeyCode.Space)) {
                state = State.Busy;
                  if (enemyCharacterBattle2.IsDead() && enemyCharacterBattle.IsDead() && enemyCharacterBattle3.IsDead()){
                    playerCharacterBattle.Attack(boosCharacterBattle, () => {
                    ChooseNextActiveCharacter();
                });
                }else if (enemyCharacterBattle2.IsDead() && enemyCharacterBattle.IsDead()){
                playerCharacterBattle.Attack(enemyCharacterBattle3, () => {
                    ChooseNextActiveCharacter();
                });
                }else if(enemyCharacterBattle.IsDead()){
                playerCharacterBattle.Attack(enemyCharacterBattle2, () => {
                ChooseNextActiveCharacter();
                });
                }else{
                  playerCharacterBattle.Attack(enemyCharacterBattle, () => {
                    ChooseNextActiveCharacter();
                }); 
                }
            }
            if (Input.GetKeyDown(KeyCode.R)) {
                state = State.Busy;
                 if (enemyCharacterBattle2.IsDead() && enemyCharacterBattle.IsDead() && enemyCharacterBattle3.IsDead()){
                    playerCharacterBattle.Attack(boosCharacterBattle, () => {
                    ChooseNextActiveCharacter();
                });
                }else if (enemyCharacterBattle2.IsDead() && enemyCharacterBattle.IsDead()){
                playerCharacterBattle.SpecialAttack(enemyCharacterBattle3, () => {
                    ChooseNextActiveCharacter();
                });
                }else if(enemyCharacterBattle.IsDead()){
                playerCharacterBattle.SpecialAttack(enemyCharacterBattle2, () => {
                ChooseNextActiveCharacter();
                });
                }else{
                  playerCharacterBattle.SpecialAttack(enemyCharacterBattle, () => {
                    ChooseNextActiveCharacter();
                }); 
                }
            }
        }
    }
    }
    private CharacterBattle SpawnCharacter(bool isPlayerTeam,bool isHealer,bool isTank,bool isEnemy2,bool isEnemy3,bool isBoos) {
        Vector3 position;
        if (isPlayerTeam) {
            position = new Vector3(-50, 0);
            if(isHealer){
                position = new Vector3(-45, 10);
            }else if(isTank){
                position = new Vector3(-45, -10);
            }
        } else {
            if(isEnemy2){
            position = new Vector3(+45, 10);
            }else if(isEnemy3){
                position = new Vector3(+50, -10);
            }else if(isBoos){
                position = new Vector3(+30, 0);
                }else{
                position = new Vector3(+50, 0);
            }
            
        }
        Transform characterTransform = Instantiate(pfCharacterBattle, position, Quaternion.identity);
        CharacterBattle characterBattle = characterTransform.GetComponent<CharacterBattle>();
        characterBattle.Setup(isPlayerTeam,isHealer,isTank,isEnemy2,isEnemy3,isBoos);
        characterBattleList.Add(characterBattle);
        return characterBattle;
    }

    private void SetActiveCharacterBattle(CharacterBattle characterBattle) {
        if (activeCharacterBattle != null) {
            activeCharacterBattle.HideSelectionCircle();
        }

        activeCharacterBattle = characterBattle;
        activeCharacterBattle.ShowSelectionCircle();
    }

    private void ChooseNextActiveCharacter() {
        if (TestBattleOver()) {
            return;
        }
        
        if (activeCharacterBattle == playerCharacterBattle) {
            if(playerCharacterBattle.IsDead()){
                SetActiveCharacterBattle(enemyCharacterBattle);
                ChooseNextActiveCharacter();
            }else if(enemyCharacterBattle.IsDead()){
                SetActiveCharacterBattle(enemyCharacterBattle);
                state = State.Busy;
                ChooseNextActiveCharacter();
            }else{
            SetActiveCharacterBattle(enemyCharacterBattle);
            state = State.Busy;
           
            enemyCharacterBattle.Attack(playerCharacterBattle, () => {
                ChooseNextActiveCharacter();
            });
            }
         }else if(activeCharacterBattle == enemyCharacterBattle){  
            if(enemyCharacterBattle.IsDead()){
                SetActiveCharacterBattle(healerCharacterBattle);
                state = State.WaitingForPlayer;
                healerMoving = true;
            }else{
            if(healerUnlocked){
                SetActiveCharacterBattle(healerCharacterBattle);
                healerMoving = true;
            }else{
                SetActiveCharacterBattle(playerCharacterBattle);
            }
               
                state = State.WaitingForPlayer;
            }
                
         }
         else if(activeCharacterBattle == enemyCharacterBattle2){
            if(enemyCharacterBattle2.IsDead()){
                SetActiveCharacterBattle(tankCharacterBattle);
                 tankMoving = true;
                 state = State.WaitingForPlayer;
            }else{
                state = State.WaitingForPlayer;
                if(tankUnlocked){
                     tankMoving = true;
                enemyCharacterBattle2.Attack(healerCharacterBattle, () => {
                SetActiveCharacterBattle(tankCharacterBattle);
            });
            }else{
                
                enemyCharacterBattle2.Attack(healerCharacterBattle, () => {
                SetActiveCharacterBattle(playerCharacterBattle);
            });
            }
            }  
         }else if(activeCharacterBattle == enemyCharacterBattle3){
            if(boosLevel){
                 if(enemyCharacterBattle3.IsDead()){
                SetActiveCharacterBattle(boosCharacterBattle);
                state = State.WaitingForPlayer;
            }else{
             state = State.WaitingForPlayer;
                enemyCharacterBattle3.Attack(tankCharacterBattle, () => {
                SetActiveCharacterBattle(boosCharacterBattle);
            });
            }
            }else{
                if(enemyCharacterBattle3.IsDead()){
                SetActiveCharacterBattle(playerCharacterBattle);
                state = State.WaitingForPlayer;
            }else{
                state = State.WaitingForPlayer;
                enemyCharacterBattle3.Attack(tankCharacterBattle, () => {
                SetActiveCharacterBattle(playerCharacterBattle);
            });
            }
            }
           
        }else if(activeCharacterBattle == boosCharacterBattle){
            if(boosCharacterBattle.IsDead()){
                SetActiveCharacterBattle(playerCharacterBattle);
                state = State.WaitingForPlayer;
            }else{
             state = State.WaitingForPlayer;
                boosCharacterBattle.SpecialAttackTank(playerCharacterBattle,healerCharacterBattle,tankCharacterBattle, () => {
                SetActiveCharacterBattle(playerCharacterBattle);
            });
            }
        }      
    }

     


    private bool TestBattleOver() {
        if (healerUnlocked && tankUnlocked == false){
           if(playerCharacterBattle.IsDead() && healerCharacterBattle.IsDead()) {
            BattleOverWindow.Show_Static("Enemy Wins!");
            Invoke("EndBattle", 2f);
            return true;
        }
        }else if(tankUnlocked){
            if(playerCharacterBattle.IsDead() && healerCharacterBattle.IsDead() && tankCharacterBattle.IsDead()) {
           
            BattleOverWindow.Show_Static("Enemy Wins!");
            Invoke("EndBattle", 2f);
            return true;
        }}else{
            if(playerCharacterBattle.IsDead()) {
            BattleOverWindow.Show_Static("Enemy Wins!");
            Invoke("EndBattle", 2f);
            return true;
        }
        }
        if(enemys3 && boosLevel){
            if(enemyCharacterBattle.IsDead() && enemyCharacterBattle2.IsDead() &&  enemyCharacterBattle3.IsDead() &&  boosCharacterBattle.IsDead()) {
            BattleOverWindow.Show_Static("Player Wins!");
            Invoke("EndBattle", 2f);
            return true;
        }
        }
        else if(enemys2 && enemys3 == false){
            if(enemyCharacterBattle.IsDead() && enemyCharacterBattle2.IsDead()) {
            BattleOverWindow.Show_Static("Player Wins!");
            Invoke("EndBattle", 2f);
            return true;
        }
        }else if(enemys3){
            if(enemyCharacterBattle.IsDead() && enemyCharacterBattle2.IsDead() &&  enemyCharacterBattle3.IsDead()) {
            BattleOverWindow.Show_Static("Player Wins!");
            Invoke("EndBattle", 2f);
            return true;
        }
        }else{
            if(enemyCharacterBattle.IsDead()) {
            BattleOverWindow.Show_Static("Player Wins!");
            Invoke("EndBattle", 2f);
            return true;
        }
        }
        return false;
    }
 
}

