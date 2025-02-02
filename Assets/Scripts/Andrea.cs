﻿// Author: Ryan C. Kruse
// VRChat: Clearly
// Discord: Clearly#3238
// GitHub: https://github.com/RyanKruse/Andrea
// Booth: https://clearly.booth.pm/
// Comments: This code is held together by gum and paperclips.
//           It is advisable to message me for coding requests.

using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System.IO;
using UnityEditor;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System.Reflection;
using System;
using System.Linq;

public class Andrea : UdonSharpBehaviour
{
    [Header("Populated Data Variables From Inspector:")]
    [SerializeField] public Memory Memory;
    [SerializeField] private bool _isShowExtraOptions;
    [SerializeField] private bool _isVRSettingsOverride;
    [SerializeField] private bool _isMinimizeCatalog;
    [SerializeField] private bool _isScrollWheelActive;
    [SerializeField] private bool _isRejectNumPadInputs;
    [SerializeField] private bool _isDontExecuteScript;
    [SerializeField] private bool _isSkipAppendFlush;
    [SerializeField] private bool _isBookStaged;
    [SerializeField] private int _defaultBlockIndex;
    [SerializeField] private int _defaultFontSizeIndex;
    [SerializeField] private int _defaultFontTypeIndex;
    [SerializeField] private int _defaultBookIndex;
    [SerializeField] private int _defaultTurnPageIndex;
    [SerializeField] private int[] _preservedLocBookList;
    [SerializeField] private string _defaultOrientation;
    [SerializeField] private AudioClip _softSwipeAudio;
    [SerializeField] private AudioClip _hardSwipeAudio;
    [SerializeField] private AudioClip _clickAudio;
    [SerializeField] private AudioClip _optionsAudio;
    [SerializeField] private AudioClip _selectBookAudio;
    [SerializeField] private AudioClip _backAudio;
    [SerializeField] private AudioSource _candleAudioSource;
    [SerializeField] private VRC_Pickup _candleVRCPickup;
    [SerializeField] private GameObject _candleGameObject;
    [SerializeField] private GameObject _compilerGameObject;
    [SerializeField] private GameObject _readingScreenGameObject;
    [SerializeField] private GameObject _mainMenuGameObject;
    [SerializeField] private GameObject _optionsMenuGameObject;
    [SerializeField] private GameObject _bottomBarGameObject;
    [SerializeField] private GameObject _screenSaverScreenGameObject;
    [SerializeField] private GameObject _screenSaverBackgroundGameObject;
    [SerializeField] private GameObject _bigButtonScreenGameObject;
    [SerializeField] private GameObject _catalogButtonGameObject;
    [SerializeField] private GameObject _backButtonGameObject;
    [SerializeField] private GameObject _rightHandGameObject;
    [SerializeField] private GameObject _leftHandGameObject;
    [SerializeField] private GameObject _categoryGameObject0;
    [SerializeField] private GameObject _categoryGameObject1;
    [SerializeField] private GameObject _categoryGameObject2;
    [SerializeField] private GameObject _categoryGameObject3;
    [SerializeField] private GameObject _categoryGameObject4;
    [SerializeField] private GameObject _minimizeButtonGameObject0;
    [SerializeField] private GameObject _minimizeButtonGameObject1;
    [SerializeField] private GameObject _minimizeButtonGameObject2;
    [SerializeField] private GameObject _minimizeButtonGameObject3;
    [SerializeField] private GameObject _miscsGameObject;
    [SerializeField] private GameObject _screenGameObject;
    [SerializeField] private GameObject _screenPhysicalGameObject;
    [SerializeField] private GameObject _homeContainerGameObject;
    [SerializeField] private GameObject _handleIconGameObject;
    [SerializeField] private GameObject _screenSaverAndreaText;
    [SerializeField] private GameObject[] _bookBannerGameObjectList;
    [SerializeField] private GameObject[] _bookButtonGameObjectList;
    [SerializeField] private GameObject[] _everySingleTextBoxGameObjectList;
    [SerializeField] private TextMeshProUGUI[] _bookBannerTMPList;
    [SerializeField] private TextAsset _styleSheetTextAsset;
    [SerializeField] private TextMeshProUGUI _helveticaTMP;
    [SerializeField] private TextMeshProUGUI _helveticaCloneTMP;
    [SerializeField] private TextMeshProUGUI _tahomaTMP;
    [SerializeField] private TextMeshProUGUI _tahomaCloneTMP;
    [SerializeField] private TextMeshProUGUI _notoSerifTMP;
    [SerializeField] private TextMeshProUGUI _notoSerifCloneTMP;
    [SerializeField] private TextMeshProUGUI _timesTMP;
    [SerializeField] private TextMeshProUGUI _timesCloneTMP;
    [SerializeField] private TextMeshProUGUI _verdanaTMP;
    [SerializeField] private TextMeshProUGUI _verdanaCloneTMP;
    [SerializeField] private TextMeshProUGUI _avenirTMP;
    [SerializeField] private TextMeshProUGUI _avenirCloneTMP;
    [SerializeField] private TextMeshProUGUI _bottomLeftTMP;
    [SerializeField] private TextMeshProUGUI _bottomLeftCloneTMP;
    [SerializeField] private TextMeshProUGUI _bottomCenterTMP;
    [SerializeField] private TextMeshProUGUI _bottomCenterCloneTMP;
    [SerializeField] private TextMeshProUGUI _bottomRightTMP;
    [SerializeField] private TextMeshProUGUI _bottomRightCloneTMP;
    [SerializeField] private TextMeshProUGUI _bottomCoreTMP;
    [SerializeField] private TextMeshProUGUI _bottomCoreCloneTMP;
    [SerializeField] private TextMeshProUGUI _topLeftTMP;
    [SerializeField] private TextMeshProUGUI _topLeftCloneTMP;
    [SerializeField] private TextMeshProUGUI _topCenterTMP;
    [SerializeField] private TextMeshProUGUI _topCenterCloneTMP;
    [SerializeField] private TextMeshProUGUI _topRightTMP;
    [SerializeField] private TextMeshProUGUI _topRightCloneTMP;
    [SerializeField] private TextMeshProUGUI[] _fontSizeTextList;
    [SerializeField] private TextMeshProUGUI[] _fontTypeTextList;
    [SerializeField] private TextMeshProUGUI[] _tabletScaleTextList;
    [SerializeField] private TextMeshProUGUI[] _tabletOrientationTextList;
    [SerializeField] private TextMeshProUGUI[] _everySingleTextBoxList;
    [SerializeField] private Sprite[] _loadingScreenSpriteList;
    [SerializeField] private Material _materialGreyLight;
    [SerializeField] private Material _materialGreyMedium;
    [SerializeField] private Material _materialGreyDark;
    [SerializeField] private Image _screenSaverHeaderImage;
    [SerializeField] private Slider _locNavigationSlider;

    [Header("Fixed Data Variables From Field:")]
    [SerializeField] private int _cloneDelay;
    [SerializeField] private int[] _header1FontSizePercentList;
    [SerializeField] private int[] _header2FontSizePercentList;
    [SerializeField] private int[] _header3FontSizePercentList;
    [SerializeField] private int[] _fontSizeList;
    [SerializeField] private float _cloneCycle;
    [SerializeField] private char[] _badCharList;
    [SerializeField] private char[] _bracketCharList;
    [SerializeField] private string[] _fontTypeList;
    [SerializeField] private string[] _locationCodeList;

    [Header("Andrea Runtime Device Status:")]
    [SerializeField] private int _mainBookIndex;
    [SerializeField] private int _mainBlockIndex;
    [SerializeField] private int _mainFontSize;
    [SerializeField] private int _mainFontSizeIndex;
    [SerializeField] private int _mainPageLength;
    [SerializeField] private int _mainPageIndex;
    [SerializeField] private int _mainCharZeroIndex;
    [SerializeField] private int _mainFontTypeIndex;
    [SerializeField] private int _mainTurnPageIndex;
    [SerializeField] private int _mainLocationHeight;
    [SerializeField] private int _mainBlockHeight;
    [SerializeField] private int _mainMaxHeight;
    [SerializeField] private string _mainFontType;
    [SerializeField] private string _mainOrientation;
    [SerializeField] private string _mainStatusCode;
    [SerializeField] private TextAsset _mainText;
    [SerializeField] private TextAsset _mainBlock;
    [SerializeField] private TextAsset[] _mainTextList;
    [SerializeField] private TextAsset[] _mainBlockList;
    [SerializeField] private TextMeshProUGUI _mainTMP;
    [SerializeField] private TextMeshProUGUI _mainCloneTMP;
    [SerializeField] private TextMeshProUGUI _mainPreviousCloneTMP;

    [Header("Global Workflow Data Variables:")]
    // [SerializeField] private bool _isShowBar;
    [SerializeField] private bool _isNoDoubleExecute;
    [SerializeField] private bool _isHaltPageTurn;
    [SerializeField] private bool _isUpTriggerActive;
    [SerializeField] private bool _isDownTriggerActive;
    [SerializeField] private bool _isRightTriggerActive;
    [SerializeField] private bool _isLeftTriggerActive;
    [SerializeField] private bool _isRightGripActive;
    [SerializeField] private bool _isLeftGripActive;
    [SerializeField] private bool _isRightHandGripActive;
    [SerializeField] private bool _isOverflowAuditDefinePage;
    [SerializeField] private bool _isTogglePickedUp;
    [SerializeField] private bool _isStringRichText;
    [SerializeField] private bool _isCategoryLerp0;
    [SerializeField] private bool _isCategoryLerp1;
    [SerializeField] private bool _isCategoryLerp2;
    [SerializeField] private bool _isCategoryLerp3;
    [SerializeField] private bool _isCategoryMinimized0;
    [SerializeField] private bool _isCategoryMinimized1;
    [SerializeField] private bool _isCategoryMinimized2;
    [SerializeField] private bool _isCategoryMinimized3;
    [SerializeField] private bool _isCategoryMinimized4;
    [SerializeField] private bool _isScreenSaverHidden;
    [SerializeField] private bool _isHomeContainerHidden;
    [SerializeField] private bool _isOptionsContainerHidden;
    [SerializeField] private bool[] _richTextBoolList;
    [SerializeField] private int _cloneTime;
    [SerializeField] private int _overflowPageIndex;
    [SerializeField] private int _endCalibrationPageIndex;
    [SerializeField] private int[] _lastCharSliceList;
    [SerializeField] private float _cloneTick;
    [SerializeField] private string _richTextString;
    [SerializeField] private string[] _richTextStringList;
    [SerializeField] private string[] _richTextOverflowList;
    [SerializeField] public string[] _styleSheetNameList;  // Formatting C#.
    [SerializeField] public string[] _styleSheetTagList;  // Formatting C#.
    [SerializeField] private Vector3 _categoryVectorDefault0;
    [SerializeField] private Vector3 _categoryVectorDefault1;
    [SerializeField] private Vector3 _categoryVectorDefault2;
    [SerializeField] private Vector3 _categoryVectorDefault3;
    [SerializeField] private Vector3 _categoryVectorDefault4;
    [SerializeField] private Vector3 _categoryVectorTarget0;
    [SerializeField] private Vector3 _categoryVectorTarget1;
    [SerializeField] private Vector3 _categoryVectorTarget2;
    [SerializeField] private Vector3 _categoryVectorTarget3;
    [SerializeField] private Vector3 _categoryVectorTarget4;
    [SerializeField] private Vector3 _categoryVectorStart0;
    [SerializeField] private Vector3 _categoryVectorStart1;
    [SerializeField] private Vector3 _categoryVectorStart2;
    [SerializeField] private Vector3 _categoryVectorStart3;
    [SerializeField] private Vector3 _categoryVectorStart4;
    [SerializeField] private Vector3 _previousRightHandPosition;
    [SerializeField] private Vector3 _previousPlayerPosition;
    [SerializeField] private Vector3 _screenSaverBackgroundShown;
    [SerializeField] private Vector3 _screenSaverBackgroundHidden;
    [SerializeField] private Vector3 _homeContainerShown;
    [SerializeField] private Vector3 _homeContainerHidden;
    [SerializeField] private Vector3 _optionsContainerShown;
    [SerializeField] private Vector3 _optionsContainerHidden;



    // JUNK VARIABLES:
    [SerializeField] private int _adjustedLocationHeight;
    [SerializeField] private int _adjustedMaxHeight;
    [SerializeField] private float timer0;
    [SerializeField] private float timer1;
    [SerializeField] private float timer2;
    [SerializeField] private float timer3;
    [SerializeField] private int _screenSaverIndex;

    [SerializeField] private float timerScreenSaver;
    [SerializeField] private bool _isScreenSaverLerp;
    [SerializeField] private Vector3 _screenSaverVectorDefault;
    [SerializeField] private Vector3 _screenSaverVectorTarget;
    [SerializeField] private Vector3 _screenSaverVectorStart;

    [SerializeField] private float timerHomeContainer;
    [SerializeField] private bool _isHomeContainerLerp;
    [SerializeField] private Vector3 _homeContainerVectorDefault;
    [SerializeField] private Vector3 _homeContainerVectorTarget;
    [SerializeField] private Vector3 _homeContainerVectorStart;

    [SerializeField] private float timerOptionsContainer;
    [SerializeField] private bool _isOptionsContainerLerp;
    [SerializeField] private Vector3 _optionsContainerVectorDefault;
    [SerializeField] private Vector3 _optionsContainerVectorTarget;
    [SerializeField] private Vector3 _optionsContainerVectorStart;

    [SerializeField] private bool _isLocNavClicked;
    [SerializeField] private bool _isLocNavHover;
    [SerializeField] private bool _isLocNavDragged;
    [SerializeField] private int _locNavMainBlockIndex;
    [SerializeField] private int _locNavMainTrueLoc;
    [SerializeField] private bool _isLoadBookNew;
    [SerializeField] private bool _isBookPresetLoc;
    [SerializeField] private int _deepFreezePageIndex;
    [SerializeField] private GameObject _optionsContainerGameObject;

    [SerializeField] private Image _smallSizeImage;
    [SerializeField] private Image _mediumSizeImage;
    [SerializeField] private Image _largeSizeImage;
    [SerializeField] private Image _helveticaTypeImage;
    [SerializeField] private Image _notoSerifTypeImage;
    [SerializeField] private Image _tahomaTypeImage;
    [SerializeField] private Image _smallScaleImage;
    [SerializeField] private Image _mediumScaleImage;
    [SerializeField] private Image _largeScaleImage;

    [SerializeField] private bool _isTutorialMode;
    [SerializeField] private bool _isFirstTimeMode;

    [SerializeField] private int _bigHammerLocCoordinates;
    [SerializeField] private Sprite _bannerInteractable;
    [SerializeField] private Sprite _bannerNotInteractable;
    [SerializeField] private float _brightnessSliderValue;
    [SerializeField] private Slider _brightnessSlider;
    [SerializeField] private GameObject _AndreaScreenModel;
    [SerializeField] private bool _isTextDarkMode;
    [SerializeField] private Image _sliderBackground;
    [SerializeField] private Image _sliderFill;
    [SerializeField] private Image _sliderButton;
    [SerializeField] private int _calibrateMemoryDeepFreeze;
    [SerializeField] private TextMeshProUGUI _topDebugTMP;
    // [SerializeField] private VRC_UiShape _screemVRCUIShape;


    [HideInInspector][UdonSynced(UdonSyncMode.None)] public int registry;

    private void Start()
    {
        if (_isDontExecuteScript) return;
        PopulateStyleSheet(_styleSheetTextAsset);
        DefineMainVariables();
        ExecuteCalibration();

        // JUNK CODE:
        // _defaultColor = _readingScreenGameObject.GetComponent<Image>().color;
        // _blankScreenGameObject.SetActive(false);
        HomeInfo();
        if (Networking.LocalPlayer.displayName != "Local Player 1")
        {
            Debug.Log($"Playername: {Networking.LocalPlayer.displayName}");
            _compilerGameObject.SetActive(false);
        }
    }

    public void PopulateStyleSheet(TextAsset styleSheetTextAsset)  // Parallel Code.
    {
        // Define list size based on how many rows there are in the style sheet text asset.
        int _styleSheetRowCount = 0;
        for (int _styleIndex = 0; _styleIndex < styleSheetTextAsset.text.Length; _styleIndex++)
        {
            if (styleSheetTextAsset.text[_styleIndex] == '\n')
            {
                _styleSheetRowCount++;
            }
        }
        _styleSheetNameList = new string[_styleSheetRowCount];
        _styleSheetTagList = new string[_styleSheetRowCount];

        // Populate style sheet name list and tag list.
        int _state = 0;
        int _index = 0;
        string _text = "";
        for (int _characterIndex = 0; _characterIndex < styleSheetTextAsset.text.Length; _characterIndex++)
        {
            char _character = styleSheetTextAsset.text[_characterIndex];
            if (_character == '\n')
            {
                // Debug.Log($"PopulateStyleSheet() | A New-Line Was Found | _state: {_state} | _index: {_index} | _text: {_text}");

                if (_state == 3)
                {
                    _styleSheetTagList[_index] = _text.Substring(0, _text.Length - 1);
                    _text = "";
                    _index++;
                }
                _state = 0;
            }
            else if (_state == 0 && (_character == '<' || char.IsLetterOrDigit(_character) || _character == '\\'))
            {
                _state = 1;
            }
            else if (_character == '\t')
            {
                if (_state == 1)
                {
                    _styleSheetNameList[_index] = _text;
                    _text = "";
                }
                _state = 2;
            }
            else if (_state == 2 && (_character == '<' || char.IsLetterOrDigit(_character) || _character == '\\'))
            {
                _state = 3;
            }
            if (_state == 1 || _state == 3)
            {
                _text += _character;
            }
        }
    }

