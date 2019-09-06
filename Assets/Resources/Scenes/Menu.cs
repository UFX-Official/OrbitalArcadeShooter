
using System;
using UdpKit;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : Bolt.GlobalEventListener
{
    private void Start()
    {
        //Debug.Log(SceneManager.sceneCount);

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Debug.Log(SceneManager.GetSceneAt(i).name);
        }
    }

    public void StartServer()
    {
        BoltLauncher.StartServer();
    }

    public void StartClient()
    {
        BoltLauncher.StartClient();
    }

    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            string matchName = "Test Match";
            
            //BoltNetwork.SetServerInfo(matchName, null);
            Bolt.Matchmaking.BoltMatchmaking.CreateSession(matchName, null);
            
            BoltNetwork.LoadScene("NetworkedScene");

            
            //Bolt.Matchmaking.BoltMatchmaking.JoinSession("NetworkedScene", null);
        }
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;

            if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltNetwork.Connect(photonSession);
            }
        }
    }
}
