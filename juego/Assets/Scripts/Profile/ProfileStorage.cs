using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;

public static class ProfileStorage{

    public static ProfileData s_currentProfile;//datos del perfil

    private static string s_indexPath = Application.streamingAssetsPath + "/Profiles/__ProfileIndex__.xml";

    public static void CreateNewGame(string profileName){
        //generar los datos de una nueva partida y guardarlos en un archivo
        s_currentProfile = new ProfileData(profileName, true, 0,0, 0);
        
        string path = Application.streamingAssetsPath + "/Profiles/" + s_currentProfile.filename;
        SaveFile<ProfileData>(path, s_currentProfile);

        //update index
        var index = GetProfileIndex();
        index.profileFileNames.Add(s_currentProfile.filename);

        //save index
        SaveFile<ProfileIndex>(s_indexPath, index);
    }

    public static ProfileIndex GetProfileIndex(){
        if(File.Exists(s_indexPath) == false){
            return new ProfileIndex();
        }
        return LoadFile<ProfileIndex>(s_indexPath);
    }

    //metodos para guardar datos en un archivo
    static void SaveFile<T>(string path, T data){
        var profileWriter = new StreamWriter(path);
        var profileSerializer = new XmlSerializer(typeof(T));
        profileSerializer.Serialize(profileWriter, data);
        profileWriter.Dispose();
    }

//cargar datos de un archivo
    static T LoadFile<T>(string path){
        var progileReader = new StreamReader(path);
        var serializer = new XmlSerializer(typeof(T));
        var obj = (T) serializer.Deserialize(progileReader);
        progileReader.Dispose();

        return obj;
    }
}