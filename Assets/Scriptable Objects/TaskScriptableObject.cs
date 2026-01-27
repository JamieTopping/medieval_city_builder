using UnityEngine;
// Use the CreateAssetMenu attribute to allow creating instances of this ScriptableObject from the Unity Editor.
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TaskScriptableObject", order = 2)]
public class TaskScriptableObject : ScriptableObject
{
    public int energyCost;
    public float baseDuration;
    
}