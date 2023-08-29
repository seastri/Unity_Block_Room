using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

//currently, this will make your object be able to be picked up by the player using the left or right grip button.  
//The player can throw the object, but to use this the object must be a rigidbody

public class Grabbable : MonoBehaviour
{
    public InputReader inputs;
    public Transform rh;
    public Transform lh;
    public Rigidbody rb;
    public float throwSpeed;

    private bool rightHandTouch;
    private bool leftHandTouch;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        throwSpeed=10000;
        leftHandTouch=false;
        rightHandTouch=false;
        rb=this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //To allow the object to be picked up by the left hand
        if (leftHandTouch && inputs.LeftGrip)
        {
            rb.useGravity=false;
            rb.isKinematic=true;
            this.transform.SetParent(lh);
            lastPosition=lh.position;
        }
        if (inputs.LeftGripUp && leftHandTouch)
        {
            rb.useGravity=true;
            rb.isKinematic=false;
            this.transform.SetParent(null);
            Vector3 force = throwSpeed*(lh.position-lastPosition);
            rb.AddForce(force);
        }

        
        //to allow the object to be picked up by the right hand
        if (rightHandTouch && inputs.RightGrip)
        {
            rb.useGravity=false;
            rb.isKinematic=true;
            this.transform.SetParent(rh);
            lastPosition=rh.position;
        }
        if (inputs.RightGripUp && rightHandTouch)
        {
            rb.useGravity=true;
            rb.isKinematic=false;
            this.transform.SetParent(null);
            Vector3 force = throwSpeed*(rh.position-lastPosition);
            rb.AddForce(force);
        }

        //just a double check to make sure the object doesn't stick to the controller
        if (!inputs.RightGrip && !inputs.LeftGrip)
        {
            this.transform.SetParent(null);
        }
    }

    //when an object is spawned, this command must be called to pass the necessary objects from the scene
    public void Setup(InputReader importInputReader, Transform importLeft, Transform importRight)
    {
        inputs=importInputReader;
        lh=importLeft;
        rh=importRight;
    }


    //these detect whether the controller is touching object
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "RH")
        {
            rightHandTouch=true;
        }
        else if (col.gameObject.tag == "LH")
        {
            leftHandTouch=true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "RH")
        {
            rightHandTouch=false;
        }
        else if (col.gameObject.tag == "LH")
        {
            leftHandTouch=false;
        }
    }
}
