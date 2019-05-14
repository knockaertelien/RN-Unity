using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateAssetBundles : MonoBehaviour
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildWindowsAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Windows",BuildAssetBundleOptions.None,BuildTarget.StandaloneWindows);
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/IOS", BuildAssetBundleOptions.None, BuildTarget.iOS);
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles/Android", BuildAssetBundleOptions.None, BuildTarget.Android);
    }

}