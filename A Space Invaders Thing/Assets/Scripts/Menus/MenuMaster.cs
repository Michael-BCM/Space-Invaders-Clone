using UnityEngine;
using UnityEngine.SceneManagement;

#region enums
public enum currentMenu { Start, Main, Options, Display, Controls } //To be amended as more menu types are added.
public enum currentSubMenu { none, ControlScheme }
#endregion

[System.Serializable]
public class TextDisplay
{
    public string[] Deselected, Selected;
    public TextDisplay(string[] deselectedText, string[] selectedText)
    {
        Deselected = deselectedText;
        Selected = selectedText;
    }
}

[System.Serializable]
public class Menu
{
    public MenuSystem1D _menuSystem;
    public TextMesh headerTM;
    public TextMesh[] itemTM;
}

public class MenuMaster : MonoBehaviour
{
    #region Variables and Objects
    [Header("Current Menu and Option")]
    public currentMenu _currentMenu;
    public currentSubMenu _currentSubMenu;
    public TextMesh selectedOption;

    [Header("Menus")]
    public Menu startMenu;
    public Menu mainMenu, optionsMenu, displayMenu, controlsMenu, controlSchemeMenu, aspectRatioMenu, resolutionMenu;

    [Header("Selection Braces")]
    public MeshRenderer controlSchemeSquare;
    public MeshRenderer controlSchemeArrow;
    
    private TextDisplay MainText = new TextDisplay(new string[] { "Play", "Options", "Quit" }, new string[] { "[   Play   ]", "[  Options  ]", "[   Quit   ]" });
    private TextDisplay OptionsText = new TextDisplay(new string[] { "Display", "Controls", "< Main Menu" }, new string[] { "[  Display  ]", "[ Controls ]", "[< Main Menu]" });
    private TextDisplay DisplayText = new TextDisplay(new string[] { "Aspect Ratio", "Resolution", "< Options" }, new string[] { "[Aspect Ratio]", "[Resolution]", "[< Options]" });
    private TextDisplay ControlsText = new TextDisplay(new string[] { "Control\nScheme", "< Options" }, new string[] {"Control\nScheme", "[< Options]" });

    private bool disableOtherMenus;
    #endregion

