using UnityEngine;

public class TreeMinePoint : HarvestPoint
{
    [SerializeField] private int woodPerHarvest = 5;

    protected override void PerformHarvest()
    {
        ResourceManager.Instance.AddWood(woodPerHarvest);
    }
}