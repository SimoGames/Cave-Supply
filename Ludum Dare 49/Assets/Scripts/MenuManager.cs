using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Canvas menu;
    public Canvas about;
    public Canvas h2p;
    public Animation transition;
    public AudioSource click;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayClick()
    {
        h2p.enabled = false;
        about.enabled = false;
        menu.enabled = false;
        click.Play();
        transition.Play("FadeIn");
        StartCoroutine(playTimer());
    }
    public void OnAboutClick()
    {
        h2p.enabled = false;
        about.enabled = true;
        menu.enabled = false;
        click.Play();
    }
    public void OnH2pClick()
    {
        h2p.enabled = true;
        about.enabled = false;
        menu.enabled = false;
        click.Play();
    }
    public void OnBackClick()
    {
        h2p.enabled = false;
        about.enabled = false;
        menu.enabled = true;
        click.Play();
    }
    public void OnQuitClick()
    {
        click.Play();
        Application.Quit();
    }
    IEnumerator playTimer()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }
}
