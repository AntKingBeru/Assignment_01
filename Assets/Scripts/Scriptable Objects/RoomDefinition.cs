using UnityEngine;

[CreateAssetMenu(fileName = "RoomDefinition", menuName = "Scriptable Objects/RoomDefinition")]
public class RoomDefinition : ScriptableObject
{
    [Header("Room")]
    public GameObject roomPrefab;
    
    // [Header("UI")]
    // public Sprite icon;

    [Header("Connections")]
    public int maxConnections;

    [Header("Cost")]
    public int woodCost;
    public int stoneCost;
}
