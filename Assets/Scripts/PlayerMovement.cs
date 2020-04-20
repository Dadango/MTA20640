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

    public float smoothTime;
    private Vector3 velocity = Vector3.zero;

    IEnumerator Move()
    {
        
        if (Input.GetKeyDown("right"))
        {
            currentPos = player.transform.position;
            while (player.transform.position != moveForward + player.transform.position)
            {
                yield return new WaitForEndOfFrame();
                player.transform.position = Vector3.SmoothDamp(player.transform.position, moveForward + currentPos, ref velocity, smoothTime = 1f);
                if (player.transform.eulerAngles.z != 0)
                {
                    player.transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }
            Debug.Log("LoopComplete");
        }
        else if (Input.GetKeyDown("up"))
        {
            player.transform.position += moveLeft;
            player.transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            if (player.transform.eulerAngles.z != 0)
            {
                player.transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
            }
        }
        else if (Input.GetKeyDown("down"))
        {
            player.transform.position += moveRight;
            player.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
            if (player.transform.eulerAngles.z != 0)
            {
                player.transform.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
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
        StartCoroutine(Move());
    }
}