using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private SpringJoint2D springJoint2D;
    void Start()
    {
        //get components
        springJoint2D = GetComponent<SpringJoint2D>();
        //set anchor to object position
        springJoint2D.anchor = Vector2.zero;
        springJoint2D.connectedAnchor = transform.position;
    }

    void Update()
    {
        
    }
}
