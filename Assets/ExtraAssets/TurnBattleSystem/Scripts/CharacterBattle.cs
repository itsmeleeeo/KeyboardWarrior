
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class CharacterBattle : MonoBehaviour {

    private Character_Base characterBase;
    private State state;
    private Vector3 slideTargetPosition;
    private Action onSlideComplete;
    public bool isPlayerTeam,isHealer;
    private GameObject selectionCircleGameObject;
    private HealthSystem healthSystem;
    private World_Bar healthBar;
    public int damageAmount; 

    private enum State {
        Idle,
        Sliding,
        Busy,
    }

    private void Awake() {
        damageAmount = HighScoreSing.Instance.GetDmg();
        characterBase = GetComponent<Character_Base>();
        selectionCircleGameObject = transform.Find("SelectionCircle").gameObject;
        HideSelectionCircle();
        state = State.Idle;
    }

   

    public void Setup(bool isPlayerTeam,bool isHealer,bool isTank,bool isEnemy2,bool isEnemy3,bool isBoos) {
        this.isPlayerTeam = isPlayerTeam;
        this.isHealer = isHealer;
        if (isPlayerTeam) {
            characterBase.SetAnimsSwordTwoHandedBack();
            characterBase.GetMaterial().mainTexture = BattleHandler.GetInstance().playerSpritesheet;
            if(isHealer){
                 characterBase.SetAnimsSwordTwoHandedBack();
                characterBase.GetMaterial().mainTexture = BattleHandler.GetInstance().healerSpritesheet;
            }else if(isTank){
                characterBase.SetAnimsSwordTwoHandedBack();
                characterBase.GetMaterial().mainTexture = BattleHandler.GetInstance().tankSpritesheet;
            }
        } else {
            if(isEnemy2){
            characterBase.SetAnimsSwordShield();
            characterBase.GetMaterial().mainTexture = BattleHandler.GetInstance().enemySpritesheet2;
            }else if(isEnemy3){
                characterBase.SetAnimsSwordShield();
            characterBase.GetMaterial().mainTexture = BattleHandler.GetInstance().enemySpritesheet3;
            }else if(isBoos){
            characterBase.SetAnimsSwordShield();
            characterBase.GetMaterial().mainTexture = BattleHandler.GetInstance().boosSpritesheet;
            }else{
            characterBase.SetAnimsSwordShield();
            characterBase.GetMaterial().mainTexture = BattleHandler.GetInstance().enemySpritesheet;
            }
        }
        healthSystem = new HealthSystem(100);
        healthBar = new World_Bar(transform, new Vector3(0, 10), new Vector3(12, 1.7f), Color.grey, Color.red, 1f, 100, new World_Bar.Outline { color = Color.black, size = .6f });
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;

        PlayAnimIdle();
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e) {
        healthBar.SetSize(healthSystem.GetHealthPercent());
    }

    private void PlayAnimIdle() {
        if (isPlayerTeam) {
            characterBase.PlayAnimIdle(new Vector3(+1, 0));
        } else {
            characterBase.PlayAnimIdle(new Vector3(-1, 0));
        }
    }

    private void Update() {
        switch (state) {
        case State.Idle:
            break;
        case State.Busy:
            break;
        case State.Sliding:
            float slideSpeed = 10f;
            transform.position += (slideTargetPosition - GetPosition()) * slideSpeed * Time.deltaTime;

            float reachedDistance = 1f;
            if (Vector3.Distance(GetPosition(), slideTargetPosition) < reachedDistance) {
                onSlideComplete();
            }
            break;
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void Damage(CharacterBattle attacker, int damageAmount) {
        healthSystem.Damage(damageAmount);
       
        Vector3 dirFromAttacker = (GetPosition() - attacker.GetPosition()).normalized;

        DamagePopup.Create(GetPosition(), damageAmount, false);
        characterBase.SetColorTint(new Color(1, 0, 0, 1f));
        Blood_Handler.SpawnBlood(GetPosition(), dirFromAttacker);

        if (healthSystem.IsDead()) {
            // Died
            characterBase.PlayAnimLyingUp();
        }
    }

    public bool IsDead() {
        return healthSystem.IsDead();
    }

    public void Attack(CharacterBattle targetCharacterBattle, Action onAttackComplete) {
        Vector3 slideTargetPosition = targetCharacterBattle.GetPosition() + (GetPosition() - targetCharacterBattle.GetPosition()).normalized * 10f;
        Vector3 startingPosition = GetPosition();
        
        // Slide to Target
        SlideToPosition(slideTargetPosition, () => {
            // Arrived at Target, attack him
            state = State.Busy;
            Vector3 attackDir = (targetCharacterBattle.GetPosition() - GetPosition()).normalized;
            characterBase.PlayAnimAttack(attackDir, () => {
                // Target hit
                if (isPlayerTeam) {
                }else{
                damageAmount = UnityEngine.Random.Range(30,40); 
                }
                targetCharacterBattle.Damage(this, damageAmount);
                }, () => {
                // Attack completed, slide back
                SlideToPosition(startingPosition, () => {
                    // Slide back completed, back to idle
                    state = State.Idle;
                    characterBase.PlayAnimIdle(attackDir);
                    onAttackComplete();
                });
                
            });
        });
    }
    public void SpecialAttackTank(CharacterBattle targetCharacterBattle,CharacterBattle targetCharacterBattle2,CharacterBattle targetCharacterBattle3, Action onAttackComplete) {
        Vector3 slideTargetPosition = targetCharacterBattle.GetPosition() + (GetPosition() - targetCharacterBattle.GetPosition()).normalized * 10f;
        Vector3 startingPosition = GetPosition();
        
        // Slide to Target
        SlideToPosition(slideTargetPosition, () => {
            // Arrived at Target, attack him
            state = State.Busy;
            Vector3 attackDir = (targetCharacterBattle.GetPosition() - GetPosition()).normalized;
            characterBase.PlayAnimAttack(attackDir, () => {

                int tankDamageAmount = 30;
                
                targetCharacterBattle.Damage(this, tankDamageAmount);
                targetCharacterBattle2.Damage(this, tankDamageAmount);
                targetCharacterBattle3.Damage(this, tankDamageAmount);
                }, () => {
                // Attack completed, slide back
                SlideToPosition(startingPosition, () => {
                    // Slide back completed, back to idle
                    state = State.Idle;
                    characterBase.PlayAnimIdle(attackDir);
                    onAttackComplete();
                });
                
            });
        });
    }
     public void SpecialAttackHealer(CharacterBattle targetCharacterBattle, Action onAttackComplete) {
        Vector3 slideTargetPosition = targetCharacterBattle.GetPosition() + (GetPosition() - targetCharacterBattle.GetPosition()).normalized * 10f;
        Vector3 startingPosition = GetPosition();
        int healerDamageAmount = -50;
        targetCharacterBattle.Damage(this, damageAmount);
        state = State.Idle;
        onAttackComplete();        
                
    }
     public void SpecialAttack(CharacterBattle targetCharacterBattle, Action onAttackComplete) {
        Vector3 slideTargetPosition = targetCharacterBattle.GetPosition() + (GetPosition() - targetCharacterBattle.GetPosition()).normalized * 10f;
        Vector3 startingPosition = GetPosition();
        int SpecialdamageAmount = 100;
        // Slide to Target
        SlideToPosition(slideTargetPosition, () => {
            // Arrived at Target, attack him
            state = State.Busy;
            Vector3 attackDir = (targetCharacterBattle.GetPosition() - GetPosition()).normalized;
            characterBase.PlayAnimAttack(attackDir, () => {
              
                targetCharacterBattle.Damage(this, SpecialdamageAmount);
                }, () => {
                // Attack completed, slide back
                SlideToPosition(startingPosition, () => {
                    // Slide back completed, back to idle
                    state = State.Idle;
                    characterBase.PlayAnimIdle(attackDir);
                    onAttackComplete();
                });
                
            });
        });
    }
    private void SlideToPosition(Vector3 slideTargetPosition, Action onSlideComplete) {
        this.slideTargetPosition = slideTargetPosition;
        this.onSlideComplete = onSlideComplete;
        state = State.Sliding;
        if (slideTargetPosition.x > 0) {
            characterBase.PlayAnimSlideRight();
        } else {
            characterBase.PlayAnimSlideLeft();
        }
    }

    public void HideSelectionCircle() {
        selectionCircleGameObject.SetActive(false);
    }

    public void ShowSelectionCircle() { 
        selectionCircleGameObject.SetActive(true);
    }

}