    private void DefineMainVariables()
    {
        _rightHandGameObject.transform.SetParent(null);
        if (_isVRSettingsOverride && Networking.LocalPlayer.IsUserInVR())
        {
            // Skip for now.
            // _defaultFontSizeIndex = 2;
            MediumScale();
        }
        Memory.PopulateTextAssetList();
        _mainMenuGameObject.SetActive(true);
        // _backButtonGameObject.SetActive(false);
        _topLeftTMP.gameObject.SetActive(false);
        _topLeftCloneTMP.gameObject.SetActive(false);
        _mainTextList = Memory.GetTextAsset(_defaultBookIndex, true);
        _mainBlockList = Memory.GetTextAsset(_defaultBookIndex, false);
        _mainBookIndex = _defaultBookIndex;
        _mainBlockIndex = Mathf.Clamp(_defaultBlockIndex, 0, _mainTextList.Length - 1);
        _mainFontSize = _fontSizeList[_defaultFontSizeIndex];
        _mainFontSizeIndex = _defaultFontSizeIndex;
        _mainTurnPageIndex = _defaultTurnPageIndex;
        _mainFontType = _fontTypeList[_defaultFontTypeIndex].Trim(_badCharList);
        _mainFontTypeIndex = _defaultFontTypeIndex;
        _mainOrientation = _defaultOrientation;
        _overflowPageIndex = -1;
        _richTextBoolList = new bool[10];
        _richTextStringList = new string[10];
        _categoryVectorDefault0 = _categoryGameObject0.transform.localPosition;
        _categoryVectorDefault1 = _categoryGameObject1.transform.localPosition;
        _categoryVectorDefault2 = _categoryGameObject2.transform.localPosition;
        _categoryVectorDefault3 = _categoryGameObject3.transform.localPosition;
        _categoryVectorDefault4 = _categoryGameObject4.transform.localPosition;
        _screenSaverVectorDefault = _screenSaverBackgroundGameObject.transform.localPosition;
        _homeContainerVectorDefault = _homeContainerGameObject.transform.localPosition;

        _categoryGameObject1.transform.SetParent(_categoryGameObject0.transform);
        _categoryGameObject2.transform.SetParent(_categoryGameObject1.transform);
        _categoryGameObject3.transform.SetParent(_categoryGameObject2.transform);
        _categoryGameObject4.transform.SetParent(_categoryGameObject3.transform);
        _categoryVectorTarget0 = _categoryVectorDefault0;
        _categoryVectorTarget1 = _categoryVectorDefault1;
        _categoryVectorTarget2 = _categoryVectorDefault2;
        _categoryVectorTarget3 = _categoryVectorDefault3;
        _categoryVectorTarget4 = _categoryVectorDefault4;
        _screenSaverVectorTarget = _screenSaverVectorDefault;
        _homeContainerVectorTarget = _homeContainerVectorDefault;
        _screenSaverBackgroundHidden = _screenSaverBackgroundGameObject.transform.localPosition;
        _screenSaverBackgroundShown = _screenSaverBackgroundHidden + new Vector3(0f, 1300f, 0f);
        _homeContainerShown = _homeContainerVectorDefault;
        _homeContainerHidden = _homeContainerVectorDefault + new Vector3(0f, -1200f, 0f);

        _bottomCoreTMP.text = "";
        _bottomCoreCloneTMP.text = _bottomCoreTMP.text;
        _bigHammerLocCoordinates = -1;
        _isTutorialMode = true;
        _isFirstTimeMode = true;

        _optionsMenuGameObject.SetActive(true);
        _optionsContainerVectorDefault = _optionsContainerGameObject.transform.localPosition;
        _optionsContainerVectorTarget = _optionsContainerVectorDefault;
        _optionsContainerShown = _optionsContainerVectorDefault;
        _optionsContainerHidden = _optionsContainerVectorDefault + new Vector3(0f, 460f, 0f);
        // Hide the options menu.
        OptionsButton();


        _bottomCoreTMP.gameObject.SetActive(false);
        _bottomCoreCloneTMP.gameObject.SetActive(false);
        _screenSaverScreenGameObject.SetActive(true);
        _locNavigationSlider.gameObject.SetActive(false);

        // We need to set the banners up.
        foreach (GameObject _banner in _bookBannerGameObjectList)
        {
            _banner.SetActive(false);
        }
        for (int i = 0; i < _preservedLocBookList.Length; i++)
        {
            _preservedLocBookList[i] = -1;
        }

        // Get text components of all children.
        int _counter = 0;
        foreach (TextMeshProUGUI g in transform.GetComponentsInChildren<TextMeshProUGUI>())
        {
            _counter++;
        }

        _everySingleTextBoxList = new TextMeshProUGUI[_counter];
        _everySingleTextBoxGameObjectList = new GameObject[_counter];
        _counter = 0;
        foreach (TextMeshProUGUI g in transform.GetComponentsInChildren<TextMeshProUGUI>())
        {
            _everySingleTextBoxList[_counter] = g;
            _everySingleTextBoxGameObjectList[_counter] = g.gameObject;
            _counter++;
        }

        for (int i = 0; i < _bookButtonGameObjectList.Length; i++)
        {
            bool _isInteractable = _bookButtonGameObjectList[i].GetComponent<Button>().interactable;
            Button _bannerButton = _bookBannerGameObjectList[i].GetComponent<Button>();
            _bannerButton.interactable = _isInteractable;
            if (!_bookButtonGameObjectList[i].transform.parent.gameObject.activeSelf)
            {
                Destroy(_bookButtonGameObjectList[i].transform.parent.gameObject);
            }
            else if (!_isInteractable)
            {
                _bannerButton.image.sprite = _bannerNotInteractable;
                _bookBannerGameObjectList[i].SetActive(true);
                foreach (TextMeshProUGUI g in _bookBannerGameObjectList[i].GetComponentsInChildren<TextMeshProUGUI>())
                {
                    g.text = $"<size=40><line-height=-57><cspace=-.00em>\nn/a";
                }
            }
            else
            {
                _bannerButton.image.sprite = _bannerInteractable;
                _bookBannerGameObjectList[i].SetActive(false);
            }
        }
        _calibrateMemoryDeepFreeze = -1;

        Color tempColor;
        tempColor.r = .898f;
        tempColor.g = .890f;
        tempColor.b = .878f;
        tempColor.a = 1.0f;

        _materialGreyLight.color = tempColor;

        ScreenSaverStart(true);
        MinimizeCategories();
        HomeInfo();
    }

    private void ExecuteCalibration()
    {
        if (_isBookStaged)
        {
            Calibrate(0);
            // _topCenterTMP.text = $"{_mainBlock.text.Substring(0, _mainBlock.text.IndexOf('\n'))}";
            // _topCenterCloneTMP.text = _topCenterTMP.text;
            if (!_isHomeContainerHidden)
            {
                HomeContainer();
            }
            //_mainMenuGameObject.SetActive(false);
            
            //_backButtonGameObject.SetActive(true);
            //_topLeftTMP.gameObject.SetActive(true);
            //_topLeftCloneTMP.gameObject.SetActive(true);
            _isOverflowAuditDefinePage = true;
        }
    }

    private void Calibrate(int blockIncrementValue)
    {
        DefineMainTMP();
        DefineMainFiles(blockIncrementValue);
        DefineLocation();
        DefineMainPageLength();
        PopulateLastCharSliceList();
        PrepareOverflowAudit();
    }

    private void DefineMainTMP()
    {
        // Can simplify this by getting direct expressions stored in list instead.
        if (_mainTMP != null)
        {
            _mainTMP.text = "";
        }
        if (_defaultFontTypeIndex == 0)
        {
            // CopyRectTransform(_helveticaTMP, _helveticaCloneTMP);
            _mainTMP = _helveticaTMP;
            _mainCloneTMP = _helveticaCloneTMP;
        }
        else if (_defaultFontTypeIndex == 1)
        {
            _mainTMP = _tahomaTMP;
            _mainCloneTMP = _tahomaCloneTMP;
        }
        else if (_defaultFontTypeIndex == 2)
        {
            _mainTMP = _notoSerifTMP;
            _mainCloneTMP = _notoSerifCloneTMP;
        }
        else if (_defaultFontTypeIndex == 3)
        {
            _mainTMP = _timesTMP;
            _mainCloneTMP = _timesCloneTMP;
        }
        else if (_defaultFontTypeIndex == 4)
        {
            _mainTMP = _verdanaTMP;
            _mainCloneTMP = _verdanaCloneTMP;
        }
        else if (_defaultFontTypeIndex == 5)
        {
            _mainTMP = _avenirTMP;
            _mainCloneTMP = _avenirCloneTMP;
        }

        // If loading book new, we need to clear all previous text.
        if (_isLoadBookNew)
        {
            _isLoadBookNew = false;
            _mainTMP.text = "";
            _mainCloneTMP.text = "";
        }
    }

    private void CopyRectTransform(TextMeshProUGUI newTMP, TextMeshProUGUI newCloneTMP)
    {
        if (_mainTMP == null) return;
        RectTransform _mainTMPRectTransform = _mainTMP.GetComponent<RectTransform>();
        RectTransform _newTMPRectTransform = newTMP.GetComponent<RectTransform>();
        RectTransform _newCloneTMPRectTransform = newCloneTMP.GetComponent<RectTransform>();
        _newTMPRectTransform.sizeDelta = _mainTMPRectTransform.sizeDelta;
        _newTMPRectTransform.rotation = _mainTMPRectTransform.rotation;
        _newTMPRectTransform.localPosition = _mainTMPRectTransform.localPosition;
        _newCloneTMPRectTransform.sizeDelta = _mainTMPRectTransform.sizeDelta;
        _newCloneTMPRectTransform.rotation = _mainTMPRectTransform.rotation;
        _newCloneTMPRectTransform.localPosition = _mainTMPRectTransform.localPosition;
    }

    private void DefineMainFiles(int blockIncrementValue)
    {
        _mainStatusCode = $"TO:{_mainOrientation},FS:{_mainFontSizeIndex},FT:{_mainFontTypeIndex},PC:";
        _mainBlockIndex += blockIncrementValue;
        _mainText = _mainTextList[_mainBlockIndex];
        _mainBlock = _mainBlockList[_mainBlockIndex];
    }

    private void DefineLocation()
    {
        // Junk Code:
        int _startLocationIndex = _mainBlock.text.IndexOf(_locationCodeList[0]) + _locationCodeList[0].Length;
        int _endLocationIndex = _mainBlock.text.IndexOf(_locationCodeList[1]) - 1;
        _mainLocationHeight = Convert.ToInt32(_mainBlock.text.Substring(_startLocationIndex, _endLocationIndex - _startLocationIndex));
        _startLocationIndex = _mainBlock.text.IndexOf(_locationCodeList[1]) + _locationCodeList[1].Length;
        _endLocationIndex = _mainBlock.text.IndexOf(_locationCodeList[2]) - 1;
        _mainBlockHeight = Convert.ToInt32(_mainBlock.text.Substring(_startLocationIndex, _endLocationIndex - _startLocationIndex));
        _startLocationIndex = _mainBlock.text.IndexOf(_locationCodeList[2]) + _locationCodeList[2].Length;
        _endLocationIndex = _mainBlock.text.IndexOf('\n', _startLocationIndex) - 1;
        _mainMaxHeight = Convert.ToInt32(_mainBlock.text.Substring(_startLocationIndex, _endLocationIndex - _startLocationIndex));
    }

    private void DefineMainPageLength()
    {
        int _startPageLengthIndex = _mainBlock.text.IndexOf(_mainStatusCode) + _mainStatusCode.Length;
        int _endPageLengthIndex = _mainBlock.text.IndexOf('\n', _startPageLengthIndex);
        string _pageLengthString = _mainBlock.text.Substring(_startPageLengthIndex, _endPageLengthIndex - _startPageLengthIndex);
        _mainPageLength = Convert.ToInt32(_pageLengthString);
        _richTextOverflowList = new string[_mainPageLength];
        _lastCharSliceList = new int[_mainPageLength + 2];  // Account for the double-0 rows.
        _mainCharZeroIndex = _endPageLengthIndex + 1;  // Account for the /n.

        // Debug.Log($"DefineMainPageLength() | _mainPageLength: {_mainPageLength} | _mainCharZeroIndex: {_mainCharZeroIndex}");
    }

    private void PopulateLastCharSliceList()
    {
        int _index = 0;
        string _text = "";
        for (int _characterIndex = _mainCharZeroIndex; _characterIndex < _mainBlock.text.Length; _characterIndex++)
        {
            char _character = _mainBlock.text[_characterIndex];
            if (_character != '\n')
            {
                _text += _character;
                continue;
            }

            // Debug.Log($"PopulateLastCharSliceList() | _index: {_index} | _text: {_text}");

            int _parsedText;
            if (int.TryParse(_text, out _parsedText))
            {
                _lastCharSliceList[_index] = _parsedText;
                _index++;
                _text = "";
            }
            else return;
        }
    }

    private void PrepareOverflowAudit()
    {
        // Debug.Log($"DefineOverflow() | _mainText.name: {_mainText.name} | _mainPageLength: {_mainPageLength} | _endCalibrationPageIndex: {_endCalibrationPageIndex}");

        _overflowPageIndex = 0;
        _endCalibrationPageIndex = _mainPageIndex;
        _mainPageIndex = _overflowPageIndex;
        _mainTMP.gameObject.SetActive(false);

        // We want clone to appear when changing chapters.
        if (!_isOverflowAuditDefinePage)
        {
            // _mainCloneTMPGameObject.SetActive(false);
        }
    }

    private void LoopOverflowAudit()
    {
        if (_overflowPageIndex < _mainPageLength)
        {
            // Populate the text of Main TMP.
            _mainPageIndex = _overflowPageIndex;

            // Debug.Log($"LoopOverflowAudit() | _mainPageIndex: {_mainPageIndex} | _lastCharSliceList[_mainPageIndex]: {_lastCharSliceList[_mainPageIndex]} | _lastCharSliceList[_mainPageIndex + 1]: {_lastCharSliceList[_mainPageIndex + 1]} | Char Count Of Sliced Page: {_lastCharSliceList[_mainPageIndex] - _lastCharSliceList[_mainPageIndex + 1]}");

            int _startIndex = _mainPageIndex == 0 ? 0 : _lastCharSliceList[_mainPageIndex] + 1;
            int _endIndex = _lastCharSliceList[_mainPageIndex + 1] - _lastCharSliceList[_mainPageIndex];
            _mainTMP.text = _mainText.text.Substring(_startIndex, _endIndex);

            // Processes the text so all rich-text is TextMeshPro compatible.
            DecompressRichText(true);
            InsertRichText();
            ReplaceRichText();
            RemoveImageRichText();
            DefineRichTextOverflowElements();
            PopulateRichTextOverflowList();
            _overflowPageIndex++;
        }
        else ExitOverflowAudit();
    }

    private void ExitOverflowAudit()
    {
        _overflowPageIndex = -1;
        if (_isBookPresetLoc)
        {
            // Do not custom set the main page index if preset loc - it is already set correctly from Deep Freeze.
            _mainPageIndex = _deepFreezePageIndex;
            _isBookPresetLoc = false;
            // Debug.Log($"LoopOverflowAudit() | Exiting Overflow | _mainPageIndex: {_mainPageIndex}");

        }
        else
        {
            _mainPageIndex = _endCalibrationPageIndex == -1 ? _mainPageLength - 1 : _endCalibrationPageIndex;
        }
        _mainPageIndex = Mathf.Clamp(_mainPageIndex, 0, _mainPageLength - 1);
        _endCalibrationPageIndex = 0;

        // Debug.Log($"LoopOverflowAudit() | Audit Complete! | _mainPageIndex: {_mainPageIndex}");

        _helveticaCloneTMP.text = "";
        _tahomaCloneTMP.text = "";
        _notoSerifCloneTMP.text = "";

        // Refresh page and make TMP visible again.
        if (_isOverflowAuditDefinePage)
        {
            // Debug.Log($"LoopOverflowAudit() | Pre-Defined Page | _mainPageIndex: {_mainPageIndex}");
            DefinePage(_mainPageIndex, true);
            _mainCloneTMP.text = _mainTMP.text;
            _mainPreviousCloneTMP = null;
            // Debug.Log($"LoopOverflowAudit() | Post-Defined Page | _mainPageIndex: {_mainPageIndex}");
            _isOverflowAuditDefinePage = false;
        }
        _mainTMP.gameObject.SetActive(true);
        _mainCloneTMP.gameObject.SetActive(true);
    }

