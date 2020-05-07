using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : AiTank
{
    [SerializeField]
    float rotSpeed = 50;

    Ray ray;
    RaycastHit hitInfo;
    LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate this object 
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime, Space.Self);

        ray = new Ray(this.transform.position, this.transform.forward);
        

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.blue);
            
            if (hitInfo.collider.tag == "Player1")
            {
                player1LastLoc = hitInfo.transform.position;
            }
        }
    }
}
