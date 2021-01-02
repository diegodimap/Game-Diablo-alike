using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealScriptMine : MonoBehaviour
{

    public float healAmount = 20f;

    // Start is called before the first frame update
    void Start()
    {
        float healthb = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthMine>().health;

        if (healthb < 75) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthMine>().health += healAmount;
        } else {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthMine>().health = 100;
        }
        float health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthMine>().health;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthMine>().healthBAR.fillAmount = health / 100;

        print("player's health =" + health);
    }

    
}
