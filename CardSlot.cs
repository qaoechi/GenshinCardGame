using IUIElement.Script;
using RTfSetter.Script;
using UnityEngine;
using UnityEngine.UI;
using RTfSetter;
using Unity.Mathematics;

namespace Assets.Script
{
    public class CardSlot : ICreateUI
    {
        public GameObject card;
        private Sprite sprite;
        private float cardScale;
        private float cardHeight;
        private float cardWidth;
        private float x, y;

        public CardSlot(GameObject card, Sprite sprite, float cardWidth, float cardHeight, float cardScale, float x, float y)
        {
            this.card = card;
            this.sprite = sprite;
            this.cardScale = cardScale;
            this.cardHeight = cardHeight;
            this.cardWidth = cardWidth;
            this.x = x;
            this.y = y;
        }

        public GameObject CreateUI(Transform parent)
        {
            GameObject slot = Object.Instantiate(card, parent);
            slot.GetComponent<Image>().sprite = sprite;
            SetRectTransform(slot.GetComponent<RectTransform>());


            slot.transform.SetParent(parent);
            slot.AddComponent<Slot>();
            //Slot slotScript = slot.AddComponent<Slot>();
            return slot;
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

    }
}
