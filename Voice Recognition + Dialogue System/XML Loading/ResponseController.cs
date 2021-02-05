using System.Collections.Generic;
using System.Xml;

using UnityEngine;


public class ResponseController : MonoBehaviour
{
    [Header("File Name")]
    public string filename = "";
    [Header("List Objects")]
    public List<string> responseList;
    public List<string> rewardAmount;

    public int curResponseList = 0;
    public XmlNodeList responseNodeList;

    // Start is called before the first frame update
    void Start()
    {
        responseList = new List<string>();
        rewardAmount = new List<string>();
        rewardAmount.Clear();
        TextAsset textAsset = (TextAsset)Resources.Load(filename);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);

        XmlNode root = doc.DocumentElement;

        responseNodeList = root.SelectNodes("//Responses_Root/Response");
    }

    public void GetResponseNames()
    {
        if (curResponseList < responseNodeList.Count)
        {
            XmlNodeList innerNodeList = responseNodeList[curResponseList].SelectNodes("response");

            foreach (XmlNode innerNode in innerNodeList)
            {
                responseList.Add(innerNode.ChildNodes[0].InnerText.ToLower());
                rewardAmount.Add(innerNode.ChildNodes[1].InnerText);
            }
        }
    }
}
