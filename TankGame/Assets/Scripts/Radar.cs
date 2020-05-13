using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : AiTank
{
    [SerializeField]
    float rotSpeed = 50;

    Ray ray;
    RaycastHit hitInfo;
    RaycastHit[] hits;
    LayerMask layerMask = ~1 << 8;

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

        // gets an array off everything the raycast hit
        hits = Physics.RaycastAll(ray, 100f);
        // checks each collider that the raycast hit
        foreach (RaycastHit hit in hits)
        {
            Debug.DrawLine(ray.origin, hit.point, Color.blue);

            if (hit.collider.tag == "Player1")
            {
                player1LastLoc = hit.transform.position;
            }
        }
    }
}
