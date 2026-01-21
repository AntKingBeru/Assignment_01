using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class HarvestPoint : MonoBehaviour, IHarvestable
{
    [SerializeField] private Collider interactionCollider;
    [SerializeField] protected float cooldownSeconds = 30f;

    protected bool PlayerInside;
    protected float LastHarvestTime = -Mathf.Infinity;

    protected virtual void Awake()
    {
        if (interactionCollider == null) return;
        
        interactionCollider.isTrigger = true;
        
        PlayerInside = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        PlayerInside = true;
        BuildUIController.Instance.ActivateInteract(true);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        PlayerInside = false;
        BuildUIController.Instance.ActivateInteract(false);
    }

    protected virtual void OnDisable()
    {
        if (PlayerInside)
        {
            BuildUIController.Instance.ActivateInteract(false);
            PlayerInside = false;
        }
    }

    public bool CanHarvest()
    {
        return PlayerInside && Time.time >= LastHarvestTime + cooldownSeconds;
    }

    public void Harvest()
    {
        if (!CanHarvest()) return;
        
        LastHarvestTime = Time.time;
        PerformHarvest();
    }
    
    protected abstract void PerformHarvest();
    
    public float CooldownRemaining()
    {
        return Mathf.Max(0, (LastHarvestTime + cooldownSeconds) - Time.time);
    }
}