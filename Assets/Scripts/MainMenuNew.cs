using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.adjust.sdk;


public class MainMenuNew : MonoBehaviour {

	Animator CameraObject;

	[Header("Loaded Scene")]
	[Tooltip("The name of the scene in the build settings that will load")]
	public string sceneName = ""; 

	[Header("Panels")]
	[Tooltip("The UI Panel that holds the CONTROLS window tab")]
	public GameObject PanelControls;
	[Tooltip("The UI Panel that holds the VIDEO window tab")]
	public GameObject PanelVideo;
	[Tooltip("The UI Panel that holds the GAME window tab")]
	public GameObject PanelGame;
	[Tooltip("The UI Panel that holds the KEY BINDINGS window tab")]
	public GameObject PanelKeyBindings;
	[Tooltip("The UI Sub-Panel under KEY BINDINGS for MOVEMENT")]
	public GameObject PanelMovement;
	[Tooltip("The UI Sub-Panel under KEY BINDINGS for COMBAT")]
	public GameObject PanelCombat;
	[Tooltip("The UI Sub-Panel under KEY BINDINGS for GENERAL")]
	public GameObject PanelGeneral;
	[Tooltip("The UI Pop-Up when 'EXIT' is clicked")]
	public GameObject PanelareYouSure;

	[Header("SFX")]
	[Tooltip("The GameObject holding the Audio Source component for the HOVER SOUND")]
	public GameObject hoverSound;
	[Tooltip("The GameObject holding the Audio Source component for the AUDIO SLIDER")]
	public GameObject sliderSound;
	[Tooltip("The GameObject holding the Audio Source component for the SWOOSH SOUND when switching to the Settings Screen")]
	public GameObject swooshSound;

	// campaign button sub menu
	[Header("PLAY Sub-Buttons")]
	[Tooltip("Continue Button GameObject Pop Up")]
	public GameObject continueBtn;
	[Tooltip("New Game Button GameObject Pop Up")]
	public GameObject newGameBtn;
	[Tooltip("Load Game Button GameObject Pop Up")]
	public GameObject loadGameBtn;

	// highlights
	[Header("Highlight Effects")]
	[Tooltip("Highlight Image for when GAME Tab is selected in Settings")]
	public GameObject lineGame;
	[Tooltip("Highlight Image for when VIDEO Tab is selected in Settings")]
	public GameObject lineVideo;
	[Tooltip("Highlight Image for when CONTROLS Tab is selected in Settings")]
	public GameObject lineControls;
	[Tooltip("Highlight Image for when KEY BINDINGS Tab is selected in Settings")]
	public GameObject lineKeyBindings;
	[Tooltip("Highlight Image for when MOVEMENT Sub-Tab is selected in KEY BINDINGS")]
	public GameObject lineMovement;
	[Tooltip("Highlight Image for when COMBAT Sub-Tab is selected in KEY BINDINGS")]
	public GameObject lineCombat;
	[Tooltip("Highlight Image for when GENERAL Sub-Tab is selected in KEY BINDINGS")]
	public GameObject lineGeneral;

    [Header("Medrick Part")]
    public int sessionNumber;
    public int playerLevel;
    public int gemInventory;
    public int coinInventory;
    public int maxStage;
    public int maxStar;

    public GameObject tutorialStepText;
    public GameObject tutorialPassedText;
    public GameObject gemText;
    public GameObject coinText;

    public int tutorialStep;
    public int maxTutorialStep;


    void Start()
    {
        sessionNumber = PlayerPrefs.GetInt("session", 0);
        sessionNumber++;
        PlayerPrefs.SetInt("session", sessionNumber);

        playerLevel = PlayerPrefs.GetInt("playerLevel", 0);
        gemInventory = PlayerPrefs.GetInt("gemInventory", 0);
        coinInventory = PlayerPrefs.GetInt("coinInventory", 0);
        maxStage = PlayerPrefs.GetInt("maxStage", 0);
        maxStar = PlayerPrefs.GetInt("maxStar", 0);



        // adjust event 
        AdjustEvent app_start = new AdjustEvent("ksgr8s");
        app_start.addPartnerParameter("session", sessionNumber.ToString());
        app_start.addPartnerParameter("page_type", "surface");
        app_start.addPartnerParameter("page_name", "main_menu");
        app_start.addPartnerParameter("activity_type", "surface");
        app_start.addPartnerParameter("activity_name", "app_start");
        app_start.addPartnerParameter("player_level", playerLevel.ToString());
        app_start.addPartnerParameter("currency1_inv", gemInventory.ToString());
        app_start.addPartnerParameter("currency2_inv", coinInventory.ToString());
        app_start.addPartnerParameter("max_stage", maxStage.ToString());
        app_start.addPartnerParameter("max_star", maxStar.ToString());

        Adjust.trackEvent(app_start);
        // adjust event

        tutorialStep = PlayerPrefs.GetInt("tutorialStep", 0);
        tutorialStepText.GetComponent<Text>().text = tutorialStep.ToString();
        gemText.gameObject.GetComponent<Text>().text = gemInventory.ToString();
        coinText.gameObject.GetComponent<Text>().text = coinInventory.ToString();


        if (PlayerPrefs.GetInt("tutorialPassed", 0) == 1)
        {
        tutorialPassedText.gameObject.SetActive(true);
        }

        CameraObject = transform.GetComponent<Animator>();
	}

