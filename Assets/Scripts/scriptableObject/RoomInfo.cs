using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
[CreateAssetMenu(fileName = "RoomInfo", menuName = "Scriptable Objects/RoomInfo")]
public class RoomInfo : ScriptableObject
{
    public int x_size;
    public int y_size;

    public List<Pole> prefabsOverwrite = new List<Pole>();
    public List<enemyPoleData> enemysPositions = new List<enemyPoleData>();
}
