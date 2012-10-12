
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.HTML.Builder.Element
{
    /// <summary>
    /// Element interface
    /// </summary>
    public interface IElement
    {
        /// <summary>
        /// converts Element to html string
        /// </summary>
        /// <returns>html representation of Element</returns>
        string ToHtml();

        string Name { get; }
    }
}
