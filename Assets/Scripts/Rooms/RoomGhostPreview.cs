using UnityEngine;

public class RoomGhostPreview : MonoBehaviour
{
    private GameObject _currentGhost;
    private Transform _ghostAttachPoint;

    public void Show(RoomDefinition room, RoomConnector connector)
    {
        Clear();

        if (room == null || room.ghostPrefab == null) return;

        _currentGhost = Instantiate(room.ghostPrefab);
        
        _ghostAttachPoint = FindAttachPoint(_currentGhost);
        if (_ghostAttachPoint == null)
        {
            Debug.LogError($"Ghost prefab '{room.name}' has no AttachPoint!");
            Clear();
            return;
        }

        AlignGhost(connector);
    }

    public void Clear()
    {
        if (_currentGhost != null) Destroy(_currentGhost);
    }
    
    private Transform FindAttachPoint(GameObject ghost)
    {
        return ghost.transform.Find("AttachPoint");
    }

    private void AlignGhost(RoomConnector connector)
    {
        var snap = connector.snapPoint;

        if (snap == null)
        {
            Debug.LogError("RoomConnector has no snapPoint!");
            return;
        }

        var rotationDelta = Quaternion.FromToRotation(_ghostAttachPoint.forward, snap.forward);

        _currentGhost.transform.rotation = rotationDelta * _currentGhost.transform.rotation;
        
        var offset = snap.position - _ghostAttachPoint.position;

        _currentGhost.transform.position += offset;
    }
}