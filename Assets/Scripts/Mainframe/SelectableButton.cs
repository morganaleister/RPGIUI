using System;

namespace Scripts.MainframeReference
{
    public class SelectableButton : BaseButton, ISelectable
    {
        public bool IsSelected => SelectionManager.IsSelected((ISelectable)this);


        public event Action Selected;
        public event Action Deselected;

        public void Deselect()
        {
            throw new NotImplementedException();
        }

        public void Select()
        {
            throw new NotImplementedException();
        }
    }
}