using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;
public class AutomationUnitTestScript : EditorWindow
{
    private bool? csvFilePresent = null;
    private bool? LandingPageImagePresent = null;
    private bool? Row1ContentPresent = null;
    private bool? objectNameInfoCountMatches = null;

    private bool? substepVoiceoverClipMatch = null;
    private bool? promptPromptClipMatch = null;
    private bool? lastSubstepTypeCon = null;

    private bool? voiceoverClipsExist = null;
    private bool? promptClipsExist = null;

    private string csvPath = "Assets/StreamingAssets/csvData_Eng.csv";
    //private bool? csvDataIntegrity = null;
    //private bool? csvAndEventMatch = null;

    [MenuItem("Tools/K12/Unit Test")]
    public static void ShowWindow()
    {
        GetWindow<AutomationUnitTestScript>("Automation Checklist");
    }

    private void OnGUI()
    {
        GUILayout.Label("Automation Checklist", EditorStyles.boldLabel);

        if (GUILayout.Button("Run Checks"))
        {
            RunAllChecks();
        }

        GUILayout.Space(10);

        DisplayCheck("CSV File Present", csvFilePresent);
        DisplayCheck("Object Content Present", Row1ContentPresent);
        DisplayCheck("Object Info Count Matches", objectNameInfoCountMatches);
        DisplayCheck("Landing page image present", LandingPageImagePresent);

        DisplayCheck("Sub-Step and Voiceover Clip Match", substepVoiceoverClipMatch);
        DisplayCheck("Prompt and Prompt Clip Match", promptPromptClipMatch);
        DisplayCheck("Last Sub-Step Type is 'Con'", lastSubstepTypeCon);

        DisplayCheck("All VoiceOver Clips Present", voiceoverClipsExist);
        DisplayCheck("All Prompt Clips Present", promptClipsExist);
    }

    private void DisplayCheck(string label, bool? checkResult)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(label);

        // Show symbols only if the checkResult is not null
        if (checkResult.HasValue)
        {
            GUI.color = checkResult.Value ? Color.green : Color.red;
            GUILayout.Label(checkResult.Value ? "✓" : "✗", GUILayout.Width(20));
            GUI.color = Color.white; // Reset color to default
        }

