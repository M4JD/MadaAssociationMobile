using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectCareMobile.Interfaces
{
    public interface IAudioRecorder
    {
        void StartRecording();
        void StopRecording();
        void StartPlaying();
    }
}
