using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class turnObject : MonoBehaviour
{
    public championStats stats;
    public Vector2Int pozycja = new Vector2Int(0,0);
    public abstract void startYourTurn();
    public abstract void endYourTurn();
}
