using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using player;
using System;

namespace UI
{
    public class UIgame : MonoBehaviour
    {
        public GameObject handgunIconOff, arIconOff, shotgunIconOff, handgunInconOn, arIconOn, shotgunIconOn, handgunAmmo, arAmmo, shotgunAmmo;
        public TextMeshProUGUI arAmmoLeft, shotgunAmmoLeft;
        public aimPlayer Player;

        private void Update()
        {
            if (Player.handgun)
            {
                handgunIconOff.SetActive(true);
                handgunInconOn.SetActive(true);
                handgunAmmo.SetActive(true);
                arIconOff.SetActive(true);
                arIconOn.SetActive(false);
                arAmmo.SetActive(false);
                shotgunIconOff.SetActive(true);
                shotgunIconOn.SetActive(false);
                shotgunAmmo.SetActive(false);
            }
            if(Player.shotgun)
            {
                handgunIconOff.SetActive(true);
                handgunInconOn.SetActive(false);
                handgunAmmo.SetActive(false);
                arIconOff.SetActive(true);
                arIconOn.SetActive(false);
                arAmmo.SetActive(false);
                shotgunIconOff.SetActive(true);
                shotgunIconOn.SetActive(true);
                shotgunAmmo.SetActive(true);
            }
            if (Player.ar)
            {
                handgunIconOff.SetActive(true);
                handgunInconOn.SetActive(false);
                handgunAmmo.SetActive(false);
                arIconOff.SetActive(true);
                arIconOn.SetActive(true);
                arAmmo.SetActive(true);
                shotgunIconOff.SetActive(true);
                shotgunIconOn.SetActive(false);
                shotgunAmmo.SetActive(false);
            }
            updateAmmo();
        }

        private void updateAmmo()
        {
            arAmmoLeft.SetText("" + Player.ammoAR);
            shotgunAmmoLeft.SetText("" + Player.ammoShotgun);
        }
    }
}
