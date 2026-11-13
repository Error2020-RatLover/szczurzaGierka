using UnityEngine;

[CreateAssetMenu(fileName = "interactiveObjectStats", menuName = "Scriptable Objects/interactiveObjectStats")]
public class interactiveObjectStats : ScriptableObject
{
    public int hp = 0;
    public bool Movable = false;
    public bool Ocupaited = false;
    public bool Attackable = true;
    public bool interactive = false;
}
