using System.Collections.Generic;
using UnityEngine;
// Use the CreateAssetMenu attribute to allow creating instances of this ScriptableObject from the Unity Editor.
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameStateScriptableObject", order = 4)]
public class GameStateScriptableObject : ScriptableObject
{
    public int saveSlot;
    public List<TileData> tiles;
    public List<GuyClass> allGuys;
    // resources
    // date/seasonal progress
}