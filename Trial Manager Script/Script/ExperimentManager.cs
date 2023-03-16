using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExperimentManager : MonoBehaviour
{
    private bool trialInit = true;
    public bool isTrialEnd = false;  // Replace this with your own function/param that check trial is done!

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Init Trial Data (Execute this before experiment starts)
        if(trialInit){
            TrialData.td.initTrialData();
            // First Trial Info
            string stim0 = TrialData.td.getTrialInfo(2);
            string stim1 = TrialData.td.getTrialInfo(3);
            trialInit = false;
        }

        // When Trial is done
        if (isTrialEnd){
            isTrialEnd = false;

            // save the trial result & get next trial line
            string[] trialResult = {"2022-05-12 12:30:22","3","5642"};
            bool readDone = TrialData.td.trialDone(false, trialResult);

            // if there is next trial, reset environment and stimuli settings
            if (readDone){
                resetEnvironment();
                // Next Trial Info
                string stim0 = TrialData.td.getTrialInfo(2);
                string stim1 = TrialData.td.getTrialInfo(3);
            }
        }
    }

    private void resetEnvironment()
    {
        // Reset Experiment Enviroment
    }
}

