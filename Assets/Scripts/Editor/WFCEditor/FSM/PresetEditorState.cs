using Editor.CommonFunctions.Layout;
using FinalStateMachine.FSM;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using WFCEditor.Data;
using Editor.CommonFunctions.AssetDataBase;

namespace WFCEditor
{
    public partial class WFCEditor
    {
        public class PresetEditorState : WFCEditorStateBaseClass
        {
            #region Global Variables
            private const string _PRESETSFOLDERNAME = "WFCPresets";
            private const string _PRESETSFOLDERPATH = _WFCFOLDERPATH + "/" + "WFCPresets";

            private Vector2 _scrollPositionPresetList = new Vector2();

            private List<WFCPreset> _presetList;
            private WFCPreset _currentPreset;

            private string _createPresetName;
            private string _deletePresetName;
            private string _copiedPresetName;
            #endregion Global Variables

            public PresetEditorState(FSMBaseClass fsm) : base(fsm)
            {

            }

            public override void Initialize()
            {
                base.Initialize();

                CreateWFCPresetsFolder();
                InitializePresetsList();
            }

            public override void Update()
            {
                base.Update();

                DisplayTitlePresetsList();
                DisplayPresetsList();

                if(_currentPreset != null)
                {
                    DisplayTitleCurrentPresetData();
                    DisplayCurrentPresetName();
                    DisplayCurrentPresetWidthAndHeight();
                    DisplayCurrentPresetGenerationPosition();
                    DisplayCurrentPresetTileDimensions();
                }

                DisplayTitleModifyingPresetList();
                CreatePreset();
                DeletePreset();
                CopyPreset();
            }

            #region Initialize Functions
            private void CreateWFCPresetsFolder()
            {
                AssetDataBaseCommonFunctions.CreateFolderOnce(_WFCFOLDERPATH, _PRESETSFOLDERNAME);
            }
            private void InitializePresetsList()
            {
                string[] folderPaths =  new string[] { _PRESETSFOLDERPATH };

                AssetDataBaseCommonFunctions.ScriptableObjectFolderToList(folderPaths, ref _presetList);
            }

            #endregion Initialize Functions

            #region Update Functions
            private void DisplayTitlePresetsList()
            {
                const int SPACE = 25;
                const string LABEL = "List Of Presets";
                const string SIDE = "=====";
                const char DELIMETER = ' ';

                EditorGUILayoutCommonFunctions.DrawCentralLabel(LABEL, SPACE, SIDE, DELIMETER, EditorStyles.boldLabel);
            }
            private void DisplayPresetsList()
            {
                const int SCROLLVIEWHEIGHT = 120;

                EditorGUILayoutCommonFunctions.ScrollListButtonsWithCurrentField<WFCPreset>(SCROLLVIEWHEIGHT, Color.green, ref _scrollPositionPresetList, ref _presetList, ref _currentPreset);
            }

