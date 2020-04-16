using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Console : MonoBehaviour
{
    List<Transform> blockSloths = new List<Transform>(); //honestly surprised this doesn't say sluts instead of slots
    public GameObject slotPrefab;
    public bool runButtonPH;
    public GameObject temp;

    // Start is called before the first frame update
    void Start()
    {
        GameObject slotClone = Instantiate(slotPrefab, new Vector2(0, 0), Quaternion.identity);
        slotClone.transform.parent = transform;
        slotClone.transform.localPosition = new Vector2(0, -1.5f);

        foreach (Transform child in transform)
        {
            Transform blockSlot = child.GetComponent<Transform>();

            if (blockSlot != null) blockSloths.Add(blockSlot);  
        }
        //blockSlots[0].GetComponent<SpriteRenderer>().color = Color.red;
        //blockSlots[0].transform.localPosition -= new Vector3(0,50);
    }

    private void Update()
    {
        if (runButtonPH) {
            runButtonPH = false;
            RunConsole();
        }
    }
    void OnBlockRecieved(GameObject block) //change to a superclass that ever block must inherit from? or interface that every block must subscribe to? or maybe just have a scriptable object or all the functions in "console" and make this a call to a specific reference... probably a dict with the function names
    {
        block.transform.SetParent(transform);
        for (int i = blockSloths.Count - 1; i >= 0; i--) { //go through list backwards to skip unessesary iterations
            Transform blockSlot = blockSloths[i];
            if (blockSlot.transform.localPosition == block.transform.localPosition) {
                print("fucking");
                if (i == blockSloths.Count - 1) {
                    GameObject slotClone = Instantiate(slotPrefab, new Vector2(0, 0), Quaternion.identity);
                    slotClone.transform.SetParent(transform);
                    slotClone.transform.localPosition = new Vector2(0, (block.transform.localPosition.y - 1.5f));
                    blockSloths.Add(slotClone.transform);
                    print("mouthbreather");
                }
                break;
            }
        }
        print(blockSloths.Count + " <- how many sloths?");

    }

    void RunConsole()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<DragNDrop>()) { print(child.name); } //if it can be dragged, it is most likely a method. Consider changing to use tags instead later
        }
        string tempName = temp.GetComponent<DragNDrop>().tempName.text;
        print(tempName);
        string tempVar = temp.GetComponent<DragNDrop>().tempVar.text;
        if (tempVar == "") tempVar= "1";
        print(tempVar);
    }
}
