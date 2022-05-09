using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Texture unselectedTexture;
    public Texture selectedTexture;
    RawImage image;
    public GameObject fadeOut;
    public string gameScene = "Game";
    public Image black;

    public RawImage menuImage;
    public Texture menuLeaderboard;
    public Texture menuNormal;
    public Texture menuQuit;

    public IEnumerator QuitFadeScene(float t, Image black)
    {
        fadeOut.SetActive(true);
        black.color = new Color(black.color.r, black.color.g, black.color.b, 0);
        while (black.color.a < 1.0f)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a + (Time.deltaTime / t));
            yield return null;
        }
        Application.Quit();

    }

    public void QuitGame()
    {
        QuitHovered();
        StartCoroutine(QuitFadeScene(0.7f, black));
    }

    public void OpenLeaderboard()
    {
        LeaderboardHovered();
    }

    public IEnumerator FadeScene(float t, string scene)
    {
        fadeOut.SetActive(true);
        black.color = new Color(black.color.r, black.color.g, black.color.b, 0);
        while (black.color.a < 1.0f)
        {
            black.color = new Color(black.color.r, black.color.g, black.color.b, black.color.a + (Time.deltaTime / t));
            yield return null;
        }
        SceneManager.LoadScene(scene, LoadSceneMode.Single);

    }


    public void SoundState()
    {
        GameObject soundObject = GameObject.FindGameObjectWithTag("Sound");
        SoundScript soundScript = soundObject.GetComponent<SoundScript>();
        if (soundScript.soundState == true)
        {

            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            camera.GetComponent<AudioListener>().enabled = false;
        }
    }

    public void Start()
    {
        image = gameObject.GetComponent<RawImage>();
    }

    void Update()
    {
        SoundState();
    }

    public void MouseD()
    {
        StartCoroutine(FadeScene(0.7f, gameScene));
        
    }

    public void MouseEn()
    {
        image.texture = selectedTexture;
    }

    public void MouseEx()
    {
        image.texture = unselectedTexture;
    }

    public void MouseHeld()
    {
        image.texture = selectedTexture;
    }

    public void MouseUp()
    {
        image.texture = unselectedTexture;
    }

    public void LeaderboardHovered()
    {
        menuImage.texture = menuLeaderboard;
    }

    public void QuitHovered()
    {
        menuImage.texture = menuQuit;
    }

    public void MenuUnhovered()
    {
        menuImage.texture = menuNormal;
    }
}
