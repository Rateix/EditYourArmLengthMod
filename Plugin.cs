using UnityEngine;
using Newtilla;
using BepInEx;


namespace EditArmLength
{
    
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private bool inRoom;

        void Start()
        {
            Newtilla.Newtilla.OnJoinModded += OnModdedJoined;
            Newtilla.Newtilla.OnLeaveModded += OnModdedLeft;
        }

        private void OnEnable()
        {

        }

        private void OnDisable()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        private void Update()
        {
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


        void OnModdedJoined(string modeName)
        {
            inRoom = true;
        }


        void OnModdedLeft(string modeName)
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);

            inRoom = false;
        }
    }
}
