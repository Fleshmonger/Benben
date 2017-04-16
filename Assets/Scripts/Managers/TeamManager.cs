using UnityEngine;

namespace GameLogic
{
    public sealed class TeamManager : Singleton<TeamManager>
    {
        public Team[] teams;

        private int activeTeamIndex;

        public int teamCount { get { return teams.Length; } }
        public Team activeTeam { get { return teams[activeTeamIndex]; } }

        public void CycleActiveTeam()
        {
            activeTeamIndex = (activeTeamIndex + 1) % teamCount;
        }
    }
}