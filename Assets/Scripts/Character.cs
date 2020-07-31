using System.Collections.Generic;

namespace Scripts
{
    public class Character : BaseObject
    {
        public const string CharName = "CharName";

        public enum CharTypes { Avatar, DM, Normal, NPC };
        public CharTypes CharType { get; set; }
        public Dictionary<string, string[]> CharacterStrings { get; set; } = new Dictionary<string, string[]>();

        public BodyPart[] BodyParts { get; set; }


        public Character(CharTypes charType, string characterName) : base(idSeed: characterName)
        {
            CharType = charType;

            CharacterStrings.Add(CharName, new string[] { characterName });
            
        }

    }
}