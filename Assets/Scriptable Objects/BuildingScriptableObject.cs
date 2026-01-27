using UnityEngine;
// Use the CreateAssetMenu attribute to allow creating instances of this ScriptableObject from the Unity Editor.
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BuildingScriptableObject", order = 1)]
public class BuildingScriptableObject : ScriptableObject
{
    public int initialHealth;
    public int buildTime;
    public float rechargeRate;
    public TaskScriptableObject taskAvailable;
    public GameObject buildingPrefab;
}