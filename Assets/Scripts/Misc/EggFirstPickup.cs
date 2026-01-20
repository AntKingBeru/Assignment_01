using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EggFirstPickup : MonoBehaviour
{
    public static EggFirstPickup Instance;
    
    private readonly List<GameObject> _eggsToDestroy = new();

    private const int EggsAmount = 1;
    private bool _playerInside;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
    }

    private void Start()
    {
        _eggsToDestroy.Clear();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        _playerInside = true;
        BuildUIController.Instance.ActivateInteract(true);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        _playerInside = false;
        BuildUIController.Instance.ActivateInteract(false);
    }
    
    public bool CanHarvest()
    {
        return _playerInside;
    }
    
    public void Harvest()
    {
        if (!CanHarvest()) return;
        
        BuildUIController.Instance.ActivateEggs();
        ResourceManager.Instance.AddEggs(EggsAmount);
        DestroyEggs();
    }

    private void ListEgg(GameObject egg)
    {
        _eggsToDestroy.Add(egg);
    }
    
    private void DestroyEggs()
    {
        foreach (var egg in _eggsToDestroy.Where(egg => egg != gameObject))
        {
            Destroy(egg);
        }

        Destroy(gameObject);
    }

    public void RegisterEggs()
    {
        var eggs = GameObject.FindGameObjectsWithTag("Egg");
        
        foreach (var egg in eggs)
        {
            ListEgg(egg);
        }
    }
}