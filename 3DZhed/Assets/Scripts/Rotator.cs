using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public static Rotator Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    Vector3 point;
    public char dir;
    [SerializeField] float rotationSpeed = 100f;

    void Start()
    {
        point = transform.position;
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.A) || dir == 'A')
        {
            transform.RotateAround(point, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || dir == 'D')
        {
            transform.RotateAround(point, -Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W) || dir == 'W')
        {
            transform.RotateAround(point, -Vector3.left, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) || dir == 'S')
        {
            transform.RotateAround(point, Vector3.left, rotationSpeed * Time.deltaTime);
        }
    }
}


