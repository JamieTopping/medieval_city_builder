using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager _instance;
    private Dictionary<ResourceScriptableObject, int> resources;
    private Dictionary<ResourceScriptableObject, TMP_Text> inventoryObjects;

    private void Awake()
    {
        _instance = this;
    }

    public int GetResourceCount(ResourceScriptableObject type)
    {
        if (resources.TryGetValue(type, out int currentCount))
        {
            return currentCount;
        }
        else
        {
            return 0;
        }
    }

    public void AddResource(ResourceScriptableObject type, int count)
    {
        if (resources.ContainsKey(type))
        {
            resources[type] += count;
        }
        else
        {
            // add new resource UI element
            GameObject newUI = Instantiate(type.inventoryPrefab, transform);
            TMP_Text newText = newUI.GetComponentInChildren<TMP_Text>();
            inventoryObjects.Add(type, newText);
            resources[type] = count;
        }
    }

    private void UpdateResourceDisplay(ResourceScriptableObject type)
    {
        if (inventoryObjects.ContainsKey(type))
        {
            inventoryObjects[type].text = type.name + ": "  + GetResourceCount(type);
        }
    }

    public bool CheckCost(Dictionary<ResourceScriptableObject, int> costToCheck)
    {
        foreach (KeyValuePair<ResourceScriptableObject, int> entry in costToCheck)
        {
            if (entry.Value > GetResourceCount(entry.Key)) return false;
        }
        return true;
    }

    public bool PayCost(Dictionary<ResourceScriptableObject, int> costToPay)
    {
        if (CheckCost(costToPay))
        {
            foreach (ResourceScriptableObject entry in costToPay.Keys)
            {
                resources[entry] -= costToPay[entry];
            }

            return true;
        }
        else
        {
            Debug.Log("Not enough resources");
            return false;
        }
    }
}

[System.Serializable]
public struct ResourceManagerData
{
    
}