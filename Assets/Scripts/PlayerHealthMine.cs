using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthMine : MonoBehaviour
{
    public float health = 100f;
    private bool isShielded;
    private Animator anim;
    public Image healthBAR; 

    private void Awake() {
        anim = GetComponent<Animator>();
        healthBAR = GameObject.Find("H").GetComponent<Image>();
    }

    public bool shielded() {
        return isShielded;
    }

    public void setShielded(bool isShielded2) {
        isShielded = isShielded2;
    }

    public void takeDamage(float amount) {
        if (!isShielded) {
            health -= amount;

            healthBAR.fillAmount = health / 100;

            print("player lascou-se, health = " + health);

            if(health <= 0) {
                print("player morreu");

                anim.SetBool("Death", true);

                if(!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Death") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9) {
                    //Destroy(gameObject, 2);
                }


            }

        }
    }
}
