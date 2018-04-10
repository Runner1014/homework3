using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mygame;

public class CharacterController
{
    readonly GameObject character;
    readonly Moveable moveableScript;
    readonly ClickGUI clickGUI;
    readonly int characterType; // 0->priest, 1->devil
    public readonly float speed = 20;
    bool _isOnBoat;
    CoastController coastController;


    public CharacterController(string which_character)
    {

        if (which_character == "priest")
        {
            character = Object.Instantiate(Resources.Load("Perfabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            characterType = 0;
        }
        else
        {
            character = Object.Instantiate(Resources.Load("Perfabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            characterType = 1;
        }
        moveableScript = character.AddComponent(typeof(Moveable)) as Moveable;

        clickGUI = character.AddComponent(typeof(ClickGUI)) as ClickGUI;
        clickGUI.SetController(this);
    }

    public void SetName(string name)
    {
        character.name = name;
    }

    public void SetPosition(Vector3 pos)
    {
        character.transform.position = pos;
    }

    public void moveToPosition(Vector3 destination)
    {
        moveableScript.SetDestination(destination);
    }

    public int GetCType()
    {   // 0->priest, 1->devil
        return characterType;
    }

    public string GetName()
    {
        return character.name;
    }

    public Vector3 GetPos()
    {
        return this.character.transform.position;
    }

    public GameObject GetGameobj()
    {
        return this.character;
    }

    public void GetOnBoat(BoatController boatCtrl)
    {
        coastController = null;
        character.transform.parent = boatCtrl.GetGameobj().transform;
        _isOnBoat = true;
    }

    public void GetOnCoast(CoastController coastCtrl)
    {
        coastController = coastCtrl;
        character.transform.parent = null;
        _isOnBoat = false;
    }

    public bool IsOnBoat()
    {
        return _isOnBoat;
    }

    public CoastController GetCoastController()
    {
        return coastController;
    }

    public void Reset()
    {
        moveableScript.Reset();
        coastController = (SSDirector.getInstance().currentSceneController as FirstController).startCoast;
        GetOnCoast(coastController);
        SetPosition(coastController.GetEmptyPosition());
        coastController.GetOnCoast(this);
    }

}