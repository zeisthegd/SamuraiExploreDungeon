using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    Vector3 offset;
    Player player;
    void Start()
    {
        player = (Player)FindObjectOfType(typeof(Player));
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        this.transform.position = player.transform.position + offset;
    }
}
