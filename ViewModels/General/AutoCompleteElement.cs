
namespace Tools.ViewModels.General
{
    /// <summary>
    /// used for JQuery Autocomplete dropdown so when an element is selected
    /// its id is sent back to the server (as opposed to its display name)
    /// </summary>
    public class AutoCompleteElement
    {
        #region Constructors

        public AutoCompleteElement () { }

        public AutoCompleteElement (string Value, string Label)
        {
            this.Value = Value;
            this.Label = Label;            
        }

        #endregion

        #region Properties

        /// <summary>
        /// object name, display in autocomplete dropdown
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// object id, hidden and sent back to server
        /// </summary>
        public string Value { get; set; }

        #endregion
    }
}