using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlockSlut {
    private Transform transform;
    private GameObject block;
    private List<BlockSlut> indentSloths = new List<BlockSlut>();

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

    public BlockSlut getItem(int i) {
        return indentSloths[i];
    }
    public void setItem(int i, BlockSlut newSloth)
    {
        indentSloths[i] = newSloth;
    }
    public void addItem(BlockSlut newSloth) {
        indentSloths.Add(newSloth);
    }
}

public class Console : MonoBehaviour
{
    List<BlockSlut> blockSloths = new List<BlockSlut>(); //honestly surprised this doesn't say sluts instead of slots
    public GameObject slotPrefab;
    public bool runButtonPH;
    public PlayerMovement playerMovement;

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
        float offsetY = -1.5f;
        float indentOffset = 1.5f;

        block.transform.SetParent(transform);
        for (int i = blockSloths.Count - 1; i >= 0; i--) { //go through list backwards to skip unessesary iterations
            BlockSlut blockSlot = blockSloths[i];
            if (blockSlot.getTrans().localPosition == block.transform.localPosition) {
                blockSloths[i].setBlock(block);

                if (block.CompareTag("Loop"))
                {
                    GameObject slotClone = Instantiate(slotPrefab, new Vector2(0, 0), Quaternion.identity);
                    slotClone.transform.SetParent(transform);
                    slotClone.transform.localPosition = new Vector2(indentOffset, (block.transform.localPosition.y + offsetY));
                    blockSlot.addItem(new BlockSlut(slotClone.transform));
                    offsetY = -3;
                }

                if (i == blockSloths.Count - 1) {
                    GameObject slotClone = Instantiate(slotPrefab, new Vector2(0, 0), Quaternion.identity);
                    slotClone.transform.SetParent(transform);
                    slotClone.transform.localPosition = new Vector2(0, (block.transform.localPosition.y + offsetY));
                    blockSloths.Add(new BlockSlut(slotClone.transform));
                }
                break;
            }
        }

    }


    IEnumerator RunConsole() //TODO - this
    {
        for (int i = 0; i < blockSloths.Count; i++) {
            BlockSlut blockSlot = blockSloths[i];
            if (blockSlot.getBlock() != null)
            {
                DragNDrop blockScript = blockSlot.getBlock().GetComponent<DragNDrop>();
                //if (blockScript.methodName.text == "LoopXTimes")
                print("Executing function:" + blockScript.methodName.text + " " + blockScript.methodVar.text);
                playerMovement.StartCoroutine(blockScript.methodName.text);
                yield return new WaitWhile(() => playerMovement.driving);
                print("Function executed");
            }
        }
        yield break; //something else probably
    }
}
