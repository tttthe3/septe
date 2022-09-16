using UnityEngine.Rendering.Universal;

public class DrawTransparentDepthRendererFeature : ScriptableRendererFeature
{
    private DrawTransparentDepthRenderPass pass;

    public override void Create()
    {
        pass = new DrawTransparentDepthRenderPass();
        pass.renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (pass != null)
        {
            renderer.EnqueuePass(pass);
        }
    }
}