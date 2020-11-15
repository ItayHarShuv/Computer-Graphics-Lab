
using UnityEngine;

public class Rotator : MonoBehaviour
{
    //[SerializeField] float rotationSpeed = 500f;
    //bool dragging = false;
    //Rigidbody rb;

    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}



    //void OnMouseDrag()
    //{
    //    dragging = true;
    //}

    //void Update()
    //{
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        dragging = false;
    //    }

    //}
    //void FixedUpdate()
    //{

    //    if (dragging)
    //    {
    //        float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime * 10000;
    //        float y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime * 10000;

    //        rb.AddTorque(Vector3.down * x);
    //        rb.AddTorque(Vector3.right * y);
    //    }
    //}

    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            mPosDelta = Input.mousePosition - mPrevPos;
            if(Vector3.Dot(transform.up, Vector3.up) >= 0)
            {
                transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
            }
            else
            {
                transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
            }

            transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up), Space.World);
        }

        mPrevPos = Input.mousePosition;
    }
}
