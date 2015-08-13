using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    #region Consts
    private const string typeName = "KillThemAll";
    private const string gameName = "KillThemAllRoom";
    #endregion

    #region Private Members
    private HostData[] hostList;
    private NetworkView netView;
    #endregion

    #region Public Members
    public Image panel;
    public GameObject playerPrefab;
    public Button btnStartServer;
    public Button btnJoinServer;
    public Button btnExit;
    #endregion

    #region UI Actions
    public void StartServer()
    {
        netView = GetComponent<NetworkView>();
        MasterServer.ipAddress = "127.0.0.1";
        Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
        MasterServer.RegisterHost(typeName, gameName);

        btnJoinServer.enabled = false;
        btnExit.enabled = false;
    }

    public void JoinServer()
    {
        netView = GetComponent<NetworkView>();
        MasterServer.ipAddress = "127.0.0.1";
        MasterServer.RequestHostList(typeName);
        btnJoinServer.enabled = false;
        btnExit.enabled = false;
        btnStartServer.enabled = false;
    }

    public void Exit()
    {
        //Application.Quit();
        panel.gameObject.SetActive(false);
    }
    #endregion

    #region Networking Events

    void OnConnectedToServer()
    {
        Debug.Log("Joined Server");
        panel.gameObject.SetActive(false);
        SpawnPlayer();
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
        {
            hostList = MasterServer.PollHostList();
            if (hostList != null && hostList.Length > 0)
            {
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
        panel.gameObject.SetActive(false);
        SpawnPlayer();
    }
    #endregion

    #region Private Methods
    private void SpawnPlayer()
    {
        Network.Instantiate(playerPrefab, new Vector3(3f, 0.05f, 3.453817f), Quaternion.identity, 0);
    }
    #endregion
}
