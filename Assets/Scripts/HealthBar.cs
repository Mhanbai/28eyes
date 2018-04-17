using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	[SerializeField] protected Text healthCounter;
	[SerializeField] protected Mask healthIndicator;
	[SerializeField] protected Mask ammoIndicator;

	// Update is called once per frame
	void Update () {
		float healthPercentage = (float)PlayerInfo.Instance.CurrentHealth() / (float)PlayerInfo.Instance.MaxHealth();
		healthIndicator.transform.localScale = new Vector3 (healthPercentage, healthIndicator.transform.localScale.y, healthIndicator.transform.localScale.z);
		healthCounter.text = PlayerInfo.Instance.CurrentHealth ().ToString();

		float ammo = (float)(PlayerInfo.Instance.AttackStyle ().Uses + PlayerInfo.Instance.ammoDiff);
		float ammoPercentage = (ammo - (float)PlayerInfo.Instance.attackCount) / ammo;
		ammoIndicator.transform.localScale = new Vector3 (ammoPercentage, ammoIndicator.transform.localScale.y, ammoIndicator.transform.localScale.z);
	}
}
