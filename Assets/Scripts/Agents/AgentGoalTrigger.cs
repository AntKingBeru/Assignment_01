using UnityEngine;
using TMPro;

public class AgentGoalTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI message;

    private string _messageText;
    private bool _shown = false;
    

    private void Start()
    {
        _messageText = transform.name + " has reached the goal!";
    }
    
    public void AddMessage()
    {
        if (_shown) return;

        _shown = true;
        
        message.text += "\n" + _messageText;
        Debug.Log(_messageText);
    }
}