
namespace Tools.ViewModels.General
{
    /// <summary>
    /// an object containing an id and display name,
    /// to be used in dropdowns
    /// </summary>
    public class SelectElement
    {
        #region Constructors

        public SelectElement () { }

        public SelectElement (long id, string DisplayName)
        {
            this.ID = id;
            this.DisplayName = DisplayName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Element id
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Element display name
        /// </summary>
        public string DisplayName { get; set; }

        #endregion
    }
}
