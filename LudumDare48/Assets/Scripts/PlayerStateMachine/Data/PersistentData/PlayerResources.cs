using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct PlayerResources {
    public int oxygen;
    public int gems; // trade in to pay off debt or purchase upgrade
    public int upgrades;
    public int debt;
}
