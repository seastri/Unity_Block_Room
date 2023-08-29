using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class ReturnBoxController : MonoBehaviour
{
    public InputReader inputs;

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
        if (col.gameObject.name == "Camera Offset")
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
