using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AgentGoalTrigger message))
        {
            message.ShowMessage();
        }
    }
}