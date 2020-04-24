using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlockSlut {
    private GameObject slot;
    private GameObject block;
    private List<BlockSlut> indentSloths = new List<BlockSlut>();

    public BlockSlut(GameObject _slot)
    {
        slot = _slot;
    }

    public GameObject getSlot() {
        return slot;
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

    public void removeItem(int i)
    {
        indentSloths.RemoveAt(i);
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
    public bool resetButtonPH;
    public PlayerMovement playerMovement;
    private float offsetY = -1.5f;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        GameObject slotClone = Instantiate(slotPrefab, new Vector2(0, 0), Quaternion.identity);
        slotClone.transform.parent = transform;
        slotClone.transform.localPosition = new Vector2(0, -1.5f);
        blockSloths.Add(new BlockSlut(slotClone));
    }

    private void Update()
    {
        if (runButtonPH) {
            runButtonPH = false;
            StartCoroutine("RunConsole");

            //TO DO: disable everything else?
        }
        if (resetButtonPH) {
            resetButtonPH = false;
            ResetScene();
        }
    }

    GameObject addNewSlot(GameObject block, float x, float y) { //adds a new slot below the current slot
        GameObject slotClone = Instantiate(slotPrefab, new Vector2(0, 0), Quaternion.identity);
        slotClone.transform.SetParent(transform);
        slotClone.transform.localPosition = new Vector2(x, (block.transform.localPosition.y + y));
        return slotClone;
    }

    void OnBlockRecieved(GameObject block) //change to a superclass that ever block must inherit from? or interface that every block must subscribe to? or maybe just have a scriptable object or all the functions in "console" and make this a call to a specific reference... probably a dict with the function names
    {
        float indentOffset = 1.5f;
        offsetY = -1.5f;
        bool marchOrder = false;
        int tempFixTillWeFindAMoreElegantSolution = 0;

        block.transform.SetParent(transform);
        for (int i = 0; i < blockSloths.Count; i++) { //no longer goes reversed
            BlockSlut blockSlot = blockSloths[i];
            if (blockSlot.getSlot().transform.localPosition == block.transform.localPosition)
            {
                blockSloths[i].setBlock(block); //set block to slot

                if (block.CompareTag("Loop") || block.CompareTag("IfStatement")) //needs to be limited: will create new slot everytime it is placed (even if placed on top of a previous loop slot)
                {
                    blockSlot.addItem(new BlockSlut(addNewSlot(block, indentOffset, offsetY))); //if it's a loop, add a new indented slot
                    offsetY = -3; //and offset the next outer slot by -3
                    marchOrder = true; //this actually might not have a purpose
                }

                if (i == blockSloths.Count - 1)
                {
                    indentOffset = 0;
                    blockSloths.Add(new BlockSlut(addNewSlot(block, indentOffset, offsetY))); //if the last slot is taken, add a new one below it
                }
                break;
            }
            else if (blockSlot.getItem(0) != null){ //if the slot has indented slots
                indentOffset = 1.5f;
                for (int j = blockSlot.count() - 1; j >= 0 ; j--) { // go through list backwards to skip unnecessary iterations 
                    if (blockSlot.getItem(j).getSlot().transform.localPosition == block.transform.localPosition) { //if block is placed inside loop
                        blockSlot.getItem(j).setBlock(block); //set block to slot
                        if (j == blockSlot.count() - 1) { 
                            blockSlot.addItem(new BlockSlut(addNewSlot(block, indentOffset, offsetY))); //if the last slot is taken, add a new one below it
                            marchOrder = true;
                            tempFixTillWeFindAMoreElegantSolution = i; //we want it to break out of this iteration of the outer (i) scope. This insures we skip the I of the loop (which we really want)
                        }
                    }
                }
            }
            if (marchOrder) {
                if (tempFixTillWeFindAMoreElegantSolution < i) {
                    for (int j = 0; j < blockSloths[i].count(); j++) { //if block has indented slots, also move all indented slots
                        blockSloths[i].getItem(j).getSlot().transform.localPosition += new Vector3(0, offsetY);
                        if (blockSloths[i].getItem(j).getBlock() != null) blockSloths[i].getBlock().transform.localPosition += new Vector3(0, offsetY); // if slots have blocks, also move blocks
                    }
                blockSloths[i].getSlot().transform.localPosition += new Vector3(0, offsetY); //move all blocks AFTER the one that pushes
                if (blockSloths[i].getBlock() != null) blockSloths[i].getBlock().transform.localPosition += new Vector3(0, offsetY); //also push all accompanying blocks with their slots
                }
            }
        }
        marchOrder = false;
    }

    void OnBlockRemoval(GameObject block) {
        //temp code below (allows for removal of methods)
        for (int i = 0; i < blockSloths.Count; i++)
        {
            BlockSlut blockSlot = blockSloths[i];
            if (blockSlot.getSlot().transform.localPosition == block.transform.localPosition)
            {
                blockSlot.setBlock(null);
            }
        }
                block.transform.SetParent(null);

/*        Vector3 test = new Vector3(0, 0);
        bool itWasALoop = false;
        for (int i = 0; i < blockSloths.Count; i++) {
            BlockSlut blockSlot = blockSloths[i];
            if (blockSlot.getSlot().transform.localPosition == block.transform.localPosition)
            {
                if (block.CompareTag("Loop") && !itWasALoop) //remove all indented methods as well
                {
                    itWasALoop = true;
                    test.y = -blockSlot.count() * offsetY;
                    for (int j = blockSlot.count()-1; j > 0 - 1; j--) {
                        BlockSlut jBlock = blockSlot.getItem(j);
                        GameObject.Destroy(jBlock.getSlot());
                        if (jBlock.getBlock() != null) GameObject.Destroy(jBlock.getBlock());
                    }
                }
                StartCoroutine("delayRemoval", i);
                //blockSloths.RemoveAt(i);
            }
            for (int j = 0; j < blockSlot.count() - 1; j++)
                {
                    if (blockSlot.getItem(j).getSlot().transform.localPosition == block.transform.localPosition) {
                    //GameObject.Destroy(blockSlot.getItem(j).getSlot());
                    blockSlot.removeItem(j);
                    break;
                    }
                }
            print(test.y);
            if (test.y != 0) {
                if (blockSlot.getBlock() != null) { blockSlot.getBlock().transform.localPosition += test;}
                blockSlot.getSlot().transform.localPosition += test;
                print("Moving");
                blockSlot.getSlot().GetComponent<SpriteRenderer>().color = Color.red;
            }
        }*/
    }

    IEnumerator delayRemoval(int i) {
        yield return new WaitForEndOfFrame();
        blockSloths.RemoveAt(i);
    }

    IEnumerator RunConsole() //TODO - this
    {
        for (int i = 0; i < blockSloths.Count-1; i++) {
            BlockSlut blockSlot = blockSloths[i];

            if (blockSlot.getBlock() != null) //if the slot is not empty (has a block)
            {
                DragNDrop blockScript = blockSlot.getBlock().GetComponent<DragNDrop>();
                if (playerMovement.crashed) { break; }

                if (blockSlot.getBlock().CompareTag("IfStatement"))
                {
                    //Vector2 direction;
                    int thot = (int)playerMovement.transform.eulerAngles.z;
                    switch (thot)
                    {
                        case 90:
                            direction = new Vector2(0, 1);
                            break;
                        case 0:
                            direction = new Vector2(1, 0);
                            break;
                        case -90:
                            direction = new Vector2(0, -1);
                            break;
                        case 180:
                            direction = new Vector2(-1, 0);
                            break;
                        default:
                            direction = new Vector2(0, 0);
                            break;
                    }
                    GameObject inFront = Physics2D.Raycast(playerMovement.transform.position, direction).collider.gameObject;
                    print(inFront.name);
                    if ((inFront.CompareTag("Building") && blockScript.methodVar == 'b') || (inFront.CompareTag("RoadBlock") && blockScript.methodVar == 'r') || (blockScript.methodVar == 'e'))
                    {
                        for (int j = 0; j < blockSlot.count() - 1; j++)
                        {
                            DragNDrop indScript = blockSlot.getItem(j).getBlock().GetComponent<DragNDrop>();
                            if (playerMovement.crashed) { break; }
                            print("Executing function:" + indScript.methodName.text);
                            playerMovement.StartCoroutine(indScript.methodName.text);
                            yield return new WaitWhile(() => playerMovement.driving);
                            print("Function executed");
                        }
                    }
                    
                }
                else if (blockSlot.getBlock().CompareTag("Loop")) //if it has indented methods (aka is a loop)
                {
                    for (int j = 0; j < int.Parse(blockScript.loopVar.text); j++) { //how many time to run the loops contents (will throw exceptions with bad user input
                        for (int k = 0; k < blockSlot.count()-1; k++) { //run all the blocks in the loop //can be simplified with above most likely
                            DragNDrop indScript = blockSlot.getItem(k).getBlock().GetComponent<DragNDrop>();
                            if (playerMovement.crashed) { break; }
                            print("Executing function:" + indScript.methodName.text);
                            playerMovement.StartCoroutine(indScript.methodName.text);
                            yield return new WaitWhile(() => playerMovement.driving);
                            print("Function executed");
                        }
                    }
                }
                else {
                    print("Executing function:" + blockScript.methodName.text);
                    playerMovement.StartCoroutine(blockScript.methodName.text);
                    yield return new WaitWhile(() => playerMovement.driving);
                    print("Function executed");
                }
            }
        }
        yield break; //something else probably
    }

    void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().path, LoadSceneMode.Single); //add "don't destroy on load" or change this, if fow is implemented
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(playerMovement.transform.position, direction);
    }

}