    private void DecompressRichText(bool isAppendNewLine)
    {
        // Here be dragons. Most complex function of Andrea.

        // Replaces all rich-text in text with custom rich-text.
        for (int _index = 0; _index < _styleSheetNameList.Length; _index++)
        {
            _mainTMP.text = _mainTMP.text.Replace(_styleSheetNameList[_index], _styleSheetTagList[_index]);
        }

        // Define variables.
        bool _isAfterNewLine = false;
        bool _isBullet = false;
        bool _isBreakBullet = false;
        bool _isDelayBreakBullet = false;
        bool _isDelayedExitBullet = false;
        bool _isTabbedBullet = false;
        bool _isIgnoreNextBullet = false;

        char _firstCharacterThisPage = _mainTMP.text[0];
        char _firstCharacterNextPage = _mainText.text[_lastCharSliceList[_mainPageIndex + 1]];
        char _lastCharacterLastPage = _mainText.text[_lastCharSliceList[_mainPageIndex]];

        string _tagStartBullet = _styleSheetTagList[Array.IndexOf(_styleSheetNameList, "<START>")];
        string _tagBreakBullet = _styleSheetTagList[Array.IndexOf(_styleSheetNameList, "<BREAK>")];
        string _tagExitBullet = _styleSheetTagList[Array.IndexOf(_styleSheetNameList, "<RETURN>")];
        string _tagWideLeft = _styleSheetTagList[Array.IndexOf(_styleSheetNameList, "<N-LEFT>")];
        string _tagBoldLeft = _styleSheetTagList[Array.IndexOf(_styleSheetNameList, "<B-LEFT>")];
        string _tagNormalLeft = _styleSheetTagList[Array.IndexOf(_styleSheetNameList, "<LEFT>")];
        string _tagBoldRight = _styleSheetTagList[Array.IndexOf(_styleSheetNameList, "<BOLD_RIGHT>")];
        string _tagNormalRight = _styleSheetTagList[Array.IndexOf(_styleSheetNameList, "<REG_RIGHT>")];
        string _tagTabBullet = _styleSheetTagList[Array.IndexOf(_styleSheetNameList, "<TAB_BULLET>")];
        string _leftInsert = "";
        string _rightInsert = "";

        // Check if we want to append a new line to the text.
        if (isAppendNewLine && _firstCharacterNextPage == '\n')
        {
            _mainTMP.text += "\n";
        }

        // Check for bullet overflow. Check if text appears after a new line.
        if (_mainPageIndex > 0)
        {
            char _secondLastCharacterLastPage = _mainText.text[_lastCharSliceList[_mainPageIndex] - 1];
            string _richTextOverflow = _richTextOverflowList[_mainPageIndex - 1];
            if (_richTextOverflow.Contains("<indent="))
            {
                _isDelayBreakBullet = true;
                _isDelayedExitBullet = true;
                _isBullet = true;
            }
            else if (_richTextOverflow.Contains(_tagStartBullet) && _firstCharacterThisPage != '\n')
            {
                _isAfterNewLine = true;
            }
            else if ((_lastCharacterLastPage == '\n' || _secondLastCharacterLastPage == '\n') && _firstCharacterThisPage != '\n' && IsCharacterABullet(_firstCharacterThisPage, 0))
            {
                _isAfterNewLine = true;
            }
        }

        // Debug.Log($"_isDelayBreakBullet: {_isDelayBreakBullet} | _isDelayedExitBullet: {_isDelayedExitBullet} | _isBullet: {_isBullet} | _isAfterNewLine: {_isAfterNewLine} | Before Loop Execution!");

        // Loops through every character on the page. Formats rich-text for bulleting. 
        for (int _characterIndex = 0; _characterIndex < _mainTMP.text.Length; _characterIndex++)
        {
            char _character = _mainTMP.text[_characterIndex];

            // Debug.Log($"_mainBlockIndex: {_mainBlockIndex} | _mainPageIndex: {_mainPageIndex} | _characterIndex: {_characterIndex} | _character: {_character} | Single Loop Execution!");

            // Inserts indent rich-text to the right of the bullet.
            if (_rightInsert != "" && Array.IndexOf(_bracketCharList, _character) >= 0)
            {
                // Debug.Log($"_mainBlockIndex: {_mainBlockIndex} | _isBreakBullet: {_isBreakBullet} | _rightInsert: {_rightInsert} | _mainPageIndex: {_mainPageIndex} | _characterIndex: {_characterIndex} | _character: {_character} | Right Insert!");

                int _insertIndex = _characterIndex + 1;
                if (_isTabbedBullet)
                {
                    int _swapNumIndex = _rightInsert.IndexOf("<indent=", 0) + "<indent=".Length;
                    int _swapNum = Int32.Parse(_rightInsert.Substring(_swapNumIndex, 1)) + Int32.Parse(_tagTabBullet);
                    _rightInsert = _rightInsert.Replace($"<indent={ _rightInsert[_swapNumIndex]}", $"<indent={_swapNum}");
                    _insertIndex++;
                }
                _mainTMP.text = _mainTMP.text.Insert(_insertIndex, _rightInsert);
                _characterIndex += _rightInsert.Length;
                _rightInsert = "";
                _isBreakBullet = true;
                _isTabbedBullet = false;
                continue;
            }

            // Breaks the bullet when a new-line appears.
            if (_character == '\n')
            {
                // Debug.Log($"_mainBlockIndex: {_mainBlockIndex} | _isBreakBullet: {_isBreakBullet} | _isDelayBreakBullet: {_isDelayBreakBullet} | _mainPageIndex: {_mainPageIndex} | _characterIndex: {_characterIndex} | _character: {_character} | That's A New-Line!");

                _isAfterNewLine = true;
                if (_isDelayBreakBullet)
                {
                    _isBreakBullet = _isDelayBreakBullet;
                    _isDelayBreakBullet = false;
                    _isIgnoreNextBullet = _characterIndex == 0 ? true : false;
                }
                else if (_isBreakBullet)
                {
                    _isBreakBullet = false;
                    _isDelayedExitBullet = false;
                    _mainTMP.text = _mainTMP.text.Insert(_characterIndex - 1, _tagBreakBullet);
                    _characterIndex += _tagBreakBullet.Length;
                }
                continue;
            }

            // Creates a bullet if the text after a new line is the start of a bullet.
            if (_isAfterNewLine && IsCharacterABullet(_character, _characterIndex))
            {
                // Debug.Log($"_mainBlockIndex: {_mainBlockIndex} | _isBreakBullet: {_isBreakBullet} | _isDelayBreakBullet: {_isDelayBreakBullet} | _mainPageIndex: {_mainPageIndex} | _characterIndex: {_characterIndex} | _character: {_character} | Create Bullet!");

                _isAfterNewLine = false;

                // Edge Case: Catch accidental bullets while already in a bullet loop.
                if (_isIgnoreNextBullet)
                {
                    _isIgnoreNextBullet = false;
                    if (_characterIndex == 1) continue;
                }

                // Begin a bullet loop if not already in one.
                if (!_isBullet)
                {
                    _isBullet = true;
                    _mainTMP.text = _mainTMP.text.Insert(_characterIndex, _tagStartBullet);
                    _characterIndex += _tagStartBullet.Length;
                }

                // Process triple-length bullet.
                if (char.IsDigit(_character) && char.IsDigit(_mainTMP.text[_characterIndex + 1]))
                {
                    _leftInsert = _tagWideLeft;
                    _rightInsert = _tagNormalRight;
                    _isTabbedBullet = _mainTMP.text[_characterIndex + 3] == '​' ? true : false;
                }

                // Process single-length bullet.
                else if (_character == '•')
                {
                    _leftInsert = _tagBoldLeft;
                    _rightInsert = _tagBoldRight;
                    _isTabbedBullet = _mainTMP.text[_characterIndex + 1] == '​' ? true : false;
                }

                // Process double-length bullet.
                else
                {
                    _leftInsert = _tagNormalLeft;
                    _rightInsert = _tagNormalRight;
                    _isTabbedBullet = _mainTMP.text[_characterIndex + 2] == '​' ? true : false;
                }

                // Make the spacing wider if this is a tabbed bullet.
                if (_isTabbedBullet)
                {
                    int _swapNumIndex = _leftInsert.IndexOf("<space=", 0) + "<space=".Length;
                    int _swapNum = Int32.Parse(_leftInsert.Substring(_swapNumIndex, 1)) + Int32.Parse(_tagTabBullet);
                    _leftInsert = _leftInsert.Replace($"<space={_leftInsert[_swapNumIndex]}", $"<space={_swapNum}");
                }

                // Insert the text.
                _mainTMP.text = _mainTMP.text.Insert(_characterIndex, _leftInsert);
                _characterIndex += _leftInsert.Length;
                _characterIndex -= _character == '•' ? 1 : 0;
                continue;
            }

            // Terminate bullet loop.
            if (_isAfterNewLine && _isBullet && !_isDelayedExitBullet)
            {
                _mainTMP.text = _mainTMP.text.Insert(_characterIndex - 2, _tagExitBullet);
                _characterIndex += _tagExitBullet.Length;
                _isBullet = false;
                _isAfterNewLine = false;
                _isBreakBullet = false;
                _rightInsert = "";
                continue;
            }

            // Nothing to process.
            _isAfterNewLine = false;

        }
    }

