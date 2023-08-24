using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DuplicateNode : MonoBehaviour
{
    public GameObject node;
    public TMP_InputField userInputField; // Reference to the Unity InputField where the user types the text

    private Vector3 firstNodeVector;
    private List<GameObject> nodeList = new List<GameObject>();

    private enum Insertion
    {
        FIRST,
        LAST,
        ORDERED
    }

    void Start()
    {
        firstNodeVector = new Vector3(node.transform.position.x,
            node.transform.position.y, node.transform.position.z);
    }

    void Update()
    {
        // Check if the user pressed "T" to create the node
        if (Input.GetKeyDown(KeyCode.T))
        {
            CreateNewNode(Insertion.LAST);
        }

        // Check if the user pressed "R" to duplicate the node on the opposite side
        if (Input.GetKeyDown(KeyCode.R))
        {
            CreateNewNode(Insertion.FIRST);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            CreateNewNode(Insertion.ORDERED);
        }
    }

    private void UpdateNodePosition()
    {
        nodeList[0].transform.position = new Vector3(firstNodeVector.x, nodeList[0].transform.position.y, nodeList[0].transform.position.z);
        for (int i = 1; i < nodeList.Count; i++)
        {
            nodeList[i].transform.position = new Vector3(nodeList[i - 1].transform.position.x - 0.659f, nodeList[i].transform.position.y, nodeList[i].transform.position.z);
        }
        
        print("UpdateNodePosition["+0+"]: " + nodeList[0].name + " " + nodeList[0].transform.position.x + "\n"
             + "UpdateNodePosition["+(nodeList.Count-1)+"]: " + nodeList[nodeList.Count-1].name + " " + nodeList[nodeList.Count-1].transform.position.x + "\n");
    }

    private void CreateNewNode(Insertion insertionType)
    {
        print("CreateNewNode\n");
        if (nodeList.Count == 10)
            ResetNodes();

        GameObject newNode = Instantiate(node, firstNodeVector, Quaternion.identity);
        newNode.SetActive(true);
        newNode.transform.parent = transform;
        newNode.name = "Node" + nodeList.Count;

        TMP_Text newTextComponent = newNode.GetComponentInChildren<TMP_Text>();
        if (newTextComponent != null)
        {
            if (!string.IsNullOrEmpty(userInputField.text))
            {
                newTextComponent.text = userInputField.text;
                userInputField.text = "";
            }
            else
            {
                newTextComponent.text = nodeList.Count.ToString();
            }
        }

        switch (insertionType)
        {
            case Insertion.FIRST:
                nodeList.Insert(0, newNode);
                break;
            case Insertion.LAST:
                nodeList.Add(newNode);
                break;
            case Insertion.ORDERED:
                for (int i = 0; i < nodeList.Count; i++)
                {
                    TMP_Text textComponent = nodeList[i].GetComponentInChildren<TMP_Text>();
                    if (textComponent != null && int.Parse(newTextComponent.text) < int.Parse(textComponent.text))
                    {
                        nodeList.Insert(i, newNode);
                        break;
                    }
                }
                break;
        }

        UpdateNodePosition();
        Debug.Log("NewNode: " + newNode.name + " " + newNode.transform.position.x);
        Debug.Log("nodeList[0]: " + nodeList[0].name + " " + nodeList[0].transform.position.x);
    }

    private void ResetNodes()
    {
        foreach (GameObject node in nodeList)
        {
            Destroy(node);
        }
        nodeList.Clear();
    }
}
