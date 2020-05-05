using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Sprite car;
    Vector3 startPos;
    Quaternion startRot;
    public bool driving;
    public bool carStalled;

    public float smoothTime;
    Vector3 velocity = Vector3.zero;

    IEnumerator Move()
    {
        driving = true;
        if (Input.GetKeyDown("right"))
        {
            Vector3 currentPos = transform.position;
            Vector3 destinationForward = currentPos + new Vector3(1.0f, 0.0f);
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
        }
        else if (Input.GetKeyDown("left"))
        {
            Vector3 currentPos = transform.position;
            Vector3 destinationBackward = currentPos + new Vector3(-1.0f, 0.0f);
            while (true)
            {
                yield return new WaitForEndOfFrame();
                transform.position = Vector3.SmoothDamp(transform.position, destinationBackward, ref velocity, smoothTime);
                if (transform.eulerAngles.z != 180.0f)
                {
                    transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
                }
                if (Mathf.Abs(transform.position.magnitude - destinationBackward.magnitude) < 0.02f)
                {
                    transform.position = destinationBackward;
                    break;
                }
            }
        }
        else if (Input.GetKeyDown("up"))
        {
            Vector3 currentPos = transform.position;
            Vector3 destinationLeft = currentPos + new Vector3(0.0f, 1.0f);
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
            Vector3 currentPos = transform.position;
            Vector3 destinationRight = currentPos + new Vector3(0.0f, -1.0f);
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
        driving = false;
        yield return null;
    }

    IEnumerator DriveRight()
    {
        driving = true;
        carStalled = true;
        Vector3 currentPos = transform.position;
        Vector3 destinationForward = currentPos + new Vector3(1.0f, 0.0f);
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
        driving = false;
        //print("Finished driving: right" + Time.time.ToString());
        yield return null;
    }

    IEnumerator DriveLeft()
    {
        driving = true;
        carStalled = true;
        Vector3 currentPos = transform.position;
        Vector3 destinationBackward = currentPos + new Vector3(-1.0f, 0.0f);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.SmoothDamp(transform.position, destinationBackward, ref velocity, smoothTime);
            if (transform.eulerAngles.z != 180.0f)
            {
                transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f);
            }
            if (Mathf.Abs(transform.position.magnitude - destinationBackward.magnitude) < 0.02f)
            {
                transform.position = destinationBackward;
                break;
            }
        }
        driving = false;
        //print("Finished driving: left" + Time.time.ToString());
        yield return null;
    }

    IEnumerator DriveUp()
    {
        driving = true;
        carStalled = true;
        Vector3 currentPos = transform.position;
        Vector3 destinationLeft = currentPos + new Vector3(0.0f, 1.0f);
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
        //print("Finished driving: up" + Time.time.ToString());
        yield return null;
    }

    IEnumerator DriveDown()
    {
        driving = true;
        carStalled = true;
        Vector3 currentPos = transform.position;
        Vector3 destinationRight = currentPos + new Vector3(0.0f, -1.0f);
        if (transform.eulerAngles.z != -90.0f)
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
        }
        while (true)
        {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.SmoothDamp(transform.position, destinationRight, ref velocity, smoothTime);
            if (Mathf.Abs(transform.position.magnitude - destinationRight.magnitude) < 0.01f)
            {
                transform.position = destinationRight;
                break;
            }
        }
        driving = false;
        //print("Finished driving: down" + Time.time.ToString());
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Building") || collision.CompareTag("RoadBlock"))
        {
            StopAllCoroutines();
            Debug.Log("Car hit a building");
            Logger.writeString("Car hit a building");
            Animator anim = gameObject.GetComponent<Animator>();
            anim.enabled = true;  //this is pure laziness to avoid the state machine
            AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
            Invoke("CarDedLul", clips[0].length); //call the function after the animation ends
        }
        else if (collision.CompareTag("Finish"))
        {
            collision.gameObject.GetComponent<levelWin>().BroadcastMessage("Save");
        }
    }

    public void CarDedLul()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.enabled = false; //this is pure laziness to avoid the state machine
        gameObject.GetComponent<SpriteRenderer>().sprite = car;
        transform.position = startPos;
        transform.rotation = startRot;
        driving = false;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        /*if ((Input.GetKeyDown("down") || Input.GetKeyDown("up") || Input.GetKeyDown("right") || Input.GetKeyDown("left")) && driving == false)
        {
            StartCoroutine(Move());
        }*/
    }
}