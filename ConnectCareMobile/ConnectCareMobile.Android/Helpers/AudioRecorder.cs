using Android.App;
using Android.Content;
using Android.Media;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.ConstraintLayout.Helper.Widget;
using ConnectCareMobile.Droid.Helpers;
using ConnectCareMobile.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioRecorder))]
namespace ConnectCareMobile.Droid.Helpers
{
    public class AudioRecorder : IAudioRecorder
    {
        MediaPlayer _player;
        MediaRecorder _recorder;
        string fileFullPath;
        string GetFileNameForRecording()
        {
            return Path.Combine(GetRootDir(), $"{Guid.NewGuid()}.3GPP");
        }
        string GetRootDir()
        {
            string fileName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            return fileName;
        }

        [Obsolete]
        public void StartRecording()
        {
            try
            {
                _recorder = new MediaRecorder();
                _recorder.SetAudioSource(AudioSource.Mic);
                _recorder.SetOutputFormat(OutputFormat.ThreeGpp);
                fileFullPath = this.GetFileNameForRecording();
                _recorder.SetOutputFile(fileFullPath);
                _recorder.SetAudioEncoder((AudioEncoder.AmrNb));

                _recorder.Prepare();
            }
            catch (Exception ioe)
            {
                Log.Error("StartRecording", ioe.Message);
            }
            _recorder.Start();

        }

        public void StopRecording()
        {
            if (_recorder == null)
            {
                return;
            }
            _recorder.Stop();
            _recorder.Release();
            _recorder = null;
            if (!string.IsNullOrEmpty(fileFullPath))
                if (File.Exists(fileFullPath))
                {
                    //_startPlaying = true;
                }
        }
        public void StartPlaying()
        {
            _player = new MediaPlayer();
            try
            {
                _player.SetDataSource(fileFullPath);
                _player.Prepare();
                _player.Start();
            }
            catch (IOException e)
            {
                Log.Error("StartPlaying", "There was an error trying to start the MediaPlayer!");
            }
        }

    }
}