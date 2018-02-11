using UnityEngine;
using UnityEditor;
using System.IO;

namespace GameDataEditor
{
	public class GDELinks : EditorWindow
	{
		const string menuItemLocation = "Window/Game Data Editor Free";
		const string contextItemLocation = "Assets/Game Data Editor";
		const int menuItemStartPriority = 300;


		[MenuItem(menuItemLocation + "/" + GDEConstants.DefineDataMenu, false, menuItemStartPriority)]
		static void showSchemaEditor()
		{
			EditorWindow.GetWindow<GDESchemaManagerWindow>(false, GDEConstants.DefineDataMenu);
		}

		[MenuItem(menuItemLocation + "/" + GDEConstants.CreateDataMenu, false, menuItemStartPriority+1)]
		static void showItemEditor()
		{
			EditorWindow.GetWindow<GDEItemManagerWindow>(false, GDEConstants.CreateDataMenu);
		}
		

		// ** Divider Here ** //


		[MenuItem(menuItemLocation + "/" + GDEConstants.GenerateExtensionsMenu, false, menuItemStartPriority+15)]
		public static void DoGenerateCustomExtensions()
		{
			GDEItemManager.Load();
			
			GDECodeGen.GenStaticKeysClass(GDEItemManager.AllSchemas);
			GDECodeGen.GenClasses(GDEItemManager.AllSchemas);
			
			AssetDatabase.Refresh();
		}


		// ** Divider Here ** //


		[MenuItem(menuItemLocation + "/GDE Forum Post", false, menuItemStartPriority+30)]
		static void GDEForumPost()
		{
			Application.OpenURL(GDEConstants.ForumURL);
		}

		[MenuItem(menuItemLocation + "/GDE Free Documentation", false, menuItemStartPriority+31)]
		static void GDEFreeDocs()
		{
			Application.OpenURL(GDEConstants.DocURL);
		}
		
		
		// ** Divider Here ** //
		

		[MenuItem(menuItemLocation + "/Buy Full Version", false, menuItemStartPriority+50)]
		static void BuyFullVersion()
		{
			Application.OpenURL(GDEConstants.BuyURL);
		}

		[MenuItem(menuItemLocation + "/" + GDEConstants.RateText, false, menuItemStartPriority+50)]
		static void RateGDEFree()
		{
			Application.OpenURL(GDEConstants.RateMeURL);
		}

		[MenuItem(menuItemLocation + "/Contact/Email", false, menuItemStartPriority+52)]
		static void GDEEmail()
		{
			Application.OpenURL(GDEConstants.MailTo);
		}
		
		[MenuItem(menuItemLocation + "/Contact/Twitter" , false, menuItemStartPriority+53)]
		static void GDETwitter()
		{
			Application.OpenURL(GDEConstants.Twitter);
		}

		// **** Context Menu Below **** //
		
		
		[MenuItem(contextItemLocation + "/" + GDEConstants.LoadDataMenu, true)]
		static bool GDELoadDataValidation()
		{
			return Selection.activeObject != null && Selection.activeObject.GetType() == typeof(TextAsset);
		}
		
		[MenuItem(contextItemLocation + "/" + GDEConstants.LoadDataMenu, false, menuItemStartPriority)]
		static void GDELoadData () 
		{
			string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
			string fullPath = Path.GetFullPath(assetPath);
			
			GDESettings.Instance.DataFilePath = fullPath;
			GDESettings.Instance.Save();
			
			GDEItemManager.Load(true);
		}
		
		[MenuItem(contextItemLocation + "/" + GDEConstants.LoadAndGenMenu, true)]
		static bool GDELoadAndGenDataValidation()
		{
			return GDELoadDataValidation();
		}
		
		[MenuItem(contextItemLocation + "/" + GDEConstants.LoadAndGenMenu, false, menuItemStartPriority+1)]
		static void GDELoadAndGenData () 
		{
			GDELoadData();
			DoGenerateCustomExtensions();
		}
	}
}