	public void  PlayCampaign (){
		PanelareYouSure.gameObject.SetActive(false);
		continueBtn.gameObject.SetActive(true);
		newGameBtn.gameObject.SetActive(true);
		loadGameBtn.gameObject.SetActive(true);
	}

    public void TutorialStepPlus()
    {
        tutorialStep = PlayerPrefs.GetInt("tutorialStep", 0);
        if (tutorialStep == maxTutorialStep)
        {
            return;
        }
        tutorialStep++;
        PlayerPrefs.SetInt("tutorialStep", tutorialStep);
        tutorialStepText.GetComponent<Text>().text = tutorialStep.ToString();

        // adjust event 
        AdjustEvent tutorial_step = new AdjustEvent("29fmdl");
        tutorial_step.addPartnerParameter("session", sessionNumber.ToString());
        tutorial_step.addPartnerParameter("page_type", "surface");
        tutorial_step.addPartnerParameter("page_name", "main_menu");
        tutorial_step.addPartnerParameter("activity_type", "tutorial");
        tutorial_step.addPartnerParameter("activity_step", tutorialStep.ToString());
        tutorial_step.addPartnerParameter("activity_name", "tutorial_step");
        tutorial_step.addPartnerParameter("player_level", playerLevel.ToString());
        tutorial_step.addPartnerParameter("currency1_inv", gemInventory.ToString());
        tutorial_step.addPartnerParameter("currency2_inv", coinInventory.ToString());
        tutorial_step.addPartnerParameter("max_stage", maxStage.ToString());
        tutorial_step.addPartnerParameter("max_star", maxStar.ToString());

        Adjust.trackEvent(tutorial_step);
        // adjust event
    }

    public void TutorialStart()
    {
        if (PlayerPrefs.GetInt("tutorialStart",0) == 0)
        {
            PlayerPrefs.SetInt("tutorialStart", 1);

            // adjust event 
            AdjustEvent tutorial_start = new AdjustEvent("8nywfe");
            tutorial_start.addPartnerParameter("session", sessionNumber.ToString());
            tutorial_start.addPartnerParameter("page_type", "surface");
            tutorial_start.addPartnerParameter("page_name", "main_menu");
            tutorial_start.addPartnerParameter("activity_type", "tutorial");
            tutorial_start.addPartnerParameter("activity_name", "tutorial_start");
            tutorial_start.addPartnerParameter("player_level", playerLevel.ToString());
            tutorial_start.addPartnerParameter("currency1_inv", gemInventory.ToString());
            tutorial_start.addPartnerParameter("currency2_inv", coinInventory.ToString());
            tutorial_start.addPartnerParameter("max_stage", maxStage.ToString());
            tutorial_start.addPartnerParameter("max_star", maxStar.ToString());

            Adjust.trackEvent(tutorial_start);
            // adjust event

        }
    }

    public void TutorialPassed()
    {
        if (PlayerPrefs.GetInt("tutorialPassed", 0) == 0)
        {
            PlayerPrefs.SetInt("tutorialPassed", 1);
            tutorialPassedText.gameObject.SetActive(true);
            // adjust event 
            AdjustEvent tutorial_passed = new AdjustEvent("37znbm");
            tutorial_passed.addPartnerParameter("session", sessionNumber.ToString());
            tutorial_passed.addPartnerParameter("flag_type", "progress");
            tutorial_passed.addPartnerParameter("flag_name", "tutorial_passed");
            tutorial_passed.addPartnerParameter("player_level", playerLevel.ToString());
            tutorial_passed.addPartnerParameter("currency1_inv", gemInventory.ToString());
            tutorial_passed.addPartnerParameter("currency2_inv", coinInventory.ToString());
            tutorial_passed.addPartnerParameter("max_stage", maxStage.ToString());
            tutorial_passed.addPartnerParameter("max_star", maxStar.ToString());

            Adjust.trackEvent(tutorial_passed);
            // adjust event

        }
    }

