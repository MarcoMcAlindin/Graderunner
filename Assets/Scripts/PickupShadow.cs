using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupShadow : MonoBehaviour {

    public Vector2 offset = new Vector2(0.1f, 0.1f);
    public Vector3 scale = new Vector3(0.5f, 0.5f, 1);

    private SpriteRenderer cast;
    private SpriteRenderer shadow;

    private Transform castTransform;
    private Transform shadowTransform;

    public Material shadowMaterial;
    public Color ShadowColor;

    public void Initialise ()
    {
        castTransform = transform;
        shadowTransform = new GameObject().transform;
        shadowTransform.parent = castTransform;
        shadowTransform.gameObject.name = "pickupshadow";

        cast = GetComponent<SpriteRenderer>();
        shadow = shadowTransform.gameObject.AddComponent<SpriteRenderer>();

        shadow.sortingLayerName = cast.sortingLayerName;
        shadow.sortingOrder = cast.sortingOrder - 1;

        shadow.material = shadowMaterial;
        shadow.color = ShadowColor;
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate () {

        shadowTransform.localScale = castTransform.localScale + scale;
        shadowTransform.position = new Vector2(castTransform.position.x + offset.y, castTransform.position.y + offset.y);

        shadow.sprite = cast.sprite;

	}
}
