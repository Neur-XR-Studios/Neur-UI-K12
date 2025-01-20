using UnityEngine;
namespace K12.UI
{
    public class DataManager : MonoBehaviour
    {
        public class StaticVariables
        {
            //***********UI Automation****************

            public static bool IS_ENGLISH = true;
            public static int STEP_COUNT = 0;
            public static string CF_OBJECT_NAME = string.Empty;

            //To parse CSV data all column value for particular row
            public static string COLUMN_00 = string.Empty;
            public static string COLUMN_01 = string.Empty;
            public static string COLUMN_02 = string.Empty;
            public static string COLUMN_03 = string.Empty;
            public static string COLUMN_04 = string.Empty;
            public static string COLUMN_05 = string.Empty;
            public static string COLUMN_06 = string.Empty;
            public static string COLUMN_07 = string.Empty;
            public static string COLUMN_08 = string.Empty;
            public static string COLUMN_09 = string.Empty;
            public static string COLUMN_10 = string.Empty;
            public static string COLUMN_11 = string.Empty;
            public static string COLUMN_12 = string.Empty;
        };

        private void Start()
        {
            StaticVariables.IS_ENGLISH = true;
            StaticVariables.STEP_COUNT = 0;
            StaticVariables.CF_OBJECT_NAME = string.Empty;
            StaticVariables.COLUMN_00 = string.Empty;
            StaticVariables.COLUMN_01 = string.Empty;
            StaticVariables.COLUMN_02 = string.Empty;
            StaticVariables.COLUMN_03 = string.Empty;
            StaticVariables.COLUMN_04 = string.Empty;
            StaticVariables.COLUMN_05 = string.Empty;
            StaticVariables.COLUMN_06 = string.Empty;
            StaticVariables.COLUMN_07 = string.Empty;
            StaticVariables.COLUMN_08 = string.Empty;
            StaticVariables.COLUMN_09 = string.Empty;
            StaticVariables.COLUMN_10 = string.Empty;
            StaticVariables.COLUMN_11 = string.Empty;
            StaticVariables.COLUMN_12 = string.Empty;
        }
    }
}