    private bool IsCharacterABullet(char character, int characterIndex)
    {
        if (char.IsDigit(character))
        {
            return true;
        }
        else if (character == '•')
        {
            return true;
        }
        else if (char.IsLetter(character) && Array.IndexOf(_bracketCharList, _mainTMP.text[characterIndex + 1]) >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void InsertRichText()
    {
        // Inserts any rich-text overflow at the start of text.
        if (_mainPageIndex > 0 && _richTextOverflowList[_mainPageIndex - 1] != null)
        {
            _mainTMP.text = _mainTMP.text.Insert(0, _richTextOverflowList[_mainPageIndex - 1]);
        }

        // Insert the proper text size.
        _mainTMP.text = _mainTMP.text.Insert(0, $"<size={_mainFontSize}>");
    }

    private void DefineRichTextOverflowElements()
    {
        // Define and format variables.
        _isStringRichText = false;
        _richTextString = "";
        for (int _index = 0; _index < _richTextBoolList.Length; _index++)
        {
            _richTextBoolList[_index] = false;
        }
        for (int _index = 0; _index < _richTextStringList.Length; _index++)
        {
            _richTextStringList[_index] = null;
        }

        // Identify rich-text overflow elements (rich-text that never had a closing command).
        for (int _characterIndex = 0; _characterIndex < _mainTMP.text.Length; _characterIndex++)
        {
            char _character = _mainTMP.text[_characterIndex];

            // Debug.Log($"_characterIndex: {_characterIndex} | _mainPageIndex: {_mainPageIndex} | _character: {_character} | _isStringRichText: {_isStringRichText} | _richTextString: {_richTextString}");

            // Means a rich-text command has opened or closed.
            if (_character == '<' || _character == '>')
            {
                if (_isStringRichText)
                {
                    bool _isExit = false;

                    // Error out code.
                    if (_richTextString == "" || _richTextString == null || _richTextString.Length == 0)
                    {
                        Debug.Log($"Error: Rich Command has a length of zero! Did you forget to add a tab somewhere?");
                        _richTextString.Substring(0, Int32.MaxValue);
                    }

                    // Debug.Log($"_characterIndex: {_characterIndex} | _mainPageIndex: {_mainPageIndex} | _character: {_character} | _isStringRichText: {_isStringRichText} | _richTextString: {_richTextString}");

                    // Closing command rich-text.
                    if (_richTextString[0] == '/')
                    {
                        _isExit = true;
                        _richTextString = _richTextString.Substring(1);
                    }

                    // Identify single-letter rich-text types (bold, italics, underline, strike-through).
                    if (_richTextString == "b")
                    {
                        _richTextBoolList[0] = !_isExit;
                    }
                    else if (_richTextString == "i")
                    {
                        _richTextBoolList[1] = !_isExit;
                    }
                    else if (_richTextString == "u")
                    {
                        _richTextBoolList[2] = !_isExit;
                    }
                    else if (_richTextString == "s")
                    {
                        _richTextBoolList[3] = !_isExit;
                    }

                    // Error out code.
                    else if (_richTextString.Length < 3)
                    {
                        Debug.Log($"Error: Rich Command ({_richTextString}) wasn't matched! Was capitalization accidently used (<B> instead of <b>)?");
                        _richTextString.Substring(0, Int32.MaxValue);
                    }

                    // Identity multi-letter rich-text types (smallcaps, align, indent, size, font, margin, cspace).
                    else if (_richTextString.Substring(0, 3) == "sma")
                    {
                        _richTextBoolList[4] = !_isExit;
                    }
                    else if (_richTextString.Substring(0, 3) == "ali")
                    {
                        _richTextStringList[0] = _isExit ? null : _richTextString.Substring(_richTextString.LastIndexOf('=') + 1);
                        if (_richTextStringList[0] == "\"justified\"")
                        {
                            _richTextStringList[0] = null;
                        }
                        // Debug.Log($"_mainPageIndex {_mainPageIndex} | _richTextStringList[0]: {_richTextStringList[0]}");
                    }
                    else if (_richTextString.Substring(0, 3) == "ind")
                    {
                        _richTextStringList[1] = _isExit ? null : _richTextString.Substring(_richTextString.LastIndexOf('=') + 1);
                    }
                    else if (_richTextString.Substring(0, 3) == "siz")
                    {
                        _richTextStringList[2] = _richTextString.Substring(_richTextString.LastIndexOf('=') + 1);

                        // If same as default font size, there is no overflow. 
                        if (_richTextStringList[2] == "100%" || _richTextStringList[2] == _mainFontSize.ToString())
                        {
                            _richTextStringList[2] = null;
                        }
                    }
                    else if (_richTextString.Substring(0, 3) == "fon")
                    {
                        _richTextStringList[3] = _isExit ? null : _richTextString.Substring(_richTextString.LastIndexOf('=') + 1);

                        // If same as default font type, there is no overflow. 
                        if (_richTextStringList[3] == '"' + _mainFontType + '"')
                        {
                            _richTextStringList[3] = null;
                        }
                    }
                    else if (_richTextString.Substring(0, 3) == "mar")
                    {
                        _richTextStringList[4] = _isExit ? null : _richTextString.Substring(_richTextString.LastIndexOf('=') + 1);
                    }
                    else if (_richTextString.Substring(0, 3) == "csp")
                    {
                        _richTextStringList[5] = _isExit ? null : _richTextString.Substring(_richTextString.LastIndexOf('=') + 1);
                    }
                    _richTextString = "";
                }
                _isStringRichText = !_isStringRichText;
            }

            // Append character to string if rich-text bracket is open.
            else if (_isStringRichText)
            {
                _richTextString += _character;
            }
        }
    }

    private void PopulateRichTextOverflowList()
    {
        // Converts the rich-text overflow into actual rich-text commands for insertion.
        string _compression = "";
        for (int _index = 0; _index < _richTextBoolList.Length; _index++)
        {
            if (_richTextBoolList[_index])
            {
                if (_index == 0)
                {
                    _compression += "<b>";
                }
                else if (_index == 1)
                {
                    _compression += "<i>";
                }
                else if (_index == 2)
                {
                    _compression += "<u>";
                }
                else if (_index == 3)
                {
                    _compression += "<s>";
                }
                else if (_index == 4)
                {
                    _compression += "<smallcaps>";
                }
            }
        }
        for (int i = 0; i < _richTextStringList.Length; i++)
        {
            if (_richTextStringList[i] != null)
            {
                if (i == 0)
                {
                    _compression += $"<align={_richTextStringList[i]}>";
                }
                else if (i == 1)
                {
                    _compression += $"<indent={_richTextStringList[i]}>";
                }
                else if (i == 2)
                {
                    _compression += $"<size={_richTextStringList[i]}>";
                }
                else if (i == 3)
                {
                    _compression += $"<font={_richTextStringList[i]}>";
                }
                else if (i == 4)
                {
                    _compression += $"<margin={_richTextStringList[i]}>";
                }
                else if (i == 5)
                {
                    _compression += $"<cspace={_richTextStringList[i]}>";
                }
            }
        }
        _richTextOverflowList[_mainPageIndex] = _compression;
    }

    private void Update()
    {
        if (registry == 0)
        {
            _candleVRCPickup.pickupable = true;
        }
        else if (Networking.IsOwner(this.gameObject) && registry != 0 && !_isLeftGripActive && !_isRightGripActive) // Incase if person holding Andrea leaves, the new owner will set registry back to zero and make it pickupable.
        {
            registry = 0;
            _candleVRCPickup.pickupable = true;
        }
        else
        {
            _candleVRCPickup.pickupable = false;
        }
        LerpScreenSaver();

        GripInputLogistics();

        if (_miscsGameObject.GetComponent<BoxCollider>() != null)
        {
            _miscsGameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
        if (_screenGameObject.GetComponent<BoxCollider>() != null)
        {
            _screenGameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
        if (_bottomBarGameObject.GetComponent<BoxCollider>() != null)
        {
            _bottomBarGameObject.GetComponent<BoxCollider>().isTrigger = true;
        }

        if (_isCategoryLerp0 || _isCategoryLerp1 || _isCategoryLerp2 || _isCategoryLerp3)
        {
            LerpUpdate();
        }

        // Process rich-text overflow.
        if (_overflowPageIndex >= 0)
        {
            ProcessRichTextOverflow();

            // Process clone timer. (Junk Code).
            /*
            if (_mainTMP != null && _overflowPageIndex == -1)
            {
                _cloneTick += Time.deltaTime;
                while (_cloneTick >= _cloneCycle)
                {
                    _cloneTick -= _cloneCycle;
                    if (_mainTMP.text != _mainCloneTMP.text)
                    {
                        _cloneTime++;
                        if (_cloneTime >= _cloneDelay)  // Hundreths of Second.
                        {
                            _cloneTime = 0;
                            _mainCloneTMP.text = _mainTMP.text;
                            if (_mainPreviousCloneTMP != null)
                            {
                                _mainPreviousCloneTMP.text = "";
                            }
                        }
                    }
                }
            }
            */

            return;
        }

        // Process clone timer.
        if (_mainTMP != null)
        {
            _cloneTick += Time.deltaTime;
            while (_cloneTick >= _cloneCycle)
            {
                _cloneTick -= _cloneCycle;
                if (_mainTMP.text != _mainCloneTMP.text)
                {
                    _cloneTime++;
                    if (_cloneTime >= _cloneDelay)  // Hundreths of Second.
                    {
                        _cloneTime = 0;
                        _mainCloneTMP.text = _mainTMP.text;
                        // Unsure why blank?
                        /*
                        if (_mainTMP.text != "")
                        {
                            _mainCloneTMP.text = _mainTMP.text;
                        }
                        */
                        if (_mainPreviousCloneTMP != null)
                        {
                            _mainPreviousCloneTMP.text = "";
                        }
                    }
                }
            }
        }

        // Process inputs only if book staged.
        if (_isBookStaged)
        {
            ProcessInputs();
        }


        // Junk Code:
        /*
        time += Time.deltaTime;
        if (time >= interpolationPeriod)
        {
            time = time - interpolationPeriod;

            if (_mainMenuGameObject.activeSelf)
            {
                HomeInfo();
            }
        }
        */
    }

    private void ProcessRichTextOverflow()
    {
        // Calculate rich-text overflows for the new text block.
        // Removed for now, distracts from reading.
        if (_mainPageLength != 1)
        {
            _bottomLeftTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}<cspace=-0.02em>Load {(_mainPageLength == 1 ? 99 : _mainPageIndex * 100 / (_mainPageLength - 1))}%";
            // _bottomLeftTMP.text = $"Loading... {(_mainPageLength == 1 ? 100 : _mainPageIndex * 100 / (_mainPageLength - 1))}%";
            _bottomLeftCloneTMP.text = _bottomLeftTMP.text;
        }

        // _bottomRightTMP.text = $"{(_mainPageLength == 1 ? 100 : _mainPageIndex * 100 / (_mainPageLength - 1))}%";
        // _bottomRightCloneTMP.text = _bottomRightTMP.text;

        // _locNavigationSlider.gameObject.SetActive(false);

        LoopOverflowAudit();

        // Overflow audits are complete.
        if (_overflowPageIndex == -1)
        {
            // _locNavigationSlider.gameObject.SetActive(true);
            RefreshBottomGUI();
        }
    }

    private void GripInputLogistics()
    {
        /*
        if (_isScreenSaverHidden)
        {
            _screenSaverBackgroundGameObject.transform.localPosition = Vector3.Slerp(_screenSaverBackgroundGameObject.transform.localPosition, _screenSaverBackgroundHidden, Time.deltaTime * 1f);
        }
        else
        {
            _screenSaverBackgroundGameObject.transform.localPosition = Vector3.Slerp(_screenSaverBackgroundGameObject.transform.localPosition, _screenSaverBackgroundShown, Time.deltaTime * 1f);
        }
        */

        if (Networking.LocalPlayer != null && !Networking.LocalPlayer.IsUserInVR()) return;
        // Hand Transform.
        // FINGER_SHADOW.transform.position = LOCAL_PLAYER.GetBonePosition(fingerBone);

        // _rightHandGameObject.transform.position = Vector3.Slerp(_previousRightHandPosition + (Networking.LocalPlayer.GetPosition() - _previousPlayerPosition), Networking.LocalPlayer.GetBonePosition(HumanBodyBones.RightHand), Time.deltaTime * 9f);

        if (_candleVRCPickup.IsHeld && registry != 0)
        {
            _candleVRCPickup.Drop();
        }

        // Test which is better, previous position or current one?
        _rightHandGameObject.transform.position = Vector3.Slerp(_previousRightHandPosition, Networking.LocalPlayer.GetBonePosition(HumanBodyBones.RightHand), Time.deltaTime * 11f);
        _rightHandGameObject.transform.rotation = Quaternion.Slerp(_rightHandGameObject.transform.rotation, Networking.LocalPlayer.GetBoneRotation(HumanBodyBones.RightHand), Time.deltaTime * 11f);
        _previousRightHandPosition = _rightHandGameObject.transform.position;
        // _previousPlayerPosition = Networking.LocalPlayer.GetPosition(); 

        _leftHandGameObject.transform.position = Vector3.Slerp(_leftHandGameObject.transform.position, Networking.LocalPlayer.GetBonePosition(HumanBodyBones.LeftHand), Time.deltaTime * 11f);
        _leftHandGameObject.transform.rotation = Quaternion.Slerp(_leftHandGameObject.transform.rotation, Networking.LocalPlayer.GetBoneRotation(HumanBodyBones.LeftHand), Time.deltaTime * 11f);

        if (_candleVRCPickup.IsHeld && _isTogglePickedUp == false && registry == 0)
        {
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
            _candleVRCPickup.Drop();
            _candleVRCPickup.pickupable = false;
            if (_candleVRCPickup.currentHand == VRC_Pickup.PickupHand.Right)
            {
                _candleGameObject.transform.SetParent(_rightHandGameObject.transform);
                _isRightGripActive = true;
                registry = 1;
            }
            else
            {
                _candleGameObject.transform.SetParent(_leftHandGameObject.transform);
                _isLeftGripActive = true;
                registry = 2;
            }


            if (!_isScreenSaverHidden)
            {
                ScreenSaverClicked();
            }
            return;
        }

        if (_isRightGripActive && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryHandTrigger") <= 0.24f) // Toggle grip logistics.
        {
            // _isRightGripActive = false;  // Toggle grip logistics.
            if (_candleGameObject.transform.parent == _rightHandGameObject.transform)
            {
                _candleVRCPickup.pickupable = true;  // Single Grip Logistics.
                _candleGameObject.transform.SetParent(null);  // Single Grip Logistics.
                _isRightGripActive = false;
                Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
                registry = 0;
                if (_isScreenSaverHidden)
                {
                    // ScreenSaverClicked();
                }
                // _isTogglePickedUp = true;  // Toggle grip logistics.
                // This is when you remove the screen saver.
            }
        }

        if (_isLeftGripActive && Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryHandTrigger") <= 0.24f) // Toggle grip logistics.
        {
            // _isLeftGripActive = false;  // Toggle grip logistics.
            if (_candleGameObject.transform.parent == _leftHandGameObject.transform)
            {
                _candleVRCPickup.pickupable = true;  // Single Grip Logistics.
                _candleGameObject.transform.SetParent(null);  // Single Grip Logistics.
                _isLeftGripActive = false;
                Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
                registry = 0;
            }
        }

        _topDebugTMP.text = $"Registry: {registry}\nRight Grip: {_isRightGripActive}\nLeft Grip: {_isLeftGripActive}\nParent: {_candleGameObject.transform.parent.gameObject.name}";

        // Toggle grip logistics.
        /*
        else if (!_isRightGripActive && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryHandTrigger") > 0.28f)
        {
            _isRightGripActive = true;
            if (_isTogglePickedUp && _candleGameObject.transform.parent == _rightHandGameObject.transform)
            {
                _isTogglePickedUp = false;
                _candleVRCPickup.pickupable = true;
                _candleGameObject.transform.SetParent(null);
                _isScreenSaverHidden = false;
                // _screenSaverBackgroundGameObject.transform.localPosition = Vector3.Slerp(_screenSaverBackgroundGameObject.transform.localPosition, _screenSaverBackgroundShown, Time.deltaTime * 7f);
                return;
            }
        }
        */

        if (!_isBookStaged) return;

        if (_isUpTriggerActive && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryThumbstickVertical") <= 0.75f)
        {
            _isUpTriggerActive = false;
        }
        else if (!_isUpTriggerActive && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0.75f)
        {
            _isUpTriggerActive = true;
            if (_candleGameObject.transform.parent == _rightHandGameObject.transform || true)
            {
                if (_isScreenSaverHidden)
                {
                    _mainPageIndex--;
                    if (_overflowPageIndex == -1 && _mainCloneTMP.text != "")
                    {
                        _bigHammerLocCoordinates = -1;
                        if (_isTutorialMode && _adjustedLocationHeight > 1)
                        {
                            DisableTutorialMode();
                        }
                        PlayAudio(_softSwipeAudio);
                        DefinePage(_mainPageIndex, false);
                    }
                    // DefinePage(_mainPageIndex, true);
                }
            }
        }

        else if (_isDownTriggerActive && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryThumbstickVertical") >= -0.75f)
        {
            _isDownTriggerActive = false;
        }
        else if (!_isDownTriggerActive && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryThumbstickVertical") < -0.75f)
        {
            _isDownTriggerActive = true;
            if (_candleGameObject.transform.parent == _rightHandGameObject.transform || true)
            {
                if (_isScreenSaverHidden)
                {
                    _mainPageIndex++;
                    if (_overflowPageIndex == -1 && _mainCloneTMP.text != "")
                    {
                        _bigHammerLocCoordinates = -1;
                        if (_isTutorialMode)
                        {
                            DisableTutorialMode();
                        }
                        PlayAudio(_softSwipeAudio);
                        DefinePage(_mainPageIndex, true);
                    }
                    // DefinePage(_mainPageIndex, true);
                }
            }
        }

        /*
        if (_isBookStaged  && _isTogglePickedUp)
        {
            // Disable active controls if trigger is not pressed hard enough.
            if (_isRightTriggerActive && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryIndexTrigger") <= 0.26f)
            {
                _isRightTriggerActive = false;
            }

            // Enable active controls is trigger is pressed hard enough.
            else if (!_isRightTriggerActive && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryIndexTrigger") > 0.58f && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryHandTrigger") <= 0.01f)  // Larger button push incase of double push.
            {
                _isRightTriggerActive = true;
                if (_isTogglePickedUp)
                {
                    _mainPageIndex++;
                    DefinePage(_mainPageIndex, true);
                }
            }

            // Disable active controls if trigger is not pressed hard enough.
            if (_isRightHandGripActive && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryHandTrigger") <= 0.26f)
            {
                _isRightHandGripActive = false;
            }

            // Enable active controls is trigger is pressed hard enough.
            else if (!_isRightHandGripActive && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryHandTrigger") > 0.58f && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryIndexTrigger") <= 0.01f)
            {
                _isRightHandGripActive = true;
                if (_isTogglePickedUp)
                {
                    _mainPageIndex--;
                    DefinePage(_mainPageIndex, true);
                }
            }
        }
        */
    }

    private void DisableTutorialMode()
    {
        if (_isOptionsContainerHidden && !_isOptionsContainerLerp)
        {
            _isTutorialMode = false;
            _topCenterTMP.text = $"{Networking.LocalPlayer.displayName}'s Library";
            _topCenterCloneTMP.text = _topCenterTMP.text;
            _topCenterCloneTMP.gameObject.SetActive(false);
            _topCenterTMP.gameObject.SetActive(false);
        }
        else
        {
            _isTutorialMode = false;
            _topCenterTMP.text = "Settings";
            _topCenterCloneTMP.text = _topCenterTMP.text;
        }
    }

    // On Pickup.

    private void ProcessInputs()
    {

        // Do not turn page if another menu is active.
        // Experimental - keep page turning on for options container.
        // !_isOptionsContainerHidden || 
        if (!_isHomeContainerHidden) return;

        // Desktop input for turning page left or right.
        if (!_isHaltPageTurn && Input.GetKeyDown("[3]") || Input.GetKey("[*]") || (Input.GetAxis("Mouse ScrollWheel") < 0f && _isScrollWheelActive))
        {
            if (_isScreenSaverHidden)
            {
                _mainPageIndex++;
                // Need to check if clone is blank, so we can ignore this input to give Andrea one frame to update the previous clone text.
                if (_overflowPageIndex == -1 && _mainCloneTMP.text != "")
                {
                    _bigHammerLocCoordinates = -1;
                    if (_isTutorialMode)
                    {
                        DisableTutorialMode();
                    }
                    PlayAudio(_softSwipeAudio);
                    DefinePage(_mainPageIndex, true);
                }
            }
        }
        else if (!_isHaltPageTurn && Input.GetKeyDown("[1]") || Input.GetKey("[/]") || (Input.GetAxis("Mouse ScrollWheel") > 0f && _isScrollWheelActive))
        {
            if (_isScreenSaverHidden)
            {
                _mainPageIndex--;
                if (_overflowPageIndex == -1 && _mainCloneTMP.text != "")
                {
                    _bigHammerLocCoordinates = -1;
                    if (_isTutorialMode && _adjustedLocationHeight > 1)
                    {
                        DisableTutorialMode();
                    }
                    PlayAudio(_softSwipeAudio);
                    DefinePage(_mainPageIndex, false);
                }
            }
        }


        return;
        /*


        // VR input for turning page left or right.
        if (Networking.LocalPlayer.IsUserInVR())
        {
            // Disable active controls if trigger is not pressed hard enough.
            if (_isRightTriggerActive && ((_mainTurnPageIndex == 0 && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryIndexTrigger") <= 0.26f) ||
                (_mainTurnPageIndex == 2 && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryHandTrigger") <= 0.26f) ||
                (_mainTurnPageIndex == 1 && (Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryHandTrigger") <= 0.26f || Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryIndexTrigger") <= 0.26f))))
            {
                _isRightTriggerActive = false;
            }
            else if (_isLeftTriggerActive && ((_mainTurnPageIndex == 0 && Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryIndexTrigger") <= 0.26f) ||
                (_mainTurnPageIndex == 2 && Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryHandTrigger") <= 0.26f) ||
                (_mainTurnPageIndex == 1 && (Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryHandTrigger") <= 0.26f || Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryIndexTrigger") <= 0.26f))))
            {
                _isLeftTriggerActive = false;
            }

            // Enable active controls is trigger is pressed hard enough.
            else if (!_isRightTriggerActive && ((_mainTurnPageIndex == 0 && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryIndexTrigger") >= 0.28f) ||
                (_mainTurnPageIndex == 2 && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryHandTrigger") >= 0.28f) ||
                (_mainTurnPageIndex == 1 && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryHandTrigger") >= 0.28f && Input.GetAxisRaw("Oculus_CrossPlatform_SecondaryIndexTrigger") >= 0.28f)))
            {
                _isRightTriggerActive = true;
                if (_candleGameObject.transform.parent == _rightHandGameObject.transform)
                {
                    _mainPageIndex++;
                    DefinePage(_mainPageIndex, true);
                }
            }
            else if (!_isLeftTriggerActive && ((_mainTurnPageIndex == 0 && Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryIndexTrigger") >= 0.28f) ||
                (_mainTurnPageIndex == 2 && Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryHandTrigger") >= 0.28f) ||
                (_mainTurnPageIndex == 1 && Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryHandTrigger") >= 0.28f && Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryIndexTrigger") >= 0.28f)))
            {
                _isLeftTriggerActive = true;
                if (_candleGameObject.transform.parent == _rightHandGameObject.transform)
                {
                    _mainPageIndex--;
                    DefinePage(_mainPageIndex, false);
                }
            }
        }

        if (_isRejectNumPadInputs) return;

        // Activate mouse scroll wheel for incrementing pages.
        if (Input.GetKeyDown("[5]") || Input.GetKeyDown("5"))
        {
            _isScrollWheelActive = !_isScrollWheelActive;
        }

        // Change text font size.
        else if (Input.GetKeyDown("[4]") || Input.GetKeyDown("4"))
        {
            ChangeFontSize((_defaultFontSizeIndex + 1) % _fontSizeList.Length);
        }

        // Change text font type.
        else if (Input.GetKeyDown("[6]") || Input.GetKeyDown("6"))
        {
            ChangeFontType((_defaultFontTypeIndex + 1) % _fontTypeList.Length);
        }

        // Expand tablet scale.
        else if (Input.GetKeyDown("[7]"))
        {
            _candleGameObject.transform.localScale = Vector3.Scale(_candleGameObject.transform.localScale, new Vector3(0.9f, 0.9f, 0.9f));
        }

        // Shrink tablet scale.
        else if (Input.GetKeyDown("[9]"))
        {
            _candleGameObject.transform.localScale = Vector3.Scale(_candleGameObject.transform.localScale, new Vector3(1.1f, 1.1f, 1.1f));
        }

        // Move tablet position upward.
        else if (Input.GetKeyDown("[-]"))
        {
            _candleGameObject.transform.Translate(Vector3.up * 0.01f);
        }

        // Move tablet position downward.
        else if (Input.GetKeyDown("[+]"))
        {
            _candleGameObject.transform.Translate(Vector3.down * 0.01f);
        }
        */
    }

    private void ChangeFontSize(int fontSizeIndex)
    {
        // if (_isOptionsContainerHidden) return;

        _defaultFontSizeIndex = fontSizeIndex;
        _mainFontSizeIndex = _defaultFontSizeIndex;
        _mainFontSize = _fontSizeList[_defaultFontSizeIndex];

        ChangeFontLogistics();
    }

    private void ChangeFontType(int fontTypeIndex)
    {
        // if (_isOptionsContainerHidden) return;

        _defaultFontTypeIndex = fontTypeIndex;
        _mainFontTypeIndex = _defaultFontTypeIndex;
        _mainFontType = _fontTypeList[_defaultFontTypeIndex].Trim(_badCharList);

        ChangeFontLogistics();
    }

    private void ChangeFontLogistics()
    {
        PlayAudio(_clickAudio);

        if (_isHomeContainerHidden)
        {
            _mainTMP.text = "";
            if (_bigHammerLocCoordinates == -1)
            {
                if (_overflowPageIndex == -1)
                {
                    _bigHammerLocCoordinates = Convert.ToInt32(Math.Max(1, Math.Floor((_mainLocationHeight + _lastCharSliceList[_mainPageIndex]) / 1f)));
                }
                else
                {
                    _bigHammerLocCoordinates = _mainLocationHeight;
                    // _bigHammerLocCoordinates = Convert.ToInt32(Math.Max(1, Math.Floor((_mainLocationHeight + _lastCharSliceList[_deepFreezePageIndex]) / 1f)));
                }
            }

            // We clear clone text when changing font types.
            _mainPreviousCloneTMP = _mainCloneTMP;

            BigHammerLogistics(_bigHammerLocCoordinates);

            // Keep this loc constant until page changed, return button, or settings closed.
            // I may want to press a bunch of buttons to compare the difference!

            // Calibrate(0);
            // Need to add loc logistics (find nearest loc before and after calibration)

            _isOverflowAuditDefinePage = true;
        }
    }

    private void ChangeTabletSize(float scaleSize)
    {
        PlayAudio(_clickAudio);

        if (!Networking.LocalPlayer.IsUserInVR())
        {
            scaleSize = scaleSize + 0.05f;
        }

        _candleGameObject.transform.localScale = new Vector3(scaleSize, scaleSize, scaleSize);

        // Need to update all fonts because Andrea changing scale size makes fonts blurry.
        SuperFontScaleUpdate();
    }

    private int DefineInsertIndex()
    {
        if (_mainPageIndex == 0)
        {
            _mainTMP.text = "\n";
        }
        else
        {
            string _richTextOverflow = _richTextOverflowList[_mainPageIndex - 1];
            if (_mainText.text.Substring(_lastCharSliceList[_mainPageIndex] + 1, 1) == "\n")
            {
                _mainTMP.text = "\n";
            }
            else if (_mainText.text.Substring(_lastCharSliceList[_mainPageIndex], 1) == "\n")
            {
                _mainTMP.text = "\n";
            }
            else if (_mainText.text.Substring(_lastCharSliceList[_mainPageIndex] - 1, 2) == "\n ")
            {
                _mainTMP.text = "\n";
            }
            else if (_richTextOverflow.Contains("<indent="))
            {
                _mainTMP.text = "\n";
            }
            else if (_richTextOverflow.Contains(_styleSheetTagList[Array.IndexOf(_styleSheetNameList, "<START>")]))
            {
                _mainTMP.text = "\n";
            }
            else
            {
                // This occurs if no new line exists at the top of this page or the preceding text page.
                _mainTMP.text = "";
                return 0;
            }
        }
        return 1;
    }

    private void DefinePage(int pageIndexUnclamped, bool isIncrement)
    {
        // Junk Code:
        // _blankScreenGameObject.SetActive(true);
        // _blankTahomaTMP.text = _mainTMP.text;

        // Detect if page has exceeded block bounds.
        _mainPageIndex = Mathf.Clamp(_mainPageIndex, 0, _mainPageLength - 1);
        if (_overflowPageIndex == -1)
        {
            // Hidden for now - too many UI movements on main screen. 
            _handleIconGameObject.transform.localEulerAngles = new Vector3(0f, 0f, isIncrement ? -90f : 90f);
            _handleIconGameObject.transform.localPosition = new Vector3(isIncrement ? 0.75f : -0.75f, 0f, 0f);
        }
        if (pageIndexUnclamped != _mainPageIndex)
        {
            ChangeBlock(pageIndexUnclamped > _mainPageIndex);
            return;
        }

        // Determine if a new-line needs to be added as starting text.
        int _insertIndex = DefineInsertIndex();

        // PlayAudio(_softSwipeAudio);

        // Populate the text.
        _mainTMP.text += _mainText.text.Substring(_mainPageIndex == 0 ? 0 : _lastCharSliceList[_mainPageIndex] + 1, _lastCharSliceList[_mainPageIndex + 1] - _lastCharSliceList[_mainPageIndex]);

        // Process the text.
        DecompressRichText(true);
        ReplaceRichText();
        RemoveImageRichText();
        FlushLastCharText();

        // Insert all rich-text.
        if (_mainPageIndex > 0)
        {
            _mainTMP.text = _mainTMP.text.Insert(_insertIndex, _richTextOverflowList[_mainPageIndex - 1] == null ? "" : _richTextOverflowList[_mainPageIndex - 1]);
        }
        _mainTMP.text = _mainTMP.text.Insert(_insertIndex, $"<size={_mainFontSize}>");
        if (_isTextDarkMode)
        {
            _mainTMP.text = _mainTMP.text.Insert(_insertIndex, $"<color=#ffffff75>");
        }

        _mainTMP.text = _mainTMP.text.Substring(_insertIndex);  // Removes \n.
        _mainTMP.text = _mainTMP.text.Replace("##", $"<align=\"center\"><size=125><sprite name=\"Image_4\"></size><align=\"justified\">\n");  // Cutoff Picture Bottom.


        // Clone copies the main text.
        // _mainCloneTMP.text = _mainTMP.text;

        // Rare scenario where next page doesn't display any text. Call itself again which will increment the block.
        if (IsPageEmpty())
        {
            _mainTMP.text = "";
            _mainPageIndex += isIncrement ? 1 : -1;
            DefinePage(_mainPageIndex, isIncrement);
            return;
        }

        if (_overflowPageIndex == -1)
        {
            RefreshBottomGUI();
        }
    }

    private void ChangeBlock(bool isIncrement)
    {
        // Return if incrementing down at block index 0.
        if (_mainBlockIndex == 0 && !isIncrement) return;

        // Return if incrementing up at max block index.
        if (_mainBlockIndex == _mainTextList.Length - 1 && isIncrement) return;

        // Page has exceeded bounds, so change text blocks.
        PlayAudio(_hardSwipeAudio);

        _mainTMP.text = "";
        Calibrate(isIncrement ? 1 : -1);
        _isOverflowAuditDefinePage = true;

        // If incrementing up, start at the first page. If incrementing down, start at the last page.
        _endCalibrationPageIndex = isIncrement ? 0 : -1;
    }

    private void SetAdjustedLocationHeightNoCollision(int locCurrent)
    {
        // If on page index 0, need to check last page index of previous block to see if same locString.
        // If same locString, need to drill in while loops checking the last page indexes of each prior until they do not match.
        // After that, add +1 to the locString for each positive drill that occured. 
        // Same logic applies to the pages as well.
        // Urgently need to remove when looping on overflow page index.]

        int _drillDepth = 1;

        // Debug.Log($"Drill Depth Loop Activated.");

        if (_mainPageIndex == 0)
        {
            // Check to make sure we're not on block 0. Can't drill past it.
            while (_mainBlockIndex - _drillDepth >= 0)
            {
                // Get the block contents of the previous block.
                _mainBlock = _mainBlockList[_mainBlockIndex - _drillDepth];

                // Gets the exact global location index of last character in the block.
                int _startLocationIndex = _mainBlock.text.IndexOf(_locationCodeList[0]) + _locationCodeList[0].Length;

                int _endLocationIndex = _mainBlock.text.IndexOf(_locationCodeList[1]) - 1;


                int _locationHeightOfDrilledBlock = Convert.ToInt32(_mainBlock.text.Substring(_startLocationIndex, _endLocationIndex - _startLocationIndex));

                // Gets the exact global block index of last character in the block.
                int _startLocationIndex2 = _mainBlock.text.IndexOf(_locationCodeList[1]) + _locationCodeList[1].Length;

                int _endLocationIndex2 = _mainBlock.text.IndexOf(_locationCodeList[2]) - 1;


                int _blockHeightOfDrilledBlock = Convert.ToInt32(_mainBlock.text.Substring(_startLocationIndex2, _endLocationIndex2 - _startLocationIndex2));

                // Debug.Log($"_mainBlock: Skipped | _startLocationIndex: {_startLocationIndex} | _endLocationIndex: {_endLocationIndex} | _locationHeightOfDrilledBlock: {_locationHeightOfDrilledBlock} | _locationHeight: {_locationHeight} | _drillDepth: {_drillDepth}");

                if (Math.Floor(_mainLocationHeight / 100f) != Math.Floor((_locationHeightOfDrilledBlock + _blockHeightOfDrilledBlock) / 100f))
                {
                    break;
                }
                _drillDepth++;
            }
        }
        else
        {
            while (_mainPageIndex - _drillDepth >= 0)
            {
                if (Math.Floor(_lastCharSliceList[_mainPageIndex] / 100f) != Math.Floor(_lastCharSliceList[_mainPageIndex - _drillDepth] / 100f))
                {
                    break;
                }
                _drillDepth++;
            }
        }

        if (_drillDepth > 1)
        {
            // Debug.Log($"Drill Detected - Adjust Loc By +{_drillDepth - 1}");
        }

        _adjustedLocationHeight = locCurrent + _drillDepth - 1;
    }

    private void RefreshBottomGUI()
    {
        if (!_isLocNavClicked)
        {
            BlockNameSet(_mainBlockIndex);  // Update block name only if not already holding.
        }

        // Junk Code (replaces RefreshBottomGUI() function):
        int _locCurrent = Convert.ToInt32(Math.Max(1, Math.Floor((_mainLocationHeight + _lastCharSliceList[_mainPageIndex]) / 100f)));
        // string _locStringRaw = $"Loc {_locCurrent}";

        SetAdjustedLocationHeightNoCollision(_locCurrent);


        _bottomLeftTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}Loc {_adjustedLocationHeight}";
        _bottomLeftCloneTMP.text = _bottomLeftTMP.text;

        if (_adjustedMaxHeight != 0 && !_isLocNavClicked)  
            // Ensures we don't move the slider while holding the slider knoob.
        {
            _locNavigationSlider.value = Mathf.Max(0f, (float)_adjustedLocationHeight / (float)_adjustedMaxHeight);
        }

        // Junk Code:
        // Refreshes the GUI text on the bottom of the page.


        // Display chapter header.
        string _blockName = _mainTextList[_mainBlockIndex].name;
        if (_blockName.IndexOf('-') >= 0)
        {
            _bottomCenterTMP.text = $"{_blockName.Substring(_blockName.IndexOf('-') + 2)}";
            _bottomCenterCloneTMP.text = $"{_blockName.Substring(_blockName.IndexOf('-') + 2)}";
        }
        else
        {
            _bottomCenterTMP.text = $"{_blockName}";
            _bottomCenterCloneTMP.text = $"{_blockName}";
        }

        // Display how far we are into the book percentage-wise.
        _adjustedMaxHeight = Convert.ToInt32(Math.Floor(_mainMaxHeight / 100f));  // Convert.ToInt32(Math.Floor(_maxHeight / 100f));

        double _checkMath = Math.Min(Math.Ceiling((double)_adjustedLocationHeight * 99 / (double)_adjustedMaxHeight), 99);

        // _mainCenterInfoTMP.text = "";  // Loop overflow audits complete.

        // Debug.Log($"Percent Complete: {_checkMath}%"); // 
        // _mainCenterInfoTMP.text = $"{_checkMath}%";
        // _bottomRightTMP.text = $"{ (_mainBlockIndex == 0 ? 0 : Convert.ToInt32(Math.Max(1, _checkMath))) }%";

        if (_mainBlockIndex == _mainTextList.Length - 1 && _mainPageIndex == _mainPageLength - 1)
        {
            _bottomCenterTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}★";
            _bottomCenterCloneTMP.text = _bottomCenterTMP.text;
            _bottomRightTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}100%";
        }
        else
        {
            _bottomCenterTMP.text = "";
            _bottomCenterCloneTMP.text = _bottomCenterTMP.text;
            _bottomRightTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}{Convert.ToInt32(Math.Max(1, _checkMath))}%";
        }

        _bottomRightCloneTMP.text = _bottomRightTMP.text;
        // $"{Math.Round((double)_adjustedLocationHeight / (double)_adjustedMaxHeight) * 100}";  // $"{(Math.Min(_adjustedLocationHeight, _adjustedMaxHeight) / _adjustedMaxHeight) * 100f}%";

        // Chapter Progress: _mainPercentageInfoTMP.text = $"{(_mainPageLength == 1 ? 100 : _mainPageIndex * 100 / (_mainPageLength - 1))}%";
    }

    private void RemoveImageRichText()  // Parallel Code.
    {
        // Experimental imaging script. Ignore this if the book does not have any images.
        for (int _index = 0; _index < 100000; _index++)
        {
            int _start = _mainTMP.text.IndexOf("<image=");
            if (_start == 0 || _start == -1)
            {
                // Debug.Log($"LOOP TERMINATED. REMOVED ({_index}) RICH-TEXT IMAGE COMMANDS.");
                break;
            }
            int _end = _mainTMP.text.IndexOf("g\">", _start) + "g\">".Length + 2;
            // Add +1 to account for the new line... but can't since newline is not in rich-text brackets.
            _mainTMP.text = _mainTMP.text.Remove(_start, _end - _start);

            // Debug.Log($"REMOVED({_start}, {_end - _start}) | START: {_start} | END: {_end} | SUCCESSFULLY REMOVED");
        }
    }

    private void ReplaceRichText()  // Parallel Code.
    {
        // Replaces rich-text with custom variable settings.
        _mainTMP.text = _mainTMP.text.Replace("##", $"<align=\"center\"><size=125><sprite name=\"Image_4\"></size><align=\"justified\">\n​​");  // There are 2 hairlines at the end of \n.
        _mainTMP.text = _mainTMP.text.Replace("~", "");
        _mainTMP.text = _mainTMP.text.Replace("<size=h1>", $"<size={_mainFontSize * (_header1FontSizePercentList[_defaultFontSizeIndex] / 100f)}>");
        _mainTMP.text = _mainTMP.text.Replace("<size=h2>", $"<size={_mainFontSize * (_header2FontSizePercentList[_defaultFontSizeIndex] / 100f)}>");
        _mainTMP.text = _mainTMP.text.Replace("<size=h3>", $"<size={_mainFontSize * (_header3FontSizePercentList[_defaultFontSizeIndex] / 100f)}>");
        _mainTMP.text = _mainTMP.text.Replace("</size>", $"<size={_mainFontSize}>");
    }

    private void FlushLastCharText()
    {
        // Return if last page, flush is disabled, or alignment is active.
        if (_isSkipAppendFlush) return;
        if (_mainTMP.text.Contains("→")) return;
        if (_mainPageIndex == _mainPageLength - 1) return;
        if (_richTextOverflowList[_mainPageIndex] != null && _richTextOverflowList[_mainPageIndex].Contains("<align=")) return;

        // Return if next page is start of new paragraph, bullet, or section.
        char _firstCharNextPage = char.Parse(_mainText.text.Substring(_lastCharSliceList[_mainPageIndex + 1] + 1, 1));
        if (_firstCharNextPage == '\t' || _firstCharNextPage == '\n')  // Last line of paragraph.
        {
            // Debug.Log("Next Page is a Different Paragraph (New-Line)");
            return;
        }
        if (_mainText.text.Substring(_lastCharSliceList[_mainPageIndex + 1] + 1, 3) == "<t>")
        {
            // Debug.Log("Next Page is Linked Paragraph (Tab)");
            return;
        }
        if (_firstCharNextPage == '•') return;

        // Return if next page is the start of bullet. Needs to scan digits to be sure.
        if (_richTextOverflowList[_mainPageIndex] != null && _richTextOverflowList[_mainPageIndex].Contains("<margin="))
        {
            char _secondCharNextPage = char.Parse(_mainText.text.Substring(_lastCharSliceList[_mainPageIndex + 1] + 2, 1));
            if (char.IsLetterOrDigit(_firstCharNextPage) && Array.IndexOf(_bracketCharList, _secondCharNextPage) >= 0) return;
            char _thirdCharNextPage = char.Parse(_mainText.text.Substring(_lastCharSliceList[_mainPageIndex + 1] + 3, 1));
            if (char.IsLetterOrDigit(_firstCharNextPage) && char.IsLetterOrDigit(_secondCharNextPage) && Array.IndexOf(_bracketCharList, _thirdCharNextPage) >= 0) return;
        }

        // Debug.Log($"Page Index: {pageIndex} | firstCharNextPage: {firstCharNextPage}");

        // Determines the exact index to place the flush text, and whether an end-space needs to be added.
        int _textLength = _mainTMP.text.Length;
        bool _tempIsRich = false;
        bool _isEndSpace = false;
        int _insertIndex = 0;
        for (int _index = 1; _index < _textLength; _index++)
        {
            char _character = _mainTMP.text[_textLength - _index];
            if (_character == '<' || _character == '>')
            {
                _tempIsRich = !_tempIsRich;
            }
            else if (!_tempIsRich && _character == ' ')
            {
                _isEndSpace = true;
            }
            else if (!_tempIsRich && (_character != ' ' && _character != '\n' && _character != '\t'))
            {
                _insertIndex = _textLength + 1 - _index;
                break;
            }
        }
        _mainTMP.text = _mainTMP.text.Insert(_insertIndex, $"<align=\"flush\">{(_isEndSpace ? "" : " ")}");
    }

    private bool IsPageEmpty()
    {
        // This checks if the current page is empty or not and returns a boolean.
        _isStringRichText = false;
        bool _isPageEmpty = true;
        foreach (char _character in _mainTMP.text)
        {
            if (_character == '<' || _character == '>')
            {
                _isStringRichText = !_isStringRichText;
            }
            else if (!_isStringRichText && (_character == '.' || char.IsLetterOrDigit(_character)) || _character == '"' || _character == '\'' || _character == '.' || _character == ',' || _character == '=')
            {
                _isPageEmpty = false;
                break;
            }
        }
        return _isPageEmpty;
    }

    public void CalibrateMemory(int index)
    {
        if (_overflowPageIndex >= 0) return;

        if (!_isOptionsContainerHidden || (_isOptionsContainerHidden && _isOptionsContainerLerp))
        {
            if (!_isOptionsContainerHidden)
            {
                OptionsButton();
            }
            _calibrateMemoryDeepFreeze = index;
            return;
        }

        _calibrateMemoryDeepFreeze = -1;
        if (!Networking.LocalPlayer.IsUserInVR())
        {
            // _screenGameObject.GetComponent<GraphicRaycaster>().enabled = false;
            // _screemVRCUIShape.enabled = false;
        }    

        // Called by memory script for processing. Not called by Andrea script.
        PlayAudio(_selectBookAudio);

        _mainPageIndex = 0;
        _mainBlockIndex = _defaultBlockIndex;
        _mainBookIndex = index;
        _mainTextList = Memory.GetTextAsset(_mainBookIndex, true);
        _mainBlockList = Memory.GetTextAsset(_mainBookIndex, false);  // Critical to set before big hammer.
        if (_mainTextList == null)
        {
            Debug.Log("_mainTextList variable not assigned!");
        }
        _mainBlockIndex = Mathf.Clamp(_mainBlockIndex, 0, _mainTextList.Length - 1);

        _isLoadBookNew = true;
        _isBookStaged = true;
        _isFirstTimeMode = false;
        // _isShowBar = false;
        _screenPhysicalGameObject.GetComponent<Renderer>().material = _materialGreyLight;
        _backButtonGameObject.SetActive(false);
        _catalogButtonGameObject.SetActive(false);
        _topCenterCloneTMP.gameObject.SetActive(false);
        _topCenterTMP.gameObject.SetActive(false);
        _topLeftTMP.gameObject.SetActive(false);
        _topLeftCloneTMP.gameObject.SetActive(false);
        _topRightTMP.gameObject.SetActive(false);
        _topRightCloneTMP.gameObject.SetActive(false);

        // Sometimes Home appears even after selecting a book.
        _topCenterTMP.text = "";
        _topCenterCloneTMP.text = _topCenterTMP.text;

        // _bigButtonScreenGameObject.SetActive(true);

        if (!_isHomeContainerHidden)
        {
            HomeContainer();
        }

        if (!_isOptionsContainerHidden)
        {
            _screenPhysicalGameObject.GetComponent<Renderer>().material = _materialGreyMedium;
            _topCenterTMP.gameObject.SetActive(true);
            _topCenterCloneTMP.gameObject.SetActive(true);
            _topCenterTMP.text = $"Settings";
            _topCenterCloneTMP.text = _topCenterTMP.text;
        }

        _backButtonGameObject.transform.localPosition = new Vector3(_backButtonGameObject.transform.localPosition.x, _backButtonGameObject.transform.localPosition.y, -2.5f);
        _catalogButtonGameObject.transform.localPosition = new Vector3(_catalogButtonGameObject.transform.localPosition.x, _catalogButtonGameObject.transform.localPosition.y, -2.5f);

        _bottomRightTMP.gameObject.SetActive(true);
        _bottomRightCloneTMP.gameObject.SetActive(true);

        // Before entering calibration, we need to validate that we don't already have a loc stored in memory.
        if (_preservedLocBookList[_mainBookIndex] != -1)
        {
            // Debug.Log($"Calibrate Memory() | Big Hammer: {_preservedLocBookList[_mainBookIndex]}");

            BigHammerLogistics(_preservedLocBookList[_mainBookIndex]);

            double _checkMath = Math.Min(Math.Ceiling((double)_adjustedLocationHeight * 99 / (double)_adjustedMaxHeight), 99);

            if (_mainBlockIndex == _mainTextList.Length - 1 && _mainPageIndex == _mainPageLength - 1)
            {
                _bottomCenterTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}★";
                _bottomCenterCloneTMP.text = _bottomCenterTMP.text;
                _bottomRightTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}100%";
                _bottomRightCloneTMP.text = _bottomRightTMP.text;
            }
            else
            {
                _bottomCenterTMP.text = "";
                _bottomCenterCloneTMP.text = _bottomCenterTMP.text;
                _bottomRightTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}{Convert.ToInt32(Math.Max(1, _checkMath))}%";
                _bottomRightCloneTMP.text = _bottomRightTMP.text;
            }
        }
        else
        {
            // Loading a brand new book, so value should be zero.
            _locNavigationSlider.value = 0;
            ExecuteCalibration();

            _bottomCenterTMP.text = "";
            _bottomCenterCloneTMP.text = _bottomCenterTMP.text;
            _bottomRightTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}1%";
            _bottomRightCloneTMP.text = _bottomRightTMP.text;
        }

        // Need to update the navslider block title and the percentages on the bottom right text.
        BlockNameSet(_mainBlockIndex);
        // Update the brightness.
        BrightnessSliderDrag();

        if (_isShowExtraOptions)
        {
            BigButton();
        }
        if (!_isScreenSaverHidden)
        {
            ScreenSaverClicked();
        }

        if (_isTutorialMode && _topCenterTMP.text != "Settings")
        {
            TutorialModeText();
        }
    }

