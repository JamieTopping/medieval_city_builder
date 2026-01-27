using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private GameStateScriptableObject gameSaveTarget;
    [SerializeField] private GameStateScriptableObject autoSaveSlot;
    [SerializeField] private bool loadFromFile;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (loadFromFile)
        {
            if (gameSaveTarget == null) gameSaveTarget = autoSaveSlot;
            GuyManager._instance.LoadGuys(gameSaveTarget.allGuys);
            TileManager._instance.LoadTiles(gameSaveTarget.tiles);
        }
    }

    public void OpenMenu()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void CloseMenu()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SaveFromButton(GameStateScriptableObject saveTarget)
    {
        gameSaveTarget = saveTarget;
        GuyManager._instance.SaveGuys(saveTarget);
        TileManager._instance.SaveTiles(saveTarget);
    }

    public void AutoSave()
    {
        GuyManager._instance.SaveGuys(autoSaveSlot);
        TileManager._instance.SaveTiles(autoSaveSlot);
        Debug.Log("Auto Save");
    }
}
