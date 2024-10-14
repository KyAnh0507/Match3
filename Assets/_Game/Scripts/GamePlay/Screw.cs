using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screw : GameUnit
{
    public int layer;
    public int screwType;
    public Transform imageScrewPins;
    public Transform imageScrew;
    public Vector3 positionButton;
    public bool canPlay = true;

    public SpriteRenderer spriteScrew;
    public SpriteRenderer spriteScrewPins;
    public void OnInit(int layer)
    {
        this.layer = layer;
    }
    
    public void Move(Vector3 pos)
    {
        Debug.Log("move" + pos);
    }

    public void Match3()
    {
        Despawn();
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    public void ChangeScrewType(int screwType)
    {
        spriteScrew.color = GamePlay.Ins.color[screwType];
        spriteScrewPins.color = GamePlay.Ins.color[screwType];
        this.screwType = screwType;
    }
    public void ChangeLayer(int layer)
    {
        gameObject.layer = layer + 6;
    }
}
