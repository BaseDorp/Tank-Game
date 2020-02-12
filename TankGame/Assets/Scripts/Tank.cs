using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    protected Transform tankTransform;
    protected Rigidbody tankRB;

    [SerializeField]
    protected float movementSpeed;
    [SerializeField]
    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        tankTransform = GetComponent<Transform>();
        tankRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
