using UnityEngine;
using System.Collections;

public class TeamManager : MonoBehaviour
{
    private int activeTeamIndex;
    private Team _activeTeam;

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