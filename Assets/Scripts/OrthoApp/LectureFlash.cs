using System;
using System.Collections.Generic;
using System.IO;
using VT.Messaging;
using VT.Observer;

namespace LectureFlash
{
    public class Words : IDisposable
    {
        public ObservableVarList<string> Lists { get; private set; } = new ObservableVarList<string>("Words.Lists");

        public Dictionary<string, ObservableVarList<string>> WordsLists { get; private set; } = new Dictionary<string, ObservableVarList<string>>();

        public void AddList(string listName, List<string> words)
        {
            ObservableVarList<string> wordList = new ObservableVarList<string>($"Words.Lists.{listName}");
            wordList.AddRange(words);
            Lists.Add(listName);
        }

        public void AddWord(string word, string listName)
        {
            ObservableVarList<string> words;
            if (WordsLists.TryGetValue(listName, out words))
            {
                if (!words.Contains(word))
                {
                    words.Add(word);
                }
            }
        }

        public void RemoveWord(string word, string listName)
        {
            ObservableVarList<string> words;
            if (WordsLists.TryGetValue(listName, out words))
            {
                words.Remove(word);
            }
        }

        public void Dispose()
        {
            Lists.Dispose();
            foreach (var wordList in WordsLists.Values)
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
            var wordsFilePath = Path.Combine(App.PersistentDataPath, "Ch.txt");
            var words = WordFileReader.GetWords(wordsFilePath);
            Words.AddList("Default", words);
        }

        public void Toggle()
        {
            new MessageListener(App.Dispatcher, App.Action.AddWord.NAME, AddWord);
            new MessageListener(App.Dispatcher, App.Action.RemoveWord.NAME, RemoveWord);
        }

        private void AddWord(Message msg)
        {
            string word;
            if (!msg.TryGet(App.Action.AddWord.P_WORD, out word))
                return;
            string list;
            if (!msg.TryGet(App.Action.AddWord.P_LIST, out list))
                return;
            Words.AddWord(word, list);
        }

        private void RemoveWord(Message msg)
        {
            string word;
            if (!msg.TryGet(App.Action.RemoveWord.P_WORD, out word))
                return;
            string list;
            if (!msg.TryGet(App.Action.RemoveWord.P_LIST, out list))
                return;
            Words.RemoveWord(word, list);
        }
    }

    public interface IModuleState
    {
        void Toggle();
    }

    public class App : IDisposable
    {
        public struct State
        {
            public const string PLAY = nameof(PLAY);
            public const string SETUP = nameof(SETUP);
        }

        public struct Var
        {
            public const string CURRENT_STATE = nameof(CURRENT_STATE);
        }

        public struct Action
        {
            public struct AddWord
            {
                public const string NAME = nameof(AddWord);
                public const string P_WORD = nameof(P_WORD);
                public const string P_LIST = nameof(P_LIST);
            }
            public struct RemoveWord
            {
                public const string NAME = nameof(RemoveWord);
                public const string P_WORD = nameof(P_WORD);
                public const string P_LIST = nameof(P_LIST);
            }
        }

        public static string PersistentDataPath { get; set; } = string.Empty;
        public static MessageDispatcher Dispatcher { get; private set; }

        private static App instance;

        private ObservableVar<string> currentState = new ObservableVar<string>(Var.CURRENT_STATE);
        private IModuleState currentStateModule;

        public App()
        {
            Dispatcher = new MessageDispatcher();
        }

        public static void Main()
        {
            instance = new App();
        }

        public static void RunState(string state)
        {
            instance?._RunState(state);
        }

        private void _RunState(string state)
        {
            if (currentState.Value == state)
            {
                return;
            }
            switch (state)
            {
                case State.SETUP:
                    currentStateModule = new SetupState();
                    currentStateModule.Toggle();
                    break;
                case State.PLAY:
                    var wordsFilePath = Path.Combine(PersistentDataPath, "Ch.txt");
                    var config = new PlayStateConfiguration()
                    {
                        Words = WordFileReader.GetWords(wordsFilePath)
                    };
                    currentStateModule = new PlayState(config);
                    currentStateModule.Toggle();
                    break;
            }
            currentState.Value = state;
        }

        public void HandleMessage(Message message)
        {

        }

        Dictionary<Type, IModuleState> states = new Dictionary<Type, IModuleState>();

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
