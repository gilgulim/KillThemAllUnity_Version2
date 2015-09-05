using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectServer : MonoBehaviour {

    public Toggle unityServerToggle;
    public InputField serverIpAddress;
    public Button btn_ok;
    public Button btn_cancel;

    public static bool UsingUnityServer;
    public static string RemoteServerIP;

    void Start()
    {
        UsingUnityServer = true;
        RemoteServerIP = string.Empty;
    }
	

    public void ok_clicked()
    {
        UsingUnityServer = unityServerToggle.isOn;
        RemoteServerIP = serverIpAddress.text;
        gameObject.SetActive(false);
    }

    public void cancel_clicked()
    {
        gameObject.SetActive(false);
    }

}
