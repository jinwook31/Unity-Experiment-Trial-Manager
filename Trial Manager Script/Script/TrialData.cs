using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrialData : MonoBehaviour{
    public static TrialData td;

    // Read CSV
    private csvReader csvreader;
    List<string[]> trialData;
    public string csvFileName = "Trial_P01";
    private int lineIdx = 0;

    // Input Trial Data, Output Trial Data
    private string[] trialInput, trialOutput;
    public int inputIdx = 6, outputIdx = 3, startFrom = 0;

    // Start is called before the first frame update
    void Start(){
        if(td && td != this)
            Destroy(this);
        else
            td = this;

        // Read Pre-generated CSV File
        csvreader = new csvReader();
        trialData = csvreader.Read(csvFileName.ToString() + ".csv");
    }

    public bool initTrialData(){
        string[] header = trialData[lineIdx];
        DataLogger.dataLogger.writeHeader(header);
        DataLogger.dataLogger.initDataLogger(trialData.Count, trialData[1][0]);  //Get total trial number & participant ID

        trialInput = new string[inputIdx];
        trialOutput = new string[outputIdx];
        
        // END Experiment
        if(!trialDone(true, null)){
            Quit();
        }

        Debug.Log("EXPERIMENT TRIALS INITIATED (Read Values with getTrialInfo(idx))");
        return true;
    }

    private void writeData2CSV(string[] output){
        trialOutput = output;

        var fullLine = new List<string>();
        fullLine.AddRange(trialInput);
        fullLine.AddRange(trialOutput);
        string[] res = fullLine.ToArray();

        // Write CSV line (lineIdx)
        DataLogger.dataLogger.writeLine(res);
    }

    private string[] readNextLine(){
        while(lineIdx < trialData.Count){
            string[] curLine = null;

            try{                
                curLine = trialData[++lineIdx];
            }catch(ArgumentOutOfRangeException e){
                Debug.Log(e);
                Debug.Log("Check the length of the trial CSV Input and Output Idx!!");
                Quit();
            }
            
            // Check the index of CSV, where to start from. (In case of restarted exp.)
            if(string.Compare(curLine[trialInput.Length], "") == 0 && int.Parse(curLine[1]) >= startFrom){
                return curLine;
            }
        }
        return null;
    }

    public bool trialDone(bool initExp, string[] output){
        if(!initExp) writeData2CSV(output);

        string[] line = readNextLine();

        if(line == null){    //EXP ENDED
            Quit();
            return false;
        }else{
            // Assign line to Input arrays
            trialInput = new string[trialInput.Length];
            for(int i=0; i<trialInput.Length; i++){
                trialInput[i] = line[i];
            }
            trialOutput = new string[trialOutput.Length];
            return true;    // Data is updated to Next Trial (Good to go!)
        }
    }

    public string getTrialInfo(int idx){
        return trialInput[idx];
    }

    //END EXPERIMENT
    private static void Quit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

