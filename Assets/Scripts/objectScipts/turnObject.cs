using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class turnObject : MonoBehaviour
{
    public championStats stats;

    public abstract void startYourTurn();
    public abstract void endYourTurn();
}
