using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : BaseHealth {

    protected override void Construct() {
        Health = 10.0f;
    }

    protected override void Kill() {
        Destroy(this);
    }
}
