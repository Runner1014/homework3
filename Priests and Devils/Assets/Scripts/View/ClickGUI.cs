using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mygame;

public class ClickGUI : MonoBehaviour {
	IUserAction action;
	CharacterController characterController;

	public void SetController(CharacterController characterCtrl) {
		characterController = characterCtrl;
	}

	void Start() {
		action = SSDirector.getInstance ().currentSceneController as IUserAction;
	}

	void OnMouseDown() {
		if (gameObject.name == "boat") {
			action.MoveBoat ();
		}
        else {
			action.CharacterIsClicked (characterController);
		}
	}
}
