using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoastController
{
    readonly GameObject coast;
    readonly Vector3 start_pos = new Vector3(9, 1, 0);
    readonly Vector3 end_pos = new Vector3(-9, 1, 0);
    readonly Vector3[] positions;
    readonly int end_or_start;    // end->-1, start->1


    CharacterController[] passengerPlaner;

    public CoastController(string _end_or_start)
    {
        positions = new Vector3[] {new Vector3(6.5F,2.25F,0), new Vector3(7.5F,2.25F,0), new Vector3(8.5F,2.25F,0),
                new Vector3(9.5F,2.25F,0), new Vector3(10.5F,2.25F,0), new Vector3(11.5F,2.25F,0)};

        passengerPlaner = new CharacterController[6];

        if (_end_or_start == "start")
        {
            coast = Object.Instantiate(Resources.Load("Perfabs/Coast", typeof(GameObject)), start_pos, Quaternion.identity, null) as GameObject;
            coast.name = "start";
            end_or_start = 1;
        }
        else
        {
            coast = Object.Instantiate(Resources.Load("Perfabs/Coast", typeof(GameObject)), end_pos, Quaternion.identity, null) as GameObject;
            coast.name = "end";
            end_or_start = -1;
        }
    }

    public int GetEmptyIndex()
    {
        for (int i = 0; i < passengerPlaner.Length; i++)
        {
            if (passengerPlaner[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public Vector3 GetEmptyPosition()
    {
        Vector3 pos = positions[GetEmptyIndex()];
        pos.x *= end_or_start;
        return pos;
    }

    public void GetOnCoast(CharacterController characterCtrl)
    {
        int index = GetEmptyIndex();
        passengerPlaner[index] = characterCtrl;
    }

    public CharacterController GetOffCoast(string passenger_name)
    {   // 0->priest, 1->devil
        for (int i = 0; i < passengerPlaner.Length; i++)
        {
            if (passengerPlaner[i] != null && passengerPlaner[i].GetName() == passenger_name)
            {
                CharacterController charactorCtrl = passengerPlaner[i];
                passengerPlaner[i] = null;
                return charactorCtrl;
            }
        }
        Debug.Log("cant find passenger on coast: " + passenger_name);
        return null;
    }

    public int Get_end_or_start()
    {
        return end_or_start;
    }

    public int[] GetCharacterNum()
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < passengerPlaner.Length; i++)
        {
            if (passengerPlaner[i] == null)
                continue;
            if (passengerPlaner[i].GetCType() == 0)
            {   // 0->priest, 1->devil
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
        passengerPlaner = new CharacterController[6];
    }
}