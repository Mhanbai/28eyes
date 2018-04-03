using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIcon : MonoBehaviour {
	CharController player;
	public UnityEngine.UI.Image icon;
	public UnityEngine.UI.Text text;

	// Update is called once per frame
	void Update () {
		if (PlayerInfo.Instance.IsAttackReady () == true) {
			text.text = (PlayerInfo.Instance.AttackStyle ().Uses - PlayerInfo.Instance.attackCount).ToString();
			text.color = Color.red;
		} else {
			text.text = (PlayerInfo.Instance.AttackStyle().ReloadTime - PlayerInfo.Instance.reloadTimer).ToString();
			text.color = Color.white;
		}
	}
}
