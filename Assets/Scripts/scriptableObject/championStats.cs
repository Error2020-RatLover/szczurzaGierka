using UnityEngine;

[CreateAssetMenu(fileName = "championStats", menuName = "Scriptable Objects/championStats")]
public class championStats : ScriptableObject
{

    [Header("Statystyki")]
    public int maxHp;
    public int hp;
    public int strenght;
    public int magic;
    public int armor;
    public int magicArmor;
    public int stamina;
    public int speed;


}
