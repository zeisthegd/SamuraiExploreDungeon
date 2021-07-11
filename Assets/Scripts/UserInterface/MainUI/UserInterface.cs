using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    protected Player player;
    public virtual void Start()
    {
        FindPlayer();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        FindPlayer();
    }

    public void FindPlayer()
    {
        if(player == null)
            player = FindObjectOfType<Player>();
    }
}
