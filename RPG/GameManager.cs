using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringTheory;

namespace RPG
{
    public class GameManager
    {
        private static GameManager instance = null;

        public Team playerTeam;
        public Team soloTeam;
        public Team boss;
        public Sequence mainSequence;
        public List<StoryEvent> eventsCompleted = new List<StoryEvent>();

        private GameManager()
        {
            mainSequence = new Sequence();
        }

        public static GameManager getInstance()
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }

        public void RunGame()
        {
            mainSequence.AddSequence(StoryManager.GetStorySequence(StorySequence.Introduction));
            mainSequence.StartSequence();
        }

        public void AddCompletedEvent(StoryEvent eventName)
        {
            eventsCompleted.Add(eventName);
        }

        public bool IsEventCompleted(StoryEvent eventName)
        {
            bool found = false;
            foreach(StoryEvent se in eventsCompleted)
            {
                if (se == eventName) found = true;
            }

            return found;
        }
    }
}