        GUILayout.EndHorizontal();
    }

    private void RunAllChecks()
    {
        LandingPageImagePresent = CheckLandingPageImage();
        csvFilePresent = CheckCSVFile();
        Row1ContentPresent = CheckRow1ContentPresence();
        objectNameInfoCountMatches = CheckObjectNameInfoCount();

        List<string> subSteps, types, voiceoverClips, prompts, promptClips;
        ParseColumns(out subSteps, out types, out voiceoverClips, out prompts, out promptClips);

        substepVoiceoverClipMatch = CheckColumnLengthsMatch(subSteps, voiceoverClips);
        promptPromptClipMatch = CheckColumnLengthsMatch(prompts, promptClips);
        lastSubstepTypeCon = CheckLastSubstepType(types, subSteps);


        voiceoverClipsExist = CheckAudioFilesExist(voiceoverClips, "VoiceOver Clip");
        promptClipsExist = CheckAudioFilesExist(promptClips, "Prompt Clip");

    }
    private bool CheckAudioFilesExist(List<string> clipNames, string clipType)
    {
        bool allClipsExist = true;
        string audioFolderPath = "Assets/Resources/Audios/";

        foreach (string clipName in clipNames)
        {
            if (!string.IsNullOrEmpty(clipName))
            {
                string clipPath = Path.Combine(audioFolderPath, clipName + ".mp3"); // Adjust extension as needed
                if (!File.Exists(clipPath))
                {
                    Debug.LogError($"{clipType} missing: {clipName} at {clipPath}");
                    allClipsExist = false;
                }
            }
        }

        return allClipsExist;
    }
    private void ParseColumns(out List<string> subSteps, out List<string> types, out List<string> voiceoverClips, out List<string> prompts, out List<string> promptClips)
    {
        subSteps = new List<string>();
        types = new List<string>();
        voiceoverClips = new List<string>();
        prompts = new List<string>();
        promptClips = new List<string>();

        string[] lines = File.ReadAllLines(csvPath);

        for (int i = 2; i < lines.Length; i++) // Start from row 2, ignoring headers and unwanted rows
        {
            List<string> cells = ParseCSVLine(lines[i]);

            if (cells.Count >= 8) // Ensure there are enough columns
            {
                types.Add(cells[1].Trim());         // Column B
                prompts.Add(cells[2].Trim());       // Column C
                subSteps.Add(cells[4].Trim());      // Column E
                voiceoverClips.Add(cells[5].Trim()); // Column F
                promptClips.Add(cells[6].Trim());   // Column G
            }
        }
    }

    private bool CheckColumnLengthsMatch(List<string> list1, List<string> list2)
    {
        // Remove empty entries from both lists before comparing lengths
        List<string> filteredList1 = list1.FindAll(item => !string.IsNullOrEmpty(item));
        List<string> filteredList2 = list2.FindAll(item => !string.IsNullOrEmpty(item));

        bool lengthsMatch = filteredList1.Count == filteredList2.Count;

        if (!lengthsMatch)
        {
            Debug.LogError("Column lengths do not match between required fields.");
        }

        return lengthsMatch;
    }

    private bool CheckLastSubstepType(List<string> types, List<string> subSteps)
    {
        int lastSubstepIndex = subSteps.FindLastIndex(item => !string.IsNullOrEmpty(item));
        if (lastSubstepIndex == -1 || lastSubstepIndex >= types.Count)
        {
            Debug.LogError("Could not find a valid last sub-step or Type column data.");
            return false;
        }

        bool lastTypeIsCon = types[lastSubstepIndex].EndsWith("Con");

        if (!lastTypeIsCon)
        {
            Debug.LogError("Last sub-step type is not 'Con'.");
        }

        return lastTypeIsCon;
    }
    private bool CheckRow1ContentPresence()
    {
        string[] lines = File.ReadAllLines(csvPath);

        if (lines.Length < 2)
        {
            Debug.LogError("CSV file does not have enough rows.");
            return false;
        }

        // Extract row 1 using the custom CSV parser
        List<string> cells = ParseCSVLine(lines[1]);

        if (cells.Count < 7)
        {
            Debug.LogError("CSV file does not have enough columns.");
            return false;
        }

        string typeText = cells[1].Trim(); // Cell B content
        string moduleName = cells[2].Trim(); // Cell C content 
        string LandingIntroText = cells[4].Trim(); // Cell E content
        string objectNames = cells[6].Trim();  // Cell G content
        string objectInfo = cells[7].Trim();   // Cell H content

        bool contentPresent = !string.IsNullOrEmpty(typeText) && typeText == "Intro" && 
            !string.IsNullOrEmpty(moduleName) &&!string.IsNullOrEmpty(LandingIntroText) &&
            !string.IsNullOrEmpty(objectNames) && !string.IsNullOrEmpty(objectInfo);

        if (!contentPresent)
        {
            Debug.LogError("Content is missing in cells B or C or E or G or H. Row 1.");
        }

        return contentPresent;
    }

    private bool CheckObjectNameInfoCount()
    {
        string[] lines = File.ReadAllLines(csvPath);

        // Extract row 1 using the custom CSV parser
        List<string> cells = ParseCSVLine(lines[1]);
        string objectNames = cells[6];  // Cell G content
        string objectInfo = cells[7];   // Cell H content

        // Split by commas for object names and by '*' for object info
        string[] objectNamesArray = objectNames.Split(new char[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
        string[] objectInfoArray = objectInfo.Split(new char[] { '*' }, System.StringSplitOptions.RemoveEmptyEntries);

        // Trim whitespace from each entry
        for (int i = 0; i < objectNamesArray.Length; i++)
            objectNamesArray[i] = objectNamesArray[i].Trim();

        for (int i = 0; i < objectInfoArray.Length; i++)
            objectInfoArray[i] = objectInfoArray[i].Trim();

        bool countMatches = objectNamesArray.Length == objectInfoArray.Length;

        if (!countMatches)
        {
            Debug.LogError("Object names and information counts do not match.");
        }

        return countMatches;
    }

    private List<string> ParseCSVLine(string line)
    {
        List<string> cells = new List<string>();
        bool inQuotes = false;
        string cell = "";

        foreach (char c in line)
        {
            if (c == '\"')
            {
                inQuotes = !inQuotes; // Toggle the inQuotes flag
            }
            else if (c == ',' && !inQuotes)
            {
                // End of cell, add it to the list
                cells.Add(cell.Trim());
                cell = "";
            }
            else
            {
                // Append character to the current cell
                cell += c;
            }
        }

        // Add the last cell
        if (!string.IsNullOrEmpty(cell))
        {
            cells.Add(cell.Trim());
        }

        return cells;
    }





    /* private bool CheckObjectContentPresence()
     {
         string csvPath = "Assets/StreamingAssets/csvData.csv";
         string[] lines = File.ReadAllLines(csvPath);

         if (lines.Length < 2)
         {
             Debug.LogError("CSV file does not have enough rows.");
             return false;
         }

         // Extract row 1, cell G and H (assuming 0-based index)
         string[] cells = lines[1].Split(',');

         if (cells.Length < 8)
         {
             Debug.LogError("CSV file does not have enough columns.");
             return false;
         }

         string objectNames = cells[6].Trim();  // Cell G content (index 6)
         string objectInfo = cells[7].Trim();   // Cell H content (index 7)

         bool contentPresent = !string.IsNullOrEmpty(objectNames) && !string.IsNullOrEmpty(objectInfo);

         if (!contentPresent)
         {
             Debug.LogError("Object names or information is missing in cells G or H.");
         }

         return contentPresent;
     }

     private bool CheckObjectInfoCount()
     {
         string csvPath = "Assets/StreamingAssets/csvData.csv";
         string[] lines = File.ReadAllLines(csvPath);

         // Extract row 1, cell G and H (assuming 0-based index)
         string[] cells = lines[1].Split(',');
         Debug.Log($"{cells[0]} \n {cells[1]} \n {cells[2]} \n {cells[3]} \n {cells[4]} \n {cells[5]} \n {cells[6]} \n {cells[7]}");
         string objectNames = cells[6];  // Cell G content (index 6)
         string objectInfo = cells[7];   // Cell H content (index 7)

         // Split by commas for object names and by '*' for object info
         string[] objectNamesArray = objectNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
         string[] objectInfoArray = objectInfo.Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);

         // Trim whitespace from each entry
         for (int i = 0; i < objectNamesArray.Length; i++)
             objectNamesArray[i] = objectNamesArray[i].Trim();

         for (int i = 0; i < objectInfoArray.Length; i++)
             objectInfoArray[i] = objectInfoArray[i].Trim();

         bool countMatches = objectNamesArray.Length == objectInfoArray.Length;

         if (!countMatches)
         {
             Debug.LogError("Object names and information counts do not match.");
         }

         return countMatches;
     }*/

    private bool CheckCSVFile()
    {
        bool fileExists = System.IO.File.Exists(csvPath);
        if (!fileExists)
        {
            Debug.LogError("CSV file is missing at: " + csvPath);
        }
        return fileExists;
    }
    private bool CheckLandingPageImage()
    {
        bool fileExists = System.IO.File.Exists("Assets/Resources/Images/Landing_Icon.png");
        if (!fileExists)
        {
            if (!System.IO.File.Exists("Assets/Resources/Images/Landing_Icon.jpg"))
            {
                Debug.LogError("Icon file is missing at: Assets/Resources/Images/Landing_Icon");                
            }
            else
            {
                fileExists = true;
            }
        }
        else
        {
            fileExists = true;
        }
        return fileExists;

    }

    private bool CheckPublicReferences()
    {
        bool allReferencesAttached = true;
        var allGameObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (var obj in allGameObjects)
        {
            var components = obj.GetComponents<MonoBehaviour>();
            foreach (var comp in components)
            {
                if (comp == null) continue;
                var fields = comp.GetType().GetFields(
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.Public);

                foreach (var field in fields)
                {
                    var value = field.GetValue(comp);
                    if (value == null)
                    {
                        Debug.LogError($"Null reference found in {comp.GetType().Name} on {obj.name}, Field: {field.Name}");
                        allReferencesAttached = false;
                    }
                }
            }
        }
        return allReferencesAttached;
    }    
}
