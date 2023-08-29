using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class BasketballController : MonoBehaviour
{
    public GameObject winnerText;

    private bool movingDown;
    private float lastHeight;
    
    // Start is called before the first frame update
    void Start()
    {
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
