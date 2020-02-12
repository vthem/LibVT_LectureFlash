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
            App.RunState(App.State.SETUP);
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