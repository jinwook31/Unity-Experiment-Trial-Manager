using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    private bool trialInit = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Init Trial Data (Execute this before experiment starts)
        if(trialInit){
            TrialData.td.initTrialData();
            trialInit = false;
        }

        //Press A to Play Demo Trial
        if(Input.GetKeyDown( KeyCode.A )){
            string[] trialResult = {"2022-05-12 12:30:22","3","5642"};
            bool readDone = TrialData.td.trialDone(false, trialResult);

            if(readDone){
                Debug.Log(TrialData.td.getTrialInfo(0));
                Debug.Log(TrialData.td.getTrialInfo(1));
                Debug.Log(TrialData.td.getTrialInfo(2));
            }
        }
    }
}
