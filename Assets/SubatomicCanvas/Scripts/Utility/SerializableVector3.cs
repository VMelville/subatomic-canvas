using System;
using UnityEngine;

namespace SubatomicCanvas.Utility
{
    [Serializable]
    public class SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        // Vector3をシリアライズ可能な形式に変換するためのコンストラクタ
        public SerializableVector3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        // SerializableVector3をVector3に変換するためのメソッド
        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }

}