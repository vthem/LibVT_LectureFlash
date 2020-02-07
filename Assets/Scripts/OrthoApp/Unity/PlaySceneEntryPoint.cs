using UnityEngine;

namespace LectureFlash.Unity
{

    public class PlaySceneEntryPoint : MonoBehaviour
    {
        private void Start()
        {
            App.RunState("play");
        }
    }
}
