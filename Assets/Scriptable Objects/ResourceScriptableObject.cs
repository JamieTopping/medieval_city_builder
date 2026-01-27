using UnityEngine;
// Use the CreateAssetMenu attribute to allow creating instances of this ScriptableObject from the Unity Editor.
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ResourceScriptableObject", order = 3)]
public class ResourceScriptableObject : ScriptableObject
{
    public string resourceName;
    public int coinsValue;
    public Sprite resourceIcon;
}