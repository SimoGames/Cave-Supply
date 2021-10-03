using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class Upgrading : MonoBehaviour
{
    public bool isHoldingConveyor = false;
    public bool isHoldingPress = false;
    public Slider conveyorBar;
    public Slider pressBar;
    public SpriteRenderer conveyorSprite;
    public SpriteRenderer pressSprite;
    public bool canPickUpConveyor;
    public bool canPickUpPress;
    public bool canUpgradeConveyor;
    public bool canUpgradePress;
    public Light2D itemLight;
    public AudioSource pickUpSFX;
    public AudioSource upgradeSFX;
    public Animation cameraShake;
    public Canvas E;
    public MoneyMachine machine;
    public ParticleSystem upgradeParticle;
    void Start()
    {
        conveyorSprite.enabled = false;
        pressSprite.enabled = false;
        E.enabled = false;
    }

    void Update()
    {
        //Upgrading items
        if (canUpgradePress == true && Input.GetKeyDown(KeyCode.E))
        {
            cameraShake.Play("CameraShake");
            upgradeSFX.Play();
            pressBar.value = 10;
            upgradeParticle.Play();
            isHoldingPress = false;
            isHoldingConveyor = false;
            canUpgradeConveyor = false;
            canUpgradePress = false;
        }
        else if (canUpgradeConveyor == true && Input.GetKeyDown(KeyCode.E))
        {
            cameraShake.Play("CameraShake");
            upgradeSFX.Play();
            conveyorBar.value = 10;
            upgradeParticle.Play();
            isHoldingConveyor = false;
            isHoldingPress = false;
            canUpgradeConveyor = false;
            canUpgradePress = false;
        }

        //Picking up items
        if(canPickUpPress == true && Input.GetKeyDown(KeyCode.E))
        {
            cameraShake.Play("CameraShake");
            pickUpSFX.Play();
            isHoldingPress = true;
            isHoldingConveyor = false;
            canPickUpConveyor = false;
            canPickUpPress = false;
        }
        else if (canPickUpConveyor == true && Input.GetKeyDown(KeyCode.E))
        {
            cameraShake.Play("CameraShake");
            pickUpSFX.Play();
            isHoldingConveyor = true;
            isHoldingPress = false;
            canPickUpConveyor = false;
            canPickUpPress = false;
        }

        //Holding coin press
        if(isHoldingPress == true)
        {
            pressSprite.enabled = true;
        }
        else
        {
            pressSprite.enabled = false;
        }

        //Holding conveyor
        if (isHoldingConveyor == true)
        {
            conveyorSprite.enabled = true;
        }
        else
        {
            conveyorSprite.enabled = false;
        }
        
        //Turning the shine of items off and on
        if(isHoldingConveyor == true || isHoldingPress == true)
        {
            itemLight.enabled = true;
        }
        else
        {
            itemLight.enabled = false;
        }

        if(canPickUpPress == true && machine.hasExploded == false || canPickUpConveyor == true && machine.hasExploded == false || canUpgradeConveyor == true && machine.hasExploded == false || canUpgradePress == true && machine.hasExploded == false)
        {
            E.enabled = true;
        }
        else
        {
            E.enabled = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Upgrading different parts
        if (collision.tag == "Machine" && isHoldingConveyor == true)
        {
            canUpgradeConveyor = true;
        }
        else
        {
            canUpgradeConveyor = false;
        }

        if (collision.tag == "Machine" && isHoldingPress == true)
        {
            canUpgradePress = true;
        }
        else
        {
            canUpgradePress = false;
        }


        //Picking up different parts
        if (collision.tag == "Press" && isHoldingPress == false)
        {
            canPickUpPress = true;
        }
        else
        {
            canPickUpPress = false;
        }
        if (collision.tag == "Conveyor" && isHoldingConveyor == false)
        {
            canPickUpConveyor = true;
        }
        else
        {
            canPickUpConveyor = false;
        }
    }
}
