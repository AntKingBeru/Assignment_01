using UnityEngine;
using System.Collections.Generic;

public class QuarryManager : MonoBehaviour
{
    public static QuarryManager Instance;
    
    [SerializeField] private QuarryRules rules;
    
    private List<QuarryInstance> _quarries = new();
    private QuarryInstance _originQuarry;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterFirstQuarry(GameObject quarryGo)
    {
        _originQuarry = quarryGo.GetComponent<QuarryInstance>();
        _originQuarry.depthFromOrigin = 0;
        _quarries.Add(_originQuarry);
    }

    public bool CanPlaceQuarry(RoomConnector connector)
    {
        if (_quarries.Count == 0)
            return false;

        if (_quarries.Count >= rules.maxQuarries)
            return false;

        var parentQuarry = connector.GetComponentInParent<QuarryInstance>();
        if (parentQuarry == null)
            return false;
        
        var newDepth = parentQuarry.depthFromOrigin + 1;
        return newDepth <= rules.maxDistanceFromOrigin;
    }

    public void RegisterNewQuarry(GameObject quarryGo, RoomConnector connector)
    {
        var parent = connector.GetComponentInParent<QuarryInstance>();
        
        var instance = quarryGo.AddComponent<QuarryInstance>();
        instance.depthFromOrigin = parent.depthFromOrigin + 1;
        
        _quarries.Add(instance);
    }
}