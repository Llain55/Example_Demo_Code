using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [Header("File Name")]
    public string filename = "";
    [Header("List Objects")]
    public List<string> dialogueList;
    public List<string> minValueList;
    public List<string> maxValueList;

    public int curDialogueList = 0;
    public XmlNodeList nodeList;

    DialogueManager diaControl;
    // Start is called before the first frame update
    void Start()
    {
        diaControl = FindObjectOfType<DialogueManager>();
        dialogueList = new List<string>();
        minValueList = new List<string>();
        maxValueList = new List<string>();
        TextAsset textAsset = (TextAsset)Resources.Load(filename);
        //TextAsset textAsset = (TextAsset)Resources.Load("DialogueQuestions");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);


        XmlNode root = doc.DocumentElement;

        nodeList = root.SelectNodes("//Dialogue_Root/Dialogue");
        GetDialogueNames();
    }

    public void GetDialogueNames()
    {
        if (curDialogueList < nodeList.Count)
        {
            XmlNodeList innerNodeList = nodeList[curDialogueList].SelectNodes("dialogue");

            foreach (XmlNode innerNode in innerNodeList)
            {
                dialogueList.Add(innerNode.ChildNodes[0].InnerText);
                minValueList.Add(innerNode.ChildNodes[1].InnerText);
                maxValueList.Add(innerNode.ChildNodes[2].InnerText);
            }
        }

        foreach (string sentence in dialogueList)
        {
            diaControl.lbl_Dialogue.text += sentence + '\n';
        }
    }
}
