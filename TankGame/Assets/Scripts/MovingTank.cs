using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTank : AiTank
{
    // Start is called before the first frame update
    void Start()
    {
        // Setting default values
        this.movementSpeed = 3f;
        player1Transform = GameObject.FindGameObjectWithTag("Player1").transform;
        this.player1LastLoc = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Sight();

        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;
    }

    void Move()
    {
        //this.transform.position = new Vector3.
    }
}
