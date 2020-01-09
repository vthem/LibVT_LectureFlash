using UnityEngine;

namespace LectureFlash.Unity
{

    public class PlaySceneEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            var lectureFlash = LectureFlash.Run();
            lectureFlash.RunState(ApplicationStates.Play);
        }
    }
}
