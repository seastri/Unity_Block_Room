using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    public Transform RightHand;
    public float initialForce;
    public float scaleInitialForce;
    public GameObject winnerText;

    private bool movingDown;
    private float lastHeight;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        RightHand = GameObject.FindWithTag("RH").transform;
        rb=GetComponent<Rigidbody>();
        direction=RightHand.forward;
        rb.AddForce(scaleInitialForce*initialForce*direction);
        movingDown=false;
        lastHeight=this.transform.position[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position[1]<=lastHeight)
        {
            movingDown=true;
        }
        else
        {
            movingDown=false;
        }
        lastHeight=this.transform.position[1];
    }

    public void Setup(float percentage, GameObject winnerMessage)
    {
        scaleInitialForce = percentage/100f;
        winnerText = winnerMessage;
    }

    //When the ball goes in the goal
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "score")
        {
            if (movingDown)
            {
                StartCoroutine(DisplayText());
            }
        }
    }

    //Time delay function
    IEnumerator DisplayText()
    {
        winnerText.SetActive(true);
        yield return new WaitForSeconds(3f);
        winnerText.SetActive(false);
    }
}
