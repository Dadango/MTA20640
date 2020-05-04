using UnityEngine;
using UnityEditor;
using System.IO;

public class Logger
{
    static public void writeString(string input) {
        string path = "Assets/Resources/";
        string filename = "DebugLog.txt";
        StreamWriter writer = new StreamWriter(path + filename, true);
        writer.WriteLine(input);
        writer.Close();
    }
}
