using UnityEngine;

public class RoomConnector : MonoBehaviour
{
    [SerializeField] public Transform clickPoint;
    private bool _playerInRange;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        _playerInRange = true;
        BuildUIController.Instance.Show(this);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _playerInRange = false;
        BuildUIController.Instance.Hide();
    }
    
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}