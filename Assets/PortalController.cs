using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using TMPro;

public class PortalController : MonoBehaviour
{
    public InputReader inputs;
    public TextMeshPro debugger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider col)
    {
        debugger.SetText(debugger.text +"\n"+ col.gameObject.name);
        if (col.gameObject.name == "Camera Offset")
        {
            SceneManager.LoadScene("Instructions");
        }
    }
}