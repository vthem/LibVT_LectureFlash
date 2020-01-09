using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VT.Observer;

namespace LectureFlash
{
    public class Values
    {
        public const string CURRENT_WORD = "CURRENT_WORD";
    }


    public class Main : MonoBehaviour
    {
        private ObservableVar<string> currentWord;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log($"File path={Application.persistentDataPath}");
            currentWord = new ObservableVar<string>(Values.CURRENT_WORD);
            var counter = new ObservableVar<int>("Counter");
            //new VT.Front.FrontObjectModifier<Text, string>(
            //    "LectureFlash.CurrentWord",
            //    Values.CURRENT_WORD,
            //    (text, ov, nv) =>
            //    {
            //        text.text = nv;
            //    });
            //new VT.Front.FrontAction(
            //    messageName: "IncrementCounter",
            //    messageHandler: (msg) =>
            //    {
            //        counter.Value = counter.Value + 1;
            //    })
            //    .AddClick("Button.Increment");

            //new VT.Front.FrontObjectModifier<Text, int>(
            //    frontObjectName: "LectureFlash.CurrentWord",
            //    varName: Values.CURRENT_WORD,
            //    modify: (text, ov, nv) =>
            //    {
            //        text.text = nv.ToString("D4");
            //    });
            // VT.Front.ViewSystem.Click("Button.Close");
            //new VT.Front.WaitForFrontMessage("Click", (_msg) =>
            //{
            //    Debug.Log("click!!");
            //});

            // StartCoroutine(ReadEachSecond(Path.Combine(Application.persistentDataPath, "Ch.txt")));

            //SceneManager.LoadScene("Setup", LoadSceneMode.Additive);
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