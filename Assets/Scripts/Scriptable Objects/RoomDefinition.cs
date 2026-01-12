using UnityEngine;

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
}
