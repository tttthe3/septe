using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DrawTransparentDepthRenderPass : ScriptableRenderPass
{
    private readonly ShaderTagId shaderTagId = new ShaderTagId("TransparentDepthOnly");
    private readonly int depthId = Shader.PropertyToID("_CameraDepthTexture");

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        var cmd = CommandBufferPool.Get("Draw Transparent Depth");

        var descriptor = renderingData.cameraData.cameraTargetDescriptor;
        descriptor.colorFormat = RenderTextureFormat.Depth;
        descriptor.depthBufferBits = 32;
        descriptor.msaaSamples = 1;
        cmd.GetTemporaryRT(depthId, descriptor, FilterMode.Point);
        cmd.SetRenderTarget(depthId, depthId);
        context.ExecuteCommandBuffer(cmd);

        var drawSettings = CreateDrawingSettings(shaderTagId, ref renderingData, SortingCriteria.CommonOpaque);
        var filterSettings = new FilteringSettings(RenderQueueRange.transparent, renderingData.cameraData.camera.cullingMask);
        context.DrawRenderers(renderingData.cullResults, ref drawSettings, ref filterSettings);

        cmd.Clear();
        cmd.ReleaseTemporaryRT(depthId);
        context.ExecuteCommandBuffer(cmd);

        CommandBufferPool.Release(cmd);
    }
}