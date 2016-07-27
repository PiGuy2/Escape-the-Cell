﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;

public class OnClick : MonoBehaviour
{
	public int id;
	public int idOfObjectToClickWith;

	public string correctMessage;
	public string incorrectMessage;

	public GameObject slots;

	// public Sprite sprite;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void OnThisObjectClicked (int clickedWithId)
	{
		if (clickedWithId == idOfObjectToClickWith) {
			printToScreen (correctMessage);
			CollectObject ();
		} else {
			printToScreen (incorrectMessage);
		}
	}

	public void CollectObject ()
	{
		Inventory_Slot_Placer inventory_Slot_Placer = slots.GetComponent<Inventory_Slot_Placer> ();
		List<GameObject> availableSlots = inventory_Slot_Placer.objectsMade;
		GameObject slotToFill = availableSlots [0];
//		List<Inventory_Slot_Placer.InventorySlotInfo> filledSlots = inventory_Slot_Placer.filledSlotInfo;

		slotToFill.SetActive (true);
		FillSlotWithData (slotToFill);

//		Inventory_Slot_Placer.InventorySlotInfo thisSlot = new Inventory_Slot_Placer.InventorySlotInfo (transform.name, id);
//		filledSlots.Add (thisSlot);

		availableSlots.RemoveAt (0);
		Destroy (gameObject);
	}

	void FillSlotWithData (GameObject slotToBeFilled)
	{
		string name = transform.name;

		string nameOfSpriteAsset = "Sprite" + id + ".jpg";
		string spritePath = "Assets/Sprites/" + nameOfSpriteAsset;

		FillText (slotToBeFilled, name);

		FillImage (slotToBeFilled, spritePath);

		Button slotButton = slotToBeFilled.GetComponent<Button> ();

		setId (slotButton);
	}

	void FillText (GameObject fillSlot, string textToAdd)
	{
		GameObject textObject = fillSlot.transform.GetChild (0).gameObject;
		Text text = textObject.GetComponent<Text> ();
		text.text = textToAdd;
	}

	void FillImage (GameObject fillSlot, string pathOfSprite)
	{
		Sprite sprite = AssetDatabase.LoadAssetAtPath (pathOfSprite, typeof(Sprite)) as Sprite;

		GameObject imageObject = fillSlot.transform.GetChild (1).gameObject;
		Image image = imageObject.GetComponent<Image> ();

		print (pathOfSprite);
		print (sprite);

		image.sprite = sprite;
	}

	void setId (Button fillSlot)
	{
		GameObject mainCamera = GameObject.Find ("Main Camera");
		ClickDetector clickDetector = mainCamera.GetComponent<ClickDetector> ();
		fillSlot.onClick.AddListener (() => {
			clickDetector.objectInHandSet (id);
		});
	}

	void printToScreen (string stringToPrint)
	{
		GameObject messageCanvas = GameObject.Find ("Canvas-Message");
		GameObject message = messageCanvas.transform.GetChild (0).gameObject;
		GameObject textObject = message.transform.GetChild (2).gameObject;
		Text text = textObject.GetComponent<Text> ();
		text.text = stringToPrint;

		message.SetActive (true);

		TimeUntilHide timeUntilHide = message.GetComponent<TimeUntilHide> ();
		timeUntilHide.timeUnhid = Time.realtimeSinceStartup;
	}
}