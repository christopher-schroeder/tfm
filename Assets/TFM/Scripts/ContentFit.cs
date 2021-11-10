using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFit : MonoBehaviour
{
    public void AdjustHorizontal(int itempadding, int outerpadding)
    {
        float totalWidth = 0;
        float totalHeight = 0;
        RectTransform rect = this.GetComponent<RectTransform>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            RectTransform childRect = this.transform.GetChild(i).GetComponent<RectTransform>();
            totalWidth += childRect.rect.width + itempadding;
            totalHeight = Math.Max(totalHeight, childRect.rect.height);
        }
        totalWidth = totalWidth - itempadding + 2 * outerpadding;
        totalHeight += 2 * outerpadding;

        rect.sizeDelta = new Vector2(totalWidth, totalHeight);

        float right = -totalWidth / 2 + outerpadding;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            RectTransform childRect = child.GetComponent<RectTransform>();
            child.localPosition = new Vector3(right + childRect.rect.width / 2, 0, 0);
            right += childRect.rect.width + itempadding;
        }
    }

    public void AdjustVertical(int itempadding, int outerpadding, bool debug=false)
    {
        float totalWidth = 0;
        float totalHeight = 0;
        RectTransform rect = this.GetComponent<RectTransform>();

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            if (!(child.gameObject.activeSelf))
                continue;
            RectTransform childRect = child.GetComponent<RectTransform>();
            totalWidth = Math.Max(totalWidth, childRect.rect.width);
            totalHeight += childRect.rect.height + itempadding;
        }

        totalWidth += 2 * outerpadding;
        totalHeight = totalHeight - itempadding + 2 * outerpadding;

        rect.sizeDelta = new Vector2(totalWidth, totalHeight);

        if (debug)
        {
            Debug.Log(this.gameObject.name);
            Debug.Log(this.transform.childCount);
        }

        float top = totalHeight / 2 - outerpadding;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            if (!(child.gameObject.activeSelf))
                continue;
            RectTransform childRect = child.GetComponent<RectTransform>();
            child.localPosition = new Vector3(0, top - childRect.rect.height / 2, 0);
            if (debug)
            {
                Debug.Log(childRect.rect.height);
            }
            top -= (childRect.rect.height + itempadding);
        }
    }
}
