

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class BattleHandler : MonoBehaviour {

//     private static BattleHandler instance;

//     public static BattleHandler GetInstance() {
//         return instance;
//     }


//     [SerializeField] private Transform pfCharacterBattle;
//     public Texture2D playerSpritesheet;
//    // private int randomPlayerAtck = Random.Range(0,2);
//     public Texture2D healerSpritesheet;
//     public Texture2D tankSpritesheet;
//     public Texture2D enemySpritesheet;
//     public static bool healerUnlocked = true, tankUnlocked;
//     public static bool healerMoving,tankMoving;
//     private int checkNextAtack = 0;
//     private CharacterBattle playerCharacterBattle;
//     private CharacterBattle waitingTime;
//     private CharacterBattle healerCharacterBattle;
//     private CharacterBattle tankCharacterBattle;
//     private CharacterBattle enemyCharacterBattle;
//     private CharacterBattle activeCharacterBattle;
//     private State state;

//     private enum State {
//         WaitingForPlayer,
//         Busy,
//     }

//     private void Awake() {
//         instance = this;
//     }

//     private void Start() {
//         if(healerUnlocked){
//         healerCharacterBattle =SpawnCharacter(true,true);
//         }
//         playerCharacterBattle = SpawnCharacter(true,false);
//         enemyCharacterBattle = SpawnCharacter(false,false);

//         SetActiveCharacterBattle(playerCharacterBattle);
//         state = State.WaitingForPlayer;
//     }

//     private void Update() {
//         if (state == State.WaitingForPlayer) {
//             if(healerMoving == true){
//                 if (Input.GetKeyDown(KeyCode.Space)) {
//                 state = State.Busy;

//                 healerCharacterBattle.Attack(enemyCharacterBattle, () => {
//                     ChooseNextActiveCharacter();
//                 });
//                healerMoving = false;
//             }
//                 if (Input.GetKeyDown(KeyCode.R)) {
//                 state = State.Busy;

//                 healerCharacterBattle.SpecialAttack(enemyCharacterBattle, () => {
//                     ChooseNextActiveCharacter();
//                 });
//                 healerMoving = false;
//             }

//             }else{
//             if (Input.GetKeyDown(KeyCode.Space)) {
//                 state = State.Busy;
//                 playerCharacterBattle.Attack(enemyCharacterBattle, () => {
//                     ChooseNextActiveCharacter();
//                 });
//             }
//             if (Input.GetKeyDown(KeyCode.R)) {
//                 state = State.Busy;
//                 playerCharacterBattle.SpecialAttack(enemyCharacterBattle, () => {
//                     ChooseNextActiveCharacter();
//                 });
//             }
//         }
//     }
//     }

//     private CharacterBattle SpawnCharacter(bool isPlayerTeam,bool isHealer) {
//         Vector3 position;
//         if (isPlayerTeam) {
//             position = new Vector3(-50, 0);
//             if(isHealer){
//                 position = new Vector3(-45, 10);
//             }
//         } else {
//             position = new Vector3(+50, 0);
//         }
//         Transform characterTransform = Instantiate(pfCharacterBattle, position, Quaternion.identity);
//         CharacterBattle characterBattle = characterTransform.GetComponent<CharacterBattle>();
//         characterBattle.Setup(isPlayerTeam,isHealer);

//         return characterBattle;
//     }

//     private void SetActiveCharacterBattle(CharacterBattle characterBattle) {
//         if (activeCharacterBattle != null) {
//             activeCharacterBattle.HideSelectionCircle();
//         }

//         activeCharacterBattle = characterBattle;
//         activeCharacterBattle.ShowSelectionCircle();
//     }

//     private void ChooseNextActiveCharacter() {
//         if (TestBattleOver()) {
//             return;
//         }

//         if (activeCharacterBattle == playerCharacterBattle) {
//             SetActiveCharacterBattle(enemyCharacterBattle);
//             state = State.Busy;

//             Debug.Log("player atack");
//             enemyCharacterBattle.Attack(playerCharacterBattle, () => {
//                 ChooseNextActiveCharacter();
//             });
//         }
//         // if(activeCharacterBattle == healerCharacterBattle){
//         //    SetActiveCharacterBattle(playerCharacterBattle);
//         //     state = State.Busy;
//         //      Debug.Log("healer turn");
//         //      enemyCharacterBattle.Attack(healerCharacterBattle, () => {
//         //      ChooseNextActiveCharacter();
//         //      });
//         // }
//         if(healerCharacterBattle == activeCharacterBattle){
//             SetActiveCharacterBattle(enemyCharacterBattle);
//             Debug.Log("healer turn");
//             healerMoving = true;

//             state = State.Busy;
//             // enemyCharacterBattle.Attack(healerCharacterBattle, () => {
//             //     ChooseNextActiveCharacter();
//             // });
//             ChooseNextActiveCharacter();
//         }
//         else
//         // if(activeCharacterBattle == enemyCharacterBattle)
//         {
//             if(checkNextAtack == 0){
//             Debug.Log("enemy atack");
//                SetActiveCharacterBattle(healerCharacterBattle);
//                 state = State.WaitingForPlayer;
//                 checkNextAtack = 1;
//                 Debug.Log("go to healer");

//             }else{
//                 SetActiveCharacterBattle(playerCharacterBattle);
//                 state = State.WaitingForPlayer;
//                 checkNextAtack = 0;
//                  healerMoving = false;
//                 Debug.Log("go to player");
//             }
//             //    healerMoving = true;

//             //    ChooseNextActiveCharacter();
//         }

//      }


//     private bool TestBattleOver() {
//         if (playerCharacterBattle.IsDead()) {
//             // Player dead, enemy wins
//             //CodeMonkey.CMDebug.TextPopupMouse("Enemy Wins!");
//             BattleOverWindow.Show_Static("Enemy Wins!");
//             return true;
//         }
//         if (enemyCharacterBattle.IsDead()) {
//             // Enemy dead, player wins
//             //CodeMonkey.CMDebug.TextPopupMouse("Player Wins!");
//             BattleOverWindow.Show_Static("Player Wins!");
//             return true;
//         }

//         return false;
//     }
// }
