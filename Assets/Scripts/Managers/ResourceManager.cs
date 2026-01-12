using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI woodText;

    public int wood;
    public int stone;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        stoneText.text = stone.ToString();
        woodText.text = wood.ToString();
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