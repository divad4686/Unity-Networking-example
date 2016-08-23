using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Server : MonoBehaviour {

    int port = 9999;
    int maxConnections = 10;

	// Use this for initialization
	void Start () {
	
	}	

    public void CreateServer() {
        
        RegisterHandlers ();
        var config = new ConnectionConfig ();

        config.AddChannel (QosType.ReliableFragmented);
        config.AddChannel (QosType.UnreliableFragmented);

        var ht = new HostTopology (config, maxConnections);

        if (!NetworkServer.Configure (ht)) {
            Debug.Log ("No server created");
            return;
        } else {
            if(NetworkServer.Listen (port))
                Debug.Log ("Server created, listening on port: " + port);   
            else
                Debug.Log ("No server created, could not listen to the port: " + port);    
        }
    }

    void OnApplicationQuit() {
        NetworkServer.Shutdown ();
    }

    private void RegisterHandlers () {
        RegisterHandler (messageType, OnMessageReceived);
        RegisterHandler (MsgType.Connect, OnClientConnected);
        RegisterHandler (MsgType.Disconnect, OnClientDisconnected);
    }

    private void RegisterHandler(short t, NetworkMessageDelegate handler) {
        NetworkServer.RegisterHandler (t, handler);
    }

    void OnClientConnected(NetworkMessage message)
    {
        // Do stuff when a client connects to this server
    }

    void OnClientDisconnected(NetworkMessage message)
    {
        // Do stuff when a client dissconnects
    }


}
