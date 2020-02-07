using UnityEngine;

namespace LectureFlash.Unity
{
    public class SetupSceneEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            App.RunState(App.State.SETUP);
        }
    }
}