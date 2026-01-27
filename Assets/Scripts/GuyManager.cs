using System;
using System.Collections.Generic;
using UnityEngine;

public class GuyManager : MonoBehaviour
{
    private List<GuyClass> allGuys;
    private List<GuyClass> storedGuys;
    [SerializeField] private GameObject guyPrefab;

    private void Start()
    {
        allGuys = new List<GuyClass>();
        for (int iGuy = 0; iGuy < 4; iGuy++)
        {
            allGuys.Add(new GuyClass());
        }
        storedGuys = new List<GuyClass>();
        storedGuys.AddRange(allGuys);
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
