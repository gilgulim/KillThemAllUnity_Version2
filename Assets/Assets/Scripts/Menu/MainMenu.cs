using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CompleteProject
{
    public class MainMenu : MonoBehaviour
    {

        #region Consts
        private const string TYPE_NAME = "KillThemAll";
        private const string GAME_NAME = "KillThemAllRoom";
        private const string LOCAL_HOST_IP = "127.0.0.1";
        #endregion

        #region Private Members
        private HostData[] hostList;
        #endregion

        #region Public Members
        public Canvas canvas;
        public GameObject playerPrefab;
        public Button btnStartServer;
        public Button btnJoinServer;
        public Button btnExit;
        public Camera camera;
        public EnemyManager mainEnemyManager;
        public Canvas hudCanvas;
        public Image damageImage;
        public Slider healthSlider;
        public Slider ammoSlider;
        #endregion


        #region UI Actions
        public void StartServer()
        {
            string ipAddress = string.Empty;

            if(!SelectServer.UsingUnityServer)
            {
                if (IsValidIPAddress(SelectServer.RemoteServerIP))
                {
                    ipAddress = SelectServer.RemoteServerIP;
                }
                else
                {
                    ipAddress = LOCAL_HOST_IP;
                }
            }

            MasterServer.ipAddress = ipAddress;
            MasterServer.port = 23466;
            Network.natFacilitatorIP = ipAddress;
            Network.natFacilitatorPort = 50005;

            Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
            MasterServer.RegisterHost(TYPE_NAME, GAME_NAME);

            btnJoinServer.enabled = false;
            btnExit.enabled = false;
        }

        public void JoinServer()
        {
            string ipAddress = string.Empty;

            if (!SelectServer.UsingUnityServer)
            {
                if (IsValidIPAddress(SelectServer.RemoteServerIP))
                {
                    ipAddress = SelectServer.RemoteServerIP;
                }
                else
                {
                    ipAddress = LOCAL_HOST_IP;
                }
            }

            MasterServer.ipAddress = ipAddress;
            MasterServer.port = 23466;
            Network.natFacilitatorIP = ipAddress;
            Network.natFacilitatorPort = 50005;


            MasterServer.RequestHostList(TYPE_NAME);
            btnJoinServer.enabled = false;
            btnExit.enabled = false;
            btnStartServer.enabled = false;
        }

        public void Exit()
        {
            Application.Quit();
        }
        #endregion

        #region Networking Events

        void OnConnectedToServer()
        {
            Debug.Log("Joined Server");
            canvas.enabled = false;
            SpawnPlayer();
        }

        void OnMasterServerEvent(MasterServerEvent msEvent)
        {
            if (msEvent == MasterServerEvent.HostListReceived)
            {
                hostList = MasterServer.PollHostList();
                if (hostList != null && hostList.Length > 0)
                {
                    Debug.Log(hostList[0]);
                    Network.Connect(hostList[0]);
                }
                else
                {
                    //TODO: Throw an error that there is no server found
                }

            }
        }

        void OnServerInitialized()
        {
            Debug.Log("Server Initializied");
            canvas.enabled = false;
            SpawnPlayer();
        }
        #endregion

        #region Private Methods
        private void SpawnPlayer()
        {
            GameObject clonedPlayer = Network.Instantiate(playerPrefab, new Vector3(-32f, 0f, -34f), Quaternion.identity, 0) as GameObject;
            

            if (clonedPlayer != null)
            {
                //Setting camera to follow the player
                CameraFollow cameraFollow = camera.GetComponent("CameraFollow") as CameraFollow;
                if (cameraFollow != null)
                {
                   cameraFollow.target = clonedPlayer.GetComponent<Transform>();
                }


                //Setting player health
                PlayerHealth clonedPlayerHealth = clonedPlayer.GetComponent<PlayerHealth>();
                if (clonedPlayerHealth != null)
                {

                    //Connecting the damage image to the player. When player is heart it should appear
                    clonedPlayerHealth.damageImage = damageImage;
                    //Connecting the healthSlider to the player. When player is heart it should change status
                    clonedPlayerHealth.healthSlider = healthSlider;
                    //Connecting the ammo slider
                    PlayerShooting playerShooting = clonedPlayer.GetComponentInChildren<PlayerShooting>();
                    if (playerShooting != null)
                    {
                        playerShooting.ammoSlider = ammoSlider;
                    }
                    else
                    {
                        Debug.Log("Errorrr! playerShooting object is null");
                    }



                    //Setting player health to the enemies
                    if (mainEnemyManager != null)
                    {
                        EnemyManager[] enemyManagersList = mainEnemyManager.GetComponents<EnemyManager>();
                        Debug.Log("Enemy length: " + enemyManagersList.Length);
                        if (enemyManagersList != null)
                        {
                            foreach (EnemyManager enemyManager in enemyManagersList)
                            {
                                enemyManager.playerTransform = clonedPlayer.GetComponent<Transform>();
                                enemyManager.playerHealth = clonedPlayerHealth;
                            }
                        }

                    }

                    //Setting player health to the score canvas
                    if (hudCanvas != null)
                    {
                        GameOverManager gameOverManager = hudCanvas.GetComponent<GameOverManager>();
                        if (gameOverManager != null)
                        {
                            gameOverManager.playerHealth = clonedPlayerHealth;
                        }

                        
                    }
                }


            }


        
        }
        private bool IsValidIPAddress(string ipAddress)
        {
            string[] octets = ipAddress.Split('.');
            if (octets!= null && octets.Length == 4)
            {
                foreach (string octet in octets)
                {
                    try
                    {
                        int octetNum = int.Parse(octet);
                        if (octetNum < 0 || octetNum > 255)
                        {
                            return false;
                        }
                    }
                    catch (UnityException)
                    {
                        return false;
                    }

                }
            }
            return true;
        }
        #endregion
    }
}
