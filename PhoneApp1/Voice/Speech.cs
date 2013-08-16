using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Phone.Speech.Synthesis;

namespace TimerUI.Voice
{
    public static class Speech
    {
        public static SpeechSynthesizer Synthesizer;
        private static bool initialized = false;

        // Must be called before using static methods.
        public static void Initialize()
        {
            if (Speech.initialized)
            {
                return;
            }

            IEnumerable<VoiceInformation> enUSMaleVoices = from voice in InstalledVoices.All
                                                           where voice.Gender == VoiceGender.Male
                                                           && voice.Language == "en-US"
                                                           select voice;

            Speech.Synthesizer = new SpeechSynthesizer();
            Speech.Synthesizer.SetVoice(enUSMaleVoices.ElementAt(0));
            Speech.initialized = true;
        }
    }
}
