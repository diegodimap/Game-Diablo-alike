using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamegeMine : MonoBehaviour
{

    public LayerMask enemyLayer;
    public float radius = 0.5f;
    public float damageCount = 10f;

    private EnemyHealthMine enemyHealthMine;
    private bool collided;

    private void Start() {
        print("YES!");
    }

    // Update is called once per frame
    void Update()
    {
        //cria uma esfera a partir do primeiro parametro, com raio do segundo parametro e atinge os objetos da camada do 3 parametro
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach(Collider c in hits) {
            //if (c.isTrigger) {
            //    continue;
            //}
            print("porrada");
            enemyHealthMine = c.gameObject.GetComponent<EnemyHealthMine>();
            collided = true;

            if (collided) {
                enemyHealthMine.takeDamage(damageCount);
                enabled = false;
            }
        }

        
    }
}
