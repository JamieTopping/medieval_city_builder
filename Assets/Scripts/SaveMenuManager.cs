using UnityEngine;

public class SaveMenuManager : MonoBehaviour
{
    [SerializeField] private bool loadFromFile;
    [SerializeField] private int initialLoadTarget;
    
    void Start()
    {
        if (loadFromFile)
        {
            SaveManager.Load(initialLoadTarget);
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
    
    public void SaveFromButton(int saveTarget)
    {
        SaveManager.Save(saveTarget);
    }
}
