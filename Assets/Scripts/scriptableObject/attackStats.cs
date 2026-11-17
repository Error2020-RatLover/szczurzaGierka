using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "attackStats", menuName = "Scriptable Objects/attackStats")]
public class attackStats : ScriptableObject
{
    public string attackName;

    public enum Type
    {
        hit,
        bullet,
        beam
    }

    public Type attackType;

    public enum DmgType
    {
        phisical,
        magical,
        prue
    }

    public DmgType damageType;

    public float dmg;
    public int range;
    public List<Vector2Int> area;
}