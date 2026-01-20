using UnityEngine;
using TMPro;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;
    
    [SerializeField] private TextMeshProUGUI stoneText;
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI eggsText;

    public int Wood { get; private set; }
    public int Stone {get; private set; }
    
    public int Eggs {get; private set; }
    
    public event Action OnResourcesChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Wood = 10;
        Stone = 10;

        Instance = this;
        stoneText.text = Stone.ToString();
        woodText.text = Wood.ToString();
        eggsText.text = Eggs.ToString();
    }

    public bool CanAfford(RoomDefinition room)
    {
        return Wood >= room.woodCost &&
               Stone >= room.stoneCost &&
               Eggs >= room.eggsCost;
    }

    public void Spend(RoomDefinition room)
    {
        Wood -= room.woodCost;
        Stone -= room.stoneCost;
        Eggs -= room.eggsCost;
        
        OnResourcesChanged?.Invoke();
    }

    public void AddStone(int amount)
    {
        Stone += amount;
        OnResourcesChanged?.Invoke();
    }
    
    public void AddWood(int amount)
    {
        Wood += amount;
        OnResourcesChanged?.Invoke();
    }

    public void AddEggs(int amount)
    {
        Eggs += amount;
        OnResourcesChanged?.Invoke();
    }

    private void OnEnable()
    {
        OnResourcesChanged += Refresh;
    }

    private void Disable()
    {
        OnResourcesChanged -= Refresh;
    }

    private void Refresh()
    {
        stoneText.text = Stone.ToString();
        woodText.text = Wood.ToString();
    }
}