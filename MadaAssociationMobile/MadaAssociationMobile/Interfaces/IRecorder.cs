using System;
using System.Collections.Generic;
using System.Text;

namespace MadaAssociationMobile.Interfaces
{
    public interface IAudioRecorder
    {
        void StartRecording();
        void StopRecording();
        void StartPlaying();
    }
}
