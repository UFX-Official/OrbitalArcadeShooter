using UnityEngine;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string scene)
    {
        var spawnList = GameObject.FindGameObjectsWithTag("SpawnPoint");        
        var spawnPosition = spawnList[Random.Range(0, spawnList.Length - 1)].transform.position;

        GameObject player = BoltNetwork.Instantiate(BoltPrefabs.NetPlayer, spawnPosition, Quaternion.identity);

        GameObject camera = GameObject.FindWithTag("MainCamera");
        if (camera != null) 
        {
            camera.transform.SetParent(player.transform);
            camera.transform.localPosition = new Vector3(0, 25, -10);
            camera.transform.localRotation = Quaternion.Euler(75, 0, 0);
        }
    }
}