    public void Purchase(int packNumber)
    {
        string isConverted = PlayerPrefs.GetString("isConverted","true");
        int eventID = PlayerPrefs.GetInt("eventID", 1);

        int coin = 1;
        int gem = 1;
        int price = 1;
        string skuName = "SKU1";

        if (packNumber == 2)
        {
            coin = 5;
            gem = 5;
            price = 4;
            skuName = "SKU2";
        }

        if (packNumber == 3)
        {
            coin = 20;
            gem = 20;
            price = 15;
            skuName = "SKU3";
        }


        // adjust event 
        AdjustEvent purchase_success = new AdjustEvent("y377cc");
        purchase_success.addPartnerParameter("session", sessionNumber.ToString());
        purchase_success.addPartnerParameter("general_type", "monetize");
        purchase_success.addPartnerParameter("general_name", "purchase");
        purchase_success.addPartnerParameter("page_type", "surface");
        purchase_success.addPartnerParameter("page_name", "ovelall");
        purchase_success.addPartnerParameter("page_source", "main_menu");
        purchase_success.addPartnerParameter("item_type", "shop");
        purchase_success.addPartnerParameter("item_name", skuName);
        purchase_success.addPartnerParameter("activity_type", "shop");
        purchase_success.addPartnerParameter("activity_name", "purchase_success");
        purchase_success.addPartnerParameter("sku_name", skuName);
        purchase_success.addPartnerParameter("price", price.ToString());
        purchase_success.addPartnerParameter("revenue", "1");
        purchase_success.addPartnerParameter("detail", "store_token_sample");
        purchase_success.addPartnerParameter("is_first", isConverted);
        purchase_success.addPartnerParameter("event_id", eventID.ToString());
        purchase_success.addPartnerParameter("player_level", playerLevel.ToString());
        purchase_success.addPartnerParameter("currency1_inv", gemInventory.ToString());
        purchase_success.addPartnerParameter("currency2_inv", coinInventory.ToString());
        purchase_success.addPartnerParameter("max_stage", maxStage.ToString());
        purchase_success.addPartnerParameter("max_star", maxStar.ToString());
        purchase_success.setRevenue(price, "USD");

        Adjust.trackEvent(purchase_success);
        // adjust event

        // adjust event 
        AdjustEvent sink_source = new AdjustEvent("d1eain");
        sink_source.addPartnerParameter("session", sessionNumber.ToString());
        sink_source.addPartnerParameter("page_type", "surface");
        sink_source.addPartnerParameter("page_name", "ovelall");
        sink_source.addPartnerParameter("activity_type", "sink_source");
        sink_source.addPartnerParameter("activity_name", "source");
        sink_source.addPartnerParameter("activity_source", "purchase");
        sink_source.addPartnerParameter("sink_source", "source");
        sink_source.addPartnerParameter("currency_name", "gem");
        sink_source.addPartnerParameter("amount", gem.ToString());
        sink_source.addPartnerParameter("event_id", eventID.ToString());
        sink_source.addPartnerParameter("player_level", playerLevel.ToString());
        sink_source.addPartnerParameter("currency1_inv", gemInventory.ToString());
        sink_source.addPartnerParameter("currency2_inv", coinInventory.ToString());
        sink_source.addPartnerParameter("max_stage", maxStage.ToString());
        sink_source.addPartnerParameter("max_star", maxStar.ToString());

        Adjust.trackEvent(sink_source);

        sink_source.addPartnerParameter("session", sessionNumber.ToString());
        sink_source.addPartnerParameter("page_type", "surface");
        sink_source.addPartnerParameter("page_name", "ovelall");
        sink_source.addPartnerParameter("activity_type", "sink_source");
        sink_source.addPartnerParameter("activity_name", "source");
        sink_source.addPartnerParameter("activity_source", "purchase");
        sink_source.addPartnerParameter("sink_source", "source");
        sink_source.addPartnerParameter("currency_name", "coin");
        sink_source.addPartnerParameter("amount", coin.ToString());
        sink_source.addPartnerParameter("event_id", eventID.ToString());
        sink_source.addPartnerParameter("player_level", playerLevel.ToString());
        sink_source.addPartnerParameter("currency1_inv", gemInventory.ToString());
        sink_source.addPartnerParameter("currency2_inv", coinInventory.ToString());
        sink_source.addPartnerParameter("max_stage", maxStage.ToString());
        sink_source.addPartnerParameter("max_star", maxStar.ToString());

        Adjust.trackEvent(sink_source);
        // adjust event

        int tempGem = PlayerPrefs.GetInt("gemInventory", 0) + gem;
        int tempCoin = PlayerPrefs.GetInt("coinInventory", 0) + coin;

        PlayerPrefs.SetInt("gemInventory", tempGem);
        PlayerPrefs.SetInt("coinInventory", tempCoin);

        gemInventory = PlayerPrefs.GetInt("gemInventory", 0);
        coinInventory = PlayerPrefs.GetInt("coinInventory", 0);
        gemText.gameObject.GetComponent<Text>().text = gemInventory.ToString();
        coinText.gameObject.GetComponent<Text>().text = coinInventory.ToString();

        eventID++;
        PlayerPrefs.SetString("isConverted", "false");
        PlayerPrefs.SetInt("evcentID", eventID);
    }



