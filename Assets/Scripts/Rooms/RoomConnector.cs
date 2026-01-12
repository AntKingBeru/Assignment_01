using UnityEngine;

public class RoomConnector : MonoBehaviour
{
    [SerializeField] public Transform snapPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        BuildUIController.Instance.Show(this);
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        BuildUIController.Instance.Hide();
    }
    
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}