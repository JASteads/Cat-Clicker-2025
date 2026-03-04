using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsUI : MonoBehaviour
{
    [SerializeField] public CategoryButton[] categoryButtons;
    public int[] tallyArray;
    public int[] countArray;
    public List<AchievementData> achievementDataList;

    [Header("Interface Elements")]
    public GameObject categoryMenu;
    public Transform achievementsList;
    public Button backButton;

    [SerializeField] public List<AchievementBlock> blocks;

    [Header("Templates")]
    [SerializeField] public GameObject categoryButtonTemplate; // Migiht remove later
    [SerializeField] public GameObject blockTemplate;

    void Start()
    {
        ConfigUI();
    }

    void OnEnable()
    {
        EventBus.OnAchievement += TallyAchievement;
    }

    void OnDisable()
    {
        EventBus.OnAchievement -= TallyAchievement;
    }

    void TallyAchievement(AchievementData a)
    {
        int type = (int)a.effect.GetAchievementType();

        ++tallyArray[type];
        UpdateCategoryButton(a.effect.GetAchievementType());
    }

    void ConfigUI()
    {
        GameData data = GameDataManager.gameData;
        blocks = new List<AchievementBlock>();

        tallyArray = new int[categoryButtons.Length];
        countArray = new int[categoryButtons.Length];

        // Count how many of each achievement type there are
        foreach (var a in achievementDataList)
        {
            int type = (int)a.effect.GetAchievementType();
            ++countArray[type];

            if (data.achivementsPool.Contains(a.achievementName))
            {
                TallyAchievement(a);
                a.isUnlocked = true;
            }
        }

        // Configure category buttons
        for (int i = 0; i < categoryButtons.Length; ++i)
        {
            CategoryButton c = categoryButtons[i];
            AchievementType type = (AchievementType)i;

            c.button.onClick.AddListener(() => ShowCategory(type));

            c.bodyColor = c.button.transform.GetComponent<Image>().color;
            c.textColor = 
                c.title.transform.GetComponent<TextMeshProUGUI>().color;
            categoryButtons[i] = c;
        }

        backButton.onClick.AddListener(() => GoBack());
        UpdateAllCategoryButtons();
        PopulateBlocksList(); // Creates a block pool that can be overwritten on-demand
    }

    void UpdateAllCategoryButtons()
    {
        for (int i = 0; i < categoryButtons.Length; ++i)
        {
            UpdateCategoryButton((AchievementType)i);
        }
    }

    void UpdateCategoryButton(AchievementType type)
    {
        string rawName = type.ToString();
        string categoryName = rawName[0] + rawName.Substring(1).ToLower();

        categoryButtons[(int)type].title.text =
            $"{categoryName}\n<size=24>" +
            $"{tallyArray[(int)type]} / {countArray[(int)type]}</size>";
    }

    void ShowCategory(AchievementType type)
    {
        // Switch views
        categoryMenu.SetActive(false);
        achievementsList.parent.gameObject.SetActive(true);
        backButton.GetComponentInChildren<TextMeshProUGUI>().text = "<<";

        int blockNum = 0;

        // Show any block that matches the selected category's type
        foreach (var a in achievementDataList)
        {
            if (a.effect.GetAchievementType() == type)
            {
                ShowBlock(blockNum, a);
                ++blockNum;
            }
        }

        // Hide the rest of the blocks
        for (int i = blockNum; i < blocks.Count; ++i)
        {
            blocks[i].icon.transform.parent.gameObject.SetActive(false);
        }
    }

    void GoBack()
    {
        if (categoryMenu.gameObject.activeSelf)
        {
            EventBus.GoInterfaceFocus(transform, false);
        }
        else
        {
            categoryMenu.SetActive(true);
            achievementsList.parent.gameObject.SetActive(false);
            backButton.GetComponentInChildren<TextMeshProUGUI>().text = "X";
            UpdateAllCategoryButtons();
        }
    }

    void PopulateBlocksList()
    {
        int blockCount = GetMaxCount();

        for (int i = 0; i < blockCount; ++i)
        {
            AddBlockToList();
        }
    }

    void AddBlockToList()
    {
        Transform newBlockObj = 
            Instantiate(blockTemplate, achievementsList, false).transform;
        Transform nameObject = newBlockObj.Find("Main Text");

        AchievementBlock newBlock = new AchievementBlock
        {
            icon = newBlockObj.GetComponent<Image>(),
            textBody = nameObject.GetComponent<TextMeshProUGUI>(),
            skillpointsText = nameObject.Find(
                "Skill Points Text").GetComponent<TextMeshProUGUI>()
        };

        blocks.Add(newBlock);
    }

    void ShowBlock(int index, AchievementData a)
    {
        AchievementBlock b = blocks[index];

        b.textBody.text = $"{a.achievementName}\n" +
            $"<size=24>{a.desc}</size>";
        b.skillpointsText.text = a.skillPoints.ToString();
    }

    int GetMaxCount()
    {
        int max = 0;

        foreach(var c in countArray)
        {
            if (c > max) max = c;
        }

        return max;
    }
}

[System.Serializable]
public struct CategoryButton
{
    public Button button;
    public Color bodyColor;
    public Color textColor;
    public TextMeshProUGUI title;
}

[System.Serializable]
public struct AchievementBlock
{
    public Image icon;
    public TextMeshProUGUI textBody;
    public TextMeshProUGUI skillpointsText;
}