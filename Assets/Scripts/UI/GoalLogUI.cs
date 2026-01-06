using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalLogUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goalLog;
    private HashSet<string> _loggedAgents = new HashSet<string>();

    public void AddAgent(string agent)
    {
        if (!_loggedAgents.Add(agent)) return;
        goalLog.text += $"\n- {agent}";
    }
}