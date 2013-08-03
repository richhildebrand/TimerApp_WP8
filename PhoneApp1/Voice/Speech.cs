using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;

namespace TimerUI.Voice
{
    public static class Speech
    {
        public static SpeechRecognizer Recognizer;
        public static SpeechSynthesizer Synthesizer;
        public static SpeechRecognizerUI RecognizerUI;

        private static bool initialized = false;

        // Must be called before using static methods.
        public static void Initialize()
        {
            if (Speech.initialized)
                return;

            Speech.Recognizer = new SpeechRecognizer();
            Speech.Recognizer.Settings.InitialSilenceTimeout = (TimeSpan)IsolatedStorageSettings.ApplicationSettings["VoiceTimeout"];


            Speech.Synthesizer = new SpeechSynthesizer();
            Speech.RecognizerUI = new SpeechRecognizerUI();
            Speech.RecognizerUI.Settings.ReadoutEnabled = false;
            Speech.RecognizerUI.Settings.ShowConfirmation = false;

            Speech.RecognizerUI.Recognizer.Grammars.AddGrammarFromList("Start", new string[] { "Start" });
            Speech.RecognizerUI.Recognizer.Grammars.AddGrammarFromList("Stop", new string[] { "Stop" });

            // Sets the en-US male voice.
            IEnumerable<VoiceInformation> enUSMaleVoices = from voice in InstalledVoices.All
                                                           where voice.Gender == VoiceGender.Male
                                                           && voice.Language == "en-US"
                                                           select voice;

            Speech.Synthesizer.SetVoice(enUSMaleVoices.ElementAt(0));

            Speech.initialized = true;
        }
    }
}
