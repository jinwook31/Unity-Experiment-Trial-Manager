# Unity-Experiment-Trial-Manager

A Unity template for 2D/3D/VR/AR Behavioral Experiment. Recent HCI and cognitive, behavioral research use Unity to implement their experiment. There are already various frameworks (https://github.com/immersivecognition/unity-experiment-framework) on Github that provide a simple and faster implementation environment. However, those frameworks do not provide functions that restart from a specific point. Escpecially for VR experiment, there are various components that could occur error such as tracking, power and connection etc. Thus, if there is an error during the experiment, it needs to be done from the beginning.

To resolve this issue, we developed a framework that provides a restart function from a specific trial number (where experiment is stopped by accident or error) and separated the trial randomization process by reading the pre-generated CSV file. Also, we tried to embed the reading and save the trial data, so users could focus on the stimuli design and ease the burden on those who are not familiar with Unity programming, and save time.


## Requirement

- Unity
- Pre-generated Trial CSV (in correct format)
- Experiment Trial Manager.unitypackage

## Usage

import the package into your Unity project and include Experiment Manager.prefab to your scene.

![ex_screenshot](https://github.com/jinwook31/Unity-Experiment-Trial-Manager/blob/main/Images/prefab.JPG)

You need to type in 4 components in Trial Data (i.e., CSV File Name, Input Idx, Output Idx, Start From)

- CSV File Name : Type the name of the pre-generated trial CSV.

- Input Idx : Type the index of input size in pre-generated trial CSV. (ex. 6 for the csv format image)

- Output Idx : Type the index of output size in pre-generated trial CSV. (ex. 3 for the csv format image)

- Start From : Type the trial number that you want to start from.


After experiemnt, you can find the result CSV file in the Asset folder.


## Experiment Manager

After setting parameters, all you need to do with this template is to link your stimuli code (visual, audio, haptic etc.) with the trial info in the ExperimentManager.

Please see the example in the 'ExperimentManager.cs'. You do not need to modify other scripts in the template! (Except when you want to modify the result CSV path.)

- Save the trial results
~~~
string[] trialResult = {"2022-05-12 12:30:22","3","5642"};
bool readDone = TrialData.td.trialDone(false, trialResult);
~~~

- Read the stimuli for next trial
~~~
string stim0 = TrialData.td.getTrialInfo(2);
string stim1 = TrialData.td.getTrialInfo(3);
~~~



### Pre-generated Trial CSV Format

- Location : ./Assets/(your file name).csv

The CSV must include 'partiNumber', 'trialNumber' in 0 and 1 header index in order to initiate the experiment. Also, the slots for output needs to be empty as the 'currentTime', 'responseIntensity' and 'responseTime' figure below.

![ex_screenshot](https://github.com/jinwook31/Unity-Experiment-Trial-Manager/blob/main/Images/csv%20format.png)



## Trial Manager Work Flow

![ex_screenshot](https://github.com/jinwook31/Unity-Experiment-Trial-Manager/blob/main/Images/Trial%20Mng%20Flow.JPG)

