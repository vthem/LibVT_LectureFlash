using System;
using System.Collections;
using UnityEngine;
using VT.Observer;

namespace LectureFlash.Unity
{
    public class UnityMain : MonoBehaviour
    {
        private ObservableVar<string> currentWord;

        // Start is called before the first frame update
        void Awake()
        {
            App.PersistentDataPath = Application.persistentDataPath;
            App.Main();

            StartCoroutine(TestApp());
        }

        private IEnumerator TestApp()
        {
            bool testFinished = false;
            bool testSucceed = false;
            new VarObserver("test", App.Var.CURRENT_STATE, (_obj) =>
            {
                testSucceed = (string)_obj == App.State.SETUP;
                testFinished = true;
            });
            App.RunState(App.State.SETUP);
            yield return new WaitUntil(() => testFinished);
            Debug.Log($"Status={testSucceed}");

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                Application.OpenURL($"file://{Application.persistentDataPath}");
            }
        }

        private IEnumerator ReadEachSecond(string file)
        {
            while (true)
            {
                var words = WordFileReader.GetWords(file);
                foreach (var word in words)
                {
                    currentWord.Value = word;
                    Debug.Log($"current={currentWord.Value}");
                    yield return new WaitForSeconds(1f);
                }
            }
        }
    }
}