using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardButtons : MonoBehaviour
{
    private DrillController drillController;

    private void Start() 
    {
        drillController = GameObject.FindGameObjectWithTag("DrillController").GetComponent<DrillController>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Index")
        {
            if (gameObject.name == drillController.drillButton1.name)
            {
                drillController.PlayPassingDrill();
            }
            else if (gameObject.name == drillController.drillButton2.name)
            {
                drillController.PlayDibblingDrill();
            }
            else if (gameObject.name == drillController.drillButton3.name)
            {
                drillController.PlayShootingDrill();
            }
            else if (gameObject.name == drillController.drillButton4.name)
            {
                drillController.PlayCrossingDrill();
            }
        }
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Index")
        {
            if(this.gameObject.name == "Drill1")
            {
                GetComponent<BoxCollider>().enabled = false;
                drillController.drillButton2.GetComponent<BoxCollider>().enabled = true;
                drillController.drillButton3.GetComponent<BoxCollider>().enabled = true;
                drillController.drillButton4.GetComponent<BoxCollider>().enabled = true;
            }
            else if(this.gameObject.name == "Drill2")
            {
                GetComponent<BoxCollider>().enabled = false;
                drillController.drillButton1.GetComponent<BoxCollider>().enabled = true;
                drillController.drillButton3.GetComponent<BoxCollider>().enabled = true;
                drillController.drillButton4.GetComponent<BoxCollider>().enabled = true;
            }
            else if(this.gameObject.name == "Drill3")
            {
                GetComponent<BoxCollider>().enabled = false;
                drillController.drillButton1.GetComponent<BoxCollider>().enabled = true;
                drillController.drillButton2.GetComponent<BoxCollider>().enabled = true;
                drillController.drillButton4.GetComponent<BoxCollider>().enabled = true;
            }
            else if(this.gameObject.name == "Drill4")
            {
                GetComponent<BoxCollider>().enabled = false;
                drillController.drillButton1.GetComponent<BoxCollider>().enabled = true;
                drillController.drillButton3.GetComponent<BoxCollider>().enabled = true;
                drillController.drillButton3.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}
