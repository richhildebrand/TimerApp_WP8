using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;

namespace TimerUI
{
    public static class Speech
    {
        public static SpeechRecognizer recognizer;
        public static SpeechSynthesizer synthesizer;
        public static SpeechRecognizerUI recognizerUI;

        private static bool initialized = false;

        // Must be called before using static methods.
        public static void Initialize()
        {
            if (Speech.initialized)
                return;

            Speech.recognizer = new SpeechRecognizer();
            Speech.synthesizer = new SpeechSynthesizer();
            Speech.recognizerUI = new SpeechRecognizerUI();
            Speech.recognizerUI.Settings.ReadoutEnabled = false;

            // Sets the en-US male voice.
            IEnumerable<VoiceInformation> enUSMaleVoices = from voice in InstalledVoices.All
                                                           where voice.Gender == VoiceGender.Male
                                                           && voice.Language == "en-US"
                                                           select voice;

            Speech.synthesizer.SetVoice(enUSMaleVoices.ElementAt(0));

            Speech.initialized = true;
        }
    }
}
