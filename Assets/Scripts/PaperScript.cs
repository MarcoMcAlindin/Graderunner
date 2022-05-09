using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperScript: MonoBehaviour {
    public float dieAtSpeedBelow = 8f; // The paper will die when it reaches a velocity below this figure
    Animator anim;
    Rigidbody2D rigid;
    public bool dying = false;
	void Start () {
        this.anim = gameObject.GetComponent<Animator>();
        this.rigid = gameObject.GetComponent<Rigidbody2D>();

    }

    public void Die()
    {
        if (!anim) return;
        dying = true;
        anim.SetTrigger("Dead");
        Destroy(gameObject, 1f);
    }
	

	void Update () {
        if (rigid.velocity.magnitude < dieAtSpeedBelow && !dying) Die();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!dying) Die();
    }
}
