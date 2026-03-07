using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsUI : MonoBehaviour
{
    [SerializeField] public CategoryButton[] categoryButtons;
    public int[] tallyArray;
    public int[] countArray;
    public AchievementsDatabase database;

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

    void OnDestroy()
    {
        EventBus.OnAchievement -= TallyAchievement;
    }

    void TallyAchievement(AchievementData a)
    {
        AchievementType type = a.effect.GetAchievementType();
        
        ++tallyArray[(int)type];
        UpdateCategoryButton(type);
    }

    void ConfigUI()
    {
        GameData data = GameDataManager.gameData;
        blocks = new List<AchievementBlock>();

        tallyArray = new int[categoryButtons.Length];
        countArray = new int[categoryButtons.Length];

        // Count how many of each achievement type there are
        foreach (var a in database.list)
        {
            int type = (int)a.effect.GetAchievementType();
            ++countArray[type];

            if (data.achivementsPool.Contains(a.achievementName))
            {
                Debug.Log($"Achievement '{a.achievementName}' is already unlocked");
                TallyAchievement(a);
                a.isUnlocked = true;
            }
        }

        // Configure category buttons
        Image[] categoryButtonImages = categoryMenu.GetComponentsInChildren<Image>();

        for (int i = 0; i < categoryButtons.Length; ++i)
        {
            CategoryButton c = categoryButtons[i];
            AchievementType type = (AchievementType)i;

            c.button.onClick.AddListener(() => ShowCategory(type));

            c.bodyColor = categoryButtonImages[i].color;
            c.textColor = categoryButtonImages[i]
                .GetComponentInChildren<TextMeshProUGUI>().color;

            // Disable Asterism for now
            if (type == AchievementType.ASTERISM)
            {
                c.button.interactable = false;
            }

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
        foreach (var a in database.list)
        {
            if (a.effect.GetAchievementType() == type)
            {
                UpdateBlock(blockNum, a);
                blocks[blockNum].block.transform.gameObject.SetActive(true);
                ++blockNum;
            }
        }

        // Hide the rest of the blocks
        for (int i = blockNum; i < blocks.Count; ++i)
        {
            blocks[i].block.transform.gameObject.SetActive(false);
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
            icon = nameObject.GetComponentInChildren<Image>(),
            block = newBlockObj.GetComponent<Image>(),
            textBody = nameObject.GetComponent<TextMeshProUGUI>(),
            skillpointsText = nameObject.Find(
                "Skill Points Text").GetComponent<TextMeshProUGUI>()
        };

        blocks.Add(newBlock);
    }

    void UpdateBlock(int index, AchievementData a)
    {
        AchievementBlock b = blocks[index];
        int effectType = (int)a.effect.GetAchievementType();

        if (a.isUnlocked)
        {
            b.icon.enabled = true;
            b.textBody.text = $"{a.achievementName}\n" +
            $"<size=24>{a.desc}</size>";
            b.textBody.color = categoryButtons[effectType].textColor;
            b.skillpointsText.text = a.skillPoints.ToString();

            // Recolor the block (and icon for now)
            b.block.color = b.icon.color =
                categoryButtons[(int)a.effect.GetAchievementType()].bodyColor;
        }
        else
        {
            b.textBody.text = "???\n<size=24>???</size>";
            b.skillpointsText.text = "";
            b.icon.enabled = false;
            b.block.color = Color.darkGray;
            b.textBody.color = Color.gray;
        }
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
    public Image block;
    public TextMeshProUGUI textBody;
    public TextMeshProUGUI skillpointsText;
}