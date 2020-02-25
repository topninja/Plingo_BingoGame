using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{	
	public AudioSource fxSound;
	
	//theme dropdown
	public GameObject themedropdown; 

	public GameObject bg_ocean;
	public GameObject bg_mermaid;
	public GameObject bg_pearl;
	public GameObject bg_sea;


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

	//start dialog
	public GameObject StartDlg_NameinputLayout;

	public GameObject StartDlg_PlayerCountDropDownbox;
	public GameObject inputplayerPerfab;
	public List<GamePlayer> players;

	public GameObject StartDlg;

	public GameObject CardsPanel;
	public GameObject PlayerCardsLayout;
	public GameObject PlayerCardPanelPrefab;

	public GameObject OnePlayerCardsPanel;
	public GameObject OnePlayerCardsLayout;

	private List<selectedWiner> selectedWinners;

	private bool gamestartstatus = false;

	//winner show

	public GameObject ShowWinnerPanel;



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

		int childs_cnt = StartDlg_NameinputLayout.transform.childCount;
		for (int i = childs_cnt - 1; i >= 0; i--)
		{
			GameObject.DestroyImmediate(StartDlg_NameinputLayout.transform.GetChild(i).gameObject);
		}
		players = new List<GamePlayer>();
		for (var i = 0; i < 2; i ++){
			GamePlayer player = new GamePlayer();

			GameObject inputplayerObj = Instantiate(inputplayerPerfab) as GameObject;
     		
     		InputField InputField = inputplayerObj.transform.Find("InputField").GetComponent<InputField>();
     		string PlayerName = "Player" + ( i + 1 ).ToString();;
     		InputField.text = PlayerName;
			InputField.onEndEdit.AddListener(delegate {NameInputChangedHandler(); });
     		player.name = PlayerName;
			player.index = i;
			player.cardsArray = RandomCardsNumGet();
			player.selectedCardsArray = new List<int>();
     		players.Add(player);

     		Transform playerNum = inputplayerObj.transform.Find("Num");
     		Text num = playerNum.gameObject.GetComponent<Text>();
     		num.text = ( i + 1 ).ToString();

			inputplayerObj.transform.SetParent(StartDlg_NameinputLayout.transform, false);
		}

		StartDlg.SetActive(true);
		int playercardslayoutChildcount = PlayerCardsLayout.transform.childCount;
		for (int i = playercardslayoutChildcount - 1; i >= 0; i--)
		{
			GameObject.DestroyImmediate(PlayerCardsLayout.transform.GetChild(i).gameObject);
		}
		selectedWinners = new List<selectedWiner>();
		for (int i = 0; i < players.Count; i++){
			GameObject playercardPrefab = Instantiate(PlayerCardPanelPrefab) as GameObject;
			playercardPrefab.transform.Find("no").gameObject.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
			playercardPrefab.transform.Find("username").gameObject.GetComponent<TextMeshProUGUI>().text = players[i].name;
			for (int j = 1; j < 7; j++){
				for (int k = 0; k < 6; k++){
					playercardPrefab.transform.Find("Cards").transform.GetChild(j).transform.GetChild(k).transform.Find("cardNum").gameObject.GetComponent<TextMeshProUGUI>().text = players[i].cardsArray[( j - 1 ) * 6 + k ].ToString();
					// Debug.Log(players[i].cardsArray[( j - 1 ) * 6 + k ].ToString());
				}
				
			}
			Button btn = playercardPrefab.gameObject.GetComponent<Button>();
			btn.onClick.AddListener(() => {cardsPanelButtonClickHandler(playercardPrefab.gameObject); });
			
			playercardPrefab.transform.SetParent(PlayerCardsLayout.transform, false);
			selectedWiner winner = new selectedWiner();
			
			winner.rowIdx = new List<int>();
			winner.colIdx = new List<int>();
			winner.leftdiagonal = false;
			winner.rightdiagonal = false;

			selectedWinners.Add(winner);
		}
		StartDlg_PlayerCountDropDownbox.gameObject.GetComponent<Dropdown>().value = 0;
		gamestartstatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!loadingState && gamestartstatus && Input.GetKeyUp(KeyCode.Space))
	    {
    		loadingState = true;
    		// Debug.Log(ballleftCount);
    		if (ballleftCount == 0){
    			initGameConfig();
    			return;
    		}
			System.Random rnd = new System.Random((int)System.DateTime.Now.Ticks);
    		int random_value = rnd.Next(ballleftCount);
    		ballleftCount--;
    		int RandomCardIndex = selectedCards[random_value];
    		selectedCards.RemoveAt(random_value);
		 	Image image = CardPanel.transform.GetChild(RandomCardIndex).transform.GetChild(0).GetComponent<Image>();
		 	Color c = image.color;
		 	c.a = 255;
		 	image.color = c;
			CardPanel.transform.GetChild(RandomCardIndex).transform.Find("cardNum").gameObject.	GetComponent<TextMeshProUGUI>().outlineColor = Color.black;
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
     		
			//player cards panel update 
			for (int i = 0; i < players.Count; i++){
				for (var j = 0; j < players[i].cardsArray.Count; j++){
					if (players[i].cardsArray[j] == RandomCardIndex){
						int row = j / 6 + 1;
						int col = j % 6 ;
						Image image1 =  PlayerCardsLayout.transform.GetChild(i).transform.Find("Cards").transform.GetChild(row).transform.GetChild(col).transform.Find("bg").gameObject.GetComponent<Image>();
						image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, 1f);

						players[i].selectedCardsArray.Add(j);
					}
				}
			} 
			AudioSource effectmusic = GetComponent<AudioSource>();
			effectmusic.Play(0);
			checkPlayersCardsforWinner();
			loadingState = false;
	    }
    }

	private void checkPlayersCardsforWinner(){
		List<int> ids = new List<int>();
		for (int i = 0; i < players.Count; i++){
			if (players[i].selectedCardsArray.Count == 0) continue;
			var selectedCardsArray = players[i].selectedCardsArray;
			//check row
			bool winstate = false;
			for (int j = 0 ; j < 6; j++){
				winstate = false;
				for (int k = 0 ; k < 6 ; k++){
					winstate = false;
					for (int l = 0; l < selectedCardsArray.Count; l++){
						if (selectedCardsArray[l] == j * 6 + k){
							winstate = true;
						}
					}
					if (!winstate) break;
				}
				// if (winstate) Debug.Log("winner" + " player :  " + (i + 1) + "  row : "  + (j + 1 ));
				if (winstate) {
					bool insertedState = false;
					for (int k = 0; k < selectedWinners[i].rowIdx.Count; k++){
						if (selectedWinners[i].rowIdx[k] == j){
							insertedState = true;
						}
					}
					if (!insertedState){
						selectedWinners[i].rowIdx.Add(j);
						//show to screen
						ids.Add(i);
						Debug.Log("winner" + " player :  " + (i + 1) + "  row : "  + (j + 1 ));
					} 
				}
			}

			//check col
			winstate = false;
			for (int j = 0 ; j < 6; j++){
				winstate = false;
				for (int k = 0 ; k < 6 ; k++){
					winstate = false;
					for (int l = 0; l < selectedCardsArray.Count; l++){
						if (selectedCardsArray[l] == j  + k * 6){
							winstate = true;
						}
					}
					if (!winstate) break;
				}
				// if (winstate) Debug.Log("winner" + " player :  " + (i + 1) + "  row : "  + (j + 1 ));
				if (winstate) {
					bool insertedState = false;
					for (int k = 0; k < selectedWinners[i].colIdx.Count; k++){
						if (selectedWinners[i].colIdx[k] == j){
							insertedState = true;
						}
					}
					if (!insertedState){
						selectedWinners[i].colIdx.Add(j);
						//show to screen
						ids.Add(i);
						Debug.Log("winner" + " player :  " + (i + 1) + "  col : "  + (j + 1 ));
					} 
				}
			}

			//check left leftdiagonal
			winstate = false;
			for (int j = 0 ; j < 6; j++){
				winstate = false;
				for (int l = 0; l < selectedCardsArray.Count; l++){
					if (selectedCardsArray[l] == j * 6 + j){
						winstate = true;
					}
				}
				if (!winstate) break;
			}
			if (winstate && selectedWinners[i].leftdiagonal == false) {
				selectedWinners[i].leftdiagonal = true;
				ids.Add(i);
				Debug.Log("winner" + " player :  " + (i + 1) + "  leftdiagonal : true");
			}

			//check right right_diagonal
			winstate = false;
			for (int j = 0 ; j < 6; j++){
				winstate = false;
				for (int l = 0; l < selectedCardsArray.Count; l++){
					if (selectedCardsArray[l] == j * 6 + (5 - j)){
						winstate = true;
					}
				}
				if (!winstate) break;
			}
			if (winstate && selectedWinners[i].rightdiagonal == false) {
				selectedWinners[i].rightdiagonal = true;
				ids.Add(i);
				Debug.Log("winner" + " player :  " + (i + 1) + "  rightdiagonal : true");
			}
		}
		ShowWinnerByUserIds(ids);
	}


	private void ShowWinnerByUserIds(List<int> _ids){
		if (_ids.Count == 0){
			ShowWinnerPanel.SetActive(false);
			return;
		} 
		var playerinfo = players[_ids[0]];
		ShowWinnerPanel.transform.Find("WinnerCards").transform.Find("no").gameObject.GetComponent<TextMeshProUGUI>().text = (playerinfo.index + 1) + "";
		ShowWinnerPanel.transform.Find("WinnerCards").transform.Find("username").gameObject.GetComponent<TextMeshProUGUI>().text = playerinfo.name;

		ShowWinnerPanel.SetActive(true);
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

	public void startDlg_PlayerCountChanged(){
		var count = StartDlg_PlayerCountDropDownbox.gameObject.GetComponent<Dropdown>().value + 2;
		//inputplayerPerfab
		int childs = StartDlg_NameinputLayout.transform.childCount;
		for (int i = childs - 1; i >= count; i--)
		{
			GameObject.DestroyImmediate(StartDlg_NameinputLayout.transform.GetChild(i).gameObject);
		}
		int currentCount = StartDlg_NameinputLayout.transform.childCount;
		for (int i = currentCount; i < count; i ++){
			GameObject inputplayerObj = Instantiate(inputplayerPerfab) as GameObject;
     		
     		InputField InputField = inputplayerObj.transform.Find("InputField").GetComponent<InputField>();
     		InputField.text = "Player" + ( i + 1 ).ToString();
			InputField.onEndEdit.AddListener(delegate {NameInputChangedHandler(); });
     		Transform playerNum = inputplayerObj.transform.Find("Num");
     		Text num = playerNum.gameObject.GetComponent<Text>();
     		num.text = ( i + 1 ).ToString();
     		
			inputplayerObj.transform.SetParent(StartDlg_NameinputLayout.transform, false);
		}
		players = new List<GamePlayer>();
		for (int i = 0; i < count; i++){
			GamePlayer player = new GamePlayer();
			player.name = StartDlg_NameinputLayout.transform.GetChild(i).transform.Find("InputField").gameObject.GetComponent<InputField>().text;
			player.cardsArray = RandomCardsNumGet();
			player.selectedCardsArray = new List<int>();
			player.index = i;
			players.Add(player);
		}

		int playercardslayoutChildcount = PlayerCardsLayout.transform.childCount;
		for (int i = playercardslayoutChildcount - 1; i >= 0; i--)
		{
			GameObject.DestroyImmediate(PlayerCardsLayout.transform.GetChild(i).gameObject);
		}
		selectedWinners = new List<selectedWiner>();
		for (int i = 0; i < players.Count; i++){
			GameObject playercardPrefab = Instantiate(PlayerCardPanelPrefab) as GameObject;
			playercardPrefab.transform.Find("no").gameObject.GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
			playercardPrefab.transform.Find("username").gameObject.GetComponent<TextMeshProUGUI>().text = players[i].name;
			for (int j = 1; j < 7; j++){
				for (int k = 0; k < 6; k++){
					playercardPrefab.transform.Find("Cards").transform.GetChild(j).transform.GetChild(k).transform.Find("cardNum").gameObject.GetComponent<TextMeshProUGUI>().text = players[i].cardsArray[( j - 1 ) * 6 + k ].ToString();
					// Debug.Log(players[i].cardsArray[( j - 1 ) * 6 + k ].ToString());
				}
				
			}
			Button btn = playercardPrefab.gameObject.GetComponent<Button>();
			btn.onClick.AddListener(() => {cardsPanelButtonClickHandler(playercardPrefab.gameObject); });
			
			playercardPrefab.transform.SetParent(PlayerCardsLayout.transform, false);
			
			selectedWiner winner = new selectedWiner();
			
			winner.rowIdx = new List<int>();
			winner.colIdx = new List<int>();
			winner.leftdiagonal = false;
			winner.rightdiagonal = false;

			selectedWinners.Add(winner);

		}
	}

	public void GameStartClickHandler()
	{
		StartDlg.SetActive(false);
		gamestartstatus = true;
	}

	private List<int> RandomCardsNumGet(){
		int allcardscount = 96;
		int allusercardscount = 36;
		List<int> cards = new List<int>();
		List<int> selcards = new List<int>();
		for (int i = 1 ; i < allcardscount + 1; i++){
			selcards.Add(i);
		}
		
		int seed = 0;
		for (var i = 0; i < allusercardscount; i++){
			if ( i % 2 == 1)
			System.Threading.Thread.Sleep(1);
			seed = (int)System.DateTime.Now.Ticks.GetHashCode() + i * 100;
			System.Random rnd = new System.Random(seed );
    		int random_value = rnd.Next(allcardscount);
			allcardscount--;
			cards.Add(selcards[random_value]);
			selcards.RemoveAt(random_value);
		}
		return cards;
	}

	public void showCardsPanel(){
		
		for(int i = 0; i < players.Count; i++){

		}
		CardsPanel.SetActive(true);
	}

	public void cardsPanelButtonClickHandler(GameObject obj){
		int idx = int.Parse(obj.transform.Find("no").gameObject.GetComponent<TextMeshProUGUI>().text);
		var playerinfo = players[idx - 1];
		OnePlayerCardsLayout.transform.Find("no").gameObject.GetComponent<TextMeshProUGUI>().text = idx.ToString();
		OnePlayerCardsLayout.transform.Find("username").gameObject.GetComponent<TextMeshProUGUI>().text = playerinfo.name;
		
		for (int i = 0; i < playerinfo.cardsArray.Count; i++){
			int row = i / 6 + 1;
			int col = i % 6 + 1;
			OnePlayerCardsLayout.transform.Find("Cards").transform.Find(row.ToString()).transform.Find(col.ToString()).transform.Find("cardNum").gameObject.GetComponent<TextMeshProUGUI>().text = playerinfo.cardsArray[i].ToString();
		}

		for (int i = 0 ; i < 36; i++){
			int row = i / 6 + 1;
			int col = i % 6 + 1;
			// OnePlayerCardsLayout.transform.Find("Cards").transform.Find(row.ToString()).transform.Find(col.ToString()).transform.Find("bg").gameObject.GetComponent<Image>().color.opaciy = playerinfo.cardsArray[i].ToString();
			Image image1 =  OnePlayerCardsLayout.transform.Find("Cards").transform.Find(row.ToString()).transform.Find(col.ToString()).transform.Find("bg").gameObject.GetComponent<Image>();
			image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, 0f);
		}

		for (int i = 0 ; i < playerinfo.selectedCardsArray.Count; i++){
			int row = playerinfo.selectedCardsArray[i] / 6 + 1;
			int col = playerinfo.selectedCardsArray[i] % 6 + 1;
			// OnePlayerCardsLayout.transform.Find("Cards").transform.Find(row.ToString()).transform.Find(col.ToString()).transform.Find("bg").gameObject.GetComponent<Image>().color.opaciy = playerinfo.cardsArray[i].ToString();
			Image image1 =  OnePlayerCardsLayout.transform.Find("Cards").transform.Find(row.ToString()).transform.Find(col.ToString()).transform.Find("bg").gameObject.GetComponent<Image>();
			image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, 1f);
		}
		OnePlayerCardsPanel.SetActive(true);
	}

	public void cardsPanelButtonOKClickHandler(){
		OnePlayerCardsPanel.SetActive(false);
	}

	public void hideCardsPanel(){
		CardsPanel.SetActive(false);
	}

	public void NameInputChangedHandler(){
		var count = StartDlg_NameinputLayout.transform.childCount;
		for (int i = 0; i < count; i++){
			players[i].name = StartDlg_NameinputLayout.transform.GetChild(i).transform.Find("InputField").gameObject.GetComponent<InputField>().text;
		}
		for (int i = 0 ; i < PlayerCardsLayout.transform.childCount; i++){
			PlayerCardsLayout.transform.GetChild(i).transform.Find("username").gameObject.GetComponent<TextMeshProUGUI>().text = players[i].name;
		}
	}

	public void BackgroundMusicOptionHandler(){
		if (fxSound.isPlaying){
			fxSound.Stop();
		}
		else fxSound.Play();
	}

	public void themeValueChangeHandler(){

		
		bg_ocean.SetActive(false);
		bg_mermaid.SetActive(false);
		bg_pearl.SetActive(false);
		bg_sea.SetActive(false);
		switch(themedropdown.gameObject.GetComponent<Dropdown>().value){
			case 0:
				bg_ocean.SetActive(true);
				break;
			case 1:
				bg_mermaid.SetActive(true);
				break;
			case 2:
				bg_pearl.SetActive(true);
				break;
			case 3:
				bg_sea.SetActive(true);
				break;
		}
	}

}