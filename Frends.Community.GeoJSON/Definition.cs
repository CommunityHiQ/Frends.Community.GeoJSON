#pragma warning disable 1591

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Frends.Community.GeoJSON
{
    /// <summary>
    /// Parameters class usually contains parameters that are required.
    /// </summary>
    public class Parameters
    {

        /// <summary>
        /// Something that will be repeated.
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue("POLYGON ((30 10, 40 40, 20 40, 10 20, 30 10))")]
        public string Wkt2 { get; set; }
    }

    /// <summary>
    /// Options class provides additional optional parameters.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Number of times input is repeated.
        /// </summary>
        [DefaultValue(3)]
        public int Amount { get; set; }

        /// <summary>
        /// How repeats of the input are separated.
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        [DefaultValue(" ")]
        public string Delimiter { get; set; }
    }

    public class Result
    {
        /// <summary>
        /// lorem
        /// </summary>
        [DisplayFormat(DataFormatString = "Text")]
        public string Ret;
    }
}
