using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpritesLoader : MonoBehaviour
{
    public static SpritesLoader instance { get; set; }

    [SerializeField] private Sprite future = null;
    [SerializeField] private Sprite rocket = null;

    private void Awake() {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    public Sprite GetFutureSprite(){
        return future;
    }

    public Sprite GetRocketSprite(){
        return rocket;
    }
}
