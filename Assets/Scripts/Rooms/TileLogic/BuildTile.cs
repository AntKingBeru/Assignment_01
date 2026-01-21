using UnityEngine;

public class BuildTile : MonoBehaviour
{
    public bool IsOccupied { get; private set; }

    public void Consume()
    {
        IsOccupied = true;
        gameObject.SetActive(false);
    }
}