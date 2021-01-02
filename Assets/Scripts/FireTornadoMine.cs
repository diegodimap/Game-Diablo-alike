﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTornadoMine : MonoBehaviour
{

    public LayerMask enemyLayer;
    public float radius = 0.5f;
    public float damageCount = 10f;
    public GameObject fireExplosion;

    private EnemyHealthMine enemyHealthMine;
    private bool collided;
    private float speed = 3f;
    
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.LookRotation(player.transform.forward);
    }

    private void Update() {
        move();
        checkForDamage();
    }

    void move() {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    // Update is called once per frame
    void checkForDamage()
    {
        //cria uma esfera a partir do primeiro parametro, com raio do segundo parametro e atinge os objetos da camada do 3 parametro
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider c in hits) {
            print("fire");
            enemyHealthMine = c.gameObject.GetComponent<EnemyHealthMine>();
            collided = true;
        }

        if (collided) {
            enemyHealthMine.takeDamage(damageCount);
            Vector3 temp = transform.position;
            temp.y += 2f;
            Instantiate(fireExplosion, temp, Quaternion.identity);
            Destroy(gameObject);
            //enabled = false;
        }
    }
}