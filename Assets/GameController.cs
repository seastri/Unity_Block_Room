using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    //So you can write the the debugger screen
    public TextMeshPro debuggerText;

    //So you can get user inputs
    public InputReader inputs;

    public GameObject Ball;
    public Transform rh;
    public GameObject PreBlock;
    public Transform lh;
    public GameObject Block;
    public Transform Basketball;
    public Rigidbody Basketballrb;
    public float scaleFactor;
    public TextMeshPro percent;
    public GameObject winnerText;

    private Vector3 scaleChange;
    private Vector3 currentSize;
    private float ballPercentage;

    

    // Start is called before the first frame update
    void Start()
    {
        currentSize=PreBlock.transform.localScale;
        ballPercentage=100;
    }

    // Update is called once per frame
    void Update()
    {
        //Clear the debugger screen when user presses B button
        if (inputs.ButtonBDown)
        {
            debuggerText.SetText("Debugger Log (Press B to clear):");
        }

        //shoot a ball
        if (inputs.RightMainTriggerDown)
        {
            SpawnBall();
        }

        //See where block will be placed
        if (inputs.LeftMainTrigger)
        {
            PreBlock.SetActive(true);
        }
        else
        {
            PreBlock.SetActive(false);
        }


        //place block
        if (inputs.LeftMainTriggerUp)
        {
            SpawnBlock();
        }
        
        //reload the scene if the user presses x
        if(inputs.ButtonXDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //if the player presses y, put the basketball in front of their right hand
        if (inputs.ButtonYDown)
        {
            Basketballrb.velocity=new Vector3(0f,0f,0f);
            Basketball.position = rh.position + 0.5f*rh.forward;
        }

        //If the player moves left joystick up or down while holding leftTrigger, scale the blocks and change the currentSize of the blocks to be spawned
        if (inputs.LeftMainTrigger && inputs.leftJoystick[1] != 0)
        {
            //if we are trying to shrink the block and it is not too small
            if(currentSize[1]>.2 && inputs.leftJoystick[1]<0)
            {
                scaleChange=new Vector3(scaleFactor*inputs.leftJoystick[1], scaleFactor*inputs.leftJoystick[1], scaleFactor*inputs.leftJoystick[1]);
                PreBlock.transform.localScale += scaleChange;
                currentSize=PreBlock.transform.localScale;
            }
            //if we are trying to expand the box and it is not already too big
            if(currentSize[1]<2 && inputs.leftJoystick[1]>0)
            {
                scaleChange=new Vector3(scaleFactor*inputs.leftJoystick[1], scaleFactor*inputs.leftJoystick[1], scaleFactor*inputs.leftJoystick[1]);
                PreBlock.transform.localScale += scaleChange;
                currentSize=PreBlock.transform.localScale;
            }
        }

        //If the player moves left joystick up or down while holding rightTrigger, the speed can be changed on the cannon
        if (inputs.RightMainTrigger && inputs.leftJoystick[1]!=0)
        {
            //If the ballpercentage isn't too low and the player is trying to lower it
            if(ballPercentage>0 && inputs.leftJoystick[1]<0)
            {
                ballPercentage+=inputs.leftJoystick[1]/2;
                percent.SetText("Speed: " + Mathf.Round(ballPercentage) + "%");
            }
            //If the ballpercentage isn't too high and the player is trying to raise it
            if(ballPercentage<100 && inputs.leftJoystick[1]>0)
            {
                ballPercentage+=inputs.leftJoystick[1]/2;
                percent.SetText("Speed: " + Mathf.Round(ballPercentage) + "%");
            }
        }

    }

    void SpawnBall()
    {
        GameObject newBall = Instantiate(Ball, rh.position, Quaternion.identity);
        var component = newBall.GetComponent<Grabbable>();
        component.Setup(inputs, lh, rh);
        var component2 = newBall.GetComponent<BallController>();
        component2.Setup(ballPercentage, winnerText);
    }

    void SpawnBlock()
    {
        GameObject newBlock=Instantiate(Block, lh.position + (0.25f + currentSize[2]/2)*lh.forward, lh.rotation);
        var component = newBlock.GetComponent<BlockController>();
        component.Setup(inputs, lh, rh);
        newBlock.transform.localScale=currentSize;
    }

    
}
