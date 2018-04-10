using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mygame;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
	UserGUI userGUI;
    private CCActionManager actionManager;

	public CoastController startCoast;
	public CoastController endCoast;
	public BoatController boat;
	private CharacterController[] characters;

    readonly Vector3 water_pos = new Vector3(0, 0.5F, 0);

    void Awake() {
		SSDirector director = SSDirector.getInstance ();
		director.currentSceneController = this;
		userGUI = gameObject.AddComponent <UserGUI>() as UserGUI;
		characters = new CharacterController[6];
        director.currentSceneController.LoadResources();
	}

	public void LoadResources() {
		GameObject water = Instantiate (Resources.Load ("Perfabs/Water", typeof(GameObject)), water_pos, Quaternion.identity, null) as GameObject;
		water.name = "water";

        boat = new BoatController ();
		startCoast = new CoastController ("start");
		endCoast = new CoastController ("end");

		LoadCharacter ();
	}

	private void LoadCharacter() {
		for (int i = 0; i < 3; i++) {
			CharacterController cha = new CharacterController ("priest");
			cha.SetName("priest" + i);
			cha.SetPosition (startCoast.GetEmptyPosition ());
			cha.GetOnCoast (startCoast);
			startCoast.GetOnCoast (cha);

			characters [i] = cha;
		}

		for (int i = 0; i < 3; i++) {
			CharacterController cha = new CharacterController ("devil");
			cha.SetName("devil" + i);
			cha.SetPosition (startCoast.GetEmptyPosition ());
			cha.GetOnCoast (startCoast);
			startCoast.GetOnCoast (cha);

			characters [i+3] = cha;
		}
	}

    void Start()
    {
        actionManager = GetComponent<CCActionManager>();
    }

	public void MoveBoat() {
		if (boat.IsEmpty ())
			return;
        if(CheckGameOver() == 0)
        {
            //actionManager.MoveBoat(boat);
            boat.Move ();
        }
        userGUI.status = CheckGameOver();
    }

	public void CharacterIsClicked(CharacterController characterCtrl) {
        if (CheckGameOver() == 0)
        {
            if (characterCtrl.IsOnBoat())
            {
                CoastController whichCoast;
                if (boat.Get_end_or_start() == -1)
                { // end->-1; start->1
                    whichCoast = endCoast;
                }
                else
                {
                    whichCoast = startCoast;
                }

                boat.GetOffBoat(characterCtrl.GetName());
                characterCtrl.moveToPosition(whichCoast.GetEmptyPosition());
                //actionManager.MoveCharacter(characterCtrl, whichCoast.GetEmptyPosition());
                characterCtrl.GetOnCoast(whichCoast);
                whichCoast.GetOnCoast(characterCtrl);

            }
            else // character on coast
            {
                CoastController whichCoast = characterCtrl.GetCoastController();

                if (boat.GetEmptyIndex() == -1)
                { // full
                    return;
                }

                if (whichCoast.Get_end_or_start() != boat.Get_end_or_start())
                    return;

                whichCoast.GetOffCoast(characterCtrl.GetName());
                characterCtrl.moveToPosition(boat.GetEmptyPosition());
                //actionManager.MoveCharacter(characterCtrl, boat.GetEmptyPosition());
                characterCtrl.GetOnBoat(boat);
                boat.GetOnBoat(characterCtrl);
            }
        }
		userGUI.status = CheckGameOver ();
	}

	int CheckGameOver() {	// 0->continue, 1->lose, 2->win
		int start_priest = 0;
		int start_devil = 0;
		int end_priest = 0;
		int end_devil = 0;

		int[] startCount = startCoast.GetCharacterNum ();
		start_priest += startCount[0];
		start_devil += startCount[1];

		int[] endCount = endCoast.GetCharacterNum ();
		end_priest += endCount[0];
		end_devil += endCount[1];

		if (end_priest + end_devil == 6)		// win
			return 2;

		int[] boatCount = boat.GetCharacterNum ();
		if (boat.Get_end_or_start () == -1) {	// boat at endCoast
			end_priest += boatCount[0];
			end_devil += boatCount[1];
		} else {	// boat at startCoast
			start_priest += boatCount[0];
			start_devil += boatCount[1];
		}
		if (start_priest < start_devil && start_priest > 0) {	// lose
			return 1;
		}
		if (end_priest < end_devil && end_priest > 0) {
			return 1;
		}
		return 0;	// continue
	}

	public void Restart() {
		boat.Reset ();
		startCoast.Reset ();
		endCoast.Reset ();
		for (int i = 0; i < characters.Length; i++) {
			characters [i].Reset ();
		}
	}
}
