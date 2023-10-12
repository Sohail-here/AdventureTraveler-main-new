using UnityEditor;

namespace Scrmizu.Tools.Editor
{
    [InitializeOnLoad]
    public class ScrmizuTool
    {
        static ScrmizuTool()
        {
#if false
            if (EditorApplication.isPlayingOrWillChangePlaymode) return;
            UnityPackage();
            SampleUnityPackage();
#endif
        }

        [MenuItem("Window/Scrmizu/Update ReduxPackage")]
        public static void UnityPackage()
        {
            var assetPathNames = new[] {"Assets/Scrmizu"};
            const string fileName = "Scrmizu.unitypackage";

            const ExportPackageOptions
                options = ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies;
            AssetDatabase.ExportPackage(assetPathNames, fileName, options);

        }

        [MenuItem("Window/Scrmizu/Update SampleReduxPackage")]
        public static void SampleUnityPackage()
        {
            var assetPathNames = new[] {"Assets/Scrmizu.Sample"};
            const string fileName = "Scrmizu.Sample.unitypackage";

            const ExportPackageOptions
                options = ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies;

            AssetDatabase.ExportPackage(assetPathNames, fileName, options);

        }

    }
}