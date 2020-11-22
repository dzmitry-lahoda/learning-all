using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeLapseExp.Imaging.Contract
{
    /// <summary>
    /// Interface for classes which provide funtionality of computing
    /// difereent meaningfull values for images.
    /// Possibly future usage with MEF.
    /// </summary>
    public interface IImageCharacteristics
    {
        /// <summary>
        /// Computes Shannon entropy of image file.
        /// <see cref="http://en.wikipedia.org/wiki/Entropy_(information_theory)"/>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        double ComputeShannonEntropy(String fileName);

        /// <summary>
        /// Get contrast measurement number
        /// (higher number returned for the same exposition the better image captured).
        /// Contrast measurement is achieved by measuring contrast within a sensor field, through the lens.
        /// The intensity difference between adjacent pixels of the sensor naturally increases with correct image focus.
        /// The optical system can thereby be adjusted until the maximum contrast is detected.
        /// In this method, AF does not involve actual distance measurement at all and is generally slower than phase detection systems,
        /// especially when operating under dim light.
        /// <see cref="http://en.wikipedia.org/wiki/Autofocus#Contrast_measurement"/>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        double ComputeContrastMeasurmentNumber(String fileName);
    }
}
