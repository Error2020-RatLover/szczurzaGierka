using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class effectData
{
    public enum effectTypes
    {
        Poison
    }

    public effectTypes effectName;
    public float effectChance = 1f;
    public int effectTime = 1;
    public bool effectOnSelf = false;

    public enum activationTypes
    {
        Start,
        End,
        Move,
        Attack,
        Interact,
        DmgTaken
    }

    public List<activationTypes> activationType;

    public effectData(effectData source)
    {
        this.effectName = source.effectName;
        this.effectChance = source.effectChance;
        this.effectTime = source.effectTime;
        this.effectOnSelf = source.effectOnSelf;

        if (source.activationType != null)
        {
            this.activationType = new List<activationTypes>(source.activationType);
        }
    }

}