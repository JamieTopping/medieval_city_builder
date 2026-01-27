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
}
