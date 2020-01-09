using UnityEngine;

namespace LectureFlash.Unity
{
    public class SetupSceneEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            var lectureFlash = LectureFlash.Run();
            lectureFlash.RunState<SetupState>();
        }
    }
}