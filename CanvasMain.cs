using UnityEngine;
using RTfSetter.Script;

namespace MainCanvas.Script
{
    public class CanvasMain : CanvasCreate
    {
        public CanvasMain(GameObject canvasPrefab, float x, float y) : base(canvasPrefab, x, y) { }
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
                new Vector3(x, y, 0),
                Vector3.one);
        }
    }
}
