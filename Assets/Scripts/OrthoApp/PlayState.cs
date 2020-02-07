using System;
using System.Collections;
using UnityEngine;
using VT.Observer;

namespace LectureFlash
{
    public class PlayState : IDisposable, IModuleState
    {
        private ObservableVar<string> currentWord = new ObservableVar<string>("CurrentWord");
        private readonly PlayStateConfiguration stateConfig;

        public PlayState(PlayStateConfiguration config)
        {
            stateConfig = config;
        }

        public void Dispose()
        {
            currentWord.Dispose();
        }

        public void Run()
        {
            BeauRoutine.Routine.Start(UpdateCurrentWord());
        }

        private IEnumerator UpdateCurrentWord()
        {
            var words = stateConfig.Words;
            while (true)
            {
                foreach (var word in words)
                {
                    currentWord.Value = word;
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        public void Toggle()
        {
            throw new NotImplementedException();
        }
    }
}
