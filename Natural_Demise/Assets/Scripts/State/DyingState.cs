using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingState : BaseState
{

    public override void Construct() {
        base.Construct();

        //TODO: End game when dying state is done
    }

    public override Vector3 ProcessMotion(Vector3 input) {
        ApplySpeed(ref input, 0f);

        return input;
    }

  
}
