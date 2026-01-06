using UnityEngine;
using TMPro;

public class AgentGoalTrigger : MonoBehaviour
{
    public GameObject messagePrefab;
    public Vector3 offset = new Vector3(0, 2f, 0);

    private GameObject _instance;
    private string _messageText;
    private bool _shown;
    

    private void Start()
    {
        _messageText = transform.name + " has reached the goal!";
    }
    
    public void ShowMessage()
    {
        if (_shown) return;

        _shown = true;
        
        _instance = Instantiate(messagePrefab, transform.position + offset, Quaternion.identity, transform);
        
        var text = _instance.GetComponentInChildren<TextMeshProUGUI>();
        text.text = _messageText;
        
        _instance.SetActive(true);
    }
}