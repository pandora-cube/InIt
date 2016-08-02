using UnityEngine;
using System.Collections;

public class SingleTone {
    public static SingleTone instance = null;

    public static SingleTone Instance {
        get {
            if(instance == null) {
                instance = new SingleTone();
            }

            return instance;
        }
    }

    private SingleTone() {
    }

    // BGM 불륨 초기값
    public float volumeEffects = .5f;
    // 효과음 불륨 초기값
    public float volumeBGM = .5f;
}