    public void NewGame(){
		if(sceneName != ""){
			SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		}
	}

	public void  DisablePlayCampaign (){
		continueBtn.gameObject.SetActive(false);
		newGameBtn.gameObject.SetActive(false);
		loadGameBtn.gameObject.SetActive(false);
	}

	public void  Position2 (){
		DisablePlayCampaign();
		CameraObject.SetFloat("Animate",1);
	}

	public void  Position1 (){
		CameraObject.SetFloat("Animate",0);
	}

	public void  GamePanel (){
		PanelControls.gameObject.SetActive(false);
		PanelVideo.gameObject.SetActive(false);
		PanelGame.gameObject.SetActive(true);
		PanelKeyBindings.gameObject.SetActive(false);

		lineGame.gameObject.SetActive(true);
		lineControls.gameObject.SetActive(false);
		lineVideo.gameObject.SetActive(false);
		lineKeyBindings.gameObject.SetActive(false);
	}

	public void  VideoPanel (){
		PanelControls.gameObject.SetActive(false);
		PanelVideo.gameObject.SetActive(true);
		PanelGame.gameObject.SetActive(false);
		PanelKeyBindings.gameObject.SetActive(false);

		lineGame.gameObject.SetActive(false);
		lineControls.gameObject.SetActive(false);
		lineVideo.gameObject.SetActive(true);
		lineKeyBindings.gameObject.SetActive(false);
	}

	public void  ControlsPanel (){
		PanelControls.gameObject.SetActive(true);
		PanelVideo.gameObject.SetActive(false);
		PanelGame.gameObject.SetActive(false);
		PanelKeyBindings.gameObject.SetActive(false);

		lineGame.gameObject.SetActive(false);
		lineControls.gameObject.SetActive(true);
		lineVideo.gameObject.SetActive(false);
		lineKeyBindings.gameObject.SetActive(false);
	}

	public void  KeyBindingsPanel (){
		PanelControls.gameObject.SetActive(false);
		PanelVideo.gameObject.SetActive(false);
		PanelGame.gameObject.SetActive(false);
		PanelKeyBindings.gameObject.SetActive(true);

		lineGame.gameObject.SetActive(false);
		lineControls.gameObject.SetActive(false);
		lineVideo.gameObject.SetActive(true);
		lineKeyBindings.gameObject.SetActive(true);
	}

	public void  MovementPanel (){
		PanelMovement.gameObject.SetActive(true);
		PanelCombat.gameObject.SetActive(false);
		PanelGeneral.gameObject.SetActive(false);

		lineMovement.gameObject.SetActive(true);
		lineCombat.gameObject.SetActive(false);
		lineGeneral.gameObject.SetActive(false);
	}

	public void CombatPanel (){
		PanelMovement.gameObject.SetActive(false);
		PanelCombat.gameObject.SetActive(true);
		PanelGeneral.gameObject.SetActive(false);

		lineMovement.gameObject.SetActive(false);
		lineCombat.gameObject.SetActive(true);
		lineGeneral.gameObject.SetActive(false);
	}

	public void GeneralPanel (){
		PanelMovement.gameObject.SetActive(false);
		PanelCombat.gameObject.SetActive(false);
		PanelGeneral.gameObject.SetActive(true);

		lineMovement.gameObject.SetActive(false);
		lineCombat.gameObject.SetActive(false);
		lineGeneral.gameObject.SetActive(true);
	}

	public void PlayHover (){
		hoverSound.GetComponent<AudioSource>().Play();
	}

	public void PlaySFXHover (){
		sliderSound.GetComponent<AudioSource>().Play();
	}

	public void PlaySwoosh (){
		swooshSound.GetComponent<AudioSource>().Play();
	}

	// Are You Sure - Quit Panel Pop Up
	public void  AreYouSure (){
		PanelareYouSure.gameObject.SetActive(true);
		DisablePlayCampaign();
	}

	public void  No (){
		PanelareYouSure.gameObject.SetActive(false);
	}

	public void  Yes (){
		Application.Quit();
	}
}