using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(MyScript))]
public class ConditionalFieldAttribute : Editor
{
    public class MyScript : MonoBehaviour
    {
        public bool hideBool;
        public bool disableBool;
        public string someString;
        public Color someColor = Color.white;
        public int someNumber = 0;
    }

}