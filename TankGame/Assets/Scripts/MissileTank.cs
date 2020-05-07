using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTank : AiTank
{
    // Start is called before the first frame update
    void Start()
    {
        player1Transform = GameObject.FindGameObjectWithTag("Player1").transform;
        player1LastLoc = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (bullets <= 0)
        {
            StartCoroutine(Reload());
        }
        else
        {
            Aim();
            Sight();
        }

        // Time since last bullet fired
        this.elapsedTime += Time.deltaTime;
    }
}
