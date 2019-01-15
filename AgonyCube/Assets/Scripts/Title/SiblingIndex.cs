using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiblingIndex : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void siblingIndex1()
    {
        button1.transform.SetSiblingIndex(4);
    }

    public void siblingIndex2()
    {
        button2.transform.SetSiblingIndex(4);
    }

    public void siblingIndex3()
    {
        button3.transform.SetSiblingIndex(4);
    }

    public void siblingIndex4()
    {
        button4.transform.SetSiblingIndex(4);
    }
}