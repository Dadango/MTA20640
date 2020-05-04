using UnityEngine;
using UnityEditor;
using System.IO;

public static class Logger
{
    static float previous = 0;
    static public void writeString(string input) {
        string path = "DebugLog.txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(input + " : Time: " + Time.time + " Time since last log : " + (Time.time - previous));
        writer.Close();
        previous = Time.time;
    }
}
