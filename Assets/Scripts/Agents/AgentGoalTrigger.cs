using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class AgentGoalTrigger : MonoBehaviour
{
    [System.Serializable]
    public class AgentReachedGoalEvent : UnityEvent<string> { }
    
    public AgentReachedGoalEvent onReachedGoal;
    private bool _triggered;

    public void TriggerGoal()
    {
        if  (_triggered) return;
        _triggered = true;
        onReachedGoal.Invoke(gameObject.name);
    }
}