using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    public float speed;
    public float leeway = 0.05f;

    private bool following;

    // Start is called before the first frame update
    void Start()
    {
        following = false;

        leeway += 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).magnitude <= leeway))
        {
            following = !following;
        }
        if (following)
        {
            transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), speed);
        }
      }
}
