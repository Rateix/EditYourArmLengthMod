using UnityEngine;
using Utilla;
using BepInEx;


namespace Mod1
{
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        private bool inRoom;

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
                if (ControllerInputPoller.instance.rightGrab)
                {
                    GorillaLocomotion.Player.Instance.transform.localScale += new Vector3(0.02f, 0.02f, 0.02f);
                    if (GorillaLocomotion.Player.Instance.transform.localScale.x > 3f)
                    {
                        GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(3f, 3f, 3f);
                    }
                }

                if (ControllerInputPoller.instance.leftGrab)
                {
                    GorillaLocomotion.Player.Instance.transform.localScale -= new Vector3(0.02f, 0.02f, 0.02f);
                    if (GorillaLocomotion.Player.Instance.transform.localScale.x < 0.2f)
                    {
                        GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    }
                }
            }
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;
        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);

            inRoom = false;
        }
    }
}