            private void DisplayTitleCurrentPresetData()
            {
                const int SPACE = 25;
                const string LABEL = "Current Preset Data";
                const string SIDE = "=====";
                const char DELIMETER = ' ';

                EditorGUILayoutCommonFunctions.DrawCentralLabel(LABEL, SPACE, SIDE, DELIMETER, EditorStyles.boldLabel);
            }
            private void DisplayCurrentPresetName()
            {
                const int SPACE = 18;
                const string LABEL = "Name: ";

                string newName = _currentPreset.name;
                newName = EditorGUILayoutCommonFunctions.TextFieldLabel(LABEL, SPACE, EditorStyles.label, newName);

                if(newName != _currentPreset.name)
                {
                    AssetDataBaseCommonFunctions.RenameScriptableObject(_currentPreset, _PRESETSFOLDERPATH, newName);
                }
            }
            private void DisplayCurrentPresetWidthAndHeight()
            {
                const int SPACE = 18;
                const int LABELWIDTH = 80;
                const int SPACEBETWEEN = 20;
                const string INT1Text = "Grid Width: ";
                const string INT2Text = "Grid Height: ";


                EditorGUILayoutCommonFunctions.Ints2FieldLabel(SPACE, LABELWIDTH, SPACEBETWEEN, INT1Text, INT2Text, ref _currentPreset.GridWidth, ref _currentPreset.GridHeight);
                EditorGUILayoutCommonFunctions.SaveScriptableObject(_currentPreset);
            }
            private void DisplayCurrentPresetGenerationPosition()
            {
                const string LABEL = "Generation Position: ";
                const int SPACE = 18;
                const bool ALLOWSCENEOBJECTS = true;

               Transform transform = EditorGUILayoutCommonFunctions.Vector3FieldTransformLabel(LABEL, SPACE, EditorStyles.label, ref _currentPreset.GenerationPosition, ALLOWSCENEOBJECTS);

                if(transform != null)
                {
                    _currentPreset.GenerationPosition = transform.position;
                }

                EditorGUILayoutCommonFunctions.SaveScriptableObject(_currentPreset);
            }
            private void DisplayCurrentPresetTileDimensions()
            {
                const string LABEL = "Tile Dimensions: ";
                const int SPACE = 18;
                const bool ALLOWSCENEOBJECTS = true;

                Transform transform = EditorGUILayoutCommonFunctions.Vector3FieldTransformLabel(LABEL, SPACE, EditorStyles.label, ref _currentPreset.TileDimensions, ALLOWSCENEOBJECTS);

                if (transform != null)
                {
                    _currentPreset.TileDimensions = transform.localScale;
                }

                EditorGUILayoutCommonFunctions.SaveScriptableObject(_currentPreset);
            }

            private void DisplayTitleModifyingPresetList()
            {
                const int SPACE = 25;
                const string LABEL = "Modify Preset List";
                const string SIDE = "=====";
                const char DELIMETER = ' ';

                EditorGUILayoutCommonFunctions.DrawCentralLabel(LABEL, SPACE, SIDE, DELIMETER, EditorStyles.boldLabel);
            }
            private void CreatePreset()
            {
                const string CENTRALLABEL = "Name Of New Preset: ";
                const string BUTTONLABEL = "Create Preset";
                const int SPACE = 18;

                _createPresetName = EditorGUILayoutCommonFunctions.TextFieldLabel(CENTRALLABEL, SPACE, EditorStyles.label, _createPresetName);
                
                if(GUILayout.Button(BUTTONLABEL))
                {
                    AssetDataBaseCommonFunctions.CreatScriptableObjectAddToList(_createPresetName, _PRESETSFOLDERPATH, _presetList);
                }
            }
            private void DeletePreset()
            {
                const string CENTRALLABEL = "Name Of Deleting Preset: ";
                const string BUTTONLABEL = "Delete Preset";
                const int SPACE = 18;

                _deletePresetName = EditorGUILayoutCommonFunctions.TextFieldLabel(CENTRALLABEL, SPACE, EditorStyles.label, _deletePresetName);

                if (GUILayout.Button(BUTTONLABEL))
                {
                    for(int i = _presetList.Count - 1; i >= 0; i--)
                    {
                        if (_presetList[i].name == _deletePresetName)
                        {
                            AssetDataBaseCommonFunctions.RemoveScriptableObjectRemoveFromList(_presetList[i], _PRESETSFOLDERPATH, _presetList);

                        }
                    }

                }
            }
            private void CopyPreset()
            {
                const string CENTRALLABEL = "Name Of Copied Preset: ";
                const string BUTTONLABEL = "Copy Preset";
                const int SPACE = 18;

                _copiedPresetName = EditorGUILayoutCommonFunctions.TextFieldLabel(CENTRALLABEL, SPACE, EditorStyles.label, _copiedPresetName);

                if (GUILayout.Button(BUTTONLABEL))
                {
                    for (int i = _presetList.Count - 1; i >= 0; i--)
                    {
                        if (_presetList[i].name == _copiedPresetName)
                        {
                            _currentPreset.GridWidth = _presetList[i].GridWidth;
                            _currentPreset.GridHeight = _presetList[i].GridHeight;

                            _currentPreset.GenerationPosition = _presetList[i].GenerationPosition;
                            _currentPreset.TileDimensions = _presetList[i].TileDimensions;

                            EditorGUILayoutCommonFunctions.SaveScriptableObject(_currentPreset);
                        }
                    }

                }
            }
            #endregion Update Functions
        }
    }
}

