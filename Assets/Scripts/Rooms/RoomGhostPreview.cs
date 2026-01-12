using UnityEngine;

public class RoomGhostPreview : MonoBehaviour
{
    [SerializeField] private Material validMaterial;
    [SerializeField] private Material invalidMaterial;
    [SerializeField] private LayerMask blockingMask;

    private Vector3 _boundsSize;
    private GameObject _currentGhost;
    private Transform _ghostAttachPoint;
    private Renderer[] _renderers;

    private bool _isValid;
    
    public bool IsValid => _isValid;

    public bool CheckPlacementValid(RoomDefinition room, RoomConnector connector)
    {
        if (room == null || connector == null)
        {
            UpdateValid(false);
            return false;
        }
        
        var blocked = IsBlocked(connector);
        var affordable = ResourceManager.Instance.CanAfford(room);
        
        var valid = !blocked && affordable;

        if (valid && room.roomType == RoomType.Quarry)
        {
            if (!QuarryManager.Instance.CanPlaceQuarry(connector))
            {
                valid = false;
            }
        }
        
        UpdateValid(valid);
        
        return valid;
    }

    public void Show(RoomDefinition room, RoomConnector connector)
    {
        Clear();

        if (room == null || room.ghostPrefab == null) return;

        _currentGhost = Instantiate(room.ghostPrefab);
        
        foreach (var col in _currentGhost.GetComponentsInChildren<Collider>())
            col.enabled = false;
        
        _ghostAttachPoint = FindAttachPoint(_currentGhost);
        
        if (_ghostAttachPoint == null)
        {
            Debug.LogError($"Ghost prefab '{room.name}' has no AttachPoint!");
            Clear();
            return;
        }
        
        _renderers = _currentGhost.GetComponentsInChildren<Renderer>();

        AlignGhost(connector);
        
        _boundsSize = CalculateBounds(_currentGhost);
    }

    public void Clear()
    {
        if (_currentGhost != null) Destroy(_currentGhost);
    }
    
    public void UpdateValid(bool valid)
    {
        _isValid = valid;
        var material = valid ? validMaterial : invalidMaterial;
        
        foreach (var render in _renderers) render.material = material;
    }

    public static void AlignRoomToConnector(Transform root, Transform attachPoint, RoomConnector targetConnector)
    {
        var rotation = Quaternion.FromToRotation(attachPoint.forward, targetConnector.snapPoint.forward);
        
        root.rotation = rotation * root.rotation;
        
        var offset = attachPoint.position - root.position;
        root.position = targetConnector.snapPoint.position - offset;
    }
    
    private Transform FindAttachPoint(GameObject ghost)
    {
        return ghost.transform.Find("AttachPoint");
    }

    private void AlignGhost(RoomConnector connector)
    {
        AlignRoomToConnector(_currentGhost.transform, _ghostAttachPoint, connector);
    }

    private Vector3 CalculateBounds(GameObject ghost)
    {
        var renderers = ghost.GetComponentsInChildren<Renderer>();

        var bounds = renderers[0].bounds;
        foreach (var render in renderers) bounds.Encapsulate(render.bounds);
        
        return bounds.size;
    }

    private bool IsBlocked(RoomConnector sourceConnector)
    {
        var hits = Physics.OverlapBox(_currentGhost.transform.position, _boundsSize * 0.5f,
            _currentGhost.transform.rotation, blockingMask, QueryTriggerInteraction.Ignore);

        foreach (var hit in hits)
        {
            Debug.Log(
                $"[BUILD BLOCKED] Hit: {hit.name} | Layer: {LayerMask.LayerToName(hit.gameObject.layer)} | Root: {hit.transform.root.name}"
            );
            
            if (hit.transform.root == sourceConnector.transform.root) continue;

            return true;
        }

        return false;
    }
}