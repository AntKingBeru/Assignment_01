using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent _agent;
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(target.position);
    }
}