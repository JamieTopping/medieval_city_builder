using System;
using System.Collections.Generic;
using UnityEngine;

public class GuyManager : MonoBehaviour
{
    public static GuyManager _instance;
    private List<GuyClass> allGuys;
    private List<GuyClass> storedGuys;
    [SerializeField] private GameObject guyPrefab;

    private void Awake()
    {
        _instance = this;
        allGuys = new List<GuyClass>();
    }

    public void LoadGuys(List<GuyClass> guysToLoad)
    {
        allGuys = new List<GuyClass>();
        allGuys.AddRange(guysToLoad);
        storedGuys = new List<GuyClass>();
        storedGuys.AddRange(allGuys);
    }

    public void SaveGuys(GameStateScriptableObject saveLocation)
    {
        saveLocation.allGuys = new List<GuyClass>();
        saveLocation.allGuys.AddRange(allGuys);
        if (saveLocation.allGuys == allGuys) Debug.Log("Guys saved successfully");
    }

    public void PlaceGuy()
    {
        if (storedGuys.Count > 0 && TileScript.selected != null)
        {
            GameObject newGuySprite = Instantiate(guyPrefab, TileScript.selected.transform);
            storedGuys[0].Drop(TileScript.selected, newGuySprite);
            StartCoroutine(storedGuys[0].PerformTask(TileScript.selected.tileTask));
            storedGuys.RemoveAt(0);
        }
    }

    public void ManagePickUp(GuyClass guyToPickUp)
    {
        guyToPickUp.Pickup();
        storedGuys.Add(guyToPickUp);
    }
}
