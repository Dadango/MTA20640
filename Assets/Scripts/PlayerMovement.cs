using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public Coroutine driving;
    public bool driving;
    Vector3 moveForward = new Vector3(1.0f, 0.0f);
    Vector3 moveLeft = new Vector3(0.0f, 1.0f);
    Vector3 moveRight = new Vector3(0.0f, -1.0f);

    Vector3 currentPos = new Vector3();
    Vector3 destinationForward = new Vector3();
    Vector3 destinationLeft = new Vector3();
    Vector3 destinationRight = new Vector3();

    public float smoothTime;
    Vector3 velocity = Vector3.zero;



    IEnumerator Move()
    {
        if (Input.GetKeyDown("right"))
        {
            currentPos = transform.position;
            destinationForward = currentPos + moveForward;
            while (true)
            {
                //Debug.Log("Total magnitude is " + Mathf.Abs(transform.position.magnitude - destinationForward.magnitude));
                yield return new WaitForEndOfFrame();
                transform.position = Vector3.SmoothDamp(transform.position, destinationForward, ref velocity, smoothTime);
                if (transform.eulerAngles.z != 0)
                {
                    transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }
                if (Mathf.Abs(transform.position.magnitude - destinationForward.magnitude) < 0.02f)
                {
                    transform.position = destinationForward;
                    break;
                }
            }

        }
        else if (Input.GetKeyDown("up"))
        {
            currentPos = transform.position;
            destinationLeft = currentPos + moveLeft;
            if (transform.eulerAngles.z != 90.0f)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
            }
            while (true)
            {
                yield return new WaitForEndOfFrame();
                transform.position = Vector3.SmoothDamp(transform.position, destinationLeft, ref velocity, smoothTime);
                if (Mathf.Abs(transform.position.magnitude - destinationLeft.magnitude) < 0.02f)
                {
                    transform.position = destinationLeft;
                    break;
                }
            }
        }
        else if (Input.GetKeyDown("down"))
        {
            currentPos = transform.position;
            destinationRight = currentPos + moveRight;
            if (transform.eulerAngles.z != -90.0f)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
            }
            while (true)
            {
                yield return new WaitForEndOfFrame();
                transform.position = Vector3.SmoothDamp(transform.position, destinationRight, ref velocity, smoothTime);
                if (Mathf.Abs(transform.position.magnitude - destinationRight.magnitude) < 0.02f)
                {
                    transform.position = destinationRight;
                    break;
                }
            }

        }
        //driving = null;
        yield return null;
    }

    IEnumerator DriveRight() {
        driving = true;
        currentPos = transform.position;
        destinationForward = currentPos + moveForward;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.SmoothDamp(transform.position, destinationForward, ref velocity, smoothTime);
            if (transform.eulerAngles.z != 0)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            }
            if (Mathf.Abs(transform.position.magnitude - destinationForward.magnitude) < 0.02f)
            {
                transform.position = destinationForward;
                break;
            }
        }
        //print("I have moved the car to the right");
        driving = false;
        yield return null;
    }

    IEnumerator DriveUp() {
        driving = true;
        currentPos = transform.position;
        destinationLeft = currentPos + moveLeft;
        if (transform.eulerAngles.z != 90.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
        }
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.SmoothDamp(transform.position, destinationLeft, ref velocity, smoothTime);
            if (Mathf.Abs(transform.position.magnitude - destinationLeft.magnitude) < 0.02f)
            {
                transform.position = destinationLeft;
                break;
            }
        }
        driving = false;
        yield return null;
    }

    IEnumerator DriveDown() {
        driving = true;
        currentPos = transform.position;
        destinationRight = currentPos + moveRight;
        if (transform.eulerAngles.z != -90.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
        }
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.SmoothDamp(transform.position, destinationRight, ref velocity, smoothTime);
            if (Mathf.Abs(transform.position.magnitude - destinationRight.magnitude) < 0.02f)
            {
                transform.position = destinationRight;
                break;
            }
        }
        driving = false;
        yield return null;
    }


    // Update is called once per frame
    /*void Update()
    {
        if ((Input.GetKeyDown("down") || Input.GetKeyDown("up") || Input.GetKeyDown("right")) && driving == null)
        {
            StartCoroutine(Move());
        }

}*/
}