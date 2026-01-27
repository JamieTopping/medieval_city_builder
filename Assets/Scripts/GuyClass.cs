using System.Collections;
using UnityEngine;

public class GuyClass
{
    public int energy;
    private int  energyMax;
    public TileScript tileLocation;
    public GuyBehaviour myGuy;

    private TaskScriptableObject currentTask;

    public void Birth(int birthEnergy = 2)
    {
        energyMax = birthEnergy;
        energy = energyMax;
    }

    public void Drop(TileScript dropTile, GameObject dropObject)
    {
        myGuy = dropObject.GetComponent<GuyBehaviour>();
        tileLocation = dropTile;
        myGuy.BirthSprite(this);
    }

    public void Pickup()
    {
        myGuy.PickupGuy();
    }
    
    public bool startTask(int energyCost = 1)
    {
        if (energy < energyCost)
        {
            return false;
        }
        else
        {
            energy -= energyCost;
            return true;
        }
    }

    public void Recharge()
    {
        energy = energyMax;
    }

    public IEnumerator PerformTask(TaskScriptableObject task)
    {
        bool canWork = startTask(task.energyCost);
        float workDuration = 0f;
        myGuy.UpdateProgressBar(0f);
        while (canWork && workDuration < task.baseDuration)
        {
            workDuration += Time.deltaTime;
            myGuy.UpdateProgressBar(workDuration / task.baseDuration);
            yield return null;
        }

        switch (task.name)
        {
            case "Rest" :
                completeRest();
                break;
            case "Idle" :
            default :
                break;
        }
    }
    
    // task completion actions

    private void completeRest()
    {
        energy = energyMax;
    }
}
