using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mygame;

public class BoatController
{
    readonly GameObject boat;
    readonly Moveable moveableScript;
    readonly Vector3 startPosition = new Vector3(5, 1, 0);
    readonly Vector3 endPosition = new Vector3(-5, 1, 0);
    readonly Vector3[] start_positions;
    readonly Vector3[] end_positions;
    public readonly float speed = 20;

    int end_or_start;
    CharacterController[] passenger = new CharacterController[2];

    public BoatController()
    {
        end_or_start = 1;

        start_positions = new Vector3[] { new Vector3(4.5F, 1.5F, 0), new Vector3(5.5F, 1.5F, 0) };
        end_positions = new Vector3[] { new Vector3(-5.5F, 1.5F, 0), new Vector3(-4.5F, 1.5F, 0) };

        boat = Object.Instantiate(Resources.Load("Perfabs/Boat", typeof(GameObject)), startPosition, Quaternion.identity, null) as GameObject;
        boat.name = "boat";

        moveableScript = boat.AddComponent(typeof(Moveable)) as Moveable;
        boat.AddComponent(typeof(ClickGUI));
    }


    /*    public void Move()
        {
            end_or_start = -end_or_start;
        }*/
    public void Move()
    {
        if (end_or_start == -1)
        {
            moveableScript.SetDestination(startPosition);
            end_or_start = 1;
        }
        else
        {
            moveableScript.SetDestination(endPosition);
            end_or_start = -1;
        }
    }

    public Vector3 GetDestination()
    {
        if (end_or_start == -1)
        {
            return startPosition;
        }
        else
        {
            return endPosition;
        }
    }

    public int GetEmptyIndex()
    {
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    public Vector3 GetEmptyPosition()
    {
        Vector3 pos;
        int emptyIndex = GetEmptyIndex();
        if (end_or_start == -1)
        {
            pos = end_positions[emptyIndex];
        }
        else
        {
            pos = start_positions[emptyIndex];
        }
        return pos;
    }

    public void GetOnBoat(CharacterController characterCtrl)
    {
        int index = GetEmptyIndex();
        passenger[index] = characterCtrl;
    }

    public CharacterController GetOffBoat(string passenger_name)
    {
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] != null && passenger[i].GetName() == passenger_name)
            {
                CharacterController charactorCtrl = passenger[i];
                passenger[i] = null;
                return charactorCtrl;
            }
        }
        Debug.Log("Cant find passenger in boat: " + passenger_name);
        return null;
    }

    public GameObject GetGameobj()
    {
        return boat;
    }

    public int Get_end_or_start()
    {
        return end_or_start;
    }

    public int[] GetCharacterNum()
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] == null)
                continue;
            if (passenger[i].GetCType() == 0) // 0->priest, 1->devil
            {
                count[0]++;
            }
            else
            {
                count[1]++;
            }
        }
        return count;
    }

    public void Reset()
    {
        moveableScript.Reset();
        if (end_or_start == -1)
        {
            Move();
        }
        //boat.transform.position = startPosition;
        passenger = new CharacterController[2];
    }
}