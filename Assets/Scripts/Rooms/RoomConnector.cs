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
        
        if (BuildController.Instance.IsPlacing) BuildController.Instance.CancelPlacement();

        BuildUIController.Instance.Hide();
    }
    
    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Consume()
    {
        gameObject.SetActive(false);
    }
}