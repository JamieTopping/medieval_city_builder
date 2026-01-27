using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public static TileScript selected;
    public enum _connectionTypes
    {
        none,
        road,
        river
    }
    public BuildingScriptableObject buildings;
    public TaskScriptableObject tileTask;

    private TileScript[] adjacentTiles; // 0: NE (+1, +0) | 1: SE (+1, -1) | 2: SW (-1, -1) | 3: NW (-1, 0)
    [SerializeField] private int[] gridCoord;
    public bool isRising{get; private set;}
    public bool isLowering{get; private set;}
    [SerializeField] private float maxRaise;
    private Vector3 defaultPosition;
    [SerializeField] private float riseStep;

    private void Awake()
    {
        defaultPosition = transform.position;
    }

    public void OnPlaceTile(int[] myCoord)
    {
        gridCoord = myCoord;
        adjacentTiles[0] = TileManager._instance.GetTile(new int[]{gridCoord[0] +1, gridCoord[1]});
        adjacentTiles[1] = TileManager._instance.GetTile(new int[]{gridCoord[0] +1, gridCoord[1] -1});
        adjacentTiles[2] = TileManager._instance.GetTile(new int[]{gridCoord[0] -1, gridCoord[1] -1});
        adjacentTiles[3] = TileManager._instance.GetTile(new int[]{gridCoord[0] -1, gridCoord[1]});
    }

    public void OnClick()
    {
        if (selected != this)
        {
            if (selected != null) StartCoroutine(selected.Lower());
            selected = this;
            StartCoroutine(Raise());
        }
    }

    public IEnumerator Raise()
    {
        isLowering = false;
        isRising = true;
        while (isRising && transform.position.y < maxRaise + defaultPosition.y)
        {
            transform.position += new Vector3(0, riseStep * Time.deltaTime, 0);
            yield return null;
        }
        isRising = false;
    }

    public IEnumerator Lower()
    {
        isRising = false;
        isLowering = true;
        while (isLowering && transform.position.y > defaultPosition.y)
        {
            transform.position -= new Vector3(0, riseStep * Time.deltaTime, 0);
            yield return null;
        }
        if (isLowering)
        {
            transform.position = defaultPosition;
        }
        isLowering = false;
    }
    
    public void buildBuilding(BuildingScriptableObject newBuilding)
    {
        if (newBuilding != null)
        {
            Debug.Log("Building " + newBuilding.name + " already built");
            return;
        }
        Instantiate(newBuilding.buildingPrefab, transform);
        tileTask = newBuilding.taskAvailable;
    }
}
