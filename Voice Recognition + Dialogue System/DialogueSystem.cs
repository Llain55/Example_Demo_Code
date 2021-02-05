using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("Text Objects")]
    public Text txt_Dialogue;
    public Text txt_Response;
    public InputField txtUserSpeech;

    [Header("Misc. Game Objects")]
    public GameObject player;
    public GameObject graphy;

    [Header("AI Dialogue Objects")]
    public List<string> aiInteractionOne;
    public List<string> aiInteractionTwo;
    public List<string> aiInteractionThree;
    public List<string> aiInteractionFour;
    public List<string> aiInteractionFive;
    public List<string> aiInteractionSix;
    public List<string> aiInteractionSeven;

    [Header("Potential Responses")]
    public List<string> potentialResponseOne;
    public List<string> potentialResponseTwo;
    public List<string> potentialResponseThree;
    public List<string> potentialResponseFour;
    public List<string> potentialResponseFive;
    public List<string> potentialResponseSix;
    public List<string> potentialResponseSeven;

    [Header("Variables")]
    public int currentInteractionCounter;
    public bool goForward;
    public bool goback;
    public int acceptableAccuracy;
    // Start is called before the first frame update
    void Start()
    {
        #region Error Checking
        if (txt_Dialogue == null)
        {
            Debug.LogError("Null ref on txt_Dialogue object");
        }
        if (txt_Response == null)
        {
            Debug.LogError("Null ref on txt_Response object");
        }
        if (player == null)
        {
            Debug.LogError("Null ref on player object");
        }
        if (aiInteractionOne.Count == 0)
        {
            Debug.LogError("aiinteractionOne is empty");
        }
        if (aiInteractionTwo.Count == 0)
        {
            Debug.LogError("aiinteractionTwo is empty");
        }
        if (aiInteractionThree.Count == 0)
        {
            Debug.LogError("aiinteractionThree is empty");
        }
        if (aiInteractionFour.Count == 0)
        {
            Debug.LogError("aiinteractionFour    is empty");
        }
        if (aiInteractionFive.Count == 0)
        {
            Debug.LogError("aiinteractionFive is empty");
        }
        if (aiInteractionSix.Count == 0)
        {
            Debug.LogError("aiinteractionSix is empty");
        }
        if (aiInteractionSeven.Count == 0)
        {
            Debug.LogError("aiinteractionSeven is empty");
        }
        #endregion
        currentInteractionCounter = 0;
        graphy = GameObject.FindGameObjectWithTag("Graphy");
        goForward = true;
        goback = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Oculus_CrossPlatform_PrimaryIndexTrigger"))
        {
            if (graphy.activeSelf)
            {
                graphy.SetActive(false);
            }
            else if (graphy.activeSelf == false)
            {
                graphy.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("space button Pressed!!");
            if (txtUserSpeech.text == "No that is not correct")
            {
                if (goForward)
                {
                    goback = true;
                    goForward = false;
                }
                txtUserSpeech.text = "";
            }

            PrintSentence();

            if (txt_Response.text == "")
            {
                goback = false;
                goForward = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (goForward)
            {
                goback = true;
                goForward = false;
            }
            else if (goback)
            {
                goForward = true;
                goback = false;
            }
        }
    }

    void PrintSentence()
    {
        bool canPrint = true;
        string printableText = "";
        if (goForward)
        {
            currentInteractionCounter++;
            switch (currentInteractionCounter)
            {
                case 1:
                    canPrint = CheckInput();
                    if (canPrint)
                    {
                        printableText = aiInteractionOne[Random.Range(0, aiInteractionOne.Count)];
                    }
                    else
                    {
                        printableText = "Please repeat yourself";
                        currentInteractionCounter--;
                    }
                    //currentInteractionCounter++;
                    break;
                case 2:
                    canPrint = CheckInput();
                    if (canPrint)
                    {
                        printableText = aiInteractionTwo[Random.Range(0, aiInteractionTwo.Count)];
                    }
                    else
                    {
                        printableText = "Please repeat yourself";
                        currentInteractionCounter--;
                    }
                    //currentInteractionCounter++;
                    break;
                case 3:
                    canPrint = CheckInput();
                    if (canPrint)
                    {
                        printableText = aiInteractionThree[Random.Range(0, aiInteractionThree.Count)];
                    }
                    else
                    {
                        printableText = "Please repeat yourself";
                        currentInteractionCounter--;
                    }
                    //currentInteractionCounter++;
                    break;
                case 4:
                    canPrint = CheckInput();
                    if (canPrint)
                    {
                        printableText = aiInteractionFour[Random.Range(0, aiInteractionFour.Count)];
                    }
                    else
                    {
                        printableText = "Please repeat yourself";
                        currentInteractionCounter--;
                    }
                    //currentInteractionCounter++;
                    break;
                case 5:
                    canPrint = CheckInput();
                    if (canPrint)
                    {
                        printableText = aiInteractionFive[Random.Range(0, aiInteractionFive.Count)];
                    }
                    else
                    {
                        printableText = "Please repeat yourself";
                        currentInteractionCounter--;
                    }
                    //currentInteractionCounter++;
                    break;
                case 6:
                    canPrint = CheckInput();
                    if (canPrint)
                    {
                        printableText = aiInteractionSix[Random.Range(0, aiInteractionSix.Count)];
                    }
                    else
                    {
                        printableText = "Please repeat yourself";
                        currentInteractionCounter--;
                    }
                    //currentInteractionCounter++;
                    break;
                case 7:
                    canPrint = CheckInput();
                    if (canPrint)
                    {
                        printableText = aiInteractionSeven[Random.Range(0, aiInteractionSeven.Count)];
                    }
                    else
                    {
                        printableText = "Please repeat yourself";
                        currentInteractionCounter--;
                    }
                    //currentInteractionCounter++;
                    break;
                default:
                    printableText = "No text avaiable";
                    currentInteractionCounter = 0;
                    break;
            }
        }
        else if(goback)
        {
            currentInteractionCounter--;
            switch (currentInteractionCounter)
            {
                case 1:
                    printableText = aiInteractionOne[Random.Range(0, aiInteractionOne.Count)];
                    //currentInteractionCounter--;
                    break;
                case 2:

                    printableText = aiInteractionTwo[Random.Range(0, aiInteractionTwo.Count)];
                    //currentInteractionCounter--;
                    break;
                case 3:

                    printableText = aiInteractionThree[Random.Range(0, aiInteractionThree.Count)];


                    //currentInteractionCounter--;
                    break;
                case 4:

                    printableText = aiInteractionFour[Random.Range(0, aiInteractionFour.Count)];

                    //currentInteractionCounter--;
                    break;
                case 5:
                    printableText = aiInteractionFour[Random.Range(0, aiInteractionFive.Count)];
                    //currentInteractionCounter--;
                    break;
                case 6:

                    printableText = aiInteractionSix[Random.Range(0, aiInteractionSix.Count)];

                    //currentInteractionCounter--;
                    break;
                case 7:

                    printableText = aiInteractionSeven[Random.Range(0, aiInteractionSeven.Count)];

                    //currentInteractionCounter--;
                    break;
                default:
                    printableText = "No text avaiable";
                    currentInteractionCounter = 0;
                    break;
            }
        }
        goback = false;
        goForward = true;
        txtUserSpeech.text = "";
        txt_Dialogue.text = printableText;
    }

    public bool CheckInput()
    {
        bool isValid = false;
        float percentage = 0f;
        Debug.Log(txtUserSpeech.text);
        if (txtUserSpeech.text.ToLower().Contains("go back"))
        {

            isValid = true;
            goback = true;
            goForward = false;
            return isValid;
        }
        switch (currentInteractionCounter)
        {
            case 1:
                percentage = CompareTexts(potentialResponseOne);

                if (percentage >= acceptableAccuracy)
                {
                    isValid = true;
                }
                break;
            case 2:
                percentage = CompareTexts(potentialResponseTwo);
                if (percentage >= acceptableAccuracy)
                {
                    isValid = true;
                }
                break;
            case 3:
                percentage = CompareTexts(potentialResponseThree);
                if (percentage >= acceptableAccuracy)
                {
                    isValid = true;
                }
                break;
            case 4:
                percentage = CompareTexts(potentialResponseFour);
                if (percentage >= acceptableAccuracy)
                {
                    isValid = true;
                }
                break;
            case 5:
                percentage = CompareTexts(potentialResponseFive);
                if (percentage >= acceptableAccuracy)
                {
                    isValid = true;
                }
                break;
            case 6:
                percentage = CompareTexts(potentialResponseSix);
                if (percentage >= acceptableAccuracy)
                {
                    isValid = true;
                }
                break;
            case 7:
                percentage = CompareTexts(potentialResponseSeven);
                if (percentage >= acceptableAccuracy)
                {
                    isValid = true;
                }
                break;
            default:
                isValid = false;
                break;
        }
        Debug.Log(percentage);
        return isValid;
    }
    float CompareTexts(List<string> currentDialogueResponses)
    {
#region Variables
        //Strings
        string inputString = txtUserSpeech.text;

        //Lists
        List<string> shorterString = new List<string>();

        //ints
        int stringLength = 0;
        int similarity = 0;
        int difference = 0;
        int iterator = 0;

        //floats
        float percentage = 0f;
#endregion
        foreach (string response in currentDialogueResponses)
        {
            Debug.Log("Input: " + inputString + "|| Response: "+ response);
            //Checking which string is shorter. 
            if (inputString.Length > response.Length)
            {
                //split shorter string to list
                foreach (char letter in response.ToCharArray())
                {
                    shorterString.Add(letter.ToString().ToLower());
                }
                //assign length of shorter string to integer
                stringLength = shorterString.Count;
                Debug.Log("String Length" + stringLength);
                foreach (char letter in inputString.ToCharArray())
                {
                    if (stringLength > 0)
                    {
                        if (letter.ToString().ToLower() == " " && shorterString[iterator] == " ")
                        {
                            Debug.Log("Ignore Whitespace");
                            similarity++;
                            difference++;
                        }
                        else if (letter.ToString().ToLower() != shorterString[iterator])
                        {
                            Debug.Log(letter);
                            difference++;
                        }
                        else if (letter.ToString().ToLower() == shorterString[iterator])
                        {
                            Debug.Log(letter);
                            similarity++;
                        }
                    }
                    else
                    {
                        difference++;
                    }
                    stringLength--;
                    iterator++;
                }
            }
            else if (inputString.Length < response.Length)
            {
                //split shorter string to list
                foreach (char letter in inputString.ToCharArray())
                {
                    shorterString.Add(letter.ToString().ToLower());
                }
                //assign length of shorter string to integer
                stringLength = shorterString.Count;

                foreach (char letter in response.ToCharArray())
                {
                    if (stringLength > 0)
                    {
                        if (letter.ToString().ToLower() == " " && shorterString[iterator] == " ")
                        {
                            Debug.Log("Ignore Whitespace");
                            similarity++;
                            difference++;
                        }
                        else if (letter.ToString().ToLower() != shorterString[iterator])
                        {
                            difference++;
                        }
                        else if (letter.ToString().ToLower() == shorterString[iterator])
                        {
                            similarity++;
                        }
                    }
                    else
                    {
                        difference++;
                    }
                    stringLength--;
                    iterator++;
                }
            }
            else if (inputString.Length == response.Length)
            {
                //split shorter string to list
                foreach (char letter in inputString.ToCharArray())
                {
                    shorterString.Add(letter.ToString().ToLower());
                }
                //assign length of shorter string to integer
                stringLength = shorterString.Count;

                foreach (char letter in response.ToCharArray())
                {
                    if (letter.ToString().ToLower() == " " && shorterString[iterator] == " ")
                    {
                        Debug.Log("Ignore Whitespace");
                        similarity++;
                        difference++;
                    }
                    else if (letter.ToString().ToLower() != shorterString[iterator])
                    {
                        Debug.Log("Diff Letter: " + letter.ToString() + "||" + shorterString[iterator]);
                        difference++;
                    }
                    else if (letter.ToString().ToLower() == shorterString[iterator])
                    {
                        Debug.Log("similar Letter: " + letter.ToString() + "||" + shorterString[iterator]);
                        similarity++;
                    }
                    iterator++;
                }
            }
        }
        Debug.Log("Similarity: "+ similarity + "|| Difference: "+difference);
        percentage = CalculatePercentage(similarity, difference);
        Debug.Log("Percentage: " + percentage + "%");
        return percentage;
    }

    float CalculatePercentage(float similarity, float difference)
    {
        float percentage = 0f;
        float temp1 = similarity;
        float temp2 = difference;

        if (difference > similarity && similarity != 0)
        {
            percentage = ((similarity / difference) * 100);
        }
        else if (difference < similarity && difference != 0)
        {
            percentage = 100 -((temp2 / temp1)*100);
        }
        else if (difference == 0 && similarity > 0)
        {
            percentage = 100;
        }
        else if (similarity == 0)
        {
            percentage = 0;
        }
        Debug.Log("PERCENT: " + percentage);
        return percentage;
    }
}
