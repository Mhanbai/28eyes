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
    float timer = 0.0f;
    Animator anim;
    public float clipLength = 2.0f;
    public int sound = 0;

    private void Start()
    {
        anim = GetComponent<Animator>();
        SoundManager.Instance.SFX.PlayOneShot(SoundManager.Instance.explosionSounds[sound], 0.5f);
    }

    // Update is called once per frame
    void Update () {
		if (timer < clipLength) {
            timer += Time.deltaTime;
		} else {
			GameObject.Destroy (gameObject);
		}
	}
}
