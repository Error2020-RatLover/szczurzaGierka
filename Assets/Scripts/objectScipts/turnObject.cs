using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class turnObject : MonoBehaviour
{
    public championStats stats;
    public Vector2Int pozycja = new Vector2Int(0,0);
    public List<wepponInfoStats> weppons = new List<wepponInfoStats>();
    public wepponInfoStats currentWeppon;
    public List<attackStats> abilities = new List<attackStats>();
    public abstract void startYourTurn();
    public abstract void endYourTurn();
    public roomGenerator generator;
    public attackStats currenAbility;

    public List<effectData> onStartEffects = new();
    public List<effectData> onEndEffects = new();
    public List<effectData> onMoveEffects = new();
    public List<effectData> onAttackEffects = new();
    public List<effectData> onInteractEffects = new();
    public List<effectData> onDmgTakentEffects = new();



    public void giveEffect(effectData efekt)
    {
        effectData newEffect = new(efekt);
        if(newEffect.activationType == effecData.activationTypes.Start)
        {
            onStartEffects.Add(newEffect);
        }
    }
}
