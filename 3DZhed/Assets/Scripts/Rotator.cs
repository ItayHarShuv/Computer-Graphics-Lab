using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    Vector3 point; 
    [SerializeField] float rotationSpeed = 100f;

    void Start()
    {
        point = transform.position;
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(point, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(point, -Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.RotateAround(point, -Vector3.left, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.RotateAround(point, Vector3.left, rotationSpeed * Time.deltaTime);
        }
    }
}


