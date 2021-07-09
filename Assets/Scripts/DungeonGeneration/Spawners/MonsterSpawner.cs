using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    enum MonsterType { Lesser, Greater };
    [SerializeField] MonsterSpawnSettings monsterSpawnSettings;
    int currentWaveCount = 1;
    int currentTotalMonsters = 0;
    List<GameObject> cellObjs;
    List<int> usedCells;

    
    DungeonTheme theme;
    Room room;

    void Start()
    {
        GetSettingsUtil.GetDungeonTheme(ref theme);
        room = GetComponentInParent<Room>();
        cellObjs = room.CellObjs;
        usedCells = new List<int>();
        room.OnPlayerEnterRoom += SpawnMonsterWave;
    }
    void Update()
    {

    }

    public int CurrentWaveCount
    {
        get { return currentWaveCount; }
        set
        {
            if (currentWaveCount != value)
            {
                currentWaveCount = value;
                OnPlayerClearCurrentWave();
            }
        }
    }



    public void OnPlayerClearCurrentWave()
    {
        if (currentWaveCount < monsterSpawnSettings.numberOfWaves)
        {
            SpawnMonsterWave();
        }
        else
        {
            //Room Clear
        }
    }

    public void SpawnMonsterWave()
    {
        SpawnMonsterOfType(MonsterType.Lesser);
        SpawnMonsterOfType(MonsterType.Greater); ///TODO: make a greater pls
    }

    private void SpawnMonsterOfType(MonsterType type)
    {
        if (type == MonsterType.Lesser)
            SpawnLesserMonsters();
        if (type == MonsterType.Greater)
            SpawnGreaterMonsters();
    }

    private void SpawnLesserMonsters()
    {
        foreach (GameObject lesserMonster in theme.LesserMonsters)
        {
            for (int i = 0; i < monsterSpawnSettings.lesserMonsterNumber; i++)
            {
                GameObject spawningCell = GetRandomSpawningCell();
                SpawnMonsterAt(lesserMonster, spawningCell);
            }
        }
    }

    private void SpawnGreaterMonsters()
    {
        for (int i = 0; i < monsterSpawnSettings.greaterMonsterNumber; i++)
        {
            int randomGreater = MathUtility.GetRandomPositiveNumber(theme.GreaterMonsters.Count);
            GameObject greaterMonster = theme.GreaterMonsters[randomGreater];
            GameObject spawningCell = GetRandomSpawningCell();
            SpawnMonsterAt(greaterMonster, spawningCell);
        }
    }

    public GameObject GetRandomSpawningCell()
    {
        int randomCell = MathUtility.GetRandomPositiveNumber(cellObjs.Count);
        if (!usedCells.Contains(randomCell))
        {
            return cellObjs[randomCell];
        }
        return GetRandomSpawningCell();
    }

    public void SpawnMonsterAt(GameObject monster, GameObject cell)
    {
        CreateSpawningVFX();
        Instantiate(monster, cell.transform.position, Quaternion.identity, this.transform);
        currentTotalMonsters++;
    }

    private void CreateSpawningVFX()
    {

    }

}

[System.Serializable]
public struct MonsterSpawnSettings
{
    public int greaterMonsterNumber;//max
    public int lesserMonsterNumber;//Each type
    public int numberOfWaves;

}
