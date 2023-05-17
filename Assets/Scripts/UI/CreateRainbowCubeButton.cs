using UnityEngine;
using TMPro;
using Zenject;
using System;

public class CreateRainbowCubeButton : ButtonBase
{
    [SerializeField]
    private GameObject rainbowCubeAvailablePanel;

    [Inject]
    private CubeSpawner cubeSpawner;

    private TMP_Text numAvailableText;
    private int numAvailable;

    private int numGrowth = 0;

    public int NumGrowth
    {
        get => numGrowth;
        set
        {
            if (value > 2)
            {
                rainbowCubeAvailablePanel.SetActive(true);
                touchController.gameObject.SetActive(false);
                numGrowth = 0;
                NumAvailable++;
                return;
            }
            numGrowth = value;
        }
    }

    public int NumAvailable {
        get => numAvailable; 
        set
        {
            numAvailableText.text = value.ToString();
            numAvailable = value;
        }
    }

    protected override void AwakeInternal()
    {
        rainbowCubeAvailablePanel.SetActive(false);
        numAvailableText = GetComponentInChildren<TMP_Text>();
        NumAvailable = 0;
    }

    protected override void OnClickInternal()
    {
        if (NumAvailable > 0)
        {
            cubeSpawner.SpawnRainbowCube();
            NumAvailable--;
        }
    }
}
