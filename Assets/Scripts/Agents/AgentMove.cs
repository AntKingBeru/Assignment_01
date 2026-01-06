using UnityEngine;
using UnityEngine.AI;

public class AgentMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float mudCost = 3;
    [SerializeField] private float roadCost = 1;
    [SerializeField] private float waterCost = 2;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetAreaCost();
        agent.SetDestination(target.position);
    }
    
    private void SetAreaCost()
    {
        NavMesh.SetAreaCost(NavMesh.GetAreaFromName("Mud"), mudCost);
        NavMesh.SetAreaCost(NavMesh.GetAreaFromName("Road"), roadCost);
        NavMesh.SetAreaCost(NavMesh.GetAreaFromName("Water"), waterCost);
    }
}