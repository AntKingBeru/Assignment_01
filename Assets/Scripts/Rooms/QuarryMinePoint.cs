using UnityEngine;

public class QuarryMinePoint : MonoBehaviour
{
    [SerializeField] private int stonePerHarvest = 5;
    [SerializeField] private float cooldownSeconds = 30f;

    private bool _playerInside;
    private float _lastHarvestTime = -Mathf.Infinity;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        _playerInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        _playerInside = false;
    }

    public bool CanHarvest()
    {
        return _playerInside && Time.time >= _lastHarvestTime + cooldownSeconds;
    }

    public void Harvest()
    {
        if (!CanHarvest()) return;

        _lastHarvestTime = Time.time;
        ResourceManager.Instance.AddStone(stonePerHarvest);
    }
    
    public float CooldownRemaining()
    {
        return Mathf.Max(0, (_lastHarvestTime + cooldownSeconds) - Time.time);
    }
}