    private void Update()
    {
        #region Disable all menus except the Start Menu on wake, and set the current menu to the 'Start' screen. 
        if (!disableOtherMenus)
        {
            disableOtherMenus = true;            
            DisableMenu(mainMenu);
            DisableMenu(optionsMenu);
            DisableMenu(displayMenu);
            DisableMenu(controlsMenu);
            DisableMenu(controlSchemeMenu);
            _currentMenu = currentMenu.Start;
        }
        #endregion
        //Above, disable any other menus that are added to the Main Menu scene, as you go along.

        #region 'Start' screen
        if (_currentMenu == currentMenu.Start)
        {
            if (Input.GetButtonDown("A Button"))
            {
                DisableMenu(startMenu);
                EnableMenu(mainMenu);
                _currentMenu = currentMenu.Main;
            }
        }
        #endregion

        #region Main Menu
        else if (_currentMenu == currentMenu.Main)
        {
            #region Text Setup
            selectedOption = mainMenu._menuSystem.currentSelection.GetComponent<TextMesh>();
            TextSetup(mainMenu._menuSystem, MainText, mainMenu.itemTM);
            #endregion   

            #region Option Selection
            if (Input.GetButtonDown("A Button"))
                MainMenuSelect(mainMenu._menuSystem.selectionID);
            #endregion
        }
        #endregion

        #region Options Menu
        else if (_currentMenu == currentMenu.Options)
        {
            #region Text Setup
            selectedOption = optionsMenu._menuSystem.currentSelection.GetComponent<TextMesh>();
            TextSetup(optionsMenu._menuSystem, OptionsText, optionsMenu.itemTM);
            #endregion   

            #region Option Selection
            if (Input.GetButtonDown("A Button"))
                OptionsMenuSelect(optionsMenu._menuSystem.selectionID);

            else if (Input.GetButtonDown("B Button"))
                OptionsMenuSelect(2);
            #endregion
        }
        #endregion

        #region Display Menu
        else if (_currentMenu == currentMenu.Display)
        {
            #region Text Setup
            selectedOption = displayMenu._menuSystem.currentSelection.GetComponent<TextMesh>();
            TextSetup(displayMenu._menuSystem, DisplayText, displayMenu.itemTM);
            #endregion

            #region Option Selection
            if (Input.GetButtonDown("A Button"))
                DisplayMenuSelect(displayMenu._menuSystem.selectionID);
            #endregion
        }
        #endregion

        #region Controls and Control Scheme Menus
        else if (_currentMenu == currentMenu.Controls)
        {
            if(_currentSubMenu == currentSubMenu.none)
            {
                #region Text Setup
                selectedOption = controlsMenu._menuSystem.currentSelection.GetComponent<TextMesh>();
                TextSetup(controlsMenu._menuSystem, ControlsText, controlsMenu.itemTM);
                if (controlsMenu._menuSystem.currentSelection == controlsMenu._menuSystem.optionsList[0])
                    controlSchemeSquare.enabled = true;
                else
                    controlSchemeSquare.enabled = false;

                controlSchemeArrow.enabled = false;
                #endregion

                #region Option Selection
                if (Input.GetButtonDown("A Button"))
                    ControlsMenuSelect(controlsMenu._menuSystem.selectionID);
                #endregion
            }
            else if (_currentSubMenu == currentSubMenu.ControlScheme)
            {
                #region Text Setup
                selectedOption = controlSchemeMenu._menuSystem.currentSelection.GetComponent<TextMesh>();

                MeshRenderer op0 = controlSchemeMenu._menuSystem.optionsList[0].GetComponent<MeshRenderer>();
                MeshRenderer op1 = controlSchemeMenu._menuSystem.optionsList[1].GetComponent<MeshRenderer>();

                controlSchemeArrow.enabled = true;

                if (controlSchemeMenu._menuSystem.selectionID == 0)
                {
                    op1.enabled = false;
                    op0.enabled = true;
                }
                else if (controlSchemeMenu._menuSystem.selectionID == 1)
                {
                    op0.enabled = false;
                    op1.enabled = true;
                }

                #endregion

                #region Option Selection 

                if(Input.GetButtonDown("A Button"))
                    ControlSchemeMenuSelect(controlSchemeMenu._menuSystem.selectionID);

                else if (Input.GetButtonDown("B Button"))
                    ControlSchemeMenuSelect(2);

                #endregion
            }
        }

        if (_currentMenu != currentMenu.Controls)
            controlSchemeSquare.enabled = controlSchemeArrow.enabled = false;
        #endregion

        transform.Rotate(0, 5 * Time.deltaTime, 0);        
    }

    private void TextSetup(MenuSystem1D MS1D, TextDisplay _textDisplay, TextMesh[] OptionsMeshes)
    {
        for (int i = 0; i < MS1D.optionsList.Length; i++)
            OptionsMeshes[i].text = _textDisplay.Deselected[i];
            
        SetUpBrackets(MS1D, _textDisplay);

        if (UtilitySet.JoyAxisIsInUse() || UtilitySet.KeyAxisIsInUse())
        {
            for (int i = 0; i < MS1D.optionsList.Length; i++)
                OptionsMeshes[i].text = _textDisplay.Deselected[i];

            SetUpBrackets(MS1D, _textDisplay);
        }
    }

    private void SetUpBrackets(MenuSystem1D MS1D, TextDisplay _textDisplay)
    {
        selectedOption = MS1D.currentSelection.GetComponent<TextMesh>();
        MS1D.currentSelection.GetComponent<TextMesh>().text = _textDisplay.Selected[MS1D.selectionID];
    }

