using UnityEngine;

namespace Simulanis.ContentSDK.K12.Assessment
{
    [CreateAssetMenu(fileName = "Button Sprite", menuName = "ScriptableObjects/Button Sprite", order = 1)]
    public class SpriteHolder : ScriptableObject
    {
        public Sprite NormalSprite;
        public Sprite HighlightSprite;
        public Sprite selectedSprite;
        public Sprite CorrectAnsSprite;
        public Sprite WrongAnsSprite;
        public Sprite ImageSprite;

    }
}
