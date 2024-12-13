using UnityEngine;
using BepInEx;
using System;
using Photon.Pun;


namespace EditArmLength
{


    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private bool inRoom;

        void Start()
        {

        }
        void OnGameInitialized(object sender, EventArgs e)
        {

        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        private void OnModdedJoined()
        {
            if (inRoom == true) return;
            inRoom = true;
        }
        private void OnModdedLeft()
        {
            if (inRoom == false) return;
            inRoom = false;
        }

        private void Update()
        {
            if (!PhotonNetwork.InRoom) OnModdedJoined();
            else if (!NetworkSystem.Instance.GameModeString.Contains("MODDED")) OnModdedLeft();

            if (inRoom)
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0)
                {
                    GorillaLocomotion.Player.Instance.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
                    if (GorillaLocomotion.Player.Instance.transform.localScale.x > 3f)
                    {
                        GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(3f, 3f, 3f);
                    }
                }

                if (ControllerInputPoller.instance.leftControllerIndexFloat > 0)
                {
                    GorillaLocomotion.Player.Instance.transform.localScale -= new Vector3(0.02f, 0.02f, 0.02f);
                    if (GorillaLocomotion.Player.Instance.transform.localScale.x < 0.2f)
                    {
                        GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    }
                }
            }
            
        }

    }
}
