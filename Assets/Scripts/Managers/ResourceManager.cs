using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public int wood;
    public int stone;

    private void Awake()
    {
        Instance = this;
    }

    public bool CanAfford(RoomDefinition room)
    {
        return wood >= room.woodCost &&
               stone >= room.stoneCost;
    }

    public void Spend(RoomDefinition room)
    {
        wood -= room.woodCost;
        stone -= room.stoneCost;
    }
}