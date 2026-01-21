using UnityEngine;

public class QuarryMinePoint : HarvestPoint
{
    [SerializeField] private int stonePerHarvest = 5;

    protected override void PerformHarvest()
    {
        ResourceManager.Instance.AddStone(stonePerHarvest);
    }
}