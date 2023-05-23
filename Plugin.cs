using System;
using UnityEngine;
using Utilla;
using UnityEngine.XR;
using System.ComponentModel;
using BepInEx;


namespace Mod1
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [Description("HauntedModMenu")]



    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private bool inRoom;
        private bool isRightHoldingTrigger;
        private bool isLeftHoldingTrigger;
        private bool willBeOn;

        private void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when Start is called */
            /* Put code in OnGameInitialized to avoid null references */

            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        private void OnEnable()
        {
            willBeOn = true;
            HarmonyPatches.ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            willBeOn = false;

            HarmonyPatches.RemoveHarmonyPatches();
        }

        private void OnGameInitialized(object sender, EventArgs e)
        {
            /* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
        }

        private void Update()
        {
            if (inRoom)
            {
                if (willBeOn)
                {
                    InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.triggerButton, out isRightHoldingTrigger);
                    InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.triggerButton, out isLeftHoldingTrigger);
                    if (isRightHoldingTrigger)
                    {
                        GorillaLocomotion.Player.Instance.transform.localScale *= 1.02f;
                        if (GorillaLocomotion.Player.Instance.transform.localScale.x > 3f)
                            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(3f, 3f, 3f);
                    }

                    if (isLeftHoldingTrigger)
                    {
                        GorillaLocomotion.Player.Instance.transform.localScale *= -0.98f;
                        if (GorillaLocomotion.Player.Instance.transform.localScale.x < 0.2f)
                            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    }



                }


            }
        }

        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {


            inRoom = true;
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);


            inRoom = false;
        }
    }
}
