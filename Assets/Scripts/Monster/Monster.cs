using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] MonsterData monsterData;
    [SerializeField] Rigidbody rgBody;
    Vector3 playerPos;
    void Start()
    {

    }


    void Update()
    {

    }
    void FixedUpdate()
    {
        ChasePlayer();

    }
    void ChasePlayer()
    {
        playerPos = FindObjectOfType<Player>().transform.position;
        var dirToPlayer = (playerPos - this.transform.position).normalized;
        rgBody.AddForce(dirToPlayer * 25 * Time.deltaTime, ForceMode.Impulse);
    }

    

}
