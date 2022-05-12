using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialData : MonoBehaviour{
    public static TrialData td;

    // Read CSV
    private csvReader csvreader;
    List<string[]> trialData;
    public string csvFileName = ".csv";
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
        trialData = csvreader.Read(csvFileName.ToString());
    }

    public void initTrialData(){
        string[] header = trialData[lineIdx];
        DataLogger.dataLogger.writeHeader(header);
        DataLogger.dataLogger.initDataLogger(trialData.Count-1, trialData[1][0]);  //Get total trial number & participant ID

        trialInput = new string[inputIdx];
        trialOutput = new string[outputIdx];
        
        // END Experiment
        if(!trialDone(true, null)){
            Quit();
        }

        Debug.Log("EXPERIMENT TRIALS INITIATED (Read Values with getTrialInfo(idx))");
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
            string[] curLine = trialData[++lineIdx];
            
            if(string.Compare(curLine[trialInput.Length], "") == 0 && int.Parse(curLine[1]) >= startFrom){
                return curLine;
            }
        }
        return null;
    }

    public bool trialDone(bool init, string[] output){
        if(!init) writeData2CSV(output);

        string[] line = readNextLine();

        if(line == null){    //EXP ENDED
            //Quit();
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

