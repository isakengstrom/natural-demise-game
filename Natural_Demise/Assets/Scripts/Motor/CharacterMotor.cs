using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMotor : BaseMotor
{
    
    protected float startHealth;

    public event Action<float> OnHealthPctChange = delegate { };

    private string _prevState;
    
    private static readonly int PlayerAnimations = Animator.StringToHash("PlayerAnimations");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Die = Animator.StringToHash("Die");
    
    public void UpdateHealth(float healthChange) {
        currentHealth += healthChange;

        OnHealthPctChange(currentHealth / startHealth);

        //HealthBar.fillAmount = CurrentHealth / StartHealth;
        Debug.Log(currentHealth);
    }

    protected override void Construct() {
        currentHealth = startHealth;
    }

    protected override void UpdateMotor() {
        //Do nothing here, inherited by child classes.
    }

    protected override void ChangeAnimation(string stateName) {
        switch (stateName) {
            case "IdleState":
                anim.SetInteger(PlayerAnimations, 1);
                break;
            case "WalkingState":
                anim.SetInteger(PlayerAnimations, 2);
                break;
            case "RunningState":
                anim.SetInteger(PlayerAnimations, 3);
                break;
            case "JumpingState":
                anim.SetTrigger(Jump);
                anim.SetInteger(PlayerAnimations, 4);
                break;
            case "LandingState":
                anim.SetInteger(PlayerAnimations, 5);
                break;
            case "DyingState":
                anim.SetTrigger(Die);
                break;
        }
    }
}
