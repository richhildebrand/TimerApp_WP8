using System;
using System.Linq;
using TimerUI.Interfaces;
using Windows.Phone.Speech.Recognition;

namespace TimerUI.Voice
{
    public class VoiceCommander
    {
        private readonly IStopWatch _stopWatch;

        public VoiceCommander(IStopWatch stopWatch)
        {
            _stopWatch = stopWatch;
        }

        //Keeping this class around so that we can hopefully steal the Grammer stuff from it.
        public async void ListenForStartCommand(object sender)
        {
            Speech.RecognizerUI.Recognizer.Grammars["Stop"].Enabled = false;
            Speech.RecognizerUI.Recognizer.Grammars["Start"].Enabled = true;

            Speech.RecognizerUI.Settings.ListenText = @"Say 'Start' to start the stopwatch.";
            SpeechRecognitionUIResult result = await Speech.RecognizerUI.RecognizeWithUIAsync();
            if (result.ResultStatus == SpeechRecognitionUIStatus.Succeeded && result.RecognitionResult.Text.Contains("Start"))
            {
                _stopWatch.Start();
                await Speech.Synthesizer.SpeakTextAsync("Timer Started");
                ListenForStopCommand(sender);
            }
            else { ListenForStartCommand(sender); }
        }

        public async void ListenForStopCommand(object sender)
        {
            Speech.RecognizerUI.Recognizer.Grammars["Stop"].Enabled = true;
            Speech.RecognizerUI.Recognizer.Grammars["Start"].Enabled = false;

            Speech.RecognizerUI.Settings.ListenText = @"Say 'Stop' to stop the stopwatch.";
            SpeechRecognitionUIResult result = await Speech.RecognizerUI.RecognizeWithUIAsync();
            if (result.ResultStatus == SpeechRecognitionUIStatus.Succeeded && result.RecognitionResult.Text.Contains("Stop"))
            {
                _stopWatch.Stop();
                await Speech.Synthesizer.SpeakTextAsync("Timer stopped at " + _stopWatch.Seconds + " seconds.");
            }
            else { ListenForStopCommand(sender); }
        }
    }
}
