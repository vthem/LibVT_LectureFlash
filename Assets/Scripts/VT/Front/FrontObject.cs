using UnityEngine;

namespace VT.Front
{
    public class FrontObject : MonoBehaviour
    {
        public string FrontObjectName => frontObjectName;

        public string Identifier => identifier;

        [SerializeField]
        private string frontObjectName = string.Empty;

        private string identifier;
        private bool register = false;

        public void Register()
        {
            if (!register)
            {
                register = true;
                identifier = $"{frontObjectName}#{gameObject.GetInstanceID()}";
                FrontSystem.Logger.Debug($"Register FrontObject Name={frontObjectName} Id={identifier}");
                FrontSystem.Objects.Add(FrontObjectName, this);
            }
        }

        private void Start()
        {
            Register();
        }

        private void OnDestroy()
        {
            FrontSystem.Logger.Debug($"Unregister FrontObject Name={frontObjectName} Id={identifier}");
            FrontSystem.Objects.Remove(FrontObjectName, this);
        }
    }
}