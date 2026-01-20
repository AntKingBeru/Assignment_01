using UnityEngine;

public class EggPickupPoint : MonoBehaviour
{
    [SerializeField] private int eggsPerHarvest = 1;
    [SerializeField] private float cooldownSeconds = 60f;

    private bool _playerInside;
    private float _lastHarvestTime = -Mathf.Infinity;

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
        return _playerInside && Time.time >= _lastHarvestTime + cooldownSeconds;
    }

    public void Harvest()
    {
        if (!CanHarvest()) return;

        _lastHarvestTime = Time.time;
        ResourceManager.Instance.AddEggs(eggsPerHarvest);
    }
    
    public float CooldownRemaining()
    {
        return Mathf.Max(0, (_lastHarvestTime + cooldownSeconds) - Time.time);
    }
}