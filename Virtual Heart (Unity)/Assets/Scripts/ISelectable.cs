
namespace Assets.Scripts
{
    public delegate void SelectionHandler(ISelectable o);

    public interface ISelectable
    {
        /// <summary>
        /// Selected event
        /// </summary>
        event SelectionHandler Selected;

        /// <summary>
        /// Deselected event
        /// </summary>
        event SelectionHandler Deselected;

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
