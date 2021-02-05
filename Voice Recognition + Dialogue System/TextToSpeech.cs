using UnityEngine;
using UnityEngine.UI;
using InfinityEngine.Localization;
using InfinityEngine.Extensions;
using System.Linq;

public class TextToSpeech : MonoBehaviour
{
    [Header("Dropdown Components")]
    public Dropdown localeDropdown;
    public Dropdown voicesDropdown;
    public Dropdown engineDropdown;

    [Header("Misc. Components")]
    public GameObject settingsPanel;

    private TTSEngine[] engines;
    private Locale[] locales;
    private Voice[] voices;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        InitaliseTTS();
#endif
    }

    private void InitaliseTTS()
    {
        SpeechEngine.AddCallback(() =>
        {
            locales = Locale.AllLocales;
            locales = locales.OrderBy(go => go.Informations).ToArray();
            voices = SpeechEngine.AvaillableVoices;
            voices = voices.OrderBy(go => go.Name).ToArray();
            engines = SpeechEngine.AvailableEngines;
            engines = engines.OrderBy(go => go.Label).ToArray();

            localeDropdown.AddOptions(locales.Select(elem => elem.Informations).ToList());
            voicesDropdown.AddOptions(voices.Select(elem => elem.Name).ToList());
            engineDropdown.AddOptions(engines.Select(elem => elem.Label).ToList());

            localeDropdown.onValueChanged.AddListener(value =>
            {
                SpeechEngine.SetLanguage(locales[value]);
                voicesDropdown.ClearOptions();
                voices = SpeechEngine.AvaillableVoices;
                voicesDropdown.AddOptions(voices.Select(elem => elem.Name).ToList());
            });

            voicesDropdown.onValueChanged.AddListener(value =>
            {
                SpeechEngine.SetVoice(voices[value]);
            });

            engineDropdown.onValueChanged.AddListener(value =>
            {
                SpeechEngine.SetEngine(engines[value]);
            });
        });
    }

    public void Speak(string input)
    {
        SpeechEngine.Speak(input);
    }
}
