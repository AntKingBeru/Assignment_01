using UnityEngine;

public enum RoomType
{
    Normal,
    Quarry
}


[CreateAssetMenu(fileName = "RoomDefinition", menuName = "Scriptable Objects/RoomDefinition")]
public class RoomDefinition : ScriptableObject
{
    [Header("Room")]
    public GameObject roomPrefab;
    public GameObject ghostPrefab;
    
    // [Header("UI")]
    // public Sprite icon;

    [Header("Connections")]
    public Transform[] attachPoints;

    [Header("Cost")]
    public int woodCost;
    public int stoneCost;

    [Header("Quarry Rules")]
    public int maxQuarries = 27;
    public int maxDistanceFromOrigin = 3;
    
    public RoomType roomType;
}