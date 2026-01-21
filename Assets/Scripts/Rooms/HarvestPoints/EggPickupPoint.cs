using UnityEngine;

public class EggPickupPoint : HarvestPoint
{
    [SerializeField] private int eggsPerHarvest = 1;
    
    protected override void PerformHarvest()
    {
        ResourceManager.Instance.AddEggs(eggsPerHarvest);
        Destroy(gameObject);
    }
}