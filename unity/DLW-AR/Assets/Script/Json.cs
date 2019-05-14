using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Json
{
    [System.Serializable]
    public class ScaleWindows
    {
        public string X;
        public string Y;
        public string Z;
    }
    [System.Serializable]
    public class ScaleAndroid
    {
        public string X;
        public string Y;
        public string Z;
    }
    [System.Serializable]
    public class ScaleiOS
    {
        public string X;
        public string Y;
        public string Z;
    }
    [System.Serializable]
    public class PositionWindows
    {
        public string X;
        public string Y;
        public string Z;
    }
    [System.Serializable]
    public class PositionAndroid
    {
        public string X;
        public string Y;
        public string Z;
    }
    [System.Serializable]
    public class PositioniOS
    {
        public string X;
        public string Y;
        public string Z;
    }
    [System.Serializable]
    public class RotationWindows
    {
        public string X;
        public string Y;
        public string Z;
    }
    [System.Serializable]
    public class RotationAndroid
    {
        public string X;
        public string Y;
        public string Z;
    }
    [System.Serializable]
    public class RotationiOS
    {
        public string X;
        public string Y;
        public string Z;
    }
    [System.Serializable]
    public class RootObject
    {
        public string urlWindows;
        public string urlAndroid;
        public string urliOS;
        public uint Version;
        public ScaleAndroid ScaleAndroid;
        public ScaleWindows ScaleWindows;
        public ScaleiOS ScaleiOS;
        public PositionWindows PositionWindows;
        public PositionAndroid PositionAndroid;
        public PositioniOS PositioniOS;
        public RotationWindows RotationWindows;
        public RotationAndroid RotationAndroid;
        public RotationiOS RotationiOS;
    }
}
