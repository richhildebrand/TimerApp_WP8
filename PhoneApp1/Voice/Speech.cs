using System;
using System.Collections.Generic;
using System.Linq;
using TimerUI.AppInit;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;

namespace TimerUI.Voice
{
    public static class Speech
    {
        public static SpeechSynthesizer Synthesizer;
        public static SpeechRecognizer Recognizer;
        private static bool initialized = false;

        // Must be called before using static methods.
        public static void Initialize()
        {
            if (Speech.initialized)
            {
                return;
            }

            Recognizer = GetSpeechRecognizer();
            Synthesizer = GetSpeechSynthesizer();
            Speech.initialized = true;
        }

        private static SpeechRecognizer GetSpeechRecognizer()
        {
            var recognizer = new SpeechRecognizer();
            recognizer.Settings.InitialSilenceTimeout = SettingsManager.Get<TimeSpan>(SettingsManager.Settings.VoiceTimeout);

            string[] startCommands = SettingsManager.Get<List<string>>(SettingsManager.Settings.StartVoiceCommands).ToArray<string>();
            recognizer.Grammars.AddGrammarFromList("Start", startCommands);

            string[] stopCommands = SettingsManager.Get<List<string>>(SettingsManager.Settings.StopVoiceCommands).ToArray<string>();
            recognizer.Grammars.AddGrammarFromList("Stop", stopCommands);

            string[] lapCommands = { "Lap" };
            recognizer.Grammars.AddGrammarFromList("Lap", lapCommands);

            string[] resetCommand = { "Clear" };
            recognizer.Grammars.AddGrammarFromList("Reset", resetCommand);
            

            return recognizer;
        }

        private static SpeechSynthesizer GetSpeechSynthesizer()
        {
            var speechSynthesizer = new SpeechSynthesizer();

            IEnumerable<VoiceInformation> enUSMaleVoices = from voice in InstalledVoices.All
                                                           where voice.Gender == VoiceGender.Male &&
                                                                 voice.Language == "en-US"
                                                           select voice;

            speechSynthesizer.SetVoice(enUSMaleVoices.ElementAt(0));
            return speechSynthesizer;
        }
    }
}