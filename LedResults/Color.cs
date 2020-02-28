namespace LedResults
{
    public class LedResult
    {
        private int maxSkills = 4;
        private Color[] skills;

        private int maxItems = 8;
        private Color[] items;

        private int maxHealthPoints = 8;
        private Color[] barHealthPoints;
        
        private int maxResources = 7;
        private Color[] barRessources;

        public LedResult()
        {
            skills = new Color[maxSkills];
            items = new Color[maxItems];
            barHealthPoints = new Color[maxHealthPoints];
            barRessources = new Color[maxResources];
        }

        public void setSkill(int index, Color color)
        {
            skills[index] = color;
        }
        public Color getSkill(int index)
        {
            return skills[index];
        }
    }

    public class Color
    {
        public readonly int R = 0;
        public readonly int G = 0;
        public readonly int B = 0;

        public Color(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}
