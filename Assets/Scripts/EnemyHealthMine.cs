using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthMine : MonoBehaviour
{

    public float health = 100f;
    private Animator anim;
    private GameObject playerTarget;
    public Image healthIMG;
    public bool isBoss = false;

    private void Awake() {
        anim = gameObject.transform.parent.gameObject.GetComponent<Animator>();
        playerTarget = GameObject.FindGameObjectWithTag("Player");

        //TODO mudar aqui para vários inimigos health bar

        /*
        if(tag == "Boss") {
            healthIMG = GameObject.Find("FGB").GetComponent<Image>();
        } else {
            healthIMG = GameObject.Find("FG").GetComponent<Image>();
        }
        */
    }

    public void takeDamage(float amount) {
        health -= amount;

        healthIMG.fillAmount = health / 100f;

        print("Enemy took Damage, health = " + health);

        if (health <= 0) {
            anim.SetBool("Death", true);
            if (!isBoss) { 
                gameObject.transform.parent.gameObject.GetComponent<EnemyControlEasy>().isAlive = false;
            }
            playerTarget.GetComponent<PlayerMove>().isAlive = false;
            //Destroy(gameObject.transform.parent.gameObject);
        }
    }    

}
