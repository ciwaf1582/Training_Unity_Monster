using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject MainCanvars;
    [SerializeField] public GameObject AdventureCanvars;
    [SerializeField] public GameObject IndexCanvars;
    [SerializeField] public GameObject FightCanvars;

    [SerializeField] public GameObject Ground_Stage;
    [SerializeField] public GameObject Jungle_Stage;
   
    public GameObject Mountain_Stage;
    public GameObject Snow_Stage;

    public void adventuerShow()
    {
        AdventureCanvars.SetActive(true);
        MainCanvars.SetActive(false);
    } // adventure show


}