    private void TutorialModeText()
    {
        //➜ 
        if (Networking.LocalPlayer.IsUserInVR())
        {
            _topCenterTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}Right Joystick Up/Down to Turn Page";
            _topCenterCloneTMP.text = _topCenterTMP.text;
            _topCenterCloneTMP.gameObject.SetActive(true);
            _topCenterTMP.gameObject.SetActive(true);
        }
        else
        {
            _topCenterTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}Mouse Scroll to Turn Page";
            _topCenterCloneTMP.text = _topCenterTMP.text;
            _topCenterCloneTMP.gameObject.SetActive(true);
            _topCenterTMP.gameObject.SetActive(true);
        }
    }

    private void BigHammerLogistics(int locTarget)
    {
        // Copied and pasted from LocNavigatinSliderDrag().

        _isBookPresetLoc = true;

        // We absolutely need to update _adjustedLocationHeight & _adjustedMaxHeight.
        // Otherwise, there is old residue data from previous books.

        if (_isLoadBookNew)  // Only need to do this when loading a new book, not when changing font types or sizes.
        {


            // Set location height (adjusted).
            SetAdjustedLocationHeightNoCollision(Convert.ToInt32(Math.Max(1, Math.Floor(locTarget / 100f))));

            // Set max height (adjusted).

            TextAsset _tempBlock = _mainBlockList[0];
            int _startLocationIndex1 = _tempBlock.text.IndexOf(_locationCodeList[2]) + _locationCodeList[2].Length;
            int _endLocationIndex1 = _tempBlock.text.IndexOf('\n', _startLocationIndex1) - 1;
            _mainMaxHeight = Convert.ToInt32(_tempBlock.text.Substring(_startLocationIndex1, _endLocationIndex1 - _startLocationIndex1));
            _adjustedMaxHeight = Convert.ToInt32(Math.Floor(_mainMaxHeight / 100f));

            // Update the slider value location.
            _locNavigationSlider.value = Mathf.Max(0f, (float)_adjustedLocationHeight / (float)_adjustedMaxHeight);

            // Testing to see if error occurs.
            // _adjustedLocationHeight = 0;

        }

        double _checkMath = Math.Min(Math.Ceiling((double)_adjustedLocationHeight * 99 / (double)_adjustedMaxHeight), 99);
        int _mainBlockIndexGuess = Convert.ToInt32(Mathf.Max(0f, (_mainBlockList.Length - 1) * (float)(_checkMath / 99f)));
        _mainBlockIndexGuess = Mathf.Min(_mainBlockList.Length - 1, _mainBlockIndexGuess);
        int _trueLoc = locTarget;

        // Debug.Log($"Before While Loop | _adjustedLocationHeight: {_adjustedLocationHeight} | _adjustedMaxHeight: {_adjustedMaxHeight} | _checkMath: {_checkMath} | _mainBlockIndexGuess: {_mainBlockIndexGuess} | _trueLoc: {_trueLoc}");

        // Check to make sure we're not on block 0. Can't drill past it.
        while (_mainBlockIndexGuess >= 0)
        {
            // Get the block contents of the previous block.
            TextAsset _mainBlockGuess = _mainBlockList[_mainBlockIndexGuess];

            // Gets the exact global location index of last character in the block.
            int _startLocationIndex = _mainBlockGuess.text.IndexOf(_locationCodeList[0]) + _locationCodeList[0].Length;

            int _endLocationIndex = _mainBlockGuess.text.IndexOf(_locationCodeList[1]) - 1;


            int _locationHeightOfDrilledBlock = Convert.ToInt32(_mainBlockGuess.text.Substring(_startLocationIndex, _endLocationIndex - _startLocationIndex));

            // Gets the exact global block index of last character in the block.
            int _startLocationIndex2 = _mainBlockGuess.text.IndexOf(_locationCodeList[1]) + _locationCodeList[1].Length;

            int _endLocationIndex2 = _mainBlockGuess.text.IndexOf(_locationCodeList[2]) - 1;


            int _blockHeightOfDrilledBlock = Convert.ToInt32(_mainBlockGuess.text.Substring(_startLocationIndex2, _endLocationIndex2 - _startLocationIndex2));

            // Debug.Log($"_mainBlockIndexGuess: {_mainBlockIndexGuess} | _startLocationIndex: {_startLocationIndex} | _endLocationIndex: {_endLocationIndex} | _locationHeightOfDrilledBlock: {_locationHeightOfDrilledBlock}");

            if (_trueLoc >= _locationHeightOfDrilledBlock && _trueLoc < _locationHeightOfDrilledBlock + _blockHeightOfDrilledBlock)
            {
                // Do not use the <= sign. Undershoots by 1.
                // Debug.Log($"Main Block Index Guessed Correctly | _mainBlockIndexGuess: {_mainBlockIndexGuess} | _trueLoc: {_trueLoc} | _locationHeightOfDrilledBlock: {_locationHeightOfDrilledBlock} | _blockHeightOfDrilledBlock: {_blockHeightOfDrilledBlock}");

                BlockNameSet(_mainBlockIndexGuess);

                _locNavMainBlockIndex = _mainBlockIndexGuess;
                _locNavMainTrueLoc = _trueLoc;

                // Now we need to actually load the block and then get the page index.

                _mainBlockIndex = _mainBlockIndexGuess;
                _mainPageIndex = 0;

                // Debug.Log($"BigHammerLogistics() - 1 | _mainBlockIndex: {_mainBlockIndex} | _mainPageIndex: {_mainPageIndex} | _deepFreezePageIndex: {_deepFreezePageIndex}");

                ExecuteCalibration();

                for (int i = 0; i < _lastCharSliceList.Length; i++)
                {
                    if (_locationHeightOfDrilledBlock + _lastCharSliceList[i] > _trueLoc)
                    {
                        i--;
                        _mainPageIndex = i;
                        break;
                    }
                }


                // Debug.Log($"_mainPageIndex: {_mainPageIndex} | _trueLoc: {_trueLoc} | _mainBlockIndex: {_mainBlockIndex} | ");

                // Deep freeze to preserve this value after calibration and richtext overflow audit.
                _deepFreezePageIndex = _mainPageIndex;

                // Debug.Log($"BigHammerLogistics() - 2 | _mainBlockIndex: {_mainBlockIndex} | _mainPageIndex: {_mainPageIndex} | _deepFreezePageIndex: {_deepFreezePageIndex}");

                // _bottomCoreTMP.text = $"{_mainBlock.text.Substring(0, _mainBlock.text.IndexOf('\n'))}";
                // _bottomCoreCloneTMP.text = _bottomCoreTMP.text;
                break;
            }
            else if (_trueLoc < _locationHeightOfDrilledBlock)
            {
                // Debug.Log($"_mainBlockIndexGuess: {_mainBlockIndexGuess} | Lower our guess by 1.");
                _mainBlockIndexGuess--;
            }
            else
            {
                // Debug.Log($"_mainBlockIndexGuess: {_mainBlockIndexGuess} | Increase our guess by 1.");
                _mainBlockIndexGuess++;
            }
        }
    }

    public void BigButton()
    {
        if (!_backButtonGameObject.activeSelf)
        {
            _backButtonGameObject.SetActive(true);
            _catalogButtonGameObject.SetActive(true);
            // _topCenterCloneTMP.gameObject.SetActive(true);
            // _topCenterTMP.gameObject.SetActive(true);
            _locNavigationSlider.gameObject.SetActive(true);
        }
        else
        {
            _backButtonGameObject.SetActive(false);
            _catalogButtonGameObject.SetActive(false);
            // _topCenterCloneTMP.gameObject.SetActive(false);
            // _topCenterTMP.gameObject.SetActive(false);
            _locNavigationSlider.gameObject.SetActive(false);
        }
    }

    public void LocNavigationSliderClick()
    {
        _isLocNavClicked = true;
        _bottomCoreTMP.gameObject.SetActive(true);
        _bottomCoreCloneTMP.gameObject.SetActive(true);

        // Need to update the loc logistics.
        LocNavigationSliderDrag();
    }

    public void LocNavigationSliderReleased()
    {
        _isLocNavClicked = false;
        _bottomCoreTMP.gameObject.SetActive(_isLocNavHover);
        _bottomCoreCloneTMP.gameObject.SetActive(_isLocNavHover);
        if (_isLocNavDragged)
        {
            _isLocNavDragged = false;


            // Page has exceeded bounds, so change text blocks.
            PlayAudio(_hardSwipeAudio);

            // Remove big hammer logistics since page index is 100% #0.
            _isBookPresetLoc = false;
            _bigHammerLocCoordinates = -1;
            _mainPageIndex = 0;

            _mainTMP.text = "";
            _mainBlockIndex = _locNavMainBlockIndex;


            // Uncommenting this takes us to page index 0.
            // Calibrate(0);
            // _isOverflowAuditDefinePage = true;
            // _endCalibrationPageIndex = 0;



            // _isLoadBookNew = true;
            // Need to subtract one from the max height, otherwise will try to overshoot beyond.
            int _locTarget = Convert.ToInt32(Math.Ceiling((_mainMaxHeight - 1) * _locNavigationSlider.value));
            // Debug.Log($"Loc Target: {_locTarget} | ");
            BigHammerLogistics(_locTarget);
        }
    }

    public void LocNavigationSliderEnter()
    {
        _isLocNavHover = true;
        _bottomCoreTMP.gameObject.SetActive(true);
        _bottomCoreCloneTMP.gameObject.SetActive(true);
    }

    public void LocNavigationSliderExit()
    {
        _isLocNavHover = false;
        _bottomCoreTMP.gameObject.SetActive(_isLocNavClicked);
        _bottomCoreCloneTMP.gameObject.SetActive(_isLocNavClicked);
    }

    private void BlockNameSet(int blockIndex)
    {
        string _blockName = _mainTextList[blockIndex].name;
        if (_blockName.IndexOf('-') >= 0)
        {
            _bottomCoreTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}{_blockName.Substring(_blockName.IndexOf('-') + 2)}";
            _bottomCoreCloneTMP.text = _bottomCoreTMP.text;
        }
        else
        {
            _bottomCoreTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}{_blockName}";
            _bottomCoreCloneTMP.text = _bottomCoreTMP.text;
        }
    }

    public void LocNavigationSliderDrag()
    {
        // Cannot interact with variables while overflow adjusting or no book staged.
        if (!_isBookStaged || _adjustedMaxHeight == 0) return;

        // if (_overflowPageIndex >= 0) return;

        _isLocNavDragged = true;

        _adjustedLocationHeight = Convert.ToInt32(Math.Ceiling(_adjustedMaxHeight * _locNavigationSlider.value));
        _bottomLeftTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}Loc { Math.Max(1, _adjustedLocationHeight) }";
        _bottomLeftCloneTMP.text = _bottomLeftTMP.text;
        double _checkMath = Math.Min(Math.Ceiling((double)_adjustedLocationHeight * 99 / (double)_adjustedMaxHeight), 99);
        _bottomRightTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}{Convert.ToInt32(Math.Max(1, _checkMath))}%";
        _bottomRightCloneTMP.text = _bottomRightTMP.text;
        _bottomCenterTMP.text = "";
        _bottomCenterCloneTMP.text = "";

        if (_locNavigationSlider.value == 1)
        {
            _bottomCenterTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}★";
            _bottomCenterCloneTMP.text = _bottomCenterTMP.text;
            _bottomRightTMP.text = $"{(_isTextDarkMode ? "<color=#ffffff75>" : "")}100%";
            _bottomRightCloneTMP.text = _bottomRightTMP.text;
        }

        int _mainBlockIndexGuess = Convert.ToInt32(Mathf.Max(0f, (_mainBlockList.Length - 1) * (float)(_checkMath / 99f)));
        int _trueLoc = Convert.ToInt32(Math.Ceiling(_mainMaxHeight * _locNavigationSlider.value));

        // Check to make sure we're not on block 0. Can't drill past it.
        while (_mainBlockIndexGuess >= 0)
        {
            // Get the block contents of the previous block.
            TextAsset _mainBlockGuess = _mainBlockList[_mainBlockIndexGuess];

            // Gets the exact global location index of last character in the block.
            int _startLocationIndex = _mainBlockGuess.text.IndexOf(_locationCodeList[0]) + _locationCodeList[0].Length;

            int _endLocationIndex = _mainBlockGuess.text.IndexOf(_locationCodeList[1]) - 1;


            int _locationHeightOfDrilledBlock = Convert.ToInt32(_mainBlockGuess.text.Substring(_startLocationIndex, _endLocationIndex - _startLocationIndex));

            // Gets the exact global block index of last character in the block.
            int _startLocationIndex2 = _mainBlockGuess.text.IndexOf(_locationCodeList[1]) + _locationCodeList[1].Length;

            int _endLocationIndex2 = _mainBlockGuess.text.IndexOf(_locationCodeList[2]) - 1;


            int _blockHeightOfDrilledBlock = Convert.ToInt32(_mainBlockGuess.text.Substring(_startLocationIndex2, _endLocationIndex2 - _startLocationIndex2));

            // Debug.Log($"_mainBlock: Skipped | _startLocationIndex: {_startLocationIndex} | _endLocationIndex: {_endLocationIndex} | _locationHeightOfDrilledBlock: {_locationHeightOfDrilledBlock} | _locationHeight: {_locationHeight} | _drillDepth: {_drillDepth}");

            if (_trueLoc >= _locationHeightOfDrilledBlock && _trueLoc <= _locationHeightOfDrilledBlock + _blockHeightOfDrilledBlock)
            {
                BlockNameSet(_mainBlockIndexGuess);

                _locNavMainBlockIndex = _mainBlockIndexGuess;
                _locNavMainTrueLoc = _trueLoc;
                // _bottomCoreTMP.text = $"{_mainBlock.text.Substring(0, _mainBlock.text.IndexOf('\n'))}";
                // _bottomCoreCloneTMP.text = _bottomCoreTMP.text;
                break;
            }
            else if (_trueLoc < _locationHeightOfDrilledBlock)
            {
                _mainBlockIndexGuess--;
            }
            else
            {
                _mainBlockIndexGuess++;
            }
        }


        // Need to showcase block name - basically table of contents while navigating slider.
        // Do not show it if not clicking and holding the slider. 
    }
    /*
    public void LovNavigationSliderHover()
    {
        _bottomCoreTMP.text = $"{_mainBlock.text.Substring(0, _mainBlock.text.IndexOf('\n'))}";
        _bottomCoreCloneTMP.text = _bottomCoreTMP.text;
    }

    public void LovNavigationSliderBreak()
    {
        _bottomCoreTMP.text = $"{_mainBlock.text.Substring(0, _mainBlock.text.IndexOf('\n'))}";
        _bottomCoreCloneTMP.text = _bottomCoreTMP.text;
    }
    */
    public void BackButton()
    {
        // Called by clicking on the back button.
        _calibrateMemoryDeepFreeze = -1;
        if (!_isOptionsContainerHidden)
        {
            // Audio already plays from this button.
            OptionsButton();
            return;
        }

        if (_overflowPageIndex >= 0) return;

        /*
        else if (!_mainMenuGameObject.activeSelf)
        {
            _mainMenuGameObject.SetActive(true);
            _confirmationMenuGameObject.SetActive(false);
            _backButtonGameObject.SetActive(true);
            _catalogButtonGameObject.SetActive(true);
            _topLeftTMP.gameObject.SetActive(false);
            _topLeftCloneTMP.gameObject.SetActive(false);
            // _confirmationMenuGameObject.SetActive(true);
        }
        */
        else if (!_isHomeContainerHidden && _isBookStaged == false)
        {
            if (_isScreenSaverHidden)
            {
                // PlayAudio(_backAudio);
                ScreenSaverClicked();
            }
        }
        else
        {
            PlayAudio(_backAudio);
            _screenPhysicalGameObject.GetComponent<Renderer>().material = _materialGreyMedium;
            _backButtonGameObject.transform.localPosition = new Vector3(_backButtonGameObject.transform.localPosition.x, _backButtonGameObject.transform.localPosition.y, -2f);
            _catalogButtonGameObject.transform.localPosition = new Vector3(_catalogButtonGameObject.transform.localPosition.x, _catalogButtonGameObject.transform.localPosition.y, -2f);
            _isBookStaged = false;
            // _mainTMP.text = "";
            // _mainCloneTMP.text = "";

            if (_isHomeContainerHidden)
            {
                HomeContainer();
            }
            //_mainMenuGameObject.SetActive(true);

            _screenSaverAndreaText.SetActive(true);
            _backButtonGameObject.SetActive(true);
            _catalogButtonGameObject.SetActive(true);

            _topCenterCloneTMP.gameObject.SetActive(true);
            _topCenterTMP.gameObject.SetActive(true);

            _bottomLeftTMP.gameObject.SetActive(true);
            _bottomLeftCloneTMP.gameObject.SetActive(true);
            _bottomRightTMP.gameObject.SetActive(true);
            _bottomRightCloneTMP.gameObject.SetActive(true);
            _bottomCenterTMP.gameObject.SetActive(true);
            _bottomCenterCloneTMP.gameObject.SetActive(true);

            // Keep disabled for now to reduce clutter.
            // _topLeftTMP.gameObject.SetActive(true);
            // _topLeftCloneTMP.gameObject.SetActive(true);
            // _topRightTMP.gameObject.SetActive(true);
            // _topRightCloneTMP.gameObject.SetActive(true);
            // _bigButtonScreenGameObject.SetActive(false);

            // banner logistics.

            int _bannerIndex = Mathf.Clamp(_mainBookIndex, 0, _bookBannerGameObjectList.Length - 1);
            GameObject _banner = _bookBannerGameObjectList[_bannerIndex];
            _banner.SetActive(true);
            TextMeshProUGUI _bannerText = _bookBannerTMPList[_bannerIndex];

            // Strange one-time-bug here.
            double _checkMath = Math.Min(Math.Ceiling((double)_adjustedLocationHeight * 99 / (double)_adjustedMaxHeight), 99);
            _bannerText.text = $"<line-height=52>{Convert.ToInt32(Math.Max(1, _checkMath))}\n%";

            if (!Networking.LocalPlayer.IsUserInVR())
            {
                // _screenGameObject.GetComponent<GraphicRaycaster>().enabled = true;
                // _screemVRCUIShape.enabled = true;
            }

            if (_mainBlockIndex == _mainTextList.Length - 1 && _mainPageIndex == _mainPageLength - 1)
            {
                _bannerText.text = $"<size=65><line-height=35>\n★";
            }

            int _locCurrent = Convert.ToInt32(Math.Max(1, Math.Floor((_mainLocationHeight + _lastCharSliceList[_mainPageIndex]) / 1f)));  // Do not divide by 100, we need the true loc.

            _preservedLocBookList[_bannerIndex] = _locCurrent; // Needs the true loc, not the adjusted loc and not loc height!

            // <line-height=40><size=35>Loc
            // <size=40>18   ·
            // ·52

            if (timerScreenSaver >= 1 && _isScreenSaverHidden)
            {
                SelectRandomScreenSaverBackground(false);
            }

            HomeInfo();
        }
    }

    public void HomeInfo()
    {
        // _mainPercentageInfoTMP.text = DateTime.Now.ToShortTimeString();

        // Junk Code: Keep displays to a minimum.
        _topCenterTMP.text = $"{Networking.LocalPlayer.displayName}'s Library";
        _topCenterCloneTMP.text = _topCenterTMP.text;
        _bottomLeftTMP.text = "Prototype";  // "Public Domain";
        _bottomLeftCloneTMP.text = _bottomLeftTMP.text;
        _bottomCenterTMP.text = "";
        _bottomCenterCloneTMP.text = _bottomCenterTMP.text;
        _bottomRightTMP.text = "Version 3";
        _bottomRightCloneTMP.text = _bottomRightTMP.text;
        _locNavigationSlider.gameObject.SetActive(false);

    }

    public void OptionsButtonClicked()
    {
        if (_calibrateMemoryDeepFreeze != -1) return;

        OptionsButton();
    }

    public void OptionsButton()
    {
        // Do not display options with screen saver shown.
        // if (!_isScreenSaverHidden) return;

        // Called by clicking on the catalog button.
        PlayAudio(_optionsAudio);

        _isFirstTimeMode = false;
        _isOptionsContainerLerp = true;
        _bigHammerLocCoordinates = -1;

        if (_isOptionsContainerHidden)
        {
            _isOptionsContainerHidden = false;
            timerOptionsContainer = 0.001f;
            _optionsContainerVectorStart = _optionsContainerGameObject.transform.localPosition;
            _optionsContainerVectorTarget = _optionsContainerShown;
            _topCenterTMP.text = "Settings";
            _topCenterCloneTMP.text = _topCenterTMP.text;
            if (_isBookStaged)
            {
                _topCenterTMP.gameObject.SetActive(true);
                _topCenterCloneTMP.gameObject.SetActive(true);
                _screenPhysicalGameObject.GetComponent<Renderer>().material = _materialGreyMedium;
                // _topCenterTMP.text = $"{_mainBlock.text.Substring(0, _mainBlock.text.IndexOf('\n'))}";
                _topCenterCloneTMP.text = _topCenterTMP.text;
            }
            if (_isBookStaged)
            {
                _bottomLeftTMP.gameObject.SetActive(_isOptionsContainerHidden);
                _bottomLeftCloneTMP.gameObject.SetActive(_isOptionsContainerHidden);
                _bottomRightTMP.gameObject.SetActive(_isOptionsContainerHidden);
                _bottomRightCloneTMP.gameObject.SetActive(_isOptionsContainerHidden);
                _bottomCenterTMP.gameObject.SetActive(_isOptionsContainerHidden);
                _bottomCenterCloneTMP.gameObject.SetActive(_isOptionsContainerHidden);
                // _bottomCoreTMP.gameObject.SetActive(_isOptionsContainerHidden);
                // _bottomCoreCloneTMP.gameObject.SetActive(_isOptionsContainerHidden);
                _locNavigationSlider.gameObject.SetActive(_isOptionsContainerHidden);
            }
        }
        else
        {
            _isOptionsContainerHidden = true;
            timerOptionsContainer = 0.001f;
            _optionsContainerVectorStart = _optionsContainerGameObject.transform.localPosition;
            _optionsContainerVectorTarget = _optionsContainerHidden;
            _calibrateMemoryDeepFreeze = -1;
        }
    }

    public void SmallSize()
    {
        Color tempColor;
        tempColor = _smallSizeImage.color;
        tempColor.a = .33f;
        _smallSizeImage.color = tempColor;
        tempColor = _mediumSizeImage.color;
        tempColor.a = .66f;
        _mediumSizeImage.color = tempColor;
        tempColor = _largeSizeImage.color;
        tempColor.a = .66f;
        _largeSizeImage.color = tempColor;

        ChangeFontSize(0);
    }

    public void MediumSize()
    {
        Color tempColor;
        tempColor = _smallSizeImage.color;
        tempColor.a = .66f;
        _smallSizeImage.color = tempColor;
        tempColor = _mediumSizeImage.color;
        tempColor.a = .33f;
        _mediumSizeImage.color = tempColor;
        tempColor = _largeSizeImage.color;
        tempColor.a = .66f;
        _largeSizeImage.color = tempColor;

        ChangeFontSize(1);
    }

    public void LargeSize()
    {
        Color tempColor;
        tempColor = _smallSizeImage.color;
        tempColor.a = .66f;
        _smallSizeImage.color = tempColor;
        tempColor = _mediumSizeImage.color;
        tempColor.a = .66f;
        _mediumSizeImage.color = tempColor;
        tempColor = _largeSizeImage.color;
        tempColor.a = .33f;
        _largeSizeImage.color = tempColor;

        ChangeFontSize(2);
    }

    public void HelveticaType()
    {
        Color tempColor;
        tempColor = _helveticaTypeImage.color;
        tempColor.a = .33f;
        _helveticaTypeImage.color = tempColor;
        tempColor = _notoSerifTypeImage.color;
        tempColor.a = .66f;
        _notoSerifTypeImage.color = tempColor;
        tempColor = _tahomaTypeImage.color;
        tempColor.a = .66f;
        _tahomaTypeImage.color = tempColor;

        if (_defaultFontTypeIndex != 0)
        {
            ChangeFontType(0);
        }
    }

    public void TahomaType()
    {
        Color tempColor;
        tempColor = _helveticaTypeImage.color;
        tempColor.a = .66f;
        _helveticaTypeImage.color = tempColor;
        tempColor = _notoSerifTypeImage.color;
        tempColor.a = .66f;
        _notoSerifTypeImage.color = tempColor;
        tempColor = _tahomaTypeImage.color;
        tempColor.a = .33f;
        _tahomaTypeImage.color = tempColor;

        if (_defaultFontTypeIndex != 1)
        {
            ChangeFontType(1);
        }
    }

    public void NotoSerifType()
    {
        Color tempColor;
        tempColor = _helveticaTypeImage.color;
        tempColor.a = .66f;
        _helveticaTypeImage.color = tempColor;
        tempColor = _notoSerifTypeImage.color;
        tempColor.a = .33f;
        _notoSerifTypeImage.color = tempColor;
        tempColor = _tahomaTypeImage.color;
        tempColor.a = .66f;
        _tahomaTypeImage.color = tempColor;

        if (_defaultFontTypeIndex != 2)
        {
            ChangeFontType(2);
        }
    }

    public void TimesType()
    {
        // Not in Andrea right now.
        if (_defaultFontTypeIndex != 3)
        {
            ChangeFontType(3);
        }
    }

    public void VerdanaType()
    {
        // Not in Andrea right now.
        if (_defaultFontTypeIndex != 4)
        {
            ChangeFontType(4);
        }
    }

    public void AvenirType()
    {
        // Not in Andrea right now.
        if (_defaultFontTypeIndex != 5)
        {
            ChangeFontType(5);
        }
    }

    public void SmallScale()
    {
        Color tempColor;
        tempColor = _smallScaleImage.color;
        tempColor.a = .33f;
        _smallScaleImage.color = tempColor;
        tempColor = _mediumScaleImage.color;
        tempColor.a = .66f;
        _mediumScaleImage.color = tempColor;
        tempColor = _largeScaleImage.color;
        tempColor.a = .66f;
        _largeScaleImage.color = tempColor;

        ChangeTabletSize(0.125f);
    }

    public void MediumScale()
    {
        Color tempColor;
        tempColor = _smallScaleImage.color;
        tempColor.a = .66f;
        _smallScaleImage.color = tempColor;
        tempColor = _mediumScaleImage.color;
        tempColor.a = .33f;
        _mediumScaleImage.color = tempColor;
        tempColor = _largeScaleImage.color;
        tempColor.a = .66f;
        _largeScaleImage.color = tempColor;

        ChangeTabletSize(0.20f);
    }

    public void LargeScale()
    {
        Color tempColor;
        tempColor = _smallScaleImage.color;
        tempColor.a = .66f;
        _smallScaleImage.color = tempColor;
        tempColor = _mediumScaleImage.color;
        tempColor.a = .66f;
        _mediumScaleImage.color = tempColor;
        tempColor = _largeScaleImage.color;
        tempColor.a = .33f;
        _largeScaleImage.color = tempColor;

        ChangeTabletSize(0.275f);
    }

    public void BrightnessSliderClicked()
    {
        BrightnessSliderDrag();
    }

    public void BrightnessSliderDrag()
    {
        // if (!_isBookStaged || _adjustedMaxHeight == 0) return;

        _brightnessSliderValue = _brightnessSlider.value + 0.1f ;

        Color tempColor;
        // tempColor = _readingScreenGameObject.GetComponent<Image>().color;
        tempColor.r = .898f * _brightnessSliderValue;
        tempColor.g = .890f * _brightnessSliderValue;
        tempColor.b = .878f * _brightnessSliderValue;
        tempColor.a = 1.0f;
        _readingScreenGameObject.GetComponent<Image>().color = tempColor;

        

        Color tempColor1;
        tempColor1.r = tempColor.r * .985f;
        tempColor1.g = tempColor.g * .985f;
        tempColor1.b = tempColor.b * .985f;
        tempColor1.a = 1.0f;
        // _sliderBackground.color = tempColor1;

        Color tempColor2;
        tempColor2.r = tempColor.r * 1.05f;
        tempColor2.g = tempColor.g * 1.05f;
        tempColor2.b = tempColor.b * 1.05f;
        tempColor2.a = 1.0f;
        _sliderBackground.color = tempColor2;
        _sliderFill.color = tempColor2;
        _sliderButton.color = tempColor2;


        tempColor.a = 0.004f;
        _optionsMenuGameObject.GetComponent<Image>().color = tempColor;

        float _test = (6 * (1 - _brightnessSlider.value)) / 255f;
        tempColor.r = tempColor.r + _test;
        tempColor.g = tempColor.g + _test;
        tempColor.b = tempColor.b + _test;
        tempColor.a = 1.0f;
        _materialGreyLight.color = tempColor;

        // if (!_isBookStaged) return;

        if (_isTutorialMode && _topCenterTMP.text != "Settings" && _isBookStaged)
        {
            TutorialModeText();
        }

        if (_brightnessSliderValue < 0.35f)
        {
            _isTextDarkMode = true;
            if (_mainTMP != null && !_mainTMP.text.Contains($"<color=#ffffff75>") && _isBookStaged && _overflowPageIndex == -1)
            {
                DefinePage(_mainPageIndex, false);
            }
        }
        else
        {
            _isTextDarkMode = false;
            if (_mainTMP != null && _mainTMP.text.Contains($"<color=#ffffff75>") && _isBookStaged && _overflowPageIndex == -1)
            {
                DefinePage(_mainPageIndex, false);
            }
        }
        // _AndreaScreenModel.GetComponent<MeshRenderer>().material.color = tempColor;
        // _readingScreenGameObject
    }

    public void EnabledPickup()
    {
        PlayAudio(_clickAudio);

        _candleVRCPickup.pickupable = true;
        _candleGameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void DisabledPickup()
    {
        PlayAudio(_clickAudio);

        _candleVRCPickup.pickupable = false;
        _candleGameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void EnabledMovement()
    {
        PlayAudio(_clickAudio);

        Networking.LocalPlayer.Immobilize(false);
    }

    public void DisabledMovement()
    {
        PlayAudio(_clickAudio);

        Networking.LocalPlayer.Immobilize(true);
    }

    public void TriggerTurnPage()
    {
        PlayAudio(_clickAudio);

        _defaultTurnPageIndex = 0;
        _mainTurnPageIndex = _defaultTurnPageIndex;
    }

    public void BothTurnPage()
    {
        PlayAudio(_clickAudio);

        _defaultTurnPageIndex = 1;
        _mainTurnPageIndex = _defaultTurnPageIndex;
    }

    public void GripTurnPage()
    {
        PlayAudio(_clickAudio);

        _defaultTurnPageIndex = 2;
        _mainTurnPageIndex = _defaultTurnPageIndex;
    }

    public void YesConfirm()
    {
        if (_isHomeContainerHidden)
        {
            HomeContainer();
        }
        // _mainMenuGameObject.SetActive(true);
        _backButtonGameObject.SetActive(true);
        _catalogButtonGameObject.SetActive(true);
        _topLeftTMP.gameObject.SetActive(false);
        _topLeftCloneTMP.gameObject.SetActive(false);
        BackButton();
    }

    public void NoConfirm()
    {
        
    }

    private void PlayAudio(AudioClip clip)
    {
        if (_candleAudioSource.isPlaying && clip == _softSwipeAudio) return;
        _candleAudioSource.clip = clip;
        _candleAudioSource.Play();
    }

    private void ScreenSaverStart(bool isStart)
    {
        ScreenSaverClicked();
        ScreenSaverClicked();
        SelectRandomScreenSaverBackground(isStart);
        _isFirstTimeMode = true;
        // _isScreenSaverHidden = false;

    }

    public void SelectRandomScreenSaverBackground(bool isStart)
    {
        int _randomInt;
        _randomInt = _screenSaverIndex;
        while (_randomInt == _screenSaverIndex)
        {
            _randomInt = UnityEngine.Random.Range(0, _loadingScreenSpriteList.Length - 1);
            if (isStart || !_isBookStaged)
            {
                // These are bad photos for the Andrea logo.
                int[] _badInts = new int[] { 11, 13, 7, 6, 3, 1, 0 };  // { -1 };
                // { 11, 13, 7, 6, 3, 1, 0 };  // Disabled for now.
                if (Array.IndexOf(_badInts, _randomInt) != -1)
                {
                    _randomInt = _screenSaverIndex;
                }
            }
        }
        _screenSaverIndex = _randomInt;
        _screenSaverBackgroundGameObject.GetComponent<Image>().sprite = _loadingScreenSpriteList[_screenSaverIndex];
    }

    public void LerpScreenSaver()
    {
        if (_isScreenSaverLerp && timerScreenSaver > 0f)
        {
            _screenSaverBackgroundGameObject.transform.localPosition = Vector3.Lerp(_screenSaverVectorStart, _screenSaverVectorTarget, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, timerScreenSaver)));
            if (timerScreenSaver >= 1)
            {
                _isScreenSaverLerp = false;
                if (_isScreenSaverHidden)
                {
                    if (_isBookStaged)
                    {
                        _screenSaverAndreaText.SetActive(false);
                    }
                    SelectRandomScreenSaverBackground(false);
                }
            }
            timerScreenSaver += Time.deltaTime * 0.35f;
        }
        if (_isHomeContainerLerp && timerHomeContainer > 0f)
        {
            _homeContainerGameObject.transform.localPosition = Vector3.Lerp(_homeContainerVectorStart, _homeContainerVectorTarget, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, timerHomeContainer)));
            if (timerHomeContainer >= 1)
            {
                _isHomeContainerLerp = false;
                if (!_isBookStaged)
                {
                    _screenSaverAndreaText.SetActive(true);
                }
            }
            timerHomeContainer += Time.deltaTime * 0.40f;
        }
        if (_isOptionsContainerLerp && timerOptionsContainer > 0f)
        {
            _optionsContainerGameObject.transform.localPosition = Vector3.Lerp(_optionsContainerVectorStart, _optionsContainerVectorTarget, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, timerOptionsContainer)));
            if (timerOptionsContainer >= 1)
            {
                _isOptionsContainerLerp = false;
                if (_isOptionsContainerHidden && _isBookStaged)
                {
                    _screenPhysicalGameObject.GetComponent<Renderer>().material = _materialGreyLight;
                    _topCenterTMP.gameObject.SetActive(false);
                    _topCenterCloneTMP.gameObject.SetActive(false);
                    _topCenterTMP.text = $"{Networking.LocalPlayer.displayName}'s Library";
                    _topCenterCloneTMP.text = _topCenterTMP.text;
                    if (_isTutorialMode)
                    {
                        TutorialModeText();
                    }
                    _bottomLeftTMP.gameObject.SetActive(_isOptionsContainerHidden);
                    _bottomLeftCloneTMP.gameObject.SetActive(_isOptionsContainerHidden);
                    _bottomRightTMP.gameObject.SetActive(_isOptionsContainerHidden);
                    _bottomRightCloneTMP.gameObject.SetActive(_isOptionsContainerHidden);
                    _bottomCenterTMP.gameObject.SetActive(_isOptionsContainerHidden);
                    _bottomCenterCloneTMP.gameObject.SetActive(_isOptionsContainerHidden);
                    // _bottomCoreTMP.gameObject.SetActive(_isOptionsContainerHidden);
                    // _bottomCoreCloneTMP.gameObject.SetActive(_isOptionsContainerHidden);
                    _locNavigationSlider.gameObject.SetActive(_isOptionsContainerHidden);

                }
                else if (_isOptionsContainerHidden && !_isBookStaged && _isScreenSaverHidden)
                {
                    _screenPhysicalGameObject.GetComponent<Renderer>().material = _materialGreyMedium;
                    _topCenterTMP.gameObject.SetActive(true);
                    _topCenterCloneTMP.gameObject.SetActive(true);
                    _topCenterTMP.text = $"{Networking.LocalPlayer.displayName}'s Library";
                    _topCenterCloneTMP.text = _topCenterTMP.text;

                    if (_calibrateMemoryDeepFreeze != -1)
                    {
                        CalibrateMemory(_calibrateMemoryDeepFreeze);
                    }
                }

                
            }
            timerOptionsContainer += Time.deltaTime * 0.5f;
        }
    }

    public void LerpUpdate()
    {
        if (_isCategoryLerp0 && timer0 > 0f)
        {
            _categoryGameObject1.transform.localPosition = Vector3.Lerp(_categoryVectorStart1, _categoryVectorTarget1, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, timer0)));
            if (timer0 >= 1)
            {
                _isCategoryLerp0 = false;
            }
            timer0 += Time.deltaTime * 0.50f;
        }
        if (_isCategoryLerp1 && timer1 > 0f)
        {
            _categoryGameObject2.transform.localPosition = Vector3.Lerp(_categoryVectorStart2, _categoryVectorTarget2, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, timer1)));
            if (timer1 >= 1)
            {
                _isCategoryLerp1 = false;
            }
            timer1 += Time.deltaTime * 0.50f;
        }
        if (_isCategoryLerp2 && timer2 > 0f)
        {
            _categoryGameObject3.transform.localPosition = Vector3.Lerp(_categoryVectorStart3, _categoryVectorTarget3, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, timer2)));
            if (timer2 >= 1)
            {
                _isCategoryLerp2 = false;
            }
            timer2 += Time.deltaTime * 0.50f;
        }
        if (_isCategoryLerp3 && timer3 > 0f)
        {
            _categoryGameObject4.transform.localPosition = Vector3.Lerp(_categoryVectorStart4, _categoryVectorTarget4, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, timer3)));
            if (timer3 >= 1)
            {
                _isCategoryLerp3 = false;
            }
            timer3 += Time.deltaTime * 0.50f;
        }
    }

    private void HomeContainer()
    {
        _isHomeContainerLerp = true;
        if (_isHomeContainerHidden)
        {
            timerHomeContainer = 0.001f;
            _homeContainerVectorStart = _homeContainerGameObject.transform.localPosition;
            _homeContainerVectorTarget = _homeContainerShown;
            _isHomeContainerHidden = false;
        }
        else
        {
            timerHomeContainer = 0.001f;
            _homeContainerVectorStart = _homeContainerGameObject.transform.localPosition;
            _homeContainerVectorTarget = _homeContainerHidden;
            _isHomeContainerHidden = true;
        }
    }

    private void TopCenterText()
    {
        if (!_isHomeContainerHidden && _isOptionsContainerHidden)
        {
            _topCenterTMP.text = $"{Networking.LocalPlayer.displayName}'s Library";

            if (_isFirstTimeMode)
            {
                // _topCenterTMP.text = "Click to Interact";  // Disabled for now.
            }

            _topCenterCloneTMP.text = _topCenterTMP.text;


        }
        if (!_isOptionsContainerHidden)
        {
            _topCenterTMP.text = "Settings";
            _topCenterCloneTMP.text = _topCenterTMP.text;

        }
    }

    public void ScreenSaverClicked()
    {
        // Prevents showing the screen saver when the home container is disappearing.
        if (_isHomeContainerHidden && _isScreenSaverHidden) return;

        _isScreenSaverLerp = true;
        if (_isScreenSaverHidden)
        {
            timerScreenSaver = 0.001f;
            _screenSaverVectorStart = _screenSaverBackgroundGameObject.transform.localPosition;
            _screenSaverVectorTarget = _screenSaverBackgroundShown;
            _isScreenSaverHidden = false;
            _screenPhysicalGameObject.GetComponent<Renderer>().material = _materialGreyDark;
            _backButtonGameObject.SetActive(false);
            _catalogButtonGameObject.SetActive(false);
            _topCenterTMP.gameObject.SetActive(false);
            _topCenterCloneTMP.gameObject.SetActive(false);
            _topLeftTMP.gameObject.SetActive(false);
            _topLeftCloneTMP.gameObject.SetActive(false);
            _topRightTMP.gameObject.SetActive(false);
            _topRightCloneTMP.gameObject.SetActive(false);
            _bottomRightTMP.gameObject.SetActive(false);
            _bottomRightCloneTMP.gameObject.SetActive(false);
            _bottomLeftTMP.gameObject.SetActive(false);
            _bottomLeftCloneTMP.gameObject.SetActive(false);
            _bottomCenterTMP.gameObject.SetActive(false);
            _bottomCenterCloneTMP.gameObject.SetActive(false);
            _screenSaverHeaderImage.color = _screenPhysicalGameObject.GetComponent<Renderer>().material.color;

            PlayAudio(_backAudio);
            _isFirstTimeMode = false;

        }
        else
        {
            timerScreenSaver = 0.001f;
            _screenSaverVectorStart = _screenSaverBackgroundGameObject.transform.localPosition;
            _screenSaverVectorTarget = _screenSaverBackgroundHidden;
            _isScreenSaverHidden = true;
            _catalogButtonGameObject.SetActive(true);
            _backButtonGameObject.SetActive(true);
            _topCenterTMP.gameObject.SetActive(true);
            _topCenterCloneTMP.gameObject.SetActive(true);
            _bottomRightTMP.gameObject.SetActive(true);
            _bottomRightCloneTMP.gameObject.SetActive(true);
            _bottomLeftTMP.gameObject.SetActive(true);
            _bottomLeftCloneTMP.gameObject.SetActive(true);
            _bottomCenterTMP.gameObject.SetActive(true);
            _bottomCenterCloneTMP.gameObject.SetActive(true);

            TopCenterText();
            PlayAudio(_selectBookAudio);

            // Keep disabled for now to reduce clutter.
            // _topRightTMP.gameObject.SetActive(true);
            // _topRightCloneTMP.gameObject.SetActive(true);
            // _topLeftTMP.gameObject.SetActive(true);
            // _topLeftCloneTMP.gameObject.SetActive(true);

            // Have to do it like this.
            _screenPhysicalGameObject.GetComponent<Renderer>().material = _materialGreyMedium;
            _screenSaverHeaderImage.color = _screenPhysicalGameObject.GetComponent<Renderer>().material.color;

            if (_isHomeContainerHidden)
            {
                _screenPhysicalGameObject.GetComponent<Renderer>().material = _materialGreyLight;
            }
        }
        // _screenSaverHeaderImage.color = _screenPhysicalGameObject.GetComponent<Renderer>().material.color;
        /*
        ColorBlock _colorBlock = _screenSaverHeaderImage.GetComponent<Button>().colors;
        _colorBlock.normalColor = _screenSaverHeaderImage.color;
        _colorBlock.highlightedColor = _screenSaverHeaderImage.color;
        _colorBlock.pressedColor = _screenSaverHeaderImage.color;
        _colorBlock.selectedColor = _screenSaverHeaderImage.color;
        _screenSaverHeaderImage.GetComponent<Button>().colors = _colorBlock;
        */
    }

    public void CategoryMinimize0()
    {
        _isFirstTimeMode = false;
        TopCenterText();
        _isCategoryLerp0 = true;
        if (_isCategoryMinimized0)
        {
            timer0 = 0.001f;
            _categoryVectorStart1 = _categoryGameObject1.transform.localPosition;
            _categoryVectorTarget1 = _categoryVectorDefault1;
            _isCategoryMinimized0 = false;
            // _minimizeButtonGameObject0.transform.Rotate(0f, 0f, -180f);
        }
        else
        {
            timer0 = 0.001f;
            _categoryVectorStart1 = _categoryGameObject1.transform.localPosition;
            _categoryVectorTarget1 = _categoryVectorDefault1 + new Vector3(0f, 800f, 0f);
            _isCategoryMinimized0 = true;
            // _minimizeButtonGameObject0.transform.Rotate(0f, 0f, 180f);
        }

        if (_isNoDoubleExecute)
        {
            _isNoDoubleExecute = false;
            return;
        }
        else if (!_isCategoryMinimized1)
        {
            _isNoDoubleExecute = true;
            CategoryMinimize1();
        }
        else if(!_isCategoryMinimized2)
        {
            _isNoDoubleExecute = true;
            CategoryMinimize2();
        }
        else if (!_isCategoryMinimized3)
        {
            _isNoDoubleExecute = true;
            CategoryMinimize3();
        }
    }

    public void CategoryMinimize1()
    {
        _isFirstTimeMode = false;
        TopCenterText();
        _isCategoryLerp1 = true;
        if (_isCategoryMinimized1)
        {
            timer1 = 0.001f;
            _categoryVectorStart2 = _categoryGameObject2.transform.localPosition;
            _categoryVectorTarget2 = _categoryVectorDefault2;
            _isCategoryMinimized1 = false;
            // _minimizeButtonGameObject1.transform.Rotate(0f, 0f, -180f);
        }
        else
        {
            timer1 = 0.001f;
            _categoryVectorStart2 = _categoryGameObject2.transform.localPosition;
            _categoryVectorTarget2 = _categoryVectorDefault2 + new Vector3(0f, 800f, 0f);
            _isCategoryMinimized1 = true;
            // _minimizeButtonGameObject1.transform.Rotate(0f, 0f, 180f);
        }

        if (_isNoDoubleExecute)
        {
            _isNoDoubleExecute = false;
            return;
        }
        else if(!_isCategoryMinimized0)
        {
            _isNoDoubleExecute = true;
            CategoryMinimize0();
        }
        else if(!_isCategoryMinimized2)
        {
            _isNoDoubleExecute = true;
            CategoryMinimize2();
        }
        else if (!_isCategoryMinimized3)
        {
            _isNoDoubleExecute = true;
            CategoryMinimize3();
        }
    }

    public void CategoryMinimize2()
    {
        _isFirstTimeMode = false;
        TopCenterText();
        _isCategoryLerp2 = true;
        if (_isCategoryMinimized2)
        {
            timer2 = 0.001f;
            _categoryVectorStart3 = _categoryGameObject3.transform.localPosition;
            _categoryVectorTarget3 = _categoryVectorDefault3;
            _isCategoryMinimized2 = false;
            // _minimizeButtonGameObject2.transform.Rotate(0f, 0f, -180f);
        }
        else
        {
            timer2 = 0.001f;
            _categoryVectorStart3 = _categoryGameObject3.transform.localPosition;
            _categoryVectorTarget3 = _categoryVectorDefault3 + new Vector3(0f, 800f, 0f);
            _isCategoryMinimized2 = true;
            // _minimizeButtonGameObject2.transform.Rotate(0f, 0f, 180f);
        }

        if (_isNoDoubleExecute)
        {
            _isNoDoubleExecute = false;
            return;
        }
        else if(!_isCategoryMinimized0)
        {
            _isNoDoubleExecute = true;
            CategoryMinimize0();
        }
        else if(!_isCategoryMinimized1)
        {
            _isNoDoubleExecute = true;
            CategoryMinimize1();
        }
        else if (!_isCategoryMinimized3)
        {
            _isNoDoubleExecute = true;
            CategoryMinimize3();
        }
    }

    public void CategoryMinimize3()
    {
        _isFirstTimeMode = false;
        TopCenterText();
        _isCategoryLerp3 = true;
        if (_isCategoryMinimized3)
        {
            timer3 = 0.001f;
            _categoryVectorStart4 = _categoryGameObject4.transform.localPosition;
            _categoryVectorTarget4 = _categoryVectorDefault4;
            _isCategoryMinimized3 = false;
            // _minimizeButtonGameObject3.transform.Rotate(0f, 0f, -180f);
        }
        else
        {
            timer3 = 0.001f;
            _categoryVectorStart4 = _categoryGameObject4.transform.localPosition;
            _categoryVectorTarget4 = _categoryVectorDefault4 + new Vector3(0f, 800f, 0f);
            _isCategoryMinimized3 = true;
            // _minimizeButtonGameObject3.transform.Rotate(0f, 0f, 180f);
        }

        if (_isNoDoubleExecute)
        {
            _isNoDoubleExecute = false;
            return;
        }
        else if (!_isCategoryMinimized0)
        {
            _isNoDoubleExecute = true;
            CategoryMinimize0();
        }
        else if (!_isCategoryMinimized1)
        {
            _isNoDoubleExecute = true;
            CategoryMinimize1();
        }
        else if (!_isCategoryMinimized2)
        {
            _isNoDoubleExecute = true;
            CategoryMinimize2();
        }
    }

    public void MinimizeCategories()
    {
        if (!_isMinimizeCatalog) return;
        if (!_isCategoryMinimized0) CategoryMinimize0();
        if (!_isCategoryMinimized1) CategoryMinimize1();
        if (!_isCategoryMinimized2) CategoryMinimize2();
        if (!_isCategoryMinimized3) CategoryMinimize3();
        CategoryMinimize0();  // Keep first category shown.
        _isFirstTimeMode = true;
    }

    public void TopCenterButtonPointerEnter()
    {
        // Button disabled for now - too many options.
        if ((_topCenterTMP.text != $"{Networking.LocalPlayer.displayName}'s Library" && _topCenterTMP.text != "Settings" && _isScreenSaverHidden && _isBookStaged) || (_isBookStaged && _topCenterTMP.text != "Settings" && _isScreenSaverHidden))
        {
            _topCenterTMP.gameObject.SetActive(true);
            _topCenterCloneTMP.gameObject.SetActive(true);
            _topCenterTMP.text = $"{_mainBlock.text.Substring(0, _mainBlock.text.IndexOf('\n'))}";
            _topCenterCloneTMP.text = _topCenterTMP.text;
        }
    }

    public void TopCenterButtonPointerExit()
    {
        // Button disabled for now - too many options.
        if (_topCenterTMP.text != $"{Networking.LocalPlayer.displayName}'s Library" && _topCenterTMP.text != "Settings")
        {
            _topCenterTMP.gameObject.SetActive(false);
            _topCenterCloneTMP.gameObject.SetActive(false);
            _topCenterTMP.text = $"";
            _topCenterCloneTMP.text = _topCenterTMP.text;
        }
    }

    public void OptionsIconClicked()
    {
        if (!_isOptionsContainerHidden)
        {
            OptionsButtonClicked();
        }
    }
       
    private void SuperFontScaleUpdate()
    {
        bool _isActive = false;
        foreach (GameObject g in _everySingleTextBoxGameObjectList)
        {
            _isActive = g.activeSelf;
            g.SetActive(!_isActive);
            g.SetActive(_isActive);
        }
    }
}
