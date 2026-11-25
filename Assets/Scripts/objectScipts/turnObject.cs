using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class turnObject : MonoBehaviour
{
    public championStats stats;
    public Vector2Int pozycja = new Vector2Int(0, 0);
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
        foreach (effectData.activationTypes type in newEffect.activationType)
        {
            switch (type)
            {
                case effectData.activationTypes.Start:
                    onStartEffects.Add(newEffect);
                    break;
                case effectData.activationTypes.End:
                    onEndEffects.Add(newEffect);
                    break;
                case effectData.activationTypes.Move:
                    onMoveEffects.Add(newEffect);
                    break;
                case effectData.activationTypes.Attack:
                    onAttackEffects.Add(newEffect);
                    break;
                case effectData.activationTypes.Interact:
                    onInteractEffects.Add(newEffect);
                    break;
                case effectData.activationTypes.DmgTaken:
                    onDmgTakentEffects.Add(newEffect);
                    break;
            }
        }
    }
    public void TriggerStartEffects() => TriggerEffects(onStartEffects);
    public void TriggerEndEffects() => TriggerEffects(onEndEffects);
    public void TriggerMoveEffects() => TriggerEffects(onMoveEffects);
    public void TriggerAttackEffects() => TriggerEffects(onAttackEffects);
    public void TriggerInteractEffects() => TriggerEffects(onInteractEffects);
    public void TriggerDmgTakenEffects() => TriggerEffects(onDmgTakentEffects);

    private void TriggerEffects(List<effectData> effectList)
    {

        for (int i = effectList.Count - 1; i >= 0; i--)
        {
            effectData currentEffect = effectList[i];


            if (currentEffect.effectTime <= 0)
            {
                RemoveEffectFromAllLists(currentEffect);
                continue;
            }


            SendMessage(currentEffect.effectName.ToString(), SendMessageOptions.DontRequireReceiver);


            currentEffect.effectTime--;


            if (currentEffect.effectTime <= 0)
            {
                RemoveEffectFromAllLists(currentEffect);
            }
        }
    }

    private void RemoveEffectFromAllLists(effectData effectToRemove)
    {
        onStartEffects.Remove(effectToRemove);
        onEndEffects.Remove(effectToRemove);
        onMoveEffects.Remove(effectToRemove);
        onAttackEffects.Remove(effectToRemove);
        onInteractEffects.Remove(effectToRemove);
        onDmgTakentEffects.Remove(effectToRemove);
    }

    public void Poison()
    {
        Debug.Log("TRUCIZNA EEEEEE");
        stats.hp -= 1;


    }
}

