using UnityEngine;

[CreateAssetMenu(fileName = "interactiveObjectStats", menuName = "Scriptable Objects/interactiveObjectStats")]
public class interactiveObjectStats : ScriptableObject
{
    public int hp = 0;
    public bool Move = false;
    public bool Occupated = false;
    public bool Attack = true;
    public bool Interact = false;
}
