using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;

public static class ProfileStorage{

    public static ProfileData s_currentProfile;//datos del perfil, // Perfil cargado actualmente

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

    //metodos recuperados
    //guardar y cargar info
    public static void LoadProfile(string filename)
    {
        var path = Application.streamingAssetsPath + "/Profiles/" + filename;
        s_currentProfile = LoadFile<ProfileData>(path);
    }

    public static void StorePlayerProfile(GameObject player){
        s_currentProfile.x = player.transform.position.x;
        s_currentProfile.y = player.transform.position.y;
        s_currentProfile.newGame = false;

        var path = Application.streamingAssetsPath + "/Profiles/" + s_currentProfile.filename;
        SaveFile<ProfileData>(path, s_currentProfile);

    }

    //metodos para guardar datos en un archivo
    static void SaveFile<T>(string path, T data){
        var profileWriter = new StreamWriter(path);
        var profileSerializer = new XmlSerializer(typeof(T));
        profileSerializer.Serialize(profileWriter, data);
        profileWriter.Dispose();
    }

    //borrar
    public static void DeleteProfile(string filename){
        var path = Application.streamingAssetsPath + "/Profiles/" + filename;
        File.Delete(path);

        var index= LoadFile<ProfileIndex>(s_indexPath);
        index.profileFileNames.Remove(filename);

        SaveFile<ProfileIndex>(s_indexPath, index);

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