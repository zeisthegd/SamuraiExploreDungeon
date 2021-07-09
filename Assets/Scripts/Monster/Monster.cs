using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] MonsterData monsterData;
    void Start()
    {
        CreateMonster();
    }


    void Update()
    {

    }

    void CreateMonster()
    {
        InstantiateMonsterModel();
    
    }

    void InstantiateMonsterModel()
    {
        var mnsInstance = Instantiate(monsterData.monsterModel, this.transform.position, Quaternion.identity, this.transform);
        mnsInstance.layer = gameObject.layer;
    }

}
