using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    //Audio Sources
    [SerializeField] AudioSource bookOpen, bookClose, pageFlip;
    [SerializeField] AudioSource cuttingVegetable, cuttingMeat, cuttingOtherSlices, cuttingOtherDices;
    [SerializeField] AudioSource pickingInstrument, pickingBread, pickingVegetable1, pickingVegetable2, pickingMeat;
    [SerializeField] AudioSource settingMeat, settingBread, settingInstrument, settingVegetable;
    [SerializeField] AudioSource fridgeOpen, fridgeClose;
    [SerializeField] AudioSource sizzling, bell, wrongBell, buttonClick;

    public void PlaySizzling()
    {
        sizzling.Play();
    }
    public void StopSizzling()
    {
        sizzling.Stop();
    }
    public void PlayBookOpen()
    {
        bookOpen.Play();
    }
    public void PlayBookClosed()
    {
        bookClose.Play();
    }
    public void PlayPageFlip()
    {
        pageFlip.Play();
    }
    public void PlayCuttingVegetable()
    {
        cuttingVegetable.Play();
    }
    public void PlayCuttingMeat()
    {
        cuttingMeat.Play();
    }
    public void PlayCuttingOtherSlices()
    {
        cuttingOtherSlices.Play();
    }
    public void PlayCuttingOtherDices()
    {
        cuttingOtherDices.Play();
    }
    public void PlayPickingInstrument()
    {
        pickingInstrument.Play();
    }
    public void PlayPickingBread()
    {
        pickingBread.Play();
    }
    public void PlayPickingMeat()
    {
        pickingMeat.Play();
    }
    public void PlayPickingVegetable1()
    {
        pickingVegetable1.Play();
    }
    public void PlayPickingVegetable2()
    {
        pickingVegetable2.Play();
    }
    public void PlaySettingMeat()
    {
        settingMeat.Play();
    }
    public void PlaySettingBread()
    {
        settingBread.Play();
    }
    public void PlaySettingInstrument()
    {
        settingInstrument.Play();
    }
    public void PlaySettingVegetable()
    {
        settingVegetable.Play();
    }
    public void PlayFridgeOpen()
    {
        fridgeOpen.Play();
    }
    public void PlayFridgeClose()
    {
        fridgeClose.Play();
    }
    public void PlayBell()
    {
        bell.Play();
    }
    public void PlayWrongBell()
    {
        wrongBell.Play();
    }
    public void PlayButton()
    {
        buttonClick.Play();
    }






}//END OF CLASS
