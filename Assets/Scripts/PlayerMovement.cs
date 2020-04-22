using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public Vector3 moveForward = new Vector3(1.0f, 0.0f);
    public Vector3 moveLeft = new Vector3(0.0f, 1.0f);
    public Vector3 moveRight = new Vector3(0.0f, -1.0f);

    public Vector3 currentPos = new Vector3();
    public Vector3 destinationForward = new Vector3();
    public Vector3 destinationLeft = new Vector3();
    public Vector3 destinationRight = new Vector3();

    public float smoothTime;
    private Vector3 velocity = Vector3.zero;

    

    IEnumerator Move()
    {
        
        if (Input.GetKeyDown("right"))
        {
            currentPos = player.transform.position;
            destinationForward = currentPos + moveForward;
            while (true)
            {
                Debug.Log("Total magnitude is " + Mathf.Abs(player.transform.position.magnitude - destinationForward.magnitude));
                yield return new WaitForEndOfFrame();
                player.transform.position = Vector3.SmoothDamp(player.transform.position, destinationForward, ref velocity, smoothTime = 0.5f);
                if (player.transform.eulerAngles.z != 0)
                {
                    player.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }
                if (Mathf.Abs(player.transform.position.magnitude - destinationForward.magnitude) < 0.02f)
                {
                    player.transform.position = destinationForward;
                    break;
                }
            }
            
        }
        else if (Input.GetKeyDown("up"))
        {
            currentPos = player.transform.position;
            destinationLeft = currentPos + moveLeft;
            if (player.transform.eulerAngles.z != 90.0f)
            {
                player.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
            }
            while (true)
            {
                yield return new WaitForEndOfFrame();
                player.transform.position = Vector3.SmoothDamp(player.transform.position, destinationLeft, ref velocity, smoothTime = 0.5f);
                if (Mathf.Abs(player.transform.position.magnitude - destinationLeft.magnitude) < 0.02f)
                {
                    player.transform.position = destinationLeft;
                    break;
                }
            }
        }
        else if (Input.GetKeyDown("down"))
        {
            currentPos = player.transform.position;
            destinationRight = currentPos + moveRight;
            if (player.transform.eulerAngles.z != -90.0f)
            {
                player.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
            }
            while (true)
            {
                yield return new WaitForEndOfFrame();
                player.transform.position = Vector3.SmoothDamp(player.transform.position, destinationRight, ref velocity, smoothTime = 0.5f);
                if (Mathf.Abs(player.transform.position.magnitude - destinationRight.magnitude) < 0.02f)
                {
                    player.transform.position = destinationRight;
                    break;
                }
            }

        }
        yield return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("down") ^ Input.GetKeyDown("up") ^ Input.GetKeyDown("right"))
        {
            StartCoroutine(Move());
        }
    }
}