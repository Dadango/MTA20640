using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockSlut {
    private Transform transform;
    private GameObject block;

    public BlockSlut(Transform _transform)
    {
        transform = _transform;
    }

    public Transform getTrans() {
        return transform;
    }
    public void setBlock(GameObject _method) {
        block = _method;
    }
    public GameObject getBlock() {
        return block;
    }
}

public class Console : MonoBehaviour
{
    List<BlockSlut> blockSloths = new List<BlockSlut>(); //honestly surprised this doesn't say sluts instead of slots
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

            if (blockSlot != null) blockSloths.Add(new BlockSlut(blockSlot));
        }
        //blockSlots[0].GetComponent<SpriteRenderer>().color = Color.red;
        //blockSlots[0].transform.localPosition -= new Vector3(0,50);
    }

    private void Update()
    {
        if (runButtonPH) {
            runButtonPH = false;
            StartCoroutine("RunConsole");
            //TO DO: disable everything else?
        }
    }

    void OnBlockRecieved(GameObject block) //change to a superclass that ever block must inherit from? or interface that every block must subscribe to? or maybe just have a scriptable object or all the functions in "console" and make this a call to a specific reference... probably a dict with the function names
    {
        block.transform.SetParent(transform);
        for (int i = blockSloths.Count - 1; i >= 0; i--) { //go through list backwards to skip unessesary iterations
            blockSloths[i].setBlock(block);
            BlockSlut blockSlot = blockSloths[i];
            if (blockSlot.getTrans().localPosition == block.transform.localPosition) {
                print("fucking");
                if (i == blockSloths.Count - 1) {
                    GameObject slotClone = Instantiate(slotPrefab, new Vector2(0, 0), Quaternion.identity);
                    slotClone.transform.SetParent(transform);
                    slotClone.transform.localPosition = new Vector2(0, (block.transform.localPosition.y - 1.5f));
                    blockSloths.Add(new BlockSlut(slotClone.transform));
                    print("mouthbreather");
                }
                break;
            }
        }
        print(blockSloths.Count + " <- how many sloths?");

    }

    IEnumerator RunConsole() //TODO - this
    {
        print("Attempting your code");
        for (int i = 0; i < blockSloths.Count; i++) {
            print(i);
            BlockSlut blockSlot = blockSloths[i];
            if (blockSlot.getBlock() != null)
            {
                var blockScript = blockSlot.getBlock().GetComponent<DragNDrop>();
            }
        }
        yield break; //something else probably
    }
}
