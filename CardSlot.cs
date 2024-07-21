using IUIElement.Script;
using RTfSetter.Script;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : ICreateUI
{
    protected GameObject card;
    protected Sprite sprite;
    protected float cardWidth;
    protected float cardHeight;
    protected float cardScale;
    protected float x, y;
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
        slot.AddComponent<Slot>();
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
