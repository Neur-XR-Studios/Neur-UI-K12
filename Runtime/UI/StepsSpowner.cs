using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;
public class StepsSpowner : MonoBehaviour
{
    /*public GameObject mainStepContainer;  // Assign your prefab for Colunm_03 in the Inspector
    public GameObject subStepContainer;  // Assign your prefab for Colunm_04 in the Inspector
    public Transform StepContainer;  // Optional: Parent for instantiated Prefabs of 03
    public UIAutomationController controller;
    void Start()
    {
        // Ensure both lists have the same count
        if (controller.Colunm_03.Count == controller.Colunm_04.Count)
        {
            for (int i = 0; i < controller.Colunm_03.Count; i++)
            {
                // Instantiate Prefab03 if Colunm_03 entry is not empty
                if (!string.IsNullOrEmpty(controller.Colunm_03[i]))
                {
                    GameObject obj03 = Instantiate(mainStepContainer, Vector3.zero, Quaternion.identity, StepContainer);
                    obj03.name = "mainStep_" + i;  // Optional: Assign a name to the instance
                                                   // Additional customization or setup for obj03 can be done here
                    Transform contenttextObj = obj03.transform.GetChild(0); // Use parentheses, not brackets

                    // Optionally set the text of the textObj if it's a TMP_Text or Text component
                    TMP_Text textComponent = contenttextObj.GetComponent<TMP_Text>();
                    if (textComponent != null)
                    {
                        textComponent.text = controller.Colunm_03[i].ToUpper(); // Set the text to the current step
                    }
                    else
                    {
                        Debug.LogWarning("Text component not found on the child object.");
                    }
                }

                // Instantiate Prefab04 if Colunm_04 entry is not empty
                if (!string.IsNullOrEmpty(controller.Colunm_04[i]))
                {
                    GameObject obj04 = Instantiate(subStepContainer, Vector3.zero, Quaternion.identity, StepContainer);
                    obj04.name = "subStep_" + i;  // Optional: Assign a name to the instance

                    Transform stepCont = obj04.transform.GetChild(1); // Use parentheses, not brackets
                    TMP_Text stepContText = stepCont.GetComponent<TMP_Text>();
                    Transform stepcount = obj04.transform.GetChild(0).GetChild(0); // Use parentheses, not brackets
                    TMP_Text countText = stepcount.GetComponent<TMP_Text>();
                    if (stepContText != null)
                    {
                        stepContText.text = controller.Colunm_04[i]; // Set the text to the current step
                        countText.text = i.ToString();
                    }
                    else
                    {
                        Debug.LogWarning("Text component not found on the child object.");
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Colunm_03 and Colunm_04 do not have the same count!");
        }
    }*/
}
