using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    public float speed;
    public float leeway = 0.05f;

    public TMP_Text tempName;
    public TMP_InputField tempVar;

    private bool following;
    private Collider2D hoveredSlot;

    // Start is called before the first frame update
    void Start()
    {
        following = false;
        hoveredSlot = null;

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
        else if (hoveredSlot != null) //if not following and hovering a slot
        {
            transform.position = hoveredSlot.transform.position;
            hoveredSlot.gameObject.GetComponentInParent<Console>().BroadcastMessage("OnBlockRecieved", gameObject);
            hoveredSlot = null; //temp
        }
      }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<DragNDrop>() == null) hoveredSlot = collision; //temporary fix, change to tags instead probably
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //hoveringSlot = null;
    }
}
