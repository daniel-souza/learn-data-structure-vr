using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateNode : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject node;
    private Vector3 firstNodeVector;
    private Vector3 lastNodeVector;
    void Start()
    {
       
        // x: 7.635, y: 0.897, z: -2.704
        // x: 6.976, y: 0.897, z: -2.704
        // diffx: 0.659
        firstNodeVector = new Vector3(node.transform.position.x, 
                node.transform.position.y, node.transform.position.z);
        lastNodeVector = new Vector3(firstNodeVector.x, firstNodeVector.y, firstNodeVector.z);
    }

    // duplicate node when key T press down, move it to the right, and update lastNodeVector
    void Update()
    {
        // duplicate node when key T press down
        if (Input.GetKeyDown(KeyCode.T))
        {
            lastNodeVector.x -= 0.659f;
            GameObject newNode = Instantiate(node, lastNodeVector, Quaternion.identity);
            newNode.transform.parent = transform;
        }
    }
}
