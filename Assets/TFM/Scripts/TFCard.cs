//using Doozy.Engine.Themes;
using MoonSharp.Interpreter;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TFCard : MonoBehaviour
{

    public TFPlayer Player;
    public CardData CardData;

    public GameObject Prefabs;

    public int SymbolSpace = 5;
    public int BorderExtraSpace = 5;

    private Transform[] symbols = new Transform[3];
    private GameObject title;
    private GameObject costs;
    private GameObject description;
    private GameObject requirement;
    private GameObject minusPrefab;
    private GameObject plusPrefab;
    private GameObject textPrefab;
    private GameObject rowPrefab;
    private GameObject productionBox;
    private GameObject symbolPrefab;
    private GameObject contentBox;

    //private GameObject incomeTarget;
    //private GameObject balanceTarget;

    //  private string code = @" 
    //      player.AddSteel(2);
    //";

    private Script script;

    public void ResetCardUI()
    {
        foreach (Transform t in this.symbols)
        {
            t.gameObject.SetActive(false);
        }

        foreach (Transform child in this.contentBox.transform)
        {
            child.gameObject.SetActive(false);
            Destroy(child.gameObject);
        }
    }

    public bool Playable()
    {
        if (this.CardData.requirement_code.Length == 0)
        {
            return true;
        }

        return this.Player.Executer.ExecuteBool(this.CardData.requirement_code);
    }

    private enum ContentType
    {
        BR,
        SYMBOL,
        OTHER,
    }

    public struct Result<S, T>
    {
        public Result(S status, T payload)
        {
            Status = status;
            Payload = payload;
        }

        public S Status { get; }
        public T Payload { get; }
    }

    private Result<ContentType, GameObject> CreateContentUI(Content element, Transform parent = null)
    {
        GameObject elementGO = null;
        int maxSymbols = 100;
        ContentType contentType = ContentType.OTHER;

        if (element.name == "text")
        {
            GameObject label = Instantiate(this.textPrefab, parent);
            label.GetComponent<TextMeshProUGUI>().text = element.text;
            elementGO = label;
        }

        // master parent
        if (element.classes != null && element.classes.Contains("content"))
        {
            elementGO = this.contentBox;
        }

        if (element.classes != null && element.classes.Contains("production-box"))
        {
            elementGO = Instantiate(this.productionBox, parent);
            elementGO.name = "ProductionBox";

            foreach (string c in element.classes)
            {
                if (c.StartsWith("production-box-size"))
                {
                    maxSymbols = int.Parse(c.Substring("production-box-size".Length, 1));
                    break;
                }
            }
        }

        if (element.classes != null && (element.classes.Contains("production") || element.classes.Contains("resource")))
        {
            elementGO = Instantiate(this.symbolPrefab, parent);
            elementGO.name = "Symbol";
            foreach (string c in element.classes)
                if (c != "production")
                {
                    elementGO.GetComponent<TFSprite>().SetSpriteName(c);
                    break;
                }
            contentType = ContentType.SYMBOL;
            //return new Result<ContentType, UIWidget>(ContentType.SYMBOL, elementWidget);
        }

        if (element.classes != null && element.classes.Contains("tile"))
        {
            foreach (string c in element.classes)
            {
                if (c.StartsWith("city") || c.StartsWith("ocean") || c.StartsWith("greenery") || c.StartsWith("special"))
                {
                    elementGO = Instantiate(this.symbolPrefab, parent);
                    string tileName = c;
                    RectTransform elementRect = elementGO.GetComponent<RectTransform>();
                    if (c.EndsWith("-small"))
                    {
                        tileName = tileName.Substring(0, c.Length - "-small".Length);
                        elementRect.sizeDelta = new Vector2(32, 32);
                    }
                    else
                    {
                        elementRect.sizeDelta = new Vector2(54, 54);
                    }
                    elementGO.GetComponent<TFSprite>().SetSpriteName(tileName);
                    return new Result<ContentType, GameObject>(ContentType.SYMBOL, elementGO);
                }
            }
        }

        if (element.classes != null && element.classes.Contains("production-prefix"))
        {
            string name = element.content[0].classes[0];

            if (name == "minus")
            {
                elementGO = Instantiate(this.minusPrefab, parent);
            }
            else if (name == "plus")
            {
                elementGO = Instantiate(this.plusPrefab, parent);
            }
            else
            {
                Debug.LogError("wrong prefix, not minus, not plus: " + element.content[0].classes[0]);
            }

            elementGO.name = name;
            return new Result<ContentType, GameObject>(ContentType.OTHER, elementGO);
        }

        if (element.name == "br")
        {
            return new Result<ContentType, GameObject>(ContentType.BR, null);
        }

        //UIWidget anchorWidgetVertical = elementGO;

        if (element.content != null && elementGO != null)
        {
            GameObject row = Instantiate(this.rowPrefab, elementGO.transform);
            row.name = "row";
            int symbolCounter = 0;

            foreach (Content c in element.content)
            {
                if (symbolCounter == maxSymbols) // break line
                {
                    // create new row
                    row.GetComponent<ContentFit>().AdjustHorizontal(10, 0);
                    row = Instantiate(this.rowPrefab, elementGO.transform);
                    row.name = "row";
                    symbolCounter = 0;
                }

                Result<ContentType, GameObject> result = CreateContentUI(c, row.transform);

                if (result.Status == ContentType.BR && row.transform.childCount > 0) // break line
                {
                    // create new row
                    row.GetComponent<ContentFit>().AdjustHorizontal(10, 0);
                    row = Instantiate(this.rowPrefab, elementGO.transform);
                    row.name = "row";
                    symbolCounter = 0;
                }

                if (result.Status == ContentType.SYMBOL)
                    symbolCounter++;
            }
            row.GetComponent<ContentFit>().AdjustHorizontal(10, 0);
            ContentFit contentFit = elementGO.GetComponent<ContentFit>();
            if (contentFit != null)
            {
                contentFit.AdjustVertical(10, 10);
            }
        }

        return new Result<ContentType, GameObject>(contentType, elementGO);
    }

    public void UpdateCardUI()
    {
        this.ResetCardUI();

        Debug.Log("Playable " +  this.Playable());

        // update symbols
        IList<string> tags = this.CardData.tags;

        for (int i = 0; i < tags.Count; i++)
        {
            //Debug.Log(tags[i]);
            this.symbols[i].gameObject.SetActive(true);
            this.symbols[i].GetComponent<TFSprite>().SetSpriteName(tags[i]);
        }

        for (int i = tags.Count; i < 3; i++)
        {
            this.symbols[i].gameObject.SetActive(false);
        }

        // update text
        this.title.GetComponent<TextMeshProUGUI>().text = this.CardData.title;
        this.costs.GetComponent<TextMeshProUGUI>().text = this.CardData.costs.ToString();
        this.description.GetComponent<TextMeshProUGUI>().text = this.CardData.description;
        this.requirement.GetComponent<TextMeshProUGUI>().text = this.CardData.requirements;

        CreateContentUI(this.CardData.content);
    }

    public List<String> GetIncomeCode()
    {
        return this.CardData.income_code;
    }

    public void Play()
    {
        this.Player.PlayCard(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        this.symbols[0] = this.transform.Find("Symbol0");
        this.symbols[1] = this.transform.Find("Symbol1");
        this.symbols[2] = this.transform.Find("Symbol2");
        this.title = this.transform.Find("Title").gameObject;
        this.costs = this.transform.Find("Costs").gameObject;
        this.description = this.transform.Find("Description").gameObject;
        this.requirement = this.transform.Find("Requirement").gameObject;
        this.contentBox = this.transform.Find("ContentBox").gameObject;
        this.textPrefab = this.Prefabs.transform.Find("Text").gameObject;
        this.productionBox = this.Prefabs.transform.Find("ProductionBox").gameObject;
        this.symbolPrefab = this.Prefabs.transform.Find("Symbol").gameObject;
        this.minusPrefab = this.Prefabs.transform.Find("Minus").gameObject;
        this.plusPrefab = this.Prefabs.transform.Find("Plus").gameObject;
        this.rowPrefab = this.Prefabs.transform.Find("Row").gameObject;

        //this.incomeTarget = this.transform.Find("IncomeTarget").gameObject;
        //this.balanceTarget = this.transform.Find("BalanceTarget").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
