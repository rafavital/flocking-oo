using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Profiling;

public class ProfilerAnalysis : MonoBehaviour
{
    int frameCount = 0;
    void Start() {
        StopAllCoroutines();
        frameCount = 0;
        StartCoroutine(DataLogger());
    }

    IEnumerator DataLogger() {        
        while (true)
        {
            string filepath = @"C:\Users\Rafa\Desktop\profile";
            Profiler.logFile = filepath;
            Profiler.enableBinaryLog = true;
            Profiler.enabled = true;
            for (int i = 0; i < 300; i++)
            {
                yield return new WaitForEndOfFrame();
                if (!Profiler.enabled)
                    Profiler.enabled = true;
            }
            frameCount++;
        }
    }
}
