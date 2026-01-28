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

    #region Save and Load
    public void Load(GuyManagerData data)
    {
        allGuys = new List<GuyClass>();
        allGuys.AddRange(data.allGuys);
        storedGuys = new List<GuyClass>();
        storedGuys.AddRange(allGuys);
    }

    public void Save(ref GuyManagerData data)
    {
        data.allGuys = new List<GuyClass>();
        data.allGuys.AddRange(allGuys);
        if (data.allGuys == allGuys) Debug.Log("Guys saved successfully");
    }
    #endregion

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

[System.Serializable]
public struct GuyManagerData
{
    public List<GuyClass> allGuys;
}
