using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class slotValue
{
    public int indentLvl;
    public int row;
    public GameObject slot;
    public GameObject method;

    public slotValue(int _indentLvl, int _row, GameObject _slot) {
        indentLvl = _indentLvl;
        row = _row;
        slot = _slot;
    }

}

public class Console : MonoBehaviour
{
    private List<slotValue> slots = new List<slotValue>();
    public GameObject slotPrefab;
    public gas_meter gasMeter;
    public bool runButtonPH;
    public bool resetButtonPH;

    public PlayerMovement playerMovement;
    private Vector2 direction;

    private bool running;
    int loopFixer = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 50; i++) { //first row already placed
            for (float j = -1.8f; j <= 1.8f; j += 1.8f) {
                GameObject slot = Instantiate(slotPrefab);
                slot.transform.SetParent(transform);
                slot.transform.localPosition = new Vector3(j, (i * -1.4f), 0);
                int indentLvl;
                if (j < 0) indentLvl = 0; else if (j == 0) indentLvl = 1; else indentLvl = 2;
                int row = i;
                slots.Add(new slotValue(indentLvl, row, slot));
            }
        }
    }

    public void OnBlockRecieved(GameObject block) {
        block.transform.SetParent(transform);
        foreach (slotValue slut in slots) {
            if (block.transform.localPosition == slut.slot.transform.localPosition) {
                slut.method = block;
            }
        }
        block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y, block.transform.position.z - 90);
    }

    public void OnBlockRemoval(GameObject block) {
        foreach (slotValue slut in slots) {
            Vector3 slotPos = slut.slot.transform.localPosition;
            float slotX = Mathf.Round(slotPos.x * 10f) / 10f; //remove everything after the first decimal
            float slotY = Mathf.Round(slotPos.y * 10f) / 10f;
            float blockX = Mathf.Round(block.transform.localPosition.x * 10f) / 10f;
            float blockY = Mathf.Round(block.transform.localPosition.y * 10f) / 10f;
            if (blockX == slotX && blockY == slotY) {
                slut.method = null;
            }
        }
        block.transform.SetParent(null);
    }

    private void Update()
    {
        if (runButtonPH) {
            runButtonPH = false;
            loopFixer = -1;
            StartCoroutine("RunConsole", 0);

            //TO DO: disable everything else?
        }
        if (resetButtonPH) {
            resetButtonPH = false;
            ResetScene();
        }
    }

    void CheckConsole() {
        //check slot above and to the left to see if it's a loop or if statement
    }


    IEnumerator RunConsole(int number) //TODO - this
    {
        int startIndent = number%3;
        for (int i = number; i < slots.Count-(2-startIndent); i += 3) {
            if (playerMovement.crashed) { break; }
            slotValue slot = slots[i];
            if (slot.method != null)
            {
                loopFixer += 1;
                DragNDrop blockScript = slot.method.GetComponent<DragNDrop>();
                if (slot.method.CompareTag("IfStatement"))
                {
                    if (startIndent > 1) { break; } //don't allow nesting within nesting!
                    if (ifif(blockScript))
                    {
                        //print("if-Block entered. Starting new console at row: " + ((i + 3) / 3) + " and " + (startIndent + 1) + " : " + Time.time.ToString());
                        startIndent = startIndent > 0 ? startIndent - 1 : startIndent;
                        yield return StartCoroutine("RunConsole", (i + 3 + (startIndent + 1)));
                        i += loopFixer * 3;
                    }
                }
                else if (slot.method.CompareTag("Loop"))
                {
                    if (startIndent > 1) { break; } //don't allow nesting within nesting!
                    //int oldLoop = loopFixer;
                    for (int j = 0; j < int.Parse(blockScript.loopVar.text); j++)
                    {
                        if (playerMovement.crashed) { break; }
                        //print("loop entered. Starting new console at row: " + ((i + 3) / 3) + " and " + (startIndent + 1) + " : " + Time.time.ToString());
                        startIndent = startIndent > 0 ? startIndent - 1 : startIndent;
                        yield return StartCoroutine("RunConsole", (i + 3 + (startIndent + 1)));;
                    }
                    //loopFixer -= int.Parse(blockScript.loopVar.text) - ((loopFixer - oldLoop) / int.Parse(blockScript.loopVar.text));
                    //print(i +" : "+ loopFixer);
                    //i += loopFixer*3;
                }
                else
                {
                    print("Executing function: " + blockScript.methodName.text + " " + Time.time.ToString());
                    yield return playerMovement.StartCoroutine(blockScript.methodName.text);
                    //yield return new WaitWhile(() => playerMovement.driving); //<-- this is still nice code. Just not used
                }
            }
            else if (startIndent > 0) { print("Method is null! " + "Looking at: " + (i/3) + "," + (i%3) +" : "+ Time.time.ToString()); break; }//if outside of loop, and no more methods remain, break (but skip any further indents?))
        }
        yield break; //something else probably
    }



    bool ifif(DragNDrop blockScript) {
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
        RaycastHit2D inFrontRay = Physics2D.Raycast(playerMovement.transform.position, direction, 1.5f);
        if (!inFrontRay) return false;
        GameObject inFront = inFrontRay.collider.gameObject;
        return ((inFront.CompareTag("Building") && blockScript.methodVar == 'b') || (inFront.CompareTag("RoadBlock") && blockScript.methodVar == 'r') || (blockScript.methodVar == 'e'));
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