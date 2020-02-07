using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using LectureFlash;
using VT.Observer;
using System;
using System.Diagnostics;

namespace Tests
{
    public class EditorTestMain
    {
        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator SwitchToSetup()
        {
            VT.Observer.ObserverSystem.Initialize();
            App.PersistentDataPath = Application.persistentDataPath;
            App.Main();

            yield return Validate("Switch to SETUP", App.Var.CURRENT_STATE, App.State.SETUP, () => App.RunState(App.State.SETUP));
            //Debug.Log($"Status={testSucceed}");

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [UnityTest]
        public IEnumerator AddWord()
        {
            VT.Observer.ObserverSystem.Initialize();
            App.PersistentDataPath = Application.persistentDataPath;
            App.Main();

            yield return Validate("Switch to SETUP", App.Var.CURRENT_STATE, App.State.SETUP, () => App.RunState(App.State.SETUP));
            //Debug.Log($"Status={testSucceed}");

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        private IEnumerator Validate<T>(string strInfo, string varName, T expected, Action funcCall)
        {
            bool end = false;
            new VarObserver(strInfo, App.Var.CURRENT_STATE, (_obj) =>
            {
                Assert.AreSame(expected, _obj, strInfo);
                end = true;
            }, notifyOnCreate: false);
            funcCall.Invoke();
            var starTime = Time.realtimeSinceStartup;
            while (!end && (Time.realtimeSinceStartup - starTime) < 1f)
            {
                yield return null;
            }
            if ((Time.realtimeSinceStartup - starTime) >= 1f)
            {
                Assert.Fail($"{strInfo} has timeout on var {varName}");
            }
        }
    }
}
