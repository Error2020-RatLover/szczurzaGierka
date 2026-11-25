using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "wepponInfoStats", menuName = "Scriptable Objects/wepponInfoStats")]
public class wepponInfoStats : ScriptableObject
{
    public string wepponName;
    public championStats statsModifier;
    public List<attackStats> attackPossible;
    public List<string> effectsOnHit;
    public List<effectData> effects;
}
