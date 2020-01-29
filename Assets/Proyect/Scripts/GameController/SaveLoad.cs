using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public int indexCurrentSceneBeforeDie = 0;
    public int previousStageScore = 0;

    public static SaveLoad saveLoad;

    private string pathPersistentFiles;
    private int indexCurrentScene;

    void Awake()
    {
        pathPersistentFiles = Application.persistentDataPath + "/datos.dat";

        if (saveLoad == null)
        {
            saveLoad = this;

            //Dont destroy the target object when loading a new scene.
            DontDestroyOnLoad(gameObject);
        }
        else if(saveLoad != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        Load();


        Debug.Log(previousStageScore);
    }

    void Load()
    {
        if(File.Exists(pathPersistentFiles))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(pathPersistentFiles, FileMode.Open);
            DataToSave dataToSave = (DataToSave)bf.Deserialize(file);

            indexCurrentSceneBeforeDie = dataToSave.indexCurrentSceneBeforeDie;
            previousStageScore = dataToSave.previousStageScore;

            file.Close();
        }
        else
        {
            indexCurrentSceneBeforeDie = 0;
            previousStageScore = 0;
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(pathPersistentFiles);

        DataToSave dataToSave = new DataToSave();

        dataToSave.indexCurrentSceneBeforeDie = indexCurrentSceneBeforeDie;
        dataToSave.previousStageScore = previousStageScore;

        bf.Serialize(file, dataToSave);

        file.Close();
    }
}


[Serializable]
class DataToSave
{
    public int indexCurrentSceneBeforeDie;
    public int previousStageScore;
}

