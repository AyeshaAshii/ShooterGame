using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class SaveLoadManager : MonoBehaviour
{

    [System.Serializable]
    public class SaveData
    {
        public float health;
        public float hunger;
        public float thirst;

        public float[] pos;

        public string level;
    }
        
        private Playerstatts player;
        public string SavePath;

        private void Awake() 
{
    // Use FindObjectsByType (plural) to return an array
    if (FindObjectsOfType<SaveLoadManager>().Length > 1) 
    {
        Destroy(gameObject);
        return;
    }
}

    void Start()
    {
        player= FindAnyObjectByType<Playerstatts>();
        SavePath = Application.persistentDataPath + "/save.json";
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F5))
        Save();

        if(Input.GetKeyDown(KeyCode.F9))
        load();
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.health = player.CurrentHealth;
        data.hunger = player.hunger;
        data.thirst = player.hunger;

        Vector3 playerPos = player.transform.position;
        data.pos = new float[] {playerPos.x, playerPos.y, playerPos.z};
        data.level = SceneManager.GetActiveScene().name;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log("data saved" + SavePath);
    }

    public void load()
    {
        if(File.Exists(SavePath))
        {
            string json = File. ReadAllText(SavePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            StartCoroutine(LoadScene(data));

            
        }
    }

    private IEnumerator LoadScene(SaveData data)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(data. level);
        while (!async.isDone) yield return null;

        yield return null;
        ApplyData(data);
    }

    public void ApplyData(SaveData data)
    {
        player = FindAnyObjectByType<Playerstatts>();
        player.CurrentHealth = data.health;
            player.thirst= data.thirst;
            player.hunger = data.hunger;

            player.transform.position = new Vector3(data.pos[0], data.pos[1], data.pos[2]);

            Debug.Log("game loaded");
    }
    
}

