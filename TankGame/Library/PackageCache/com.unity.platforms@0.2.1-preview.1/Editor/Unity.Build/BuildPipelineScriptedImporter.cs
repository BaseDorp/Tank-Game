

namespace Unity.Build
{
    [UnityEditor.AssetImporters.ScriptedImporter(1, new[] { BuildPipeline.AssetExtension })]
    sealed class BuildPipelineScriptedImporter : UnityEditor.AssetImporters.ScriptedImporter
    {
        public override void OnImportAsset(UnityEditor.AssetImporters.AssetImportContext context)
        {
            var asset = BuildPipeline.CreateInstance();
            if (BuildPipeline.DeserializeFromPath(asset, context.assetPath))
            {
                context.AddObjectToAsset("asset", asset/*, icon*/);
                context.SetMainObject(asset);
            }
        }
    }
}
