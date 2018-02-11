using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace GameDataEditor
{
    public class GDECodeGen
    {
        public static void GenClasses(Dictionary<string, Dictionary<string, object>> allSchemas)
        {
            foreach (KeyValuePair<string, Dictionary<string, object>> pair in allSchemas)
            {
                GenClass(pair.Key, pair.Value);
            }
        }

		public static void GenStaticKeysClass(Dictionary<string, Dictionary<string, object>> allSchemas)
		{
			Debug.Log(GDEConstants.GeneratingLbl + " " + GDECodeGenConstants.StaticKeysFileName);
			StringBuilder sb = new StringBuilder();

			sb.Append(GDECodeGenConstants.AutoGenMsg);
            sb.Append("\n");
			sb.Append(GDECodeGenConstants.StaticKeyClassHeader);

			foreach (KeyValuePair<string, Dictionary<string, object>> pair in allSchemas)
			{
				string schema = pair.Key;

				List<string> items = GDEItemManager.GetItemsOfSchemaType(schema);
				foreach(string item in items)
				{
					sb.Append("\n");
					sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel2));
					sb.AppendFormat(GDECodeGenConstants.StaticKeyFormat, schema, item);
				}
			}

			sb.Append("\n");
			sb.Append("}".PadLeft(GDECodeGenConstants.IndentLevel1+1));
			sb.Append("\n");
			sb.Append("}");
			sb.Append("\n");

			WriteFile(sb, GDECodeGenConstants.StaticKeysFileName);
			//File.WriteAllText(GDESettings.FullRootDir + Path.DirectorySeparatorChar + GDECodeGenConstants.StaticKeysFilePath, sb.ToString());

			//Debug.Log(GDEConstants.DoneGeneratingLbl + " " + GDECodeGenConstants.StaticKeysFilePath);
		}

        static void GenClass(string schemaKey, Dictionary<string, object> schemaData)
        {
            StringBuilder sb = new StringBuilder();
            string className = string.Format(GDECodeGenConstants.DataClassNameFormat, schemaKey);
			string fileName = string.Format(GDECodeGenConstants.ClassFileNameFormat, className);
            Debug.Log(GDEConstants.GeneratingLbl + " " + fileName);

            // Add the auto generated comment at the top of the file
            sb.Append(GDECodeGenConstants.AutoGenMsg);
            sb.Append("\n");

            // Append all the using statements
            sb.Append(GDECodeGenConstants.DataClassHeader);
            sb.Append("\n");

            // Append the class declaration
            sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel1));
            sb.AppendFormat(GDECodeGenConstants.ClassDeclarationFormat, className);
            sb.Append("\n");
            sb.Append("{".PadLeft(GDECodeGenConstants.IndentLevel1+1));
            sb.Append("\n");

            // Append all the data variables
			AppendVariableDeclarations(sb, schemaKey, schemaData);
            sb.Append("\n");

			// Append constructors
			sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel2));
			sb.AppendFormat(GDECodeGenConstants.ClassConstructorsFormat, className);
			sb.Append("\n");

            // Append the load from dict method
            AppendLoadDictMethod(sb, schemaKey, schemaData);
            sb.Append("\n");

            // Append the close class brace
            sb.Append("}".PadLeft(GDECodeGenConstants.IndentLevel1+1));
            sb.Append("\n");

            // Append the close namespace brace
            sb.Append("}");
            sb.Append("\n");

            //File.WriteAllText(GDESettings.FullRootDir + Path.DirectorySeparatorChar + filePath, sb.ToString());
            //Debug.Log(GDEConstants.DoneGeneratingLbl + " " + filePath);
			WriteFile(sb, fileName);
        }

		static void WriteFile(StringBuilder sb, string fileName)
		{
			string fullPath = string.Empty;
			var results = AssetDatabase.FindAssets(Path.GetFileNameWithoutExtension(fileName) + " t:Script");
			if (results != null && results.Length > 0)
			{
				string assetPath = AssetDatabase.GUIDToAssetPath(results[0]);
				fullPath = Path.Combine(Environment.CurrentDirectory, assetPath);
			}
			else
				fullPath = Path.Combine(GDESettings.FullRootDir, GDECodeGenConstants.ClassFileDefaultPath + fileName);

			File.WriteAllText(fullPath, sb.ToString());
			Debug.Log(GDEConstants.DoneGeneratingLbl + " " + fileName);
		}


        static void AppendVariableDeclarations(StringBuilder sb, string schemaKey, Dictionary<string, object> schemaData)
        {
            bool didAppendSpaceForSection = false;
            bool shouldAppendSpace = false;
			bool isFirstSection = true;

			string variableType;

            // Append all the single variables first
            foreach(BasicFieldType fieldType in GDEItemManager.BasicFieldTypes)
            {
				variableType = GDEItemManager.GetVariableTypeFor(fieldType);

                List<string> fieldKeys = GDEItemManager.SchemaFieldKeysOfType(schemaKey, fieldType.ToString());
                foreach(string fieldKey in fieldKeys)
                {
                    if (shouldAppendSpace)
                        sb.Append("\n");

                    AppendVariable(sb, variableType, fieldKey);
                    shouldAppendSpace = true;
					isFirstSection = false;
                }
            }

            // Append the custom types
            foreach(string fieldKey in GDEItemManager.SchemaCustomFieldKeys(schemaKey))
            {
                if (shouldAppendSpace && !didAppendSpaceForSection && !isFirstSection)
                {
                    sb.Append("\n");
                }

                schemaData.TryGetString(string.Format(GDMConstants.MetaDataFormat, GDMConstants.TypePrefix, fieldKey), out variableType);
                variableType = string.Format(GDECodeGenConstants.DataClassNameFormat, variableType);
                AppendCustomVariable(sb, variableType, fieldKey);

                shouldAppendSpace = true;
				isFirstSection = false;
				didAppendSpaceForSection = true;
            }
            didAppendSpaceForSection = false;

            // Append the basic lists
		    foreach(BasicFieldType fieldType in GDEItemManager.BasicFieldTypes)
            {
                List<string> fieldKeys = GDEItemManager.SchemaListFieldKeysOfType(schemaKey, fieldType.ToString());
				variableType = GDEItemManager.GetVariableTypeFor(fieldType);

                foreach(string fieldKey in fieldKeys)
                {
                    if (shouldAppendSpace && !didAppendSpaceForSection && !isFirstSection)
                    {
                        sb.Append("\n");
                    }

                    AppendListVariable(sb, variableType, fieldKey);

                    shouldAppendSpace = true;
					didAppendSpaceForSection = true;
					isFirstSection = false;
                }
            }
            didAppendSpaceForSection = false;

            // Append the custom lists
            foreach(string fieldKey in GDEItemManager.SchemaCustomListFields(schemaKey))
            {
                if (shouldAppendSpace && !didAppendSpaceForSection && !isFirstSection)
                {
                    sb.Append("\n");
                }

                schemaData.TryGetString(string.Format(GDMConstants.MetaDataFormat, GDMConstants.TypePrefix, fieldKey), out variableType);
                variableType = string.Format(GDECodeGenConstants.DataClassNameFormat, variableType);
                AppendCustomListVariable(sb, variableType, fieldKey);

                shouldAppendSpace = true;
				isFirstSection = false;
				didAppendSpaceForSection = true;
            }
        }

        static void AppendLoadDictMethod(StringBuilder sb, string schemaKey, Dictionary<string, object> schemaData)
        {
            sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel2));
            sb.Append(GDECodeGenConstants.LoadDictMethod);
            sb.Append("\n");

            bool shouldAppendSpace = false;
            bool didAppendSpaceForSection = false;
			bool isFirstSection = true;

            string variableType;

            // Append all the single variables first
            foreach(BasicFieldType fieldType in GDEItemManager.BasicFieldTypes)
            {
                variableType = fieldType.ToString();
                List<string> fieldKeys = GDEItemManager.SchemaFieldKeysOfType(schemaKey, fieldType.ToString());
                foreach(string fieldKey in fieldKeys)
                {
                    AppendLoadVariable(sb, variableType, fieldKey);
                    shouldAppendSpace = true;
					isFirstSection = false;
                }
            }

            // Append the custom types
            bool appendTempKeyDeclaration = true;
            foreach(string fieldKey in GDEItemManager.SchemaCustomFieldKeys(schemaKey))
            {
                if (shouldAppendSpace && !didAppendSpaceForSection && !isFirstSection)
                {
                    sb.Append("\n");
                }

                schemaData.TryGetString(string.Format(GDMConstants.MetaDataFormat, GDMConstants.TypePrefix, fieldKey), out variableType);
                variableType = string.Format(GDECodeGenConstants.DataClassNameFormat, variableType);

                if (appendTempKeyDeclaration)
                {
                    sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel4));
                    sb.Append(GDECodeGenConstants.TempStringKeyDeclaration);
                    sb.Append("\n");
                    appendTempKeyDeclaration = false;
                }

                AppendLoadCustomVariable(sb, variableType, fieldKey);
                shouldAppendSpace = true;
				didAppendSpaceForSection = true;
				isFirstSection = false;
            }
            didAppendSpaceForSection = false;

            // Append the basic lists
		    foreach(BasicFieldType fieldType in GDEItemManager.BasicFieldTypes)
            {
                List<string> fieldKeys = GDEItemManager.SchemaListFieldKeysOfType(schemaKey, fieldType.ToString());
                variableType = fieldType.ToString();

                foreach(string fieldKey in fieldKeys)
                {
                    if (shouldAppendSpace && !didAppendSpaceForSection && !isFirstSection)
                    {
                        sb.Append("\n");
                    }

                    AppendLoadListVariable(sb, variableType, fieldKey);
                    shouldAppendSpace = true;
					didAppendSpaceForSection = true;
					isFirstSection = false;
                }
            }
            didAppendSpaceForSection = false;


            // Append the custom lists
            foreach(string fieldKey in GDEItemManager.SchemaCustomListFields(schemaKey))
            {
                if (shouldAppendSpace && !didAppendSpaceForSection && !isFirstSection)
                {
                    sb.Append("\n");
                }

                schemaData.TryGetString(string.Format(GDMConstants.MetaDataFormat, GDMConstants.TypePrefix, fieldKey), out variableType);
				variableType = "Custom";
                AppendLoadListVariable(sb, variableType, fieldKey);

				shouldAppendSpace = true;
				isFirstSection = false;
				didAppendSpaceForSection = true;
            }

			sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel3));
			sb.Append(GDECodeGenConstants.LoadDictMethodEnd);
            sb.Append("\n");
        }

        #region Gen Variable Declaration Methods
        static void AppendCustomVariable(StringBuilder sb, string type, string name)
        {
			sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel2));
			sb.AppendFormat(GDECodeGenConstants.VariableFormat, type, name, "Custom");
			sb.Append("\n");
		}

		static void AppendVariable(StringBuilder sb, string type, string name)
        {
            sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel2));
            sb.AppendFormat(GDECodeGenConstants.VariableFormat, type, name, type.UppercaseFirst());
            sb.Append("\n");
        }

        static void AppendListVariable(StringBuilder sb, string type, string name)
        {
            sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel2));
			sb.AppendFormat(GDECodeGenConstants.OneDListVariableFormat, type, name, type.UppercaseFirst());
            sb.Append("\n");
        }

		static void AppendCustomListVariable(StringBuilder sb, string type, string name)
		{
			sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel2));
			sb.AppendFormat(GDECodeGenConstants.OneDListVariableFormat, type, name, "Custom");
			sb.Append("\n");
		}
		#endregion

        #region Gen Load Variable Methods
        static void AppendLoadVariable(StringBuilder sb, string type, string name)
        {
            sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel4));
            sb.AppendFormat(GDECodeGenConstants.LoadVariableFormat, type, name, type.UppercaseFirst());
            sb.Append("\n");
        }

        static void AppendLoadCustomVariable(StringBuilder sb, string type, string name)
        {
            sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel4));
            sb.AppendFormat(GDECodeGenConstants.LoadCustomVariableFormat, type, name);
            sb.Append("\n");
        }

        static void AppendLoadListVariable(StringBuilder sb, string type, string name)
        {
            sb.Append("".PadLeft(GDECodeGenConstants.IndentLevel4));
      		sb.AppendFormat(GDECodeGenConstants.LoadVariableListFormat, type, name);
            sb.Append("\n");
        }
        #endregion
    }
}
