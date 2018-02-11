using System;

namespace GameDataEditor
{
    public class GDEConstants {
        #region Header Strings
        public const string CreateDataHeader = "GDE Item Editor";
        public const string DefineDataHeader = "GDE Schema Editor";
        public const string CreateNewItemHeader = "Create a New Item";
        public const string ItemListHeader = "Item List";
        public const string SearchHeader = "Search";
        public const string CreateNewSchemaHeader = "Create a New Schema";
        public const string SchemaListHeader = "Schema List";
        public const string NewFieldHeader = "Add a new field";
        #endregion

        #region Button Strings
        public const string SaveBtn = "Save";
        public const string SaveNeededBtn = "Save Needed";
        public const string LoadBtn = "Load";
        public const string ClearSearchBtn = "Clear Search";
        public const string CreateNewItemBtn = "Create New Item";
        public const string DeleteBtn = "Delete";
        public const string ResizeBtn = "Resize";
        public const string AddFieldBtn = "Add Field";
        public const string AddCustomFieldBtn = "Add Custom Field";
        public const string CreateNewSchemaBtn = "Create New Schema";
        public const string RenameBtn = "Rename";
        public const string CancelBtn = "Cancel";
        public const string DeleteSchemaBtn = "Delete Schema";
        #endregion

        #region Label Strings
        public const string FilterBySchemaLbl = "Show Items Containing Schema:";
        public const string SchemaLbl = "Schema:";
        public const string ItemNameLbl = "Item Name:";
        public const string ExpandAllLbl = "Expand All";
        public const string CollapseAllLbl = "Collapse All";
        public const string ValueLbl = "Value:";
        public const string ValuesLbl = "Values:";
        public const string SizeLbl = "Size:";
        public const string SchemaNameLbl = "Schema Name:";
        public const string DefaultValueLbl = "Default Value:";
        public const string DefaultValuesLbl = "Default Values:";
        public const string DefaultSizeLbl = "Default Size:";
        public const string BasicFieldTypeLbl = "Basic Field Type:";
        public const string CustomFieldTypeLbl = "Custom Field Type:";
        public const string FieldNameLbl = "Field Name:";
        public const string IsListLbl = "Is List:";
		public const string GeneratingLbl = "Generating";
		public const string DoneGeneratingLbl = "Done Generating";
		public const string LoadDataMenu = "Load Data";
		public const string LoadAndGenMenu = "Load Data\xA0& Generate Data Classes";
		public const string GenerateExtensionsMenu = "Generate GDE Data Classes";
		public const string FileSettingsLbl = "File Settings";
		public const string PreferencesLbl = "GDE Free";
		public const string CreateDataFileLbl = "Data File";
		public const string FileDoesNotExistWarning = "Data file not found.";
		public const string BrowseBtn = "Browse ...";
		public const string OpenDataFileLbl = "Open Data File";
		public const string CreateFileLbl = "Create Data File";
		public const string ColorsLbl = "Colors";
		public const string CreateDataColorLbl = "Item Editor Headers";
		public const string DefineDataColorLbl = "Schema Editor Headers";
		public const string HighlightLbl = "Highlight";
		public const string UseDefaults = "Use Defaults";
		public const string ApplyLbl = "Apply";
		public const string RateText = "Rate\xA0& Review GDE Free";
		public const string CreateDataMenu = "Item Editor";
		public const string DefineDataMenu = "Schema Editor";
        #endregion

        #region Error Strings
        public const string ErrorLbl = "Error!";
        public const string OkLbl = "Ok";
        public const string ErrorCreatingItem = "Error creating item!";
        public const string NoOrInvalidSchema = "No schema or invalid schema selected.";
        public const string SchemaNotFound = "Schema data not found";
        public const string InvalidCustomFieldType = "Invalid custom field type selected.";
        public const string ErrorCreatingField = "Error creating field!";
        public const string ErrorCreatingSchema = "Error creating Schema!";
        public const string SureDeleteSchema = "Are you sure you want to delete this schema?";
        public const string DirectoryNotFound = "Could not find part of the path: {0}";
        #endregion

		#region Window Constants
		public const float MinLabelWidth = 200f;
		public const int Indent = 20;
		public const float LineHeight = 20f;
		public const float TopBuffer = 2f;
		public const float LeftBuffer = 2f;
		public const float RightBuffer = 2f;
		public const float VectorFieldBuffer = 0.75f;
		public const float MinTextAreaWidth = 100f;
		public const float MinTextAreaHeight = LineHeight;
		public const double DoubleClickTime = 0.5;
		public const double AutoSaveTime = 30;
		public const float PreferencesMinWidth = 640f;
		public const float PreferencesMinHeight = 280f;
		#endregion

		#region Preference Keys
		public const string CreateDataColorKey = "gde_createdatacolor";
		public const string DefineDataColorKey = "gde_definedatacolor";
		public const string HighlightColorKey = "gde_highlightcolor";
		public const string DataFileKey = "gde_datafile";
		#endregion
		
		#region Default Preference Settings
		public const string CreateDataColor = "#013859";
		public const string CreateDataColorPro = "#36ccdb";
		public const string DefineDataColor = "#185e65";
		public const string DefineDataColorPro = "#0488d7";
		public const string HighlightColor = "#f15c25";
		public const string DefaultDataFilePath =  "Resources";
		public const string RootDir = "GameDataEditor";
		public const string SettingsPath = "Editor/gde_editor_settings.bytes";
		public const string DataFile = "gde_data.txt";

		#endregion

		#region Link Strings
		public const string RateMeText = "Click To Rate!";
		public const string ForumLinkText = "Suggest Features in the Forum";
		public const string BuyLinkText = "Buy Full Version :)";
		public const string RateMeURL = "http://u3d.as/9Vb";
		public const string BuyURL = "http://u3d.as/7YN";
		public const string ForumURL = "http://forum.unity3d.com/threads/game-data-editor-the-visual-data-editor-released.250379/";
		public const string DocURL = "http://gamedataeditor.com/en/docs/gde-free-quickstart.html";
		public const string MailTo = "mailto:celeste%40stayathomedevs.com?subject=Question%20about%20GDE%20Free&cc=steve%40stayathomedevs.com";
		public const string Twitter = "https://twitter.com/celestipoo";
		public const string BorderTexturePath = "Assets/GameDataEditor/Editor/Textures/boarder.png";
		#endregion
		
		#region Import Workbook Keys
		public const string WorkbookFilePathKey = "gde_workbookpath";
		#endregion
    }
}