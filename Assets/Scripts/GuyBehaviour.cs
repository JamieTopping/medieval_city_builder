using Unity.VisualScripting;
using UnityEngine;

public class GuyBehaviour : MonoBehaviour
{
    /*
     * attached to character sprites to manage behavior independently of game logic
     * will contain wander and working animations
     */
    
    [SerializeField] private GameObject progressBar;
    private GuyClass myGuyClass;

    public void PickupGuy()
    {
        Destroy(gameObject);
    }
    
    public void UpdateProgressBar(float fractionProgress)
    {
        progressBar.transform.localScale = new Vector3(fractionProgress, 1, 1);
    }

    public void BirthSprite(GuyClass newClassLink)
    {
        myGuyClass = newClassLink;
    }
}
