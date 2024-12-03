using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using System.Timers;

// NOTE: this is a reference file. not actually used for the game.

public class UImanager : MonoBehaviour
{
    public int healthValue;
    public TextMeshProUGUI healthValueText;
    public TextMeshProUGUI stamValueText;
    public int stamValue;
    // Start is called before the first frame update
    void Start()
    {
        stamValueText.text = stamValue.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        healthValueText.text = healthValue.ToString();
    }
    public void DamagePlayer()
    {
        healthValue -= 300;
    }
    public void poisonPlayer()
    {
        healthValue -= 50;
    }
    public void healPlayer()
    {
        healthValue += 200;
    }
}
