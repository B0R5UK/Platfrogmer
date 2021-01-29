using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private string myLevelName;

    private void Start()
    {
        Button myButton = GetComponent<Button>(); 
        if (PlayerPrefs.HasKey(myLevelName) && PlayerPrefs.GetInt(myLevelName) == 1)
            myButton.interactable = true;
        else
            myButton.interactable = false;
    }
}
