namespace Scripts
{
    public interface IEquippable
    {
        bool IsEquipped { get; }
        BodyPart EquippedAt { get; }

        void Equip(BodyPart at);
        void Unequip();
    }
}