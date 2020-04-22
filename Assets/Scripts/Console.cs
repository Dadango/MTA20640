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
        return block != null ? block : null;
    }

    public BlockSlut getItem(int i) {
        return indentSloths.Count > 0 ? indentSloths[i] : null;
    }
    public void setItem(int i, BlockSlut newSloth)
    {
        indentSloths[i] = newSloth;
    }
    public void addItem(BlockSlut newSloth) {
        indentSloths.Add(newSloth);
    }
    public int count() {
        return indentSloths.Count;
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

    GameObject addNewSlot(GameObject block, float x, float y) {
        GameObject slotClone = Instantiate(slotPrefab, new Vector2(0, 0), Quaternion.identity);
        slotClone.transform.SetParent(transform);
        slotClone.transform.localPosition = new Vector2(x, (block.transform.localPosition.y + y));
        return slotClone;
    }

    void OnBlockRecieved(GameObject block) //change to a superclass that ever block must inherit from? or interface that every block must subscribe to? or maybe just have a scriptable object or all the functions in "console" and make this a call to a specific reference... probably a dict with the function names
    {
        float offsetY = -1.5f;
        float indentOffset = 1.5f;
        bool marchOrder = false;
        int tempFixTillWeFindAMoreElegantSolution = 0;

        block.transform.SetParent(transform);
        for (int i = 0; i < blockSloths.Count; i++) { //no longer goes reversed
            BlockSlut blockSlot = blockSloths[i];
            if (blockSlot.getTrans().localPosition == block.transform.localPosition)
            {
                blockSloths[i].setBlock(block);

                if (block.CompareTag("Loop"))
                {
                    blockSlot.addItem(new BlockSlut(addNewSlot(block, indentOffset, offsetY).transform));
                    offsetY = -3;
                }

                if (i == blockSloths.Count - 1)
                {
                    indentOffset = 0;
                    blockSloths.Add(new BlockSlut(addNewSlot(block, indentOffset, offsetY).transform));
                }
                break;
            }
            else if (blockSlot.getItem(0) != null){
                indentOffset = 1.5f;
                for (int j = blockSlot.count() - 1; j >= 0 ; j--) { // go through list backwards to skip unnecessary iterations 
                    if (blockSlot.getItem(j).getTrans().localPosition == block.transform.localPosition) { //if block placed inside loop
                        blockSlot.getItem(j).setBlock(block);
                        if (j == blockSlot.count() - 1) { 
                            blockSlot.addItem(new BlockSlut(addNewSlot(block, indentOffset, offsetY).transform));
                            marchOrder = true;
                            tempFixTillWeFindAMoreElegantSolution = i; //we want it to break out of this iteration of the outer (i) scope. This insures we skip the I of the loop (which we really want)
                        }
                    }
                }
            }
            if (marchOrder) {
                if (tempFixTillWeFindAMoreElegantSolution < i) { 
                blockSloths[i].getTrans().localPosition += new Vector3(0, offsetY); //move all blocks AFTER the one that pushes
                if (blockSloths[i].getBlock() != null) blockSloths[i].getBlock().transform.localPosition += new Vector3(0, offsetY); //also push all accompanying blocks with their slots
                }
            }
        }
        marchOrder = false;
    }


    IEnumerator RunConsole() //TODO - this
    {
        for (int i = 0; i < blockSloths.Count; i++) {
            BlockSlut blockSlot = blockSloths[i];
            DragNDrop blockScript = blockSlot.getBlock().GetComponent<DragNDrop>();

            if (blockSlot.getBlock() != null) //if the slot is not empty (has a block)
            {
                if (blockSlot.getItem(0) != null) //if it has indented methods (aka is a loop)
                {
                    for (int j = 0; j < int.Parse(blockScript.methodVar.text); j++) { //how many time to run the loops contents (will throw exceptions with bad user input
                        for (int k = 0; k < blockSlot.count(); k++) { //run all the blocks in the loop //can be simplified with above most likely
                            DragNDrop indScript = blockSlot.getItem(k).getBlock().GetComponent<DragNDrop>();
                            print("Executing function:" + indScript.methodName.text + " " + indScript.methodVar.text);
                            playerMovement.StartCoroutine(indScript.methodName.text);
                            yield return new WaitWhile(() => playerMovement.driving);
                            print("Function executed");
                        }
                    }
                }
                else { 
                    print("Executing function:" + blockScript.methodName.text + " " + blockScript.methodVar.text);
                    playerMovement.StartCoroutine(blockScript.methodName.text);
                    yield return new WaitWhile(() => playerMovement.driving);
                    print("Function executed");
                }
            }
        }
        yield break; //something else probably
    }
}
