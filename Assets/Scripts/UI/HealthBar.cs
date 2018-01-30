using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	[SerializeField] protected Text counter;
	[SerializeField] protected Mask indicator;

	// Update is called once per frame
	void Update () {
		float percentage = PlayerInfo.Instance.CurrentHealth() / PlayerInfo.Instance.MaxHealth();
		indicator.transform.localScale = new Vector3 (percentage, indicator.transform.localScale.y, indicator.transform.localScale.z);
		counter.text = PlayerInfo.Instance.CurrentHealth ().ToString();
	}
}
