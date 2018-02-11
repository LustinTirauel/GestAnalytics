using System;

namespace GameDataEditor
{
    public class GDMConstants
	{
        #region Metadata Constants
        public const string MetaDataFormat = "{0}{1}"; //{0} is the metadata prefix, {1} is the field the metadata is for

        // Metadata prefixes
        public const string TypePrefix = "_gdeType_";
        public const string IsListPrefix = "_gdeIsList_";
        public const string SchemaPrefix = "_gdeSchema_";       
        #endregion

        #region Item Metadata Constants
        public const string SchemaKey = "_gdeSchema";
        #endregion

        #region Error Strings
        public const string ErrorLoadingValue = "Could not load {0} value from item name:{1}, field name:{2}!";
        #endregion
    }
}
