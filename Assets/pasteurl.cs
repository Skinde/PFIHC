using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class pasteurl : MonoBehaviour
{
    [SerializeField] TMP_InputField UItextfield;

    public void Click()
    {
        UItextfield.text = GUIUtility.systemCopyBuffer;
    }
}
