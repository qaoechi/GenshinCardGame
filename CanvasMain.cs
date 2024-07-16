using UnityEngine;
using IUIElement;
using RTfSetter.Script;

namespace OriginalCanvas.Script
{
    public class CanvasMain : CanvasCreate
    {
        public CanvasMain(GameObject canvasPrefab, float emptyX, float emptyY)
            : base(canvasPrefab, emptyX, emptyY)
        {
        }

        protected override string GetCanvasName()
        {
            return "MainCanvas";
        }

        public override void SetRectTransform(RectTransform rectTransform)
        {
            RectTransformSetter.SetRectTransform(
                rectTransform,
                new Vector2(0.5f, 0.5f),
                new Vector2(0.5f, 0.5f),
                new Vector2(0.5f, 0.5f),
                new Vector2(1900, 1060),
                Vector3.zero,
                Vector3.one
                );
        }
    }
}
