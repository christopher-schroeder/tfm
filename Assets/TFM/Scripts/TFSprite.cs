using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class TFSprite : MonoBehaviour
{
    public SpriteAtlas Atlas;
    public string SpriteName;

    void Start()
    {
        //this.GetComponent<Image>().sprite = this.Atlas.GetSprite(this.SpriteName);
        this.UpdateSprite();
    }

    private void UpdateSprite()
    {
        this.GetComponent<Image>().sprite = GetSprite(this.Atlas, this.SpriteName);
    }

    public void SetSpriteName(string Name)
    {
        this.SpriteName = Name;
        this.UpdateSprite();
    }

    void OnValidate()
    {
        this.UpdateSprite();
    }

    public static Sprite GetSprite(SpriteAtlas atlas, string name)
    {
#if UNITY_EDITOR
        var packables = SpriteAtlasExtensions.GetPackables(atlas);
        foreach (var packable in packables)
        {
            if (packable is Sprite)
            {
                if (packable.name == name)
                {
                    return (Sprite)packable;
                }
            }
            else if (packable is DefaultAsset)
            {
                var dirPath = AssetDatabase.GetAssetPath(packable);
                var spriteGUIDs = AssetDatabase.FindAssets($"{name} t:sprite", new string[] { dirPath });
                if (spriteGUIDs.Length == 0)
                    continue;
                var spritePath = AssetDatabase.GUIDToAssetPath(spriteGUIDs[0]);
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
                return sprite;
            }
        }
        return null;
#else
        return atlas.GetSprite(name);
#endif
    }
}
