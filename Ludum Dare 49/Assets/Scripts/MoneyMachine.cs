using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class MoneyMachine : MonoBehaviour
{
    public Slider conveyorBar;
    public Slider pressBar;
    public float pressDecrease = 0.15f;
    public float conveyorDecrease = 0.15f;
    public PlayerController hp;
    public Animation anim;
    public ParticleSystem explosion1;
    public ParticleSystem explosion2;
    public ParticleSystem smoke;
    public Light2D furnaceLight;
    public MovingBetweenRooms cam;
    public bool hasExploded = false;
    public SpriteRenderer rend;
    public Image fill1;
    public Image fill2;
    public Image fillBackground1;
    public Image fillBackground2;
    public Light2D coinLight;
    public SpriteRenderer coin;
    public Text sliderText1;
    public Text sliderText2;
    public AudioSource explosion;
    public Timer timer;
    public Animation cameraShake;
    public Animation speedUp;
    public AudioSource speedUpSFX;
    public Animation transition;
    void Start()
    {
        StartCoroutine(firstDecrease());
        StartCoroutine(secondDecrease());
        StartCoroutine(thirdDecrease());
        StartCoroutine(fourthDecrease());
    }
    void Update()
    {
        //Decreasing the stability of different parts
        pressBar.value -= pressDecrease * Time.deltaTime;
        conveyorBar.value -= conveyorDecrease * Time.deltaTime;

        if (conveyorBar.value == 0 && hp.hasDied == false && hasExploded == false || pressBar.value == 0 && hp.hasDied == false && hasExploded == false)
        {
            sliderText1.enabled = false;
            sliderText2.enabled = false;
            hasExploded = true;
            cameraShake.Play("CameraShake");
            fill1.enabled = false;
            fill2.enabled = false;
            timer.StopTimer();
            fillBackground1.enabled = false;
            fillBackground2.enabled = false;
            coinLight.enabled = false;
            coin.enabled = false;
            smoke.Stop();
            if (cam.IsInLeft == true)
            {
                cam.camAnim.Play("PressToMachine");
            }
            if (cam.IsInRight == true)
            {
                cam.camAnim.Play("ConveyorToMachine");
            }
            anim.Play("Explode");
            StartCoroutine(explosionTimer());
        }
    }
    void FixedUpdate()
    {

    }



    //Making the game harder as time goes by
    IEnumerator firstDecrease()
    {
        yield return new WaitForSeconds(30);
        cameraShake.Play("CameraShake");
        speedUp.Play("SpeedUp");
        speedUpSFX.Play();
        conveyorDecrease = 0.15f;
        pressDecrease = 0.15f;
    }
    IEnumerator secondDecrease()
    {
        yield return new WaitForSeconds(60);
        cameraShake.Play("CameraShake");
        speedUp.Play("SpeedUp");
        speedUpSFX.Play();
        conveyorDecrease = 0.20f;
        pressDecrease = 0.20f;
    }
    IEnumerator thirdDecrease()
    {
        yield return new WaitForSeconds(150);
        cameraShake.Play("CameraShake");
        speedUp.Play("SpeedUp");
        speedUpSFX.Play();
        conveyorDecrease = 0.25f;
        pressDecrease = 0.25f;
    }
    IEnumerator fourthDecrease()
    {
        yield return new WaitForSeconds(300);
        cameraShake.Play("CameraShake");
        speedUp.Play("SpeedUp");
        speedUpSFX.Play();
        conveyorDecrease = 0.3f;
        pressDecrease = 0.3f;
    }

    IEnumerator explosionTimer()
    {
        yield return new WaitForSeconds(3);
        cameraShake.Play("CameraShake");
        anim.Stop("Explode");
        explosion.Play();
        rend.enabled = false;
        furnaceLight.enabled = false;
        explosion1.Play();
        explosion2.Play();
        StartCoroutine(replayTimer());
    }
    IEnumerator replayTimer()
    {
        yield return new WaitForSeconds(3);
        transition.Play("FadeIn");
        StartCoroutine(menuTimer());
    }
    IEnumerator menuTimer()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Menu");
    }
}
