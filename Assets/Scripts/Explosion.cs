using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
	public Collider collider;
	public float maxSize;
	public float damage;
	public bool causesSlow;
	public bool causesBleed;
	public bool causesPosion;

    private void Start()
    {
        SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.explosion, 0.1f);
    }

    // Update is called once per frame
    void Update () {
		if (collider.transform.localScale.x < maxSize) {
			collider.transform.localScale += new Vector3 (0.1f, 0.1f, 0.1f);
		} else {
			GameObject.Destroy (gameObject);
		}
	}
}
