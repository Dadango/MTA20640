using TMPro;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    public float speed;
    public float leeway = 0.05f;

    public TMP_Text methodName;
    public TMP_InputField loopVar;
    public char methodVar;

    private bool following;
    private Collider2D hoveredSlot;
    private bool trasher;

    public static bool dupeFix; //makes certain only 1 item is following the cursor at a time. Should fix the "placing 2 methods on top of each other" - bug

    // Start is called before the first frame update
    void Start()
    {
        following = true;
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
            if (transform.parent != null) {
                transform.parent.GetComponent<Console>().BroadcastMessage("OnBlockRemoval", gameObject);
                hoveredSlot = null; //temp
            }
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
            hoveredSlot = null;
        }
        else if (trasher) { GameObject.Destroy(gameObject); }
      }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Slot"))
        {
            if (hoveredSlot == null || (collision.transform.position - transform.position).magnitude < (hoveredSlot.transform.position - transform.position).magnitude)
            {
                if (following) { 
                   hoveredSlot = collision;
                }
            }
        }
        else if (collision.CompareTag("TrashCan"))
        {
            trasher = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (hoveredSlot == collision) { hoveredSlot = null; }
        trasher = false;
    }

}
