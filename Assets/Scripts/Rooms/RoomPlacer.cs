using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    public static RoomPlacer Instance;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public void PlaceRoom(RoomDefinition roomDef, RoomConnector connector)
    {
        var room = Instantiate(roomDef.roomPrefab);

        var targetPoint = connector.snapPoint;
        var newRoomEntry = FindOppositeConnection(room, targetPoint.forward);

        // Align rotation
        var rotationOffset =
            Quaternion.FromToRotation(newRoomEntry.forward, -targetPoint.forward);

        room.transform.rotation = rotationOffset * room.transform.rotation;

        // Align position
        var offset = targetPoint.position - newRoomEntry.position;
        room.transform.position += offset;
    }
    
    private Transform FindOppositeConnection(GameObject room, Vector3 forward)
    {
        var connections = room.transform.Find("Connections");

        Transform best = null;
        var bestDot = -1f;

        foreach (Transform conn in connections)
        {
            var dot = Vector3.Dot(conn.forward, -forward);
            if (dot > bestDot)
            {
                bestDot = dot;
                best = conn;
            }
        }

        return best;
    }
}