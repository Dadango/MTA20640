using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    List<Transform> blockSlots = new List<Transform>();
    public GameObject slotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject slotClone = Instantiate(slotPrefab, new Vector2(0, 0), Quaternion.identity);
        slotClone.transform.parent = transform;
        slotClone.transform.localPosition = new Vector2(0, -1.5f);

        foreach (Transform child in transform)
        {
            Transform blockSlot = child.GetComponent<Transform>();

            if (blockSlot != null) blockSlots.Add(blockSlot);
        }
        //blockSlots[0].GetComponent<SpriteRenderer>().color = Color.red;
        //blockSlots[0].transform.localPosition -= new Vector3(0,50);
    }


    void OnBlockRecieved(MethodScriptPH thisIsPoorlyMade) //change to a superclass that ever block must inherit from? or interface that every block must subscribe to?
    {
        thisIsPoorlyMade.BroadcastMessage("someFunctionalityFunction");
    }
}
