﻿using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityUtils {
    public static class Helpers {
        static readonly Dictionary<float, WaitForSeconds> WaitForSecondsDict = new(100, new FloatComparer());

        /// <summary>
        /// Returns a WaitForSeconds object for the specified duration. </summary>
        /// <param name="seconds">The duration in seconds to wait.</param>
        /// <returns>A WaitForSeconds object.</returns>
        public static WaitForSeconds GetWaitForSeconds(float seconds) {
            if (seconds < 1f / Application.targetFrameRate) return null;

            if (!WaitForSecondsDict.TryGetValue(seconds, out var forSeconds)) {
                forSeconds = new WaitForSeconds(seconds);
                WaitForSecondsDict[seconds] = forSeconds;
            }

            return forSeconds;
        }

        /// <summary>
        /// Generate a random id string
        /// </summary>
        /// <returns></returns>
        public static string GenerateID()
        {
            return Guid.NewGuid().ToString();
        }

        class FloatComparer : IEqualityComparer<float> {
            public bool Equals(float x, float y) => Mathf.Abs(x - y) <= Mathf.Epsilon;
            public int GetHashCode(float obj) => obj.GetHashCode();
        }

        /// <summary>
        /// Clears the console log in the Unity Editor.
        /// </summary
#if UNITY_EDITOR        
        public static void ClearConsole() {
            var assembly = Assembly.GetAssembly(typeof(SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method?.Invoke(new object(), null);
        }
#endif        
    }
}
