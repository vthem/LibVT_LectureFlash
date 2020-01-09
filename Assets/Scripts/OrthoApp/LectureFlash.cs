using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VT.Observer;

namespace LectureFlash
{
    public class Words : IDisposable
    {
        public ObservableVarList<string> Lists { get; private set; } = new ObservableVarList<string>("Words.Lists");

        public List<ObservableVarList<string>> WordsLists { get; private set; } = new List<ObservableVarList<string>>();

        public void AddList(string listName, List<string> words)
        {
            ObservableVarList<string> wordList = new ObservableVarList<string>($"Words.Lists.{listName}");
            wordList.AddRange(words);
            Lists.Add(listName);
        }

        public void Dispose()
        {
            Lists.Dispose();
            foreach(var wordList in WordsLists)
            {
                wordList.Dispose();
            }
        }
    }

    public class SetupState : IModuleState
    {
        public Words Words { get; private set; } = new Words();

        public SetupState()
        {
            var wordsFilePath = Path.Combine(Application.persistentDataPath, "Ch.txt");
            var words = WordFileReader.GetWords(wordsFilePath);
            Words.AddList("Default", words);
        }

        public void Toggle()
        {
        }
    }

    public interface IModuleState
    {
        void Toggle();
    }

    public class LectureFlash : IDisposable
    {
        private static LectureFlash instance;

        private ObservableVar<ApplicationStates> currentState = new ObservableVar<ApplicationStates>("LectureFlashState");

        public static LectureFlash Run()
        {
            if (instance == null)
            {
                instance = new LectureFlash();
            }

            return instance;
        }

        public void RunState(ApplicationStates state)
        {
            if (currentState.Value == state)
            {
                return;
            }
            switch (state)
            {
                case ApplicationStates.Setup:
                    break;
                case ApplicationStates.Play:
                    var wordsFilePath = Path.Combine(Application.persistentDataPath, "Ch.txt");
                    var config = new PlayStateConfiguration()
                    {
                        Words = WordFileReader.GetWords(wordsFilePath)
                    };
                    new PlayState(config).Run();
                    break;
            }
        }

        Dictionary<Type, IModuleState> states = new Dictionary<Type, IModuleState>();

        public void RunState<State>() where State : IModuleState, new()
        {
            if (!states.TryGetValue(typeof(State), out IModuleState stateObj))
            {
                stateObj = new State();
                states.Add(typeof(State), stateObj);
            }
            stateObj.Toggle();
        }

        public static void Quit()
        {
            if (instance == null)
            {
                return;
            }
            instance.Dispose();
        }

        public void Dispose()
        {
            instance = null;
        }
    }
}
