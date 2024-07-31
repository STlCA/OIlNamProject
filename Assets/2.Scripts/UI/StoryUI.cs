using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Story
{
    public string text;
    public bool delete;

    public void SetStory(string text, bool delete)
    {
        this.text = text;
        this.delete = delete;
    }
}

public class StoryUI : MonoBehaviour
{
    public DataManager dataManager;
    private DataTable_StoryLoader storyDataBase;
    //private StoryDataBase storyDataBase;

    public Story[] story = new Story[13];

    public void Init()
    {
        storyDataBase = dataManager.dataTable_StoryLoader;

        for (int i = 0; i < story.Length; i++)
        {
            story[i] = new Story();
            story[i].SetStory(storyDataBase.GetByKey(i).Text.Replace("&", "\n"), storyDataBase.GetByKey(i).Delete);
        }

/*        story[0].SetStory("콰지직.", false);
        story[1].SetStory("\n\n전 세계에 균열이 발생했다.", true);
        story[2].SetStory("차원 간의 균열이 열리며\n튀어나온 온갖 마물들은\n그야말로 자연재해였다.", true);
        story[3].SetStory("하지만,\n인류는 멸망하지 않았다.\n\n균열에서 새어 나오는 마나를 받아들여 특별한 힘을 지니게 된 사람들 덕분이었다.", true);
        story[4].SetStory("내가 바로 그 힘을 가진\n\n'헌터'다.", true);
        story[5].SetStory("나는 협회에 소속된 헌터다.\n그것도....\n헌터에 관련된 계약에 허점이 많은 시절에 계약하여 노예와 다름 없는 계약...", true);
        story[6].SetStory("큰 계약금을 제시하여 계약금에 홀린 헌터들을 협회에 묶어놓은 다음, 거액의 빚을 지게 하는 계약.", true);
        story[7].SetStory("그래서 나는 그 빚을 갚기 위해 매일매일을 싸움터에서 뛰어다녀야 한다…", true);
        story[8].SetStory("나에게는 아내를 똑 닮은 딸이 하나 있는데 매일 일하느라 엄마의 빈자리를 채워주지 못해서 미안하다…", true);
        story[9].SetStory("하지만 빚을 갚기 위해서라도 계속 일을 해야 한다.", true);
        story[10].SetStory("(사이렌 소리)하… 또 균열이 발생했다.\n뭔 놈의 마물이 이렇게 하루가 멀다 하고 매일 나오는지 지겹다 지겨워…", true);
        story[11].SetStory("(전화벨소리)왜?\n(중얼거리며) 아니...학교에서 필요한 준비물이 있는데..\n귀찮게 연락하지 말고, 카드로 사\n(뚝 끊음)\n", true);
        story[12].SetStory("하…귀찮네…", false);*/
    }
}
