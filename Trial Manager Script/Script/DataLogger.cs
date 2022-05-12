using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataLogger : MonoBehaviour{
    public static DataLogger dataLogger;

    // New CSV Info
    private string testStartTime, participantNum;
    private string[] header;
    
    // DataLogger param
    private string[][] result; 
    private int countRes = 0, allTrialNum;

    //Start is called before the first frame update
    void Start(){
        if(dataLogger && dataLogger != this)
            Destroy(this);
        else
            dataLogger = this;
    }

    public void initDataLogger(int trialNum, string participantNum){
        this.participantNum = participantNum;
        this.allTrialNum = trialNum;
        result = new string[allTrialNum][];
    }

    //Add line to result array
    public void writeLine(string[] line){
        printArray(line);

        if(countRes < allTrialNum)
            result[countRes++] = line;
        
        makeCSV();
    }

    public void writeHeader(string[] header){
        this.header = (string[])header.Clone();
    }

    //Save it to CSV File
    private void makeCSV() {
        if(countRes == 1)   testStartTime = Timer.timer.currentTime4Filepath();

		using (var writer = new CsvFileWriter(Application.dataPath + "/" + testStartTime + "_" + participantNum + "_Experiment Result.csv")){
			List<string> columns = new List<string>(header);  // making Index Row
			writer.WriteRow(columns);
			columns.Clear();

            for(int i = 0; i < countRes; i++){
                for(int j = 0; j < result[i].Length; j++){
                    columns.Add(result[i][j]);
                }
			    writer.WriteRow(columns);
			    columns.Clear();
            }
		}
	}

    private void printArray(string[] line){
        string res = "";
        for(int i=0; i<line.Length; i++){
            res += line[i] + " / ";
        }
        Debug.Log(res);
    }
}
