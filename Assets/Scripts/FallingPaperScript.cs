using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingPaperScript : MonoBehaviour {

    public Sprite[] sprites;
    public float animationSpeed;
    

    public IEnumerator FallingPaper()
    {
        //destroy all game objects
        for (int i = 0; i < sprites.Length; i++)
        {
            GetComponent<Image>().sprite = sprites[i];
            yield return new WaitForSeconds(animationSpeed*Time.deltaTime);
            if (i < 30 || i > 50)
            {
                transform.Translate(new Vector3(5, 0, 0),Space.World);

            }
            else if (i < 50 || i <75)
            {
                transform.Translate(new Vector3(-5, 0, 0), Space.World);
            }

            //RectTransform rt = GetComponent<RectTransform>();
            //rt.sizeDelta = new Vector2(,);

            if (i != sprites.Length)
            {
                transform.Translate(new Vector3(0, -4f, 0), Space.World);
            }



        }

        
    }

    private void Start()
    {
        StartCoroutine(FallingPaper());
    }
}
