﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PurchaseMenu : MonoBehaviour {

	private float targetHeight = -Screen.height / 2 - 64;

	public bool isOpen;
	public RectTransform rect;

	public GameObject[] purchaseables;
	public RectTransform firstTopButton;
	public RectTransform firstButtomButton;

	public GameObject buttonPrefab;
	public Transform buttonMask;

	public PlayerInput playerInput;

	// Use this for initialization
	// TODO Implement multiple raycasts before placing objects.
	// TODO Implement "collision check points" on structural modules.

	public void InitializePurchaseMenu () {

		// Count weapons and other types
		int w = 0;
		int o = 0;

		// Itereate through each purchaseable, and instantiate a button for it.
		for (int i = 0; i < purchaseables.Length; i++ ) {

			Module m = purchaseables[i].GetComponent<Module>();

			// Instantiate weapon buttons on lower button row,
			// and all other types on top row.

			GameObject newButton = null;
			if (m.moduleType == Module.Type.Weapon) {
				newButton = (GameObject)Instantiate (buttonPrefab, firstButtomButton.position + Vector3.right * 94 * w, Quaternion.identity);
				w++;
			}else{
				newButton = (GameObject)Instantiate (buttonPrefab, firstTopButton.position + Vector3.right * 94 * o, Quaternion.identity);
				o++;
			}
			Button button = newButton.GetComponent<Button>();
			newButton.transform.GetChild (0).GetComponent<Image>().sprite = purchaseables[i].transform.FindChild ("Sprite").GetComponent<SpriteRenderer>().sprite;
			newButton.transform.SetParent (buttonMask, true);
			newButton.transform.localScale = Vector3.one;
			AddPurchaseButtonListener (button, i);

		}
	
	}

	void AddPurchaseButtonListener (Button button, int index) {
		button.onClick.AddListener (() => {
			SelectPurchaseable (index);
		});
	}

	public void SelectPurchaseable (int index) {
		playerInput.SelectPurchaseable (purchaseables[index]);
	}

	// Update is called once per frame
	void Update () {

		// Animate closing and opening
		rect.localPosition = new Vector3 (rect.localPosition.x, Mathf.Lerp (rect.localPosition.y, targetHeight, 30f * Time.deltaTime));

		if (isOpen && Input.mousePosition.y > 226) {
			isOpen = false;
			targetHeight = -Screen.height / 2 - 64;
		}

		if (Input.mousePosition.y < 53) {
			isOpen = true;
			targetHeight = -Screen.height / 2 + 117;
		}
	
	}
}