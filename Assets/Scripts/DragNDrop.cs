﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    public float speed;
    public float leeway = 0.05f;

    public TMP_Text methodName;
    public TMP_InputField methodVar;

    private bool following;
    private Collider2D hoveredSlot;

    public static bool dupeFix; //makes certain only 1 item is following the cursor at a time. Should fix the "placing 2 methods on top of each other" - bug

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
            //following = !following;
            if (following) following = !following;
            else if (!dupeFix) following = true;
            dupeFix = following? true : false;
        }
        if (following)
        {
            transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), speed);
        }
        else if (hoveredSlot != null) //if not following and hovering a slot
        {
            transform.position = hoveredSlot.transform.position;
            hoveredSlot.gameObject.GetComponentInParent<Console>().BroadcastMessage("OnBlockRecieved", gameObject);
            following = false;
            hoveredSlot = null; //temp
        }
      }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DragNDrop>() == null) //temporary fix, change to tags instead probably
        {
            if (hoveredSlot == null || (collision.transform.position - transform.position).magnitude < (hoveredSlot.transform.position - transform.position).magnitude) {
                hoveredSlot = collision;
                collision.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<DragNDrop>() == null) { collision.GetComponent<SpriteRenderer>().color = Color.gray; }
    }
}
