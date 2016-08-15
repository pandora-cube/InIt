using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PlayerData {
    [Serializable]
    public class PLAYER {
        public int Level = 0;
        public float[] Position = { 0f };

        public string Location = string.Empty;
        public string CreatedTime = string.Empty;
        public float PlayTime = 0f;
    }
    public static PLAYER Player_Default = new PLAYER();
    public static PLAYER Player = new PLAYER();
    
    [Serializable]
    public class OPTION {
        public float volumeEffects = .5f;
        public float volumeBGM = .5f;
    }
    public static OPTION Option = new OPTION();

    public static bool flagLoadPlayerData = true;
    public static string collidedNPC = string.Empty;

    public static void DeletePlayerData() {
        PlayerPrefs.DeleteKey("PlayerData");
    }

	public static void SavePlayerData(bool showmsg=true) {
        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();

        // 데이터를 Byte 배열 형태로 변환
        bf.Serialize(ms, Player);
        // 문자열로 변환하여 저장
        PlayerPrefs.SetString("PlayerData", Convert.ToBase64String(ms.GetBuffer()));

        GameObject.Find("SaveMessage UI").transform.localScale = new Vector3(1f, 1f, 1f);

        Debug.Log("저장");
    }

    public static PLAYER ReadPlayerData() {
        string data;
        PLAYER temp;

        try {
            // 문자열 데이터 불러옴
            data = PlayerPrefs.GetString("PlayerData");

            // 빈 데이터가 아닌 경우
            if(!string.IsNullOrEmpty(data)) {
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();

                // 문자열 데이터를 Byte 배열 형태로 변환
                ms = new MemoryStream(Convert.FromBase64String(data));
                temp = (PLAYER)bf.Deserialize(ms);
                
                return temp;
            }
        } catch { }

        return null;
    }

    public static void LoadPlayerData() {
        // 불러오기에 실패한 경우
        if((Player = ReadPlayerData()) == null) {
            // 데이터를 기본 값으로 설정
            Player = Player_Default;
        } else {
            // 데이터 적용
            GameObject.Find("Character").transform.position = new Vector3(Player.Position[0], Player.Position[1], Player.Position[2]);
        }
    }
}
