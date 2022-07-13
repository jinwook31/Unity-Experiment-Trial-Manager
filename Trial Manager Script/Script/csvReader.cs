using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class csvReader : MonoBehaviour
{
    public List<string[]> Read(string file)
    {
        var list = new List<string[]>();
        StreamReader sr = new StreamReader(Application.dataPath + "/" + file);

        bool endOfFile = false;
        while (!endOfFile){

            string data_String = sr.ReadLine();

            if (data_String == null){
                endOfFile = true;
                break;
            }

            var data_values = data_String.Split(','); //string, string Type
            list.Add(data_values);
        }

        return list;
    }
}
