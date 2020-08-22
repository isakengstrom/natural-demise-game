using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMotor : BaseMotor {
    
    protected float startHealth;
    public event Action<float> OnHealthPctChange = delegate { };

    private string _prevState;
    
    //String to hash to be more efficient at run time
    private static readonly int PlayerAnimations = Animator.StringToHash("PlayerAnimations");
    private static readonly int Jump = Animator.StringToHash("Jump");
    private static readonly int Die = Animator.StringToHash("Die");

    //Update the health of the character
    public void UpdateHealth(float healthChange) {
        currentHealth += healthChange;
        OnHealthPctChange(currentHealth / startHealth);
    }

    protected override void Construct() {
        currentHealth = startHealth;
    }

    protected override void UpdateMotor() {
        //Do nothing here, inherited by child classes.
    }

    //Change (humanoid) animations depending on the state of the character
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
