﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    void OnTriggerEnter(Collider obstacle)
    {
		if (obstacle.CompareTag("projectile")) {
			try {
            obstacle.GetComponent<ProjectileBehaviour>().Die();
			} catch {
			}
			}
    }
}
