using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


[ExecuteInEditMode]
[RequireComponent(typeof(UnityEngine.UI.GridLayoutGroup))]
public class GameManager : MonoBehaviour
{
	public GameObject CardPanel;

	//cards prefab
	public GameObject blackcard;
	public GameObject bluecard;
	public GameObject greencard;
	public GameObject purplecard;
	public GameObject redcard;
	public GameObject yellowcard;

	//ball prefab
	public GameObject ballPrefab;
	public GameObject ballPosition;

	//ball called info
	public GameObject ballcalledinfoPrefab;
	public GameObject ballcalledinfoShow; 

	//texture for ball prefab
	public Sprite blackballTexture;
	public Sprite blueballTexture;
	public Sprite greenballTexture;
	public Sprite purpleballTexture;
	public Sprite redballTexture;
	public Sprite yellowballTexture;

	//balls called, left label 

	public GameObject ballscalled;
	public GameObject ballsleft;
    
	//setting dialog
	public GameObject settingDlg;

	private GridLayoutGroup CardsLayout;
	private int ballleftCount = 96;
	private List<int> selectedCards;
	private bool loadingState;

    // Start is called before the first frame update

    void Start()
    {
    	initGameConfig();
    }

    void initGameConfig(){
    	loadingState = true;
    	CardsLayout = CardPanel.gameObject.GetComponent<GridLayoutGroup>();
    	RectTransform rt = (RectTransform)CardPanel.transform;

    	float screenWidth = rt.rect.width;
		float screenHeight = rt.rect.height;

		// size fix of the cards panel
    	int childs = CardsLayout.transform.childCount;
    	CardsLayout.cellSize = new Vector2(screenWidth / 16, screenHeight/6);

		//instantiate the cards panel;
		for (int i = childs - 1; i >= 0; i--)
		{
			GameObject.DestroyImmediate(CardsLayout.transform.GetChild(i).gameObject);
		}
		
		int cardIndex = 0;

		for (int i = 0; i < 16; i++){
			GameObject blackcardobj = Instantiate(blackcard) as GameObject;
			
     		Transform cardNum = blackcardobj.transform.Find("cardNum");
     		TextMeshProUGUI cardNumText = cardNum.gameObject.GetComponent<TextMeshProUGUI>();
     		cardNumText.text = cardIndex.ToString();

			blackcardobj.transform.SetParent(CardsLayout.transform, false);
			cardIndex++;
		}
		for (int i = 0; i < 16; i++){
			GameObject bluecardobj = Instantiate(bluecard) as GameObject;
			
     		Transform cardNum = bluecardobj.transform.Find("cardNum");
     		TextMeshProUGUI cardNumText = cardNum.gameObject.GetComponent<TextMeshProUGUI>();
     		cardNumText.text = cardIndex.ToString();

			bluecardobj.transform.SetParent(CardsLayout.transform, false);
			cardIndex++;
		}
		for (int i = 0; i < 16; i++){
			GameObject greencardobj = Instantiate(greencard) as GameObject;
			
     		Transform cardNum = greencardobj.transform.Find("cardNum");
     		TextMeshProUGUI cardNumText = cardNum.gameObject.GetComponent<TextMeshProUGUI>();
     		cardNumText.text = cardIndex.ToString();

			greencardobj.transform.SetParent(CardsLayout.transform, false);
			cardIndex++;
		}
		for (int i = 0; i < 16; i++){
			GameObject purplecardobj = Instantiate(purplecard) as GameObject;
			
     		Transform cardNum = purplecardobj.transform.Find("cardNum");
     		TextMeshProUGUI cardNumText = cardNum.gameObject.GetComponent<TextMeshProUGUI>();
     		cardNumText.text = cardIndex.ToString();

			purplecardobj.transform.SetParent(CardsLayout.transform, false);
			cardIndex++;
		}
		for (int i = 0; i < 16; i++){
			GameObject redcardobj = Instantiate(redcard) as GameObject;
			
     		Transform cardNum = redcardobj.transform.Find("cardNum");
     		TextMeshProUGUI cardNumText = cardNum.gameObject.GetComponent<TextMeshProUGUI>();
     		cardNumText.text = cardIndex.ToString();

			redcardobj.transform.SetParent(CardsLayout.transform, false);
			cardIndex++;
		}
		for (int i = 0; i < 16; i++){
			GameObject yellowcardobj = Instantiate(yellowcard) as GameObject;
			
     		Transform cardNum = yellowcardobj.transform.Find("cardNum");
     		TextMeshProUGUI cardNumText = cardNum.gameObject.GetComponent<TextMeshProUGUI>();
     		cardNumText.text = cardIndex.ToString();

			yellowcardobj.transform.SetParent(CardsLayout.transform, false);
			cardIndex++;
		}
		ballleftCount = 96;
		selectedCards = new List<int>();
		for (int i = 0; i < 96; i++){
			selectedCards.Add(i);
		}
		ballscalled.gameObject.GetComponent<Text>().text = (96 - ballleftCount).ToString();
 		ballsleft.gameObject.GetComponent<Text>().text = ballleftCount.ToString();
 		loadingState = false;
 		if (ballcalledinfoShow.transform.childCount > 0)
	 	for (int i = ballcalledinfoShow.transform.childCount - 1; i >= 0; i--)
		{
			GameObject.DestroyImmediate(ballcalledinfoShow.transform.GetChild(i).gameObject);
		}
		

		if (ballPosition.transform.childCount > 0)
	 	for (int i = ballPosition.transform.childCount - 1; i >= 0; i--)
		{
			GameObject.DestroyImmediate(ballPosition.transform.GetChild(i).gameObject);
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (!loadingState && Input.GetKeyUp(KeyCode.Space))
	    {
    		loadingState = true;
    		Debug.Log(ballleftCount);
    		if (ballleftCount == 0){
    			initGameConfig();
    			return;
    		}
    		int random_value = (int)Random.Range(0, ballleftCount - 1);
    		ballleftCount--;
    		int RandomCardIndex = selectedCards[random_value];
    		selectedCards.RemoveAt(random_value);
		 	Image image = CardPanel.transform.GetChild(RandomCardIndex).transform.GetChild(0).GetComponent<Image>();
		 	Color c = image.color;
		 	c.a = 255;
		 	image.color = c;
		 	string str_selectedObjectName = CardPanel.transform.GetChild(RandomCardIndex).transform.name;
		 	string str_selectedColor = "";
		 	switch (str_selectedObjectName){
		 		case "blackcard(Clone)":
		 			str_selectedColor = "black";
		 			break;
	 			case "bluecard(Clone)":
		 			str_selectedColor = "blue";
		 			break;
	 			case "greencard(Clone)":
		 			str_selectedColor = "green";
		 			break;
	 			case "purplecard(Clone)":
		 			str_selectedColor = "purple";
		 			break;
	 			case "redcard(Clone)":
		 			str_selectedColor = "red";
		 			break;
	 			case "yellowcard(Clone)":
		 			str_selectedColor = "yellow";
		 			break;
		 	}


		 	if (ballcalledinfoShow.transform.childCount > 0)
		 	for (int i = ballcalledinfoShow.transform.childCount - 1; i >= 0; i--)
			{
				GameObject.DestroyImmediate(ballcalledinfoShow.transform.GetChild(i).gameObject);
			}
			GameObject ballcalled = Instantiate(ballcalledinfoPrefab) as GameObject;

			if (ballPosition.transform.childCount > 0)
		 	for (int i = ballPosition.transform.childCount - 1; i >= 0; i--)
			{
				GameObject.DestroyImmediate(ballPosition.transform.GetChild(i).gameObject);
			}
			GameObject ball = Instantiate(ballPrefab) as GameObject;

     		Image ballcalledimage = ballcalled.gameObject.GetComponent<Image>();
			Image ballimage = ball.gameObject.GetComponent<Image>();

     		string mark = "";
     		switch (str_selectedColor){
     			case "black":
     				ballcalledimage.sprite = blackballTexture;
     				ballimage.sprite = blackballTexture;
     				mark = "P";
     				break;
 				case "blue":
     				ballcalledimage.sprite = blueballTexture;
     				ballimage.sprite = blueballTexture;
     				mark = "L";
     				break;
 				case "green":
     				ballcalledimage.sprite = greenballTexture;
     				ballimage.sprite = greenballTexture;
     				mark = "I";
     				break;
 				case "purple":
     				ballcalledimage.sprite = purpleballTexture;
     				ballimage.sprite = purpleballTexture;
     				mark = "N";
     				break;
 				case "red":
     				ballcalledimage.sprite = redballTexture;
     				ballimage.sprite = redballTexture;
     				mark = "G";
     				break;
 				case "yellow":
     				ballcalledimage.sprite = yellowballTexture;
     				ballimage.sprite = yellowballTexture;
     				mark = "O";
     				break;
     		}
     		TextMeshProUGUI ballcalledmark = ballcalled.transform.Find("BallMark").gameObject.GetComponent<TextMeshProUGUI>();
     		ballcalledmark.text = mark;
     		Text ballcallednum = ballcalled.transform.Find("BallNum").gameObject.GetComponent<Text>();
     		ballcallednum.text = RandomCardIndex.ToString();
     		ballcalled.transform.SetParent(ballcalledinfoShow.transform);
     		RectTransform ballcalledrect = (RectTransform)ballcalled.transform;
     		ballcalledrect.offsetMin = new Vector2(0, 0);
     		ballcalledrect.offsetMax = new Vector2(0, 0);

     		Text ballmark = ball.transform.Find("BallMark").gameObject.GetComponent<Text>();
     		ballmark.text = mark;
     		Text ballnum = ball.transform.Find("BallNum").gameObject.GetComponent<Text>();
     		ballnum.text = RandomCardIndex.ToString();
     		ball.transform.SetParent(ballPosition.transform);
     		RectTransform ballrect = (RectTransform)ball.transform;
     		ballrect.offsetMin = new Vector2(0, 0);
     		ballrect.offsetMax = new Vector2(0, 0);

     		ballscalled.gameObject.GetComponent<Text>().text = (96 - ballleftCount).ToString();
     		ballsleft.gameObject.GetComponent<Text>().text = ballleftCount.ToString();
     		loadingState = false;
	    }
    }

    public void ClickExit()
	{
	    Application.Quit();
	}

	public void ClickRestart(){
		initGameConfig();
	}
	
	public void ClickSetting(){
		settingDlg.SetActive(true);
	}

	public void settingOkClick(){
		settingDlg.SetActive(false);
	}

	public void settingCancelClick(){
		settingDlg.SetActive(false);
	}

}
