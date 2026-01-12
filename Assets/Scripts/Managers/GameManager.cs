using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject originQuarry;

    private void Start()
    {
        QuarryManager.Instance.RegisterFirstQuarry(originQuarry);
    }
}