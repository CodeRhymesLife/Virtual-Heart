
namespace Assets.Scripts
{
    public interface ISelectable
    {
        /// <summary>
        /// Called when an item is selected
        /// </summary>
        void Select();

        /// <summary>
        /// Called when an item is deselected
        /// </summary>
        void Deselect();
    }
}
