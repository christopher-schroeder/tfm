using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TFMRadialBar : MonoBehaviour
{
    public int Range = 0;
    public int Multiplier = 1;
    public int Bias = 0;
    public float Distance = 150;
    public string Unit = "";

    public int Value
    {
        get { return this.value; }
        set
        {
            this.value = value;
            this.bar.fillAmount = value / (float)this.Range;
        }
    }
    public GameObject NumberPrefab;
    public GameObject CoreText;

    public int value = 0;
    private bool recreate;
    public Image bar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        this.recreate = true;
    }

    private void LateUpdate()
    {
        if (this.recreate)
        {
            Transform numbers = this.transform.Find("Numbers");
            foreach (Transform child in numbers)
            {
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    if (child != null)
                    {
                        UnityEditor.Undo.DestroyObjectImmediate(child.gameObject);
                    }
                };
            }

            if (this.Range > 0)
            {
                float angle = 360 / ((float)this.Range-1);
                for (int i = 1; i < this.Range; i++)
                {
                    Vector3 vPos = new Vector3(Mathf.Cos((angle * -(i-1) - 90 - angle / 2) * Mathf.Deg2Rad), Mathf.Sin((angle * -(i - 1) - 90 - angle / 2) * Mathf.Deg2Rad), 0);
                    var child = Instantiate(this.NumberPrefab, numbers);
                    child.name = i.ToString();
                    child.transform.localPosition = vPos * this.Distance;
                    child.GetComponent<TextMeshPro>().text = (i * this.Multiplier + this.Bias).ToString();
                }
                this.recreate = false;
            }
            this.bar.fillAmount = ((value - this.Bias) / this.Multiplier) / ((float)this.Range - 1);
            this.CoreText.GetComponent<TextMeshProUGUI>().text = this.value.ToString() + Unit;
        }
    }
#endif
}
