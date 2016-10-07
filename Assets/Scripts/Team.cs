using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "NewTeam", menuName = "Entity/Team", order = 1)]
public class Team : ScriptableObject
{
    public string player;
    public Material material;
}
