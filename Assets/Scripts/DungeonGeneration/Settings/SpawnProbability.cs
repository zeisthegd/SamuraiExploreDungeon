using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Rate", menuName = "Dungeon/Generation/SpawnRate")]
public class SpawnProbability : ScriptableObject
{
    public float monsterSpawnChance;
    public float chestSpawnChance;
    public MaterialDropChance materialDropChance;


    [System.Serializable]
    public struct MaterialDropChance
    {
        public float grade1Material;
        public float grade2Material;
        public float grade3Material;
    }

}
