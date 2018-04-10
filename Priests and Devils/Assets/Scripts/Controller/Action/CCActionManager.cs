using Mygame;
using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : SSActionManager, ISSActionCallback
{
    public void MoveBoat(BoatController boat)
    {
        CCMoveToAction action = CCMoveToAction.GetSSAction(boat.GetDestination(), boat.speed);
        this.RunAction(boat.GetGameobj(), action, this);
    }

    public void MoveCharacter(CharacterController characterCtrl, Vector3 destination)
    {
        Vector3 currentPos = characterCtrl.GetPos();
        Vector3 middlePos = currentPos;
        if (destination.y > currentPos.y)
        {
            middlePos.y = destination.y;
        }
        else
        {
            middlePos.x = destination.x;
        }
        SSAction action1 = CCMoveToAction.GetSSAction(middlePos, characterCtrl.speed);
        SSAction action2 = CCMoveToAction.GetSSAction(destination, characterCtrl.speed);
        SSAction seqAction = CCSequenceAction.GetSSAction(1, 0, new List<SSAction> { action1, action2 });
        this.RunAction(characterCtrl.GetGameobj(), seqAction, this);
    }
}