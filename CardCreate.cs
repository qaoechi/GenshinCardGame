using IUIElement.Script;
using RTfSetter.Script;
using UnityEngine;
using UnityEngine.UI;

namespace OriginalCard.Script
{
    public class CardCreate : ICreateUI
    {
        public GameObject card;
        private Sprite sprite;
        private float cardWidth;
        private float cardHeight;
        private float cardScale;
        private float x, y;
        private Transform OnDragParent;
        private Transform cementry;
        public CardCreate(GameObject card, Sprite sprite, float cardWidth, float cardHeight, float cardScale, float x, float y, Transform onDragParent, Transform cementry)
        {
            this.card = card;
            this.sprite = sprite;
            this.cardWidth = cardWidth;
            this.cardHeight = cardHeight;
            this.cardScale = cardScale;
            this.x = x;
            this.y = y;
            this.OnDragParent = onDragParent;
            this.cementry = cementry;
        }
        public GameObject CreateUI(Transform parent)
        {
            GameObject image = UnityEngine.Object.Instantiate(card, parent);
            image.GetComponent<Image>().sprite = sprite;
            SetRectTransform(image.GetComponent<RectTransform>());
            return image;
        }

        public void SetRectTransform(RectTransform rectTransform)
        {
            RectTransformSetter.SetRectTransform(
               rectTransform,
               new Vector2(0.5f, 0.5f),
               new Vector2(0.5f, 0.5f),
               new Vector2(0.5f, 0.5f),
               new Vector3(cardWidth, cardHeight),
               new Vector3(x, y, 0),
               new Vector3(cardScale, cardScale, 1)
           );
        }
        public void ItsMyTurnDraw(Transform transform, Transform cementry)
        {
            var cardDraw = new CardCreate(card, sprite, cardWidth, cardHeight, cardScale, 0, 0, OnDragParent, cementry);
            GameObject newCard = cardDraw.CreateUI(transform);
            newCard.name = "card";
            DragAndDrop dADrop = newCard.AddComponent<DragAndDrop>();
            newCard.AddComponent<CanvasGroup>();
            dADrop.onDragParent = OnDragParent;
        }
    }
}
