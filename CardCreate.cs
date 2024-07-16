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
        private Vector2 size;
        private Vector3 position, scale;
        private Transform OnDragParent;

        public CardCreate(GameObject card, Sprite sprite, Vector2 size, Vector3 position, Vector3 scale, Transform onDragParent)
        {
            this.card = card;
            this.sprite = sprite;
            this.size = size;
            this.position = position;
            this.scale = scale;
            this.OnDragParent = onDragParent;
        }

        public GameObject CreateUI(Transform parent)
        {
            GameObject image = UnityEngine.Object.Instantiate(card, parent);
            image.GetComponent<Image>().sprite = sprite;
            SetRectTransform(image.GetComponent<RectTransform>());

            /*GameObject a = new GameObject("imsi");
            DragAndDrop dragHandler = a.AddComponent<DragAndDrop>();*/
            /*DragAndDrop a = image.GetComponent<DragAndDrop>();
            a.onDragParent = OnDragParent;*/
            
            return image;
        }

        public void SetRectTransform(RectTransform rectTransform)
        {
            /*rectTransform.sizeDelta = size;
            rectTransform.localPosition = position;
            rectTransform.localScale = scale;*/
            RectTransformSetter.SetRectTransform(
                rectTransform,
                new Vector2(0.5f, 1),
                new Vector2(0.5f, 1),
                new Vector2(0.5f, 0.5f),
                new Vector3(15, 15 * 2048f / 1329f, 1),
                new Vector3(0, -346.7269f / 2f, 0),
                new Vector3(15, 15, 1)

            );
            /*new Vector2(0.5f, 1);
            new Vector2(0.5f, 1);
            new Vector2(0.5f, 0.5f);
            new Vector3(card_size, card_size * card_ratio, 1);
            new Vector3(0, -card_height / 2f, 0);
            new Vector3(card_size, card_size, 1);*/
        }
    }
}