    private void DisableMenu(Menu _menu)
    {
        _menu._menuSystem.selectionID = 0; //Resets the selectionID to 0. The menu system's current selection will change automatically.

        _menu._menuSystem.currentSelection = _menu._menuSystem.optionsList[_menu._menuSystem.selectionID];

        for(int i = 0; i < 30; i++)
            print("");

        _menu._menuSystem.enabled = false; //Disables the menu system.

        _menu.headerTM.gameObject.GetComponent<MeshRenderer>().enabled = false; //Disables the header's mesh renderer. 

        foreach (TextMesh op in _menu.itemTM)
            op.gameObject.GetComponent<MeshRenderer>().enabled = false; //Disables the menu items' mesh renderers.
    }

    private void EnableMenu(Menu _menu)
    {
        _menu.headerTM.gameObject.GetComponent<MeshRenderer>().enabled = true; //Enables the header's mesh renderer. 

        foreach (TextMesh op in _menu.itemTM)
            op.gameObject.GetComponent<MeshRenderer>().enabled = true; //Enables the menu items' mesh renderers. 

        _menu._menuSystem.enabled = true; //Enables the menu system.    

        _menu._menuSystem.selectionID = 0; //Resets the selectionID to 0. The menu system's current selection will change automatically. 
    }
    
    private void MainMenuSelect(int _case)
    {
        if (_case == 0)
        {
            //Animation. 
            SceneManager.LoadScene(1);
        }
        else if (_case == 1)
        {
            DisableMenu(mainMenu);
            //Animation.
            EnableMenu(optionsMenu);
            _currentMenu = currentMenu.Options;
        }
        else if (_case == 2)
        {
            //'Are you sure?'-type dialog box?
            Application.Quit();
        }
    }

    private void OptionsMenuSelect(int _case)
    {
        if (_case == 0) //Display,
        {
            DisableMenu(optionsMenu);
            EnableMenu(displayMenu);
            _currentMenu = currentMenu.Display;
        }
        else if (_case == 1) //Controls,
        {
            DisableMenu(optionsMenu);
            EnableMenu(controlsMenu);
            _currentMenu = currentMenu.Controls;
        }
        else if (_case == 2) //Main Menu,
        {
            DisableMenu(optionsMenu);
            EnableMenu(mainMenu);
            _currentMenu = currentMenu.Main;
        }
    }

    private void DisplayMenuSelect(int _case)
    {
        if (_case == 0) //Aspect Ratio,
        {
            //Omit the aspect ratio option and find a way to force 16:9 resolution on all monitors. 
        }

        else if (_case == 1) //Resolution,
        {

        }

        else if (_case == 2) //Options,
        {
            DisableMenu(displayMenu);
            EnableMenu(optionsMenu);
            _currentMenu = currentMenu.Options;
        }
    }

    private void ControlsMenuSelect(int _case)
    {
        if(_case == 0) //Control Scheme,
        {
            controlsMenu._menuSystem.enabled = false;
            for(int i = 0; i < 50; i++)
            {
                print(" ");
            }
            EnableMenu(controlSchemeMenu);
            _currentSubMenu = currentSubMenu.ControlScheme;
        }
        else if (_case == 1) //Options,
        {
            DisableMenu(controlsMenu);
            EnableMenu(optionsMenu);
            _currentMenu = currentMenu.Options;
        }
    }

    private void ControlSchemeMenuSelect (int _case)
    {
        if(_case == 0) //Keyboard,
        {

        }
        else if (_case == 1) //Controller,
        {

        }
        else if (_case == 2) //B Button Press,
        {
            _currentSubMenu = currentSubMenu.none;
            DisableMenu(controlSchemeMenu);
            controlSchemeMenu.headerTM.GetComponent<MeshRenderer>().enabled = true;
            controlsMenu._menuSystem.enabled = true;
        }
    }
}