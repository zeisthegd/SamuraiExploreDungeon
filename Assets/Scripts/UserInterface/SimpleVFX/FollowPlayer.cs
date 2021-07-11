using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector3 offSet;
    Player player;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    [ContextMenu("Follow Player")]
    void Follow()
    {
        if (player == null)
            player = FindObjectOfType<Player>();
        else this.transform.position = player.transform.position + offSet;
    }
}
