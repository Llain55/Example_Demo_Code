using System.Collections;
using UnityEngine;
using KKSpeech;
using InfinityEngine.Localization;
using UnityEngine.UI;

#if UNITY_STANDALONE_WIN
using UnityEngine.Windows.Speech;
#endif

public class VoiceRecognition : MonoBehaviour
{
    //Declare variables for controllers and Bool to control if the microphone is active or inactive.
    public DialogController dialogueController;
    public AnimationController animController;
    public bool micIsActive;
    public Text debugText;
    string temporaryDialogueResponse;

#if UNITY_STANDALONE_WIN
    //declare the dictationRecognizer - a system of windows speech which takes the users voice input and converts it to text.
    private DictationRecognizer dictationRecognizer;
#endif

    //Stop the recognizer if it is running. This clears the recognizer cache.
    //Initialise the variables and controllers. By default the microphone is inactive.
    void Start()
    {
        temporaryDialogueResponse = "";
        RecognizerStop();
        micIsActive = false;
        animController = FindObjectOfType<AnimationController>();
        dialogueController = FindObjectOfType<DialogController>();

        if (SpeechRecognizer.ExistsOnDevice())
        {
            debugText.text = "Speech recogniser is enabled";
            SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();
            listener.onAuthorizationStatusFetched.AddListener(OnAuthorizationStatusFetched);
            listener.onAvailabilityChanged.AddListener(OnAvailabilityChange);
            listener.onErrorDuringRecording.AddListener(OnError);
            listener.onErrorOnStartRecording.AddListener(OnError);
            listener.onFinalResults.AddListener(OnFinalResult);
            //listener.onPartialResults.AddListener(OnPartialResult);
            listener.onEndOfSpeech.AddListener(RecognizerStop);
            micIsActive = false;
            SpeechRecognizer.RequestAccess();
        }
        else
        {
            dialogueController.playerText.text = "Sorry, but this device doesn't support speech recognition";
        }
    }

    //Debugging: Enable and disable the microphone by pressing "K" key.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            //MicrophoneEnabler();
        }
    }

    //Method to start the recognizer. 
    //Print to console the recognizer is starting, and initalise a new dictationRecognizer.
    //Lambda expressions to perform the various checks.
    public void RecognizerStart()
    {
        print("starting recognizer...");
        dialogueController.playerText.text = "starting recognizer..";

    #if UNITY_STANDALONE_WIN
        dictationRecognizer = new DictationRecognizer();

        //Display the result of the user input.
        // convert the user voice input to text and ensure it is all lower case 
        //Run coRoutine to type user input to text box. Visual cue of what they said.
        //Check if the dialogue sequence is not the first sentence, if not, call the displayNextSentence() method.
        dictationRecognizer.DictationResult += (text, confidence) =>
        {
            Debug.LogFormat("Dictation result: {0}", text, confidence);
            dialogueController.txtField.text += text.ToLower();
            StopAllCoroutines();
            StartCoroutine(TypeUserInput(text));
            dialogueController.hasInputTxt = true;
            if (!dialogueController.canStart)
            {
                dialogueController.DisplayNextSentence();
            }
        };

        //Lambda expression to display what the voice recognition system thinks is being said.
        dictationRecognizer.DictationHypothesis += (text) =>
        {
            Debug.LogFormat("Dictation hypothesis: {0}", text);
        };

        //Lamda expression to display if the dictation is completed.
        dictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        };

        //Lambda expression to display if the voice recognition system hits an error. Displays the error.
        dictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
        };

        //Starts the recoginizer
        dictationRecognizer.Start();
    #endif
    }

    //Method used to control the stopping of the recognizer.
    //Print to console the recognizer is being stopped.
    //Stop the recognizer and clear its cache.
    public void RecognizerStop()
    {
        print("stopping recognizer...");

#if UNITY_STANDALONE_WIN
        dictationRecognizer.Stop();
        dictationRecognizer.Dispose();
#elif UNITY_ANDROID
        //animController.MicUI();
#endif
    }

    //CoRoutiune for displaying the text in the user input box located at the bottom of the screen. Provides visual cue on what the user has said.
    //Make sure the player text is cleared before writing  to text.
    //Split the text string input to the coroutine into a character array and iterate through the array, writing each character to screen.
    //When completed return out of the method.
    IEnumerator TypeUserInput(string input)
    {
        dialogueController.txtDialog.text = "";
        foreach (char letter in input.ToCharArray())
        {
            SpeechEngine.Speak(input);
            dialogueController.txtDialog.text += letter;
            yield return null;
        }
        dialogueController.txtDialog.text = temporaryDialogueResponse;
    }
#if UNITY_ANDROID
    public void OnFinalResult(string result)
    {
        dialogueController.playerText.text = result;
        //dialogueController.txtField.text += result.ToLower();
        micIsActive = false;
        animController.MicUI();
        dialogueController.hasInputTxt = true;

        if (!dialogueController.canStart)
        {
            dialogueController.DisplayNextSentence();
        }
        
    }

    //public void OnPartialResult(string result)
    //{
    //    txt.text = result;
    //    dialogueController.playerText.text = result;
    //    dialogueController.txtField.text += result.ToLower();
    //    //StopAllCoroutines();
    //    //StartCoroutine(TypeUserInput(txt.text));

    //    dialogueController.hasInputTxt = true;

    //    if (!dialogueController.canStart)
    //    {
    //        dialogueController.DisplayNextSentence();
    //    }
    //}

    public void OnAvailabilityChange(bool available)
    {
        micIsActive = available;
        if (!available)
        {
            dialogueController.playerText.text = "Speech recognition not available";
        }
        else
        {
            dialogueController.playerText.text = "Say something";
            StopAllCoroutines();
            StartCoroutine(TypeUserInput("Say something :-)"));
        }
    }

    public void OnAuthorizationStatusFetched(AuthorizationStatus status)
    {
        switch (status)
        {
            case AuthorizationStatus.Authorized:
                micIsActive = true;
                break;
            default:
                micIsActive = false;
                StopAllCoroutines();
                StartCoroutine(TypeUserInput("Cannot use Speech Recognition, authorization status is " + status));
                break;
        }
    }

    public void OnError(string error)
    {
        Debug.LogError(error);
        micIsActive = false;
        dialogueController.lblErrorLog.text = "Something went wrong... Try again! \n [" + error + "]";
        temporaryDialogueResponse = dialogueController.txtDialog.text;
        StopAllCoroutines();
        StartCoroutine(TypeUserInput("I'm sorry, I don't understand, could you repeat that please?"));
        animController.MicUI();
    }
    public void MicrophoneEnabler()
    {
        if (!micIsActive)
        {
            micIsActive = true;
            print(micIsActive);
            RecognizerStart();
        }
        else if (micIsActive)
        {
            micIsActive = false;
            print(micIsActive);
            RecognizerStop();
        }
        //animController.MicUI();
    }

    public void OnStartRecordingPressed()
    {
        print("Starting Speech Recognition!!");
        
        if (SpeechRecognizer.IsRecording())
        {
            micIsActive = false;
            SpeechRecognizer.StopIfRecording();
            animController.MicUI();
        }
        else
        {
            debugText.text = "Starting Speech Recognition";
            micIsActive = true;
            SpeechRecognizer.StartRecording(true);
            animController.MicUI();
        }
    }
#endif
}
