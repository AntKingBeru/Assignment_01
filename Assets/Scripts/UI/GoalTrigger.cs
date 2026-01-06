using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Agent"))
        {
           other.GetComponent<AgentGoalTrigger>().AddMessage();
        }
    }
}