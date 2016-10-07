using UnityEngine;
using System.Collections;

public class TeamsManager : MonoBehaviour
{
    private int activeTeamIndex = 0;
    private Team _activeTeam = null;

    public Team[] teams;

    public Team activeTeam
    {
        get
        {
            return _activeTeam;
        }
    }

    private void Awake()
    {
        _activeTeam = teams[activeTeamIndex];
    }

    public void CycleActiveTeam()
    {
        activeTeamIndex = (activeTeamIndex + 1) % teams.Length;
        _activeTeam = teams[activeTeamIndex];
    }
}