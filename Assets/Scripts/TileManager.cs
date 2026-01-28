using System;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager _instance;
    private Dictionary<int[], TileScript> tiles;
    [SerializeField] private List<GameObject> tileObjects;
    [SerializeField] private float xMultiplier;
    [SerializeField] private float yMultiplier;
    [SerializeField] private GameObject defaultTilePrefab;

    private void Awake()
    {
        _instance = this;
        tiles = new Dictionary<int[], TileScript>();
    }
    
    #region Save and Load
    public void Load(TileManagerData data)
    {
        tiles = new Dictionary<int[], TileScript>();
        foreach (TileData iTile in data.tiles)
        {
            GameObject newTile = Instantiate(defaultTilePrefab, GetTilePosition(iTile.gridCoord), Quaternion.identity);
            tileObjects.Add(newTile);
            tiles.Add(iTile.gridCoord, newTile.GetComponent<TileScript>());
            tiles[iTile.gridCoord].OnPlaceTile(iTile);
        }
    }
    public void Save(ref TileManagerData data)
    {
        List<TileData> tilesToSave = new List<TileData>();
        foreach (TileScript iTile in tiles.Values)
        {
            tilesToSave.Add(iTile.GetStorableTileData());
        }
        data.tiles = tilesToSave;
        if (data.tiles == tilesToSave) Debug.Log("Tiles saved successfully");
    }
    #endregion

    public TileScript GetTile(int[] checkCoord)
    {
        // ReSharper disable once CanSimplifyDictionaryLookupWithTryGetValue
        return tiles.ContainsKey(checkCoord) ? tiles[checkCoord] : null;
    }

    public void PlaceTile(int[] emptyCoord, GameObject tilePrefab = null)
    {
        if (tiles.ContainsKey(emptyCoord))
        {
            Debug.LogError("tile already exists");
        }
        else
        {
            if (tilePrefab == null)
            {
                tilePrefab = defaultTilePrefab;
            }
            GameObject newTileObj = Instantiate(tilePrefab, GetTilePosition(emptyCoord), Quaternion.identity);
            tiles.Add(emptyCoord, newTileObj.GetComponent<TileScript>());
        }
    }

    private Vector3 GetTilePosition(int[] abstractCoord)
    {
        float x = abstractCoord[0];
        float y = abstractCoord[1];
        y = y * 2 * yMultiplier;
        if (x % 2 == 0)
        {
            y += yMultiplier;
        }
        x = x * xMultiplier;
        // matches coordinate to isometric position
        Vector3 isoPos = new Vector3(x, y, 0);
        return isoPos;
    }

    public void OnBuild(BuildingScriptableObject building)
    {
        if (building != null && TileScript.selected != null)
        {
            TileScript.selected.buildBuilding(building);
        }
    }

    [SerializeField] private int emptyX;
    [SerializeField] private int emptyY;
    public void CreateNextTile()
    {
        PlaceTile(new int[2]{emptyX, emptyY});
    }

    public void GenerateFullBoard()
    {
        
    }
}

[System.Serializable]
public struct TileManagerData
{
    public List<TileData> tiles;